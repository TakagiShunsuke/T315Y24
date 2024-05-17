using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CArrowRain : MonoBehaviour
{
    //＞変数宣言

    private double m_dArrowRainCoolTime = 0.0d;                  // 地雷クールタイム[s]
    [SerializeField] private double m_dArrowRainInterval = 5.0d; // 地雷再利用までの時間[s]
    private bool m_bCanArrowRain = true;                         // 地雷利用 true:可能 false:不可
   // [SerializeField] private GameObject m_ArrowRainEffectPrefab; // 爆発時生成されるプレハブ
    private Text m_CoolDownText;                                 // クールダウン表示テキスト
    [SerializeField] private int m_nFontSize = 24;               // クールダウンのフォントサイズ変更用


    [SerializeField] private GameObject arrowPrefab; // 矢のPrefab
    [SerializeField] private Transform[] shootPoints; // 矢を発射する位置
    [SerializeField] private int arrowsPerPress = 1; // 一度に発射する矢の本数
    [SerializeField] private float shootForce = 100f; // 矢の発射速度

    // Start is called before the first frame update
    void Start()
    {
        m_CoolDownText = GetComponentInChildren<Text>();
        m_CoolDownText.text = m_dArrowRainCoolTime.ToString();   // textの初期化
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
        if (collision.gameObject.CompareTag("Enemy") && m_bCanArrowRain)  // Enemyタグがついている＆地雷使用可能
        {
            m_bCanArrowRain = false;                      // 同時に複数個同じ場所に生成されるのを防止
            //爆発エフェクト生成
            
            ShootArrows();
            m_dArrowRainCoolTime = m_dArrowRainInterval;  //地雷の再利用時間計測
            m_CoolDownText.gameObject.SetActive(true);
        }
    }
    public void ShootArrows()
    {
        for (int i = 0; i < arrowsPerPress; i++)
        {
            foreach (Transform shootPoint in shootPoints)
            {
                GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
                Rigidbody rb = arrow.GetComponent<Rigidbody>();
                if (rb != null)
                {

                    // 矢を発射点の向きに飛ばす
                    Vector3 shootDirection = shootPoint.forward;
                    rb.velocity = shootDirection * shootForce;

                    // 矢の進行方向に合わせて回転を設定
                    arrow.transform.rotation = Quaternion.LookRotation(shootDirection);
                    //rb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
                }
            }
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
        //＞地雷の再利用カウントダウン
        if (m_dArrowRainCoolTime > 0.0d)   //クールダウン中
        {
            m_dArrowRainCoolTime -= Time.fixedDeltaTime;
            m_CoolDownText.text = m_dArrowRainCoolTime.ToString("F1");   //小数点以下一桁までクールダウン表示
            if (m_dArrowRainCoolTime < 0.0d)
            {
                m_bCanArrowRain = true;
                m_CoolDownText.gameObject.SetActive(false);
            }  //地雷利用可能に
        }
    }

}
