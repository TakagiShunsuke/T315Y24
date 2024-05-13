/*=====
<UI3dCamera.cs> 
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
public class UI3dCamera : MonoBehaviour
{
    /*＞更新関数
  引数１：なし
  ｘ
  戻値：なし
  ｘ
  概要：更新処理
  */
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;    //カメラの角度を更新
    }
}
