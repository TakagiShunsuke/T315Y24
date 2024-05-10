/*=====
<CMine.cs> 
└作成者：yamamoto

＞内容
地雷オブジェクトにアタッチするスクリプト

＞更新履歴
__Y24
_M05
D
03:プログラム作成:yamamoto
06:インターフェース追加:yamamoto

=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//＞クラス定義
public class CMine : MonoBehaviour
{
    //＞変数宣言
    private double dTimeToExplosion = 0.0d;                     //爆発までの時間[s]
    [SerializeField] private double dExplosionDelay = 2.0d;     // 爆発までの待機時間[s]
    private double dMineCoolTime = 0.0d;                        //地雷クールタイム[s]
    [SerializeField] private double dMineInterval = 5.0d;       // 地雷再利用までの時間[s]
    private bool bCanExplode = true;                            //地雷利用 true:可能 false:不可
    
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
        }

        //＞地雷の再利用カウントダウン
        if (dMineCoolTime > 0.0d)   //クールダウン中
        {
            dMineCoolTime -= Time.fixedDeltaTime;
            if (dMineCoolTime < 0.0d) { bCanExplode = true; Debug.LogWarning("再利用"); }  //地雷利用可能に
        }

        if ((!bCanExplode) && dTimeToExplosion <= 0.0d && dMineCoolTime<=0.0d) //地雷が踏まれた＆爆発のカウントダウンが終了
        {
            Debug.LogWarning("使用中");
            ExplosionManager.Instance.CreateExplosion(transform.position);
            dMineCoolTime = dMineInterval;
        }
    }
}

