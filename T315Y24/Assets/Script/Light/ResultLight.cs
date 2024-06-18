/*=====
<ResultLight.cs>
└作成者：takagi

＞内容
リザルト用ライト回転

＞更新履歴
__Y24
_M06
D
18:プログラム作成:takagi
=====*/

//＞名前空間宣言
using System;
using System.Collections;   //list
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;  //Unity

//＞クラス定義
public class CResultLight : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] private Vector3 m_vInitShiftRotate;
    [SerializeField] private Vector3 m_vRotate;

    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(m_vInitShiftRotate);
    }

    /*＞物理更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    private void FixedUpdate()
    {
        transform.Rotate(m_vRotate);
    }
}