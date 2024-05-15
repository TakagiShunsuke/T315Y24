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
13:プレイヤーの移動と角度の修正、unity上でスピードを変更できるように変更:iwamuro
13:変数名変更:takagi
=====*/

//＞名前空間宣言
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//＞クラス定義
public class PlayerScript : MonoBehaviour, IDamageable
{
    //＞変数宣言
    Rigidbody m_Rb;      // Rigidbodyを追加
    [SerializeField] private float m_fSpeed; //プレイヤーの移動速度を設定
    [SerializeField] private double m_dHp;   // HP

    //＞プロパティ定義
    public double HP
    {
        get { return m_dHp; }
        private set { m_dHp = value; }
    }

    /*＞初期処理関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：Rigidbodyコンポーネントを追加
    */
    void Start()    //自動で追加される
    {
        m_Rb = GetComponent<Rigidbody>(); //Rigidbodyコンポーネントを追加
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
        Vector3 moveDirection = Vector3.zero; // 移動方向の初期化
        Vector3 target_dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));    //プレイヤーの向きを変えるベクトル

        if (target_dir.magnitude > 0.1) //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える
        {
            //体の向きを変更
            transform.rotation = Quaternion.LookRotation(target_dir);
            //前方へ移動
            transform.Translate(Vector3.forward * Time.deltaTime * m_fSpeed);
        }
    
        // 斜め移動
        if (moveDirection != Vector3.zero)
        {
    
            // 正規化して移動速度を一定に保つ
            moveDirection.Normalize();
            m_Rb.velocity = moveDirection * m_fSpeed;

        }
        else
        {
            // 何もキーが押されていない場合は停止する
            m_Rb.velocity = Vector3.zero;
        }

       
    }

    /*＞被ダメージ関数
    引数：double dDamageVal
    ｘ
    戻値：なし
    ｘ
    概要：ダメージを受ける
    */
    public void Damage(double dDamageVal)
    {
        //＞ダメージ計算
        HP -= dDamageVal;    //HP減少
    }
}

