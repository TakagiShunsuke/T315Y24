/*=====
<Arrow.cs> 
└作成者：yamamoto

＞内容
発射される矢に付けるスクリプト

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

//＞クラス定義
public class CArrow : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] private float lifetime = 5f; // 矢の寿命（秒）

    /*＞初期化関数
   引数１：なし
   ｘ
   戻値：なし
   ｘ
   概要：インスタンス生成時に行う処理
   */
    void Start()
    {
        Rigidbody　rb = GetComponent<Rigidbody>();

        if (rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);  //進行方向に沿うように回転
        }
    }

    /*＞更新関数
     引数：なし
     ｘ
     戻値：なし
     ｘ
     概要：一定時間ごとに行う更新処理
     */
    void Update()
    {
        // 矢の寿命を設定し、一定時間後に破棄
        Destroy(gameObject, lifetime);
    }

    /*＞矢当たり判定関数
  引数１：当たり判定があったオブジェクトの情報
  ｘ
  戻値：なし
  ｘ
  概要：矢が何かに触れたときのみ処理される
  */
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        // 矢が敵に当たった場合
        if (other.gameObject.CompareTag("Enemy"))
        {
            //IFeatureMineがついるか確認
            if (other.gameObject.TryGetComponent<IFeatureMine>(out var destroy))
                destroy.TakeDestroy();  //敵削除
        }

        Destroy(gameObject);    //何かに当たれば消滅させる
    }
}
