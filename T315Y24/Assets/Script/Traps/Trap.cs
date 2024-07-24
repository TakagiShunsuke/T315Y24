/*=====
<Trap.cs> 
└作成者：yamamoto

＞内容
罠の親クラス

＞更新履歴
__Y24   
_M06
D
08：プログラム作成：yamamoto
13：爆発時SE追加：nieda
18：SE追加：nieda
26：コメント追加：yamamoto
27：SE関係リファクタリング：nieda
=====*/

//＞名前空間宣言
using Effekseer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

//＞クラス定義
public class CTrap : MonoBehaviour
{
    //変数宣言
    [Header("ステータス")]
    [SerializeField, Tooltip("再使用できるまでの時間(秒)")] private double m_dInterval = 5.0d; // インターバル
    private double m_dCoolTime = 0.0d;                  // インターバル計測用
    [Tooltip("設置する高さ")] public float m_fPosY;     // 設置する高さ
    [Tooltip("触らないで")] public bool m_bMove = true;                         // true:配置中 false:配置後
    [Tooltip("触らないで")] public bool m_bSetting = true;  //true:設置可能　false:設置不可
    [Tooltip("触らないで")] public bool m_bUse = true;  // 利用 true:可能 false:不可

    [Header("エフェクト")]
    [SerializeField, Tooltip("設置時再生するエフェクト")] private EffekseerEffectAsset m_SetEffect;  // 設置時再生するエフェクト

    [Header("テキスト")]
    [SerializeField,Tooltip("表示用text")] private Text m_CoolDownText;       // クールダウン表示テキスト
    [SerializeField,Tooltip("フォントサイズ")] private int m_nFontSize = 24;  // クールダウンのフォントサイズ変更用
                                                                         
    [Header("音")]
    [Tooltip("AudioSourceを追加")] protected AudioSource m_audioSource;    // AudioSourceを追加
    [SerializeField,Tooltip("罠設置時のSE")] protected AudioClip SE_SetTrap;   // 罠設置時のSE
    [SerializeField,Tooltip("罠爆発時のSE")] protected AudioClip SE_ExpTrap;   // 罠爆発時のSE

    private GameObject player;  //player格納用
    public Material material; // 半透明にしたいマテリアル

    //＞プロパティ定義
    virtual public int Cost { get; protected set; }//コスト
    virtual public Sprite ImageSprite { get; protected set; } //UIアセットを画像に変換したもの


    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    void Start()
    {
        // テキストの初期化
        m_CoolDownText = GetComponentInChildren<Transform>().GetComponentInChildren<Text>();    // 子のText取得
        m_CoolDownText.text = m_dCoolTime.ToString();       // textの初期化
        m_CoolDownText.fontSize = m_nFontSize;              // フォントサイズを変更
        m_CoolDownText.alignment = TextAnchor.MiddleCenter; // textの表示位置を真ん中に
        m_CoolDownText.gameObject.SetActive(false);         // 使用前なので非表示

        player = GameObject.Find("Player");                 // Playerを検索
        Settings();         // プレイヤーが向いている方向に罠を仮セット
        m_bSetting = true;  // 設置可能
        m_audioSource = GetComponent<AudioSource>();        // AudioSourceを取得

       Transparent transparent = GetComponent<Transparent>();
        transparent.color.a = 0.8f;
        transparent.ClearMaterialInvoke();
        
    }

    /*＞罠発動チェック関数
    引数１：Collision _Collision : 当たっているものの情報
    ｘ
    戻値：罠発動可能→true 不可→false
    ｘ
    概要：罠発動可能かどうかのチェック
    */
    public bool Check(Collision _collision,bool _Use)
    {
        if (_collision.gameObject.CompareTag("Enemy") && m_bUse && !m_bMove)
        {// Enemyタグがついている＆地雷使用可能なら
            SetCoolTime();      //クールタイムをセット
            m_bUse = _Use;     // 使用不可
            return true;
        }
        return false;
    }

    /*＞設置不可判定関数
    引数１：Collision _Collision : 当たっているものの情報
    ｘ
    戻値：なし
    ｘ
    概要：建物か罠に当たっていたら設置不可にする
    */
    public virtual void SetCheck(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Trap"))
        {//当たったのが建物か罠だったら
            m_bSetting =false;      //設置不可
        }
        
    }
    /*＞設置判定関数
    引数１：Collision _Collision : 当たっているものの情報
    ｘ
    戻値：なし
    ｘ
    概要：建物か罠に当たらなくなったら設置可能にする
    */
    public virtual void OutCheck(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Trap"))
        {//当たらなくなったのが建物か罠だったら
            m_bSetting = true;  //設置可能
        }
    }

    /*＞物理更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    private void FixedUpdate()
    {
        if(m_bMove)
        {
            Vector3 vPos = player.transform.forward * 2;    //プレイヤーの正面方向のベクトルを入手
            transform.position = player.transform.position + vPos;  //足してプレイヤーの少し前に罠を持ってくる
            Settings();     //高さだけ元に戻す
        }
        CooltimeCount();    //クールタイムの計算
        
    }

    /*＞クールタイム計測関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：クールタイムがある時のみ処理される
    */
    private void CooltimeCount()
    {
        //＞再利用カウントダウン
        if (m_dCoolTime > 0.0d)   //クールダウン中
        {
            m_dCoolTime -= Time.fixedDeltaTime;
            m_CoolDownText.text = m_dCoolTime.ToString("F1");   //小数点以下一桁までクールダウン表示
            if (m_dCoolTime <= 0.0d)
            {//クールタイムが終わったら
                m_bUse = true;      //使用可能
                m_CoolDownText.gameObject.SetActive(false); //Text非表示
            }
        }
    }

    /*＞罠の高さ(y)変更関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：高さ(y)変更
    */
    public void Settings()
    {
        Vector3 pos = transform.position;   //現在の位置を取得
        pos.y = m_fPosY;                    //ｙのみ変更
        transform.position = pos;           //変更後を代入

    }

    /*＞クールタイムセット関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：クールタイムをセットする
    */
    public void SetCoolTime()
    {
        m_dCoolTime = m_dInterval;                  // クールタイムセット
        m_CoolDownText.gameObject.SetActive(true);  //Textを表示
        m_bUse=false;                               //使用不可
    }

    /*＞罠設置関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：罠を設置する
    */
    public void SetTrap()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Decision")||Input.GetKeyDown(KeyCode.Return)) && m_bSetting && m_bMove)
        {//置ける条件だったら入る
            m_audioSource.PlayOneShot(SE_SetTrap);  //配置SE再生
            m_bMove = false;                        //場所固定のためfalseに

            //配置する罠を選択可能に
            GameObject TrapManager;                 //"TrapManager"格納用　
            CTrapSelect TrapSelect;                 //CTrapSelect格納用
            TrapManager = GameObject.Find("TrapSelect");           //”TrapManager”をさがし取得
            TrapSelect = TrapManager.GetComponent<CTrapSelect>();   //CTrapSelectを取得
            TrapSelect.SetSelect();               //配置する罠を選択可能  に変更

            Destroy(GetComponent<Rigidbody>());     //Rigidbodyだけを破壊
            SetCount();

            Transparent transparent = GetComponent<Transparent>();
            transparent.NotClearMaterialInvoke();

            //＞保全
            if (m_SetEffect != null)   //エフェクトがない
            {
                //＞設置エフェクト再生
                EffekseerSystem.PlayEffect(m_SetEffect, transform.position);  //設置位置に再生
            }
#if UNITY_EDITOR    //エディタ使用中
            else
            {
                //＞エラー出力
                UnityEngine.Debug.LogWarning("必要な要素が不足しています");  //警告ログ出力
            }
#endif
        }
    }

    /*＞罠設置関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：各罠のスクリプトで書く。
    */
    public virtual void SetCount()
    {
        Debug.LogWarning("必要な要素が不足しています");  //警告ログ出力
    }
}
