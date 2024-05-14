/*=====
<GameControlScript.cs> 
└作成者：iwamuro

＞内容
変数の内容をUI Textに表示するスプリクト

＞更新履歴
__Y24
_M05
D
09:プログラム作成:iwamuro

=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //UI表示用

//名前空間定義
namespace UI
{
    public static class CUI
    {
        //＞定数定義
        private const uint CONST = 0;    //仮置きのfps値

        //＞変数宣言
        public static readonly double ms_Temp = 0.0d;   //readonlyな変数も書き方は同じ
    }
}

//＞クラス定義
public class GameControlScript : MonoBehaviour
{
    // Start is called before the first frame update
    //＞定数定義
    public Text TextFrame;  //テキストを表示する場所
    public Text variable;  //変数

    //＞変数宣言
    private int Iframe;     //表示する変数

 
    private int Iframe2;     //表示する変数

    /*＞初期化関数
   引数：なし
   ｘ
   戻値：なし
   ｘ
   概要：表示する変数を初期化する
   */
    void Start()
    {
        Iframe = 0; //値の初期化
    }
    /*＞更新関数
  引数：なし
  ｘ
  戻値：なし
  ｘ
  概要：指定した変数を表示する
  */

    void Update()
    {
      //  Iframe2 = "討伐数";
        TextFrame.text = string.Format("{0:00000} 討伐数", Iframe);　//指定した値を表示する
        Iframe++;   //変数をプラスする
    }
}
