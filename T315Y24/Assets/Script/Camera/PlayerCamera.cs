/*=====
<PlayerCamera.cs> 
└作成者：isi

＞内容
プレイヤーを追従するカメラのスクリプト

＞更新履歴
__Y24   
_M05    
D
10:スクリプト作成:isi
11:雛型作成:takagi
12:変数名変更:yamamoto
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//＞クラス定義
public class camera : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] private Vector3 m_Pos;             //カメラの位置
    [SerializeField] private string m_PlayerName;       //追従するプレイヤーオブジェクトの名前
    private GameObject m_Player;                        //プレイヤーオブジェクト用
    
    /*＞初期化関数
   引数１：なし
   ｘ
   戻値：なし
   ｘ
   概要：インスタンス生成時に行う処理
   */
    void Start()
    {
        m_Player = GameObject.Find(m_PlayerName);
        Debug.Log(m_Player);
    }

    /*＞更新関数
   引数１：なし
   ｘ
   戻値：なし
   ｘ
   概要：更新処理
   */
    void Update()
    {
        var PlayerPosition = m_Player.gameObject.transform.position;  //プレイヤーの座標取得

        transform.position = PlayerPosition + m_Pos;                  //カメラの座標を計算
    }
}