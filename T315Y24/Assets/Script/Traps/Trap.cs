using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CCodingRule;

public class CTrap : MonoBehaviour
{
    [SerializeField] private double m_dInterval = 5.0d; // インターバル
    private double m_dCoolTime = 0.0d;                  // インターバル計測用
    public bool m_bUse = true;                         // 利用 true:可能 false:不可
    public Text m_CoolDownText;                         // クールダウン表示テキスト
    [SerializeField] private int m_nFontSize = 24;      // クールダウンのフォントサイズ変更用
    public bool m_bMove = true;                        // true:配置中 false:配置後
    public float m_fPosY;
    public bool m_bSetting = true;
    private GameObject player;

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
        m_CoolDownText = GetComponentInChildren<Text>();
        m_CoolDownText.text = m_dCoolTime.ToString();   // textの初期化
        m_CoolDownText.fontSize = m_nFontSize;              // フォントサイズを変更
        m_CoolDownText.alignment = TextAnchor.MiddleCenter; // textの表示位置を真ん中に
        m_CoolDownText.gameObject.SetActive(false);         // 使用前なので非表示

        player = GameObject.Find("Player");//検索
        Settings();
    }

/*＞罠発動チェック関数
引数１：当たり判定があったオブジェクトの情報
ｘ
戻値：罠発動可能→true 不可→false
ｘ
概要：罠発動可能かどうかのチェック
*/
    public bool Check(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && m_bUse && !m_bMove)  // Enemyタグがついている＆地雷使用可能
        {
            m_bUse = false;                     // 使用不可
            SetCoolTime();
           
            return true;
        }
        return false;
    }
    public virtual void SetCheck(Collision collision)
    {
        if (/*collision.gameObject.CompareTag("Map") || */collision.gameObject.CompareTag("Trap"))
        {
           m_bSetting=false;
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
            Vector3 p = player.transform.forward * 2;
            transform.position = player.transform.position + p;
            Settings();
           // if (Input.GetKeyDown(KeyCode.R)&& m_bSetting)
           // {
           //     Debug.Log("dddd");
           //     m_bMove = false;
           //     GameObject A;
           //     CTrapSelect T;
           //    A = GameObject.Find("TrapManager");
           //     T = A.GetComponent<CTrapSelect>();
           //     T.SetSelect();
           // }
           // m_bSetting = true;
        }
        CooltimeCount();
        
    }
    private void CooltimeCount()
    {
        //＞再利用カウントダウン
        if (m_dCoolTime > 0.0d)   //クールダウン中
        {
            m_dCoolTime -= Time.fixedDeltaTime;
            m_CoolDownText.text = m_dCoolTime.ToString("F1");   //小数点以下一桁までクールダウン表示
            if (m_dCoolTime <= 0.0d)
            {
                m_bUse = true;
                m_CoolDownText.gameObject.SetActive(false);
            }  //利用可能に
        }
    }
    
    public  void Settings()
    {
        Vector3 pos = transform.position;
        pos.y = m_fPosY;
        transform.position = pos;

    }
    public void SetCoolTime()
    {
        m_dCoolTime = m_dInterval;          // 再利用時間計測
        m_CoolDownText.gameObject.SetActive(true);
        m_bUse=false;
    }

    public void aaa()
    {
        if (Input.GetKeyDown(KeyCode.R) && m_bSetting)
        {
            Debug.Log("dddd");
            m_bMove = false;
            GameObject A;
            CTrapSelect T;
            A = GameObject.Find("TrapManager");
            T = A.GetComponent<CTrapSelect>();
            T.SetSelect();
        }
        m_bSetting = true;
    }
}
