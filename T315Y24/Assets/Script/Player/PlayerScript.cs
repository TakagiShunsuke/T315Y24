/*=====
<PlayerScript.cs> 
└作成者：iwamuro

＞内容
Playerを動かすスプリクト

＞更新履歴
__Y24
_M05
D
04:プログラム作成:iwamuro

=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//＞クラス定義
public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    
    [SerializeField] private float fspeed; //プレイヤーの移動速度を設定

    /*＞初期処理関数
引数：なし
ｘ
戻値：なし
ｘ
概要：Rigidbodyコンポーネントを追加
*/
    void Start()    //自動で追加される
    {
      
        rb = GetComponent<Rigidbody>(); //Rigidbodyコンポーネントを追加
    }


    /*＞移動処理関数
引数：なし
ｘ
戻値：なし
ｘ
概要：キーが押されたら移動をを行う
*/
    void Update()
    {
        Vector3 moveDirection = Vector3.zero; // 移動方向の初期化

        if (Input.GetKey(KeyCode.UpArrow))  // 上Arrowキーでプレイヤーを上に移動させる
        {
            moveDirection += transform.forward * fspeed;
      //      transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        }
    
        if (Input.GetKey(KeyCode.DownArrow)) // 下Arrowキーでプレイヤーを下に移動させる
        {
            moveDirection -= transform.forward * fspeed;
         //   transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        if (Input.GetKey(KeyCode.RightArrow)) // 右Arrowキーでプレイヤーを右に移動させる
        {
            moveDirection += transform.right * fspeed;
       //    transform.rotation = Quaternion.Euler(0f, 270f, 0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) // 左Arrowキーでプレイヤーを左に移動させる
        {
            moveDirection -= transform.right * fspeed;
     //       transform.rotation = Quaternion.Euler(0f, 90f, 0f);
          
        }

        // 斜め移動
        if (moveDirection != Vector3.zero)
        {
    
            // 正規化して移動速度を一定に保つ
            moveDirection.Normalize();
            rb.velocity = moveDirection * fspeed;

        }
        else
        {
            // 何もキーが押されていない場合は停止する
            rb.velocity = Vector3.zero;
        }

    }

}

