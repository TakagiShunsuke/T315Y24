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
11:体力・攻撃を受ける処理追加:takagi
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//＞クラス定義
public class PlayerScript : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    Rigidbody rb;
    float fspeed = 3.0f;    //プレイヤーの移動速度
    [SerializeField] private double m_dHP = 10.0d;   //体力


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

    /*＞移動処理関数
    引数：double dDamageVal
    ｘ
    戻値：なし
    ｘ
    概要：ダメージを受ける
    */
    public void Damage(double dDamageVal)
    {
        //＞ダメージ計算
        m_dHP -= dDamageVal;    //HP減少
    }
}
