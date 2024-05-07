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
    float speed = 3.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Rigidbodyを追加
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))  //Arrowキーでプレイヤーを移動させる
        {
            rb.velocity = transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = -transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = transform.right * speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = -transform.right * speed;
        }
    }
}
