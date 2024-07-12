/*=====
<Random.cs> //スクリプト名
└作成者：takagi

＞内容
ランダマイズ

＞更新履歴
__Y24
_M05
D
06:プログラム作成:takagi

_M06
D
21:リファクタリング:takagi
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System;

//＞名前空間定義
namespace UseRandom
{
    //＞クラス定義
    static class CUseRandom
    {
        /*＞コンストラクタ
        引数１：なし
        ｘ
        戻値：なし
        ｘ
        概要：生成時に行う処理
        */
        static CUseRandom()
        {
            //＞乱数シード初期化
            UnityEngine.Random.InitState(DateTime.Now.Millisecond); //乱数(線形合同法)

#if UNITY_EDITOR    //エディタ使用中
            //＞エラー出力
            UnityEngine.Debug.Log("ランダム初期化");   //ログ出力
#endif
        }
    }
}