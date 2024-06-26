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
=====*/

//＞名前空間宣言
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CCodingRule;

//＞クラス定義
public class CTrapSelect : MonoBehaviour
{
    //変数宣言
    private GameObject player;

    //＞構造体定義
    [Serializable]
    public struct TrapInfo
    {
        [Tooltip("生成するオブジェクト")]public GameObject m_Trap;    // オブジェクト
        [Tooltip("表示するUI")]public Image m_Image;                  // UI
        [Tooltip("コスト")]public int m_Cost;                         // コスト
        [Tooltip("表示用のText")]public TMP_Text m_CostText;          // コストテキスト
    }
    [Header("罠の情報")]
    [SerializeField,Tooltip("罠の情報")] private TrapInfo[] m_TrapInfo;    //罠の情報

    [Header("ステータス")]
    [SerializeField, Tooltip("最初から持っているコスト")] private int m_FirstCost;
    [Tooltip("コストを増やす間隔(秒)")]public float m_fIncreaseInterval = 5.0f;  // コストを増やす間隔（秒）
    [Tooltip("触らないで")] public bool m_bSelect=true;     // true:選択可能　false:選択不可
    private int m_nNum;             // 今選んでいる罠の番号を格納
    public static int m_Cost;

    [Header("音")]
    [SerializeField,Tooltip("罠選択時のSE")] public AudioClip SE_Select;  // 罠選択時のSE
    [SerializeField,Tooltip("罠設置時のSE")] public AudioClip SE_Set;  // 罠設置時のSE
    AudioSource m_As; // AudioSourceを追加


    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    void Start()
    {
        player = GameObject.Find("Player"); // 検索
        m_nNum = 0;                         // 初期化
        //RectTransformを取得
        RectTransform rectTransform = m_TrapInfo[m_nNum].m_Image.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 200);    // 選択中のUIの大きさを変更
        m_As = GetComponent<AudioSource>();                 // AudioSourceコンポーネントを追加
        m_bSelect = true;                                   // 選択可能
        m_Cost = m_FirstCost;                               // 初期コスト
        m_TrapInfo[0].m_CostText.SetText($"{m_TrapInfo[0].m_Cost}");  //Textをセット
        m_TrapInfo[1].m_CostText.SetText($"{m_TrapInfo[1].m_Cost}");  //Textをセット
    }

    /*＞更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    void Update()
    {
        if (m_bSelect)
        {//選択可能なら
            Select();   //選択
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Decision"))&& CostCheck(m_nNum))
            {//決定したなら
                m_As.PlayOneShot(SE_Set);   // SE再生
                Generation(m_nNum);         //オブジェクト作成
                m_bSelect = false;          //選択不可
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
        GameObject TrapObject= Instantiate(m_TrapInfo[_nNum].m_Trap, player.transform.position + vPos, Quaternion.identity);
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
            m_As.PlayOneShot(SE_Select);    // SE再生
            ChangeSize(100);                // サイズを変更
            m_nNum += 1;                    // 次の番号
            // 罠の種類よりも大きくなったら最大に戻す
            if (m_nNum > m_TrapInfo.Length - 1) m_nNum = m_TrapInfo.Length - 1; 
            ChangeSize(200);                // サイズを変更
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetButtonDown("Left"))
        {
            m_As.PlayOneShot(SE_Select);    // SE再生
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
    private bool CostCheck(int i)
    {
        m_Cost-= m_TrapInfo[i].m_Cost;  // 今あるコストから選択した罠のコストを引く
        if(m_Cost>=0) { return true; }  // 0以上なら問題なし選択可能
        else
        {// 0より小さいとき
            m_Cost += m_TrapInfo[i].m_Cost; // 引いたコストを戻す
            return false;                   // 選択不可
        }
        
    }
}
