using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mine : MonoBehaviour
{
    //＞変数宣言
    private double dTimeToExplosion = 0.0d;                     //爆発までの時間[s]
    [SerializeField] private double dExplosionDelay = 2.0d;     // 爆発までの待機時間[s]
    private double dMineCoolTime = 0.0d;                        //地雷クールタイム[s]
    [SerializeField] private double dMineInterval = 5.0d;       // 地雷再利用までの時間[s]
    private bool bCanExplode = true;                            //地雷利用 true:可能 false:不可
    public GameObject ExplosionEffectPrefab;

    /*＞地雷当たり判定関数
    引数１：当たり判定があったオブジェクトの情報
    ｘ
    戻値：なし
    ｘ
    概要：敵が地雷に触れたときのみ処理される
    */
    private void OnCollisionEnter(Collision collision)     //地雷に何かが当たってきたとき
    {
        if (collision.gameObject.CompareTag("Enemy") && bCanExplode)  //Enemyタグがついている＆地雷使用可能
        {
            Debug.LogWarning("最初");
            bCanExplode = false;
            dTimeToExplosion = dExplosionDelay;     //爆発までの時間計測
        }
    }

    private void FixedUpdate()
    {
        //＞爆発カウントダウン
        if (dTimeToExplosion > 0.0d)   //待機中
        {
            dTimeToExplosion -= Time.fixedDeltaTime;
            if (dTimeToExplosion <= 0.0d)
            {
                //ExplosionManager.Instance.CreateExplosion(transform.position);
                Instantiate(ExplosionEffectPrefab, transform.position, Quaternion.identity);
                dMineCoolTime = dMineInterval;
            }
        }

        //＞地雷の再利用カウントダウン
        if (dMineCoolTime > 0.0d)   //クールダウン中
        {
            dMineCoolTime -= Time.fixedDeltaTime;
            if (dMineCoolTime < 0.0d) { bCanExplode = true; Debug.LogWarning("再利用"); }  //地雷利用可能に
        }
    }
}
