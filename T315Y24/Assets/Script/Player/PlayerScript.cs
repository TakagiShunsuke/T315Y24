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

//＞クラス定義
public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    float fspeed = 3.0f;    //プレイヤーの移動速度

    void Start()    //自動で追加される
    {
        rb = GetComponent<Rigidbody>(); //Rigidbodyコンポーネントを追加
    }

    void Update()   //キーが押されたときに更新を行う
    {
        if (Input.GetKey(KeyCode.UpArrow))  //上Arrowキーでプレイヤーを上に移動させる
        {
            rb.velocity = transform.forward * fspeed;
        }
        if (Input.GetKey(KeyCode.DownArrow)) //下Arrowキーでプレイヤーを下に移動させる
        {
            rb.velocity = -transform.forward * fspeed;
        }
        if (Input.GetKey(KeyCode.RightArrow)) //右Arrowキーでプレイヤーを右に移動させる
        {
            rb.velocity = transform.right * fspeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) //左Arrowキーでプレイヤーを左に移動させる
        {
            rb.velocity = -transform.right * fspeed;
        }
    }
}
