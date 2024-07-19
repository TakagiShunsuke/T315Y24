/*=====
<TrapSelect.cs> 
└作成者：yamamoto

＞内容
配置する罠を選択するためのスクリプト

＞更新履歴
__Y24   
_M06    
D
12: プログラム作成: yamamoto
18: SE追加: nieda
26: コメント追加: yamamoto
27: SE関係リファクタリング: nieda
=====*/

//＞名前空間宣言
using Effekseer;
using EffekseerTool.Data.Value;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static CCodingRule;
using static CFeatures;
using static InputDeviceManager;

//＞クラス定義
public class CTrapSelect : CMonoSingleton<CTrapSelect>
{
    //＞変数宣言
    private GameObject player;

    //＞構造体定義
    //[Serializable]
    //public struct TrapInfo
    //{
    //    [Tooltip("生成するオブジェクト")]public GameObject m_Trap;
    //    [Tooltip("表示するUI")]public Image m_Image;                  
    //    [Tooltip("コスト")]public int m_Cost;                         // コスト
    //    [Tooltip("表示用のText")]public TMP_Text m_CostText;          
    //}
    [Serializable]
    private struct OutputTrapInfo
    {
        [Tooltip("UI表示")] public Image m_Image;    // UI
        [Tooltip("コスト表示テキスト")] public TMP_Text m_CostText;  // コストテキスト
    }   //罠の情報表示場所
     
    [Header("罠の情報")]
    [SerializeField, Tooltip("罠表示")] private OutputTrapInfo[] m_TrapInfo;   //罠表示用情報

    [Header("ステータス")]
    [SerializeField, Tooltip("最初から持っているコスト")] private int m_FirstCost;
    [Tooltip("コストを増やす間隔(秒)")]public float m_fIncreaseInterval = 5.0f;  // コストを増やす間隔（秒）
    [SerializeField, Tooltip("選択確定キー")] KeyCode m_DecideKey;    //決定キー
    [Tooltip("触らないで")] public bool m_bSelect=true;     // true:選択可能　false:選択不可
    private int m_nNum;             // 今選んでいる罠の番号を格納
    public static int m_nCost;

    [Header("音")]
    [Tooltip("AudioSourceを追加")] private AudioSource m_AudioSource;      // AudioSourceを追加
    [SerializeField,Tooltip("罠選択時のSE")] private AudioClip SE_Select;  // 罠選択時のSE
    [SerializeField,Tooltip("罠設置時のSE")] private AudioClip SE_Set;     // 罠設置時のSE

    //＞プロパティ定義
    public int HavableTrapNum => m_TrapInfo.Length;    //持てる罠数 = 表示情報の用意数
    private CTrap[] TrapComps { get; set; }  //罠のコンポーネント部分


    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    protected override void Start()
    {
        player = GameObject.Find("Player"); // 検索
        m_nNum = 0;                         // 初期化
        //RectTransformを取得
        RectTransform rectTransform = m_TrapInfo[m_nNum].m_Image.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 200);    // 選択中のUIの大きさを変更
        m_AudioSource = GetComponent<AudioSource>();        // AudioSourceコンポーネントを追加
        m_bSelect = true;                                   // 選択可能
        m_nCost = m_FirstCost;                               // 初期コスト
        InputDeviceManager.Instance.OnChangeDeviceType.AddListener(OnChangeDeviceTypeHandler);  //デバイス初期化

        TrapComps = new CTrap[HavableTrapNum];

        //＞罠表示情報秘匿
        for (int _nIdx = 0; _nIdx < m_TrapInfo.Length; _nIdx++) //情報表示できる範囲内
        {
            //m_TrapInfo[_nIdx].m_CostText.gameObject.SetActive(false);  //初期Textを見せない
            //m_TrapInfo[_nIdx].m_Image.gameObject.SetActive(false);  //初期Imageを見せない
        }
    }

    private void OnChangeDeviceTypeHandler()
    {
        // 入力デバイスの種別が変更されたときの処理
        Debug.Log("入力デバイスの種別が変更されました。\n現在の入力デバイスの種別：" + InputDeviceManager.Instance.CurrentDeviceType);
    }

    /*＞更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    protected override void Update()
    {
        ////＞保全
        //if(TrapComps != null && m_TrapInfo != null) //ヌルチェック
        //{
        //    //＞罠表示UI更新  ※画像生成処理(初期化)が非同期処理なためにCTrapから画像データを正しく受け取れないことがあるためここに記載
        //    for (int _nIdx = 0; _nIdx < m_TrapInfo.Length; _nIdx++) //情報表示できる範囲内
        //    {
        //        if (TrapComps[_nIdx].ImageSprite != null && m_TrapInfo[_nIdx].m_Image.sprite == null)   //まだ画像設定されておらず、設定されるべき
        //        {
        //            m_TrapInfo[_nIdx].m_Image.sprite = TrapComps[_nIdx].ImageSprite;   //Imageに画像を設定
        //        }
        //    }
        //}

        //＞罠情報表示初期化
            for (int _nIdx = 0; _nIdx < CTrapManager.Instance.HaveTraps.Count; _nIdx++) //情報表示できる範囲内
            {
                //＞初回更新
                if (TrapComps[_nIdx] != null) //ヌルチェック
                {
                    break;  //初回以外更新しない
                }

                //＞変数宣言
                var _Obj = CTrapManager.Instance.HaveTraps[_nIdx]; //該当罠取得
                bool _bMakeObj = false;  //オブジェクトを生成したか

                //＞保全
                if (_Obj == null)  //ヌルチェック
                {
                    //continue;   //ヌルアクセス防止
                    Instantiate(_Obj, Vector3.zero, Quaternion.identity);  //一時的にヌルじゃなくする
                    _bMakeObj = true;  //生成した
                }

                //＞変数宣言
                var _Trap = _Obj.GetComponent<CTrap>(); //罠のコンポーネントを取り出す

                //＞保全
                if (_Trap == null)  //該当コンポーネントがない
                {
                    if (_bMakeObj)  //生成していた時
                    {
                        Destroy(_Obj);  //生成を元に戻す
                    }
                    continue;   //ヌルアクセス防止
                }

                //＞初期化
                TrapComps[_nIdx] = _Trap;   //コンポーネント登録
                m_TrapInfo[_nIdx].m_CostText.SetText($"{_Trap.Cost}");  //Textにコスト値をセット
            m_TrapInfo[_nIdx].m_CostText.gameObject.SetActive(true);  //更新後Textを見せる
            m_TrapInfo[_nIdx].m_Image.sprite = _Trap.ImageSprite;   //Imageに画像を設定
            m_TrapInfo[_nIdx].m_Image.gameObject.SetActive(true);  //更新後Imageを見せる

            //＞片付け
            if (_bMakeObj)  //生成していた時
                {
                    Destroy(_Obj);  //生成を元に戻す
                }
        }

        //＞罠表示UI更新  ※画像生成処理(初期化)が非同期処理なためにCTrapから画像データを正しく受け取れないことがあるためここに記載
        for (int _nIdx = 0; _nIdx < m_TrapInfo.Length; _nIdx++) //情報表示できる範囲内
        {
            //＞保全
            if (m_TrapInfo[_nIdx].m_Image.sprite != null || TrapComps[_nIdx] == null) //ヌルチェック
            {
                continue;
            }
            if (TrapComps[_nIdx].ImageSprite != null && m_TrapInfo[_nIdx].m_Image.sprite == null)   //まだ画像設定されておらず、設定されるべき
            {
                m_TrapInfo[_nIdx].m_Image.sprite = TrapComps[_nIdx].ImageSprite;   //Imageに画像を設定
            }
        }

        if (m_bSelect)
        {//選択可能なら
            Select();   //選択
            
            if (InputDeviceManager.Instance != null)
            {
                //＞変数宣言
                bool _bDownDesideKey = false;   //決定キーを離したか

                // 現在の入力デバイスタイプを取得
                InputDeviceManager.InputDeviceType currentDeviceType = InputDeviceManager.Instance.CurrentDeviceType;

                //決定
                // 現在のデバイスタイプに応じた処理を行う
                switch (currentDeviceType)
                {
                    case InputDeviceManager.InputDeviceType.Keyboard:
                        _bDownDesideKey = Input.GetKeyDown(m_DecideKey) && CostCheck(m_nNum); //決定キー入力判定
                        break;
                    case InputDeviceManager.InputDeviceType.Xbox:
                        _bDownDesideKey = Input.GetButtonDown("Decision") && CostCheck(m_nNum); //決定キー入力判定
                        break;
                    case InputDeviceManager.InputDeviceType.DualShock4:
                        _bDownDesideKey = Input.GetButtonDown("Decision") && CostCheck(m_nNum); //決定キー入力判定
                        break;
                    case InputDeviceManager.InputDeviceType.DualSense:
                        _bDownDesideKey = Input.GetButtonDown("Decision") && CostCheck(m_nNum); //決定キー入力判定
                        break;
                    case InputDeviceManager.InputDeviceType.Switch:
                        _bDownDesideKey = Input.GetButtonDown("Decision") && CostCheck(m_nNum); //決定キー入力判定
                        break;
#if UNITY_EDITOR    //エディタ使用中
                    default:
                        Debug.Log("未知の入力デバイスが使用されています");
                        break;
#endif
                }

                if (_bDownDesideKey) //決定入力時
                {//決定したなら
                    m_AudioSource.PlayOneShot(SE_Set);   // SE再生
                    Generation(m_nNum);                  //オブジェクト作成
                    m_bSelect = false;                   //選択不可
                    m_bSelect = true;
                }
            }
        }
    }

    /*＞オブジェクト生成関数
    引数１：int _nNum : 選択している罠の番号
    ｘ
    戻値：なし
    ｘ
    概要：オブジェクトの生成
    */
    private void Generation(int _nNum)
    {
        //生成用
        Vector3 vPos = player.transform.forward * 2;    //プレイヤーの正面方向のベクトルを取得
                                                        //オブジェクトの生成
        GameObject TrapObject = Instantiate(CTrapManager.Instance.HaveTraps[_nNum], player.transform.position + vPos, Quaternion.identity);
        TrapObject.SetActive(true); //コピー元はオブジェクトがアクティブでないのでアクティブにする
        TrapObject.GetComponent<CTrap>().m_bSetting = false;    //設置不可
    }

    /*＞罠選択関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：罠の選択
    */
    private void Select()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetButtonDown("Right"))
        {
            m_AudioSource.PlayOneShot(SE_Select);    // SE再生
            ChangeSize(100);                // サイズを変更
            m_nNum += 1;                    // 次の番号
            // 罠の種類よりも大きくなったら最大に戻す
            if (m_nNum > m_TrapInfo.Length - 1) m_nNum = m_TrapInfo.Length - 1; 
            ChangeSize(200);                // サイズを変更
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetButtonDown("Left"))
        {
            m_AudioSource.PlayOneShot(SE_Select);    // SE再生
            ChangeSize(100);                // サイズを変更
            m_nNum -= 1;                    // 次の番号
            if (m_nNum < 0) m_nNum = 0;     // 負の数にならないように
            ChangeSize(200);                // サイズを変更
        }
    }

    /*＞UIサイズ変更関数
    引数１：int _nSize : この値のサイズに変更
    ｘ
    戻値：なし
    ｘ
    概要：UIサイズ変更
    */
    private void ChangeSize(int _nSize)
    {
        // RectTransformの取得
        RectTransform rectTransform = m_TrapInfo[m_nNum].m_Image.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(_nSize, _nSize);// サイズ変更
    }

    /*＞選択可能関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：選択可能に変更
    */
    public void SetSelect()
    {
        m_bSelect = true;   //選択可能
    }

    /*＞選択可能関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：選択可能に変更
    */
    private bool CostCheck(int _nNum)
    {

        //＞変数宣言
        var _nCost = m_nCost - TrapComps[_nNum].Cost; //コスト消費後の値

        //＞条件分岐
        if(_nCost >= 0) //選択可能
        {
            m_nCost = _nCost;   //値を反映
            return true; // 0以上なら問題なし
        }
        else
        {//選択不可
            return false;                   // 選択不可
        }
        //m_Cost -= m_TrapInfo[i].m_Cost;  // 今あるコストから選択した罠のコストを引く
        //if(m_Cost>=0) { return true; }  // 0以上なら問題なし選択可能
        //else
        //{// 0より小さいとき
        //    m_Cost += m_TrapInfo[i].m_Cost; // 引いたコストを戻す
        //    return false;                   // 選択不可
        //}
        
    }
}
