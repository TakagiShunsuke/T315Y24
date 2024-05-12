/*=====
<Explosion.cs> 
└作成者：yamamoto

＞内容
地雷エフェクト用に付けるスクリプト

＞注意事項  
地雷エフェクト用にIsTriggerを付けないと動作しません。

＞更新履歴
__Y24   
_M05    
D
9 :プログラム作成:yamamoto
10:コメント追加:yamamoto
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

//＞クラス定義
public class Explosion : MonoBehaviour
{
    //＞変数宣言
    private float ObjectRadius;      //オブジェクトの半径
    private Vector3 InitialObjectPos; // オブジェクトの初期位置
    [SerializeField] private double LowerSpeed = 0.1d;   //オブジェクトが下に消えていく速度

    /*＞初期化関数
  引数１：なし
  ｘ
  戻値：なし
  ｘ
  概要：インスタンス生成時に行う処理
  */
    void Start()
    {
        ObjectRadius = transform.localScale.x / 2.0f;   // オブジェクトの半径を取得
        InitialObjectPos = transform.position;          // 初期位置を設定

        // 範囲内の敵を検出
        Collider[] colliders = Physics.OverlapSphere(transform.position, ObjectRadius);
        foreach (Collider collider in colliders)    //Collider[]の中に入っているだけループする
        {
            if (collider.CompareTag("Enemy"))   //当たり判定の中にあるものに敵タグがついてるか確認
            {
                //IFeatureMineがついるか確認
                if (collider.gameObject.TryGetComponent<IFeatureMine>(out var destroy))
                    destroy.TakeDestroy();  //敵削除
            }
        }
    }

    //仕様変更があるかもだから一応残す

    /*＞敵to爆破当たり判定関数
  引数１：当たり判定があったオブジェクトの情報
  ｘ
  戻値：なし
  ｘ
  概要：当たったとき敵を削除する関数
  */
    /*private void OnTriggerStay(Collider other)
    {
        // 範囲内の敵を検出
        Collider[] colliders = Physics.OverlapSphere(transform.position, ObjectRadius);
        foreach (Collider collider in colliders)    //Collider[]の中に入っているだけループする
        {
            if (collider.CompareTag("Enemy"))   //当たり判定の中にあるものに敵タグがついてるか確認
            {
                //IFeatureMineがついるか確認
                if (collider.gameObject.TryGetComponent<IFeatureMine>(out var destroy))
                    destroy.TakeDestroy();  //敵削除
            }
        }
    }*/

    /*＞更新関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：更新処理
    */
    // Update is called once per frame
    void Update()
    {
        // オブジェクトを下に移動させる
        transform.position -= new Vector3(0f, (float)LowerSpeed * Time.deltaTime, 0f);

        // 半径分だけ下に移動したかどうかを判断し、破壊する
        if (transform.position.y <= InitialObjectPos.y - ObjectRadius)
        {
            Destroy(gameObject);
        }
    }
}
