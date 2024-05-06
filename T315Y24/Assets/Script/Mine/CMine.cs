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

//＞クラス定義
public class CMine : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] private double ExplosionRadius = 5.0d;  // 地雷爆発範囲
    [SerializeField] private double ExplosionDelay = 2.0d;   // 爆発までの待機時間
    [SerializeField] private double RechargeDelay = 7.0d;    // 再利用までの時間
    private LayerMask EnemyLayer;        // 地雷の影響を受けるオブジェクトを指定
    private bool CanExplode = true;     //地雷利用 true:可能 false:不可

    /*＞地雷当たり判定関数
    引数１：当たり判定があったオブジェクトの情報
    ｘ
    戻値：なし
    ｘ
    概要：敵が地雷に触れたときのみ処理される
    */
    private void OnTriggerEnter(Collider other)     //地雷に何かが当たってきたとき
    {
        if (other.CompareTag("Enemy")&&CanExplode)  //Enemyタグがついている＆地雷使用可能
        {
            ExplodeAfterDelay();            //爆発ディレイ計測
            StartCoroutine(RechargeMine()); //再利用時間計測
        }
    }

    /*＞地雷再利用時間計測の関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：再利用の時間をカウント
    */
    IEnumerator RechargeMine()
    {
        CanExplode = false;     //地雷使用不可
        yield return new WaitForSeconds((float)RechargeDelay);      //引数分待機する
        CanExplode = true;      //待機が終わると地雷使用可能にする
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
        yield return new WaitForSeconds((float)ExplosionDelay);     //引数分待機する

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
        //地雷の範囲に入ってる敵（EnemyLayer）のみ配列に入れる
        Collider[] colliders = Physics.OverlapSphere(transform.position, (float)ExplosionRadius, EnemyLayer);

        foreach (Collider hit in colliders) //collidersの中に入ってる全部for文で繰り返す
        {
            // IDestroyを実装していたら
            if (hit.gameObject.TryGetComponent<IFeatureMine>(out var destroy))
                if (destroy != null)            //取得成功しているか
                {
                    destroy.TakeDestroy();      //破壊の処理をする
                } 
        }
    }
}

