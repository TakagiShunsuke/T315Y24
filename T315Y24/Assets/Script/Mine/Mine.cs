/*=====
<Mine.cs> 
└作成者：yamamoto

＞内容
地雷に付けるスクリプト。
爆破エフェクトの生成はここで

＞注意事項  
地雷にIsTriggerがついていると動作しません。
Prefabを設定していないと爆発エフェクトが生成されない。

＞更新履歴
__Y24   
_M05    
D
8 :プログラム作成:yamamoto 
9 :仕様変更の為処理を変更:yamamoto
10:コメント追加:yamamoto
12:リキャスト時間追加:yamamoto
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;

//＞クラス定義
public class Mine : MonoBehaviour
{
    //＞変数宣言
    private double m_dTimeToExplosion = 0.0d;                    // 爆発までの時間[s]
    [SerializeField] private double m_dExplosionDelay = 2.0d;    // 爆発までの待機時間[s]
    private double m_dMineCoolTime = 0.0d;                       // 地雷クールタイム[s]
    [SerializeField] private double m_dMineInterval = 5.0d;      // 地雷再利用までの時間[s]
    private bool m_bCanExplode = true;                           // 地雷利用 true:可能 false:不可
    [SerializeField] private GameObject m_ExplosionEffectPrefab; // 爆発時生成されるプレハブ
    private Text m_CoolDownText;                                 // クールダウン表示テキスト
    [SerializeField] private int m_nFontSize=24;                 // クールダウンのフォントサイズ変更用

    /*フォントを変えるときは外してください
    [SerializeField] private Font m_CustomFont;                  // フォント変更用
    */

    /*＞初期化関数
  引数１：なし
  ｘ
  戻値：なし
  ｘ
  概要：インスタンス生成時に行う処理
  */
    void Start()
    {
        m_CoolDownText = GetComponentInChildren<Text>();
        m_CoolDownText.text = m_dMineCoolTime.ToString();   // textの初期化
        m_CoolDownText.fontSize = m_nFontSize;              // フォントサイズを変更
        m_CoolDownText.alignment = TextAnchor.MiddleCenter; // textの表示位置を真ん中に
        m_CoolDownText.gameObject.SetActive(false);         // 使用前なので非表示
    }

    /*＞地雷当たり判定関数
    引数１：当たり判定があったオブジェクトの情報
    ｘ
    戻値：なし
    ｘ
    概要：敵が地雷に触れたときのみ処理される
    */
    private void OnCollisionStay(Collision collision)     //地雷に何かが当たってきたとき
    {
        if (collision.gameObject.CompareTag("Enemy") && m_bCanExplode)  // Enemyタグがついている＆地雷使用可能
        {
            m_bCanExplode = false;                      // 同時に複数個同じ場所に生成されるのを防止
            m_dTimeToExplosion = m_dExplosionDelay;     // 爆発までの時間計測
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
        //＞爆発カウントダウン
        if (m_dTimeToExplosion > 0.0d)   //待機中
        {
            m_dTimeToExplosion -= Time.fixedDeltaTime;
            if (m_dTimeToExplosion <= 0.0d)   //爆発までの待機時間が終わったら
            {   //爆発エフェクト生成
                Instantiate(m_ExplosionEffectPrefab, transform.position, Quaternion.identity);
                m_dMineCoolTime = m_dMineInterval;  //地雷の再利用時間計測
                m_CoolDownText.gameObject.SetActive(true);
            }
        }

        //＞地雷の再利用カウントダウン
        if (m_dMineCoolTime > 0.0d)   //クールダウン中
        {
            m_dMineCoolTime -= Time.fixedDeltaTime;
            m_CoolDownText.text = m_dMineCoolTime.ToString("F1");   //小数点以下一桁までクールダウン表示
            if (m_dMineCoolTime < 0.0d) 
            { 
                m_bCanExplode = true;
                m_CoolDownText.gameObject.SetActive(false);
            }  //地雷利用可能に
        }
    }
}
