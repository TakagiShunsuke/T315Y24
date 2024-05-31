/*=====
<ArrowRain.cs> 
└作成者：yamamoto

＞内容
矢を発射罠に付けるスクリプト。

＞注意事項  
罠起爆スイッチにIsTriggerがついていると動作しません。

＞更新履歴
__Y24   
_M05    
D
16 :プログラム作成:yamamoto 
30 :コメント追加  :yamamoto

=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//＞クラス定義
public class CArrowRain : MonoBehaviour
{
    //＞変数宣言
    private double m_dArrowRainCoolTime = 0.0d;                  // 地雷クールタイム[s]
    [SerializeField] private double m_dArrowRainInterval = 5.0d; // 地雷再利用までの時間[s]
    private bool m_bCanArrowRain = true;                         // 地雷利用 true:可能 false:不可
    private Text m_CoolDownText;                                 // クールダウン表示テキスト
    [SerializeField] private int m_nFontSize = 24;               // クールダウンのフォントサイズ変更用
    [SerializeField] private GameObject arrowPrefab;             // 矢のPrefab
    [SerializeField] private Transform[] shootPoints;            // 矢を発射する位置
    [SerializeField] private int arrowsPerPress = 1;             // 一度に発射する矢の本数
    [SerializeField] private float shootForce = 100f;            // 矢の発射速度

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
        m_CoolDownText.text = m_dArrowRainCoolTime.ToString();// textの初期化
        m_CoolDownText.fontSize = m_nFontSize;              // フォントサイズを変更
        m_CoolDownText.alignment = TextAnchor.MiddleCenter; // textの表示位置を真ん中に
        m_CoolDownText.gameObject.SetActive(false);         // 使用前なので非表示
    }

    /*＞起爆スイッチ当たり判定関数
    引数１：当たり判定があったオブジェクトの情報
    ｘ
    戻値：なし
    ｘ
    概要：敵が起爆スイッチに触れたときのみ処理される
    */
    private void OnCollisionStay(Collision collision)     //何かが当たってきたとき
    {
        if (collision.gameObject.CompareTag("Enemy") && m_bCanArrowRain)  // Enemyタグがついている＆罠使用可能
        {
            m_bCanArrowRain = false;    // 同時に複数個同じ場所に生成されるのを防止
            
            ShootArrows();                                //矢発射関数呼び出し
            m_dArrowRainCoolTime = m_dArrowRainInterval;  //再利用時間設定
            m_CoolDownText.gameObject.SetActive(true);
        }
    }

    /*＞矢発射関数
   引数１：当たり判定があったオブジェクトの情報
   ｘ
   戻値：なし
   ｘ
   概要：敵が起爆スイッチに触れたときのみ処理される
   */
    public void ShootArrows()
    {
        for (int i = 0; i < arrowsPerPress; i++)    //同じ場所から飛ばす本数
        {
            foreach (Transform shootPoint in shootPoints)   //飛ばす場所分
            {
                GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);//生成
                Rigidbody rb = arrow.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    //矢を発射点の向きに飛ばす
                    Vector3 shootDirection = shootPoint.forward;
                    rb.velocity = shootDirection * shootForce;

                    // 矢の進行方向に合わせて回転を設定
                    arrow.transform.rotation = Quaternion.LookRotation(shootDirection);
                }
            }
        }
    }
    /*＞更新関数
      引数：なし
      ｘ
      戻値：なし
      ｘ
      概要：一定時間ごとに行う更新処理
      */
    private void FixedUpdate()
    {
        //＞再利用カウントダウン
        if (m_dArrowRainCoolTime > 0.0d)   //クールダウン中
        {
            m_dArrowRainCoolTime -= Time.fixedDeltaTime;
            m_CoolDownText.text = m_dArrowRainCoolTime.ToString("F1");   //小数点以下一桁までクールダウン表示
            if (m_dArrowRainCoolTime < 0.0d)
            {
                m_bCanArrowRain = true;
                m_CoolDownText.gameObject.SetActive(false);
            }  //利用可能に
        }
    }

}
