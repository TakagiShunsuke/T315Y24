/*=====
<PlayerCamera.cs> 
└作成者：yamamoto

＞内容
プレイヤーを追従するカメラのスクリプト

＞更新履歴
__Y24   
_M05    
D
11:スクリプト作成:yamamoto
=====*/


//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//＞クラス定義
public class camera : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] private Vector3 pos;   //カメラの位置
    GameObject Player;                      //プレイヤーオブジェクト用

    /*＞初期化関数
   引数１：なし
   ｘ
   戻値：なし
   ｘ
   概要：インスタンス生成時に行う処理
   */
    void Start()
    {
        Player = GameObject.Find("Player");
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
        var playerPosition = Player.gameObject.transform.position;  //プレイヤーの座標取得

        transform.position = playerPosition + pos;                  //カメラの座標を計算
    }
}