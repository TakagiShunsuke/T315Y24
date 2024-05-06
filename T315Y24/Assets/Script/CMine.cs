/*=====
<Mine.cs> 
└作成者：yamamoto

＞内容
地雷オブジェクトにアタッチするスクリプト

＞注意事項   //ないときは省略OK
この規約書に記述のないものは判明次第、適宜追加する

＞更新履歴
__Y24   //'24年
_M05    //05月
D       //日
03:プログラム作成:yamamoto
06:インターフェース追加:yamamoto

=====*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMine : MonoBehaviour
{
    public float ExplosionRadius = 5f;  // 地雷爆発範囲
    public float ExplosionDelay = 2f;   // 爆発までの待機時間
    public float RechargeDelay = 7f;    // 再充電までの待機時間
    public LayerMask EnemyLayer;        // 地雷の影響を受けるオブジェクトを指定

    private bool CanExplode = true;

    public interface IDestroy
    { 
        void TakeDestroy();
    }

    /*＞地雷当たり判定関数
    引数１：当たり判定があったオブジェクトの情報
    ｘ
    戻値：なし
    ｘ
    概要：敵が地雷に触れたときのみ爆発する
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")&&CanExplode)  //Enemyタグがついている＆地雷使用可能
        {
            ExplodeAfterDelay();            //爆発ディレイ計測
            StartCoroutine(RechargeMine()); //再利用時間計測
        }
    }

    /*＞地雷再利用の判定の関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：再利用の時間をカウント
    */
    IEnumerator RechargeMine()
    {
        CanExplode = false;
        yield return new WaitForSeconds(RechargeDelay);
        CanExplode = true;
    }

    /*＞爆発時差関数
   引数１：なし
   ｘ
   戻値：なし
   ｘ
   概要：爆発までの時間をカウント
   */
    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(ExplosionDelay);

        // 爆発処理をここに記述
        Explode();
    }

    /*＞爆発処理関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：爆発処理
    */
    void Explode()
    {
        //地雷の範囲に入ってる敵のみ配列に入れる
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, EnemyLayer);

        foreach (Collider hit in colliders) //collidersの中に入ってる全部for文で繰り返す
        {
            // IDestroyを実装していたら
            if (hit.gameObject.TryGetComponent<IDestroy>(out var destroy))
                if (destroy != null)
                {
                    destroy.TakeDestroy();
                } 
        }
    }
}

