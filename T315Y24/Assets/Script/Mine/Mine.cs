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
    private double dTimeToExplosion = 0.0d;                    // 爆発までの時間[s]
    [SerializeField] private double dExplosionDelay = 2.0d;    // 爆発までの待機時間[s]
    public double dMineCoolTime = 0.0d;                        // 地雷クールタイム[s]
    [SerializeField] public double dMineInterval = 5.0d;       // 地雷再利用までの時間[s]
    private bool bCanExplode = true;                           // 地雷利用 true:可能 false:不可
    public GameObject ExplosionEffectPrefab;                   // 爆発時生成されるプレハブ
    private Text CoolDownText;                                 // クールダウン表示テキスト
    [SerializeField] private int nFontSize=24;                 // クールダウンのフォントサイズ変更用
    /*フォントを変えるときは外してください
    [SerializeField] private Font customFont;                  // フォント変更用
    */
    void Start()
    {
        CoolDownText = GetComponentInChildren<Text>();
        CoolDownText.text = dMineCoolTime.ToString();   //textの初期化
        CoolDownText.fontSize = nFontSize;              // フォントサイズを変更
        CoolDownText.alignment = TextAnchor.MiddleCenter;
        CoolDownText.gameObject.SetActive(false);       //使用前なので非表示

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
        if (collision.gameObject.CompareTag("Enemy") && bCanExplode)  //Enemyタグがついている＆地雷使用可能
        {
            bCanExplode = false;                    //同時に複数個同じ場所に生成されるのを防止
            dTimeToExplosion = dExplosionDelay;     //爆発までの時間計測
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
        if (dTimeToExplosion > 0.0d)   //待機中
        {
            dTimeToExplosion -= Time.fixedDeltaTime;
            if (dTimeToExplosion <= 0.0d)   //爆発までの待機時間が終わったら
            {   //爆発エフェクト生成
                Instantiate(ExplosionEffectPrefab, transform.position, Quaternion.identity);
                dMineCoolTime = dMineInterval;  //地雷の再利用時間計測
                CoolDownText.gameObject.SetActive(true);
            }
        }
        //＞地雷の再利用カウントダウン
        if (dMineCoolTime > 0.0d)   //クールダウン中
        {

            dMineCoolTime -= Time.fixedDeltaTime;
            CoolDownText.text = dMineCoolTime.ToString("F1");   //小数点以下一桁までクールダウン表示
            if (dMineCoolTime < 0.0d) 
            { 
                bCanExplode = true;
                CoolDownText.gameObject.SetActive(false);
            }  //地雷利用可能に
        }
    }
}
