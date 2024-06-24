/*=====
<TrackCamera.cs> 
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

_M06
D
21:リファクタリング:takagi
24:リファクタリング:takagi
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//＞クラス定義
public class CTrackCamera : MonoBehaviour
{
    //＞変数宣言
    [Header("追跡情報")]
    [SerializeField, Tooltip("追跡対象")] private GameObject m_Target;  //追従するオブジェクト
    [SerializeField, Tooltip("相対位置")] private Vector3 m_RelativePosition; //カメラの位置


    /*＞更新関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：更新処理
    */
    private void Update()
    {
        //＞変数宣言
        var _PlayerPosition = m_Target.gameObject.transform.position;  //プレイヤーの座標取得

        //＞更新
        transform.position = _PlayerPosition + m_RelativePosition;     //カメラの座標を計算
    }
}