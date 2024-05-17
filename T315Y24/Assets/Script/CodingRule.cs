/*=====
<CodingRule.cs> //スクリプト名
└作成者：takagi

＞内容
コーディング規約を記述

＞注意事項   //ないときは省略OK
この規約書に記述のないものは判明次第、適宜追加する

＞更新履歴
__Y24   //'24年
_M04    //04月
D       //日
16:プログラム作成:takagi   //日付:変更内容:施行者
17:あいうえお:takagi

_M05
D
03:いろはにほへと:takagi
04:いくつかの記法追加・誤った表記を修正:takagi
14:インターフェースについて記述:takagi
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//＞名前空間定義
namespace Space
{
    //＞クラス定義
    public static class CSpace
    {
        //＞定数定義
        private const uint CONST = 0;    //仮置きのfps値

        //＞変数宣言
        public static readonly double ms_Temp = 0.0d;   //readonlyな変数も書き方は同じ
    }
}

//＞インターフェース定義
public interface IInterface //インターフェースの頭文字にIをつける
{
    //＞プロパティ定義
    public double Prop { get; set; }    //自動実装プロパティはハンガリアン記法を無視してよい

    //＞プロトタイプ宣言
    public void Signaled();
}

//＞クラス定義
public class CCodingRule : MonoBehaviour    //クラス型の頭文字にCをつける
{
    //＞列挙定義
    public enum E_ENUM  //列挙は接頭字をE_とする
    {
        E_ENUM_A,
        E_ENUM_B,
    }

    //＞構造体定義
    private struct Struct
    {
        GameObject m_Object;    //クラス型のネーミングはハンガリアン記法に従わない
                                //※m_やs_などの型とは関係ない部分では従う
        Ray m_Ray;
    }

    //＞変数宣言
    private int m_nInt; //通常の型はハンガリアン記法に従う[メンバ変数はm_と付ける]
    private static string m_szStr;  //メンバ変数∧静的変数なら融合してms_と記述する
    [SerializeField] private uint m_uInner;   //属性は記法に影響しない

    //＞プロパティ定義
    public double PriProp { get; private set; } //readonlyな形式でも記法は無し

    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    void Start()
    {
        
    }

    /*＞物理更新関数
    引数：なし   //引数がない場合は１を省略してもよい
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    private void FixedUpdate()
    {
    }

    /*＞xx関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：関数例
    */
    public void Function()
    {
        GameObject Object;  //接頭字が無い場合、頭文字を大文字にする 
    }

    /*＞例関数
    引数１：double dDouble：数値   //引数：内容の形で記述
    引数２：GameObject GameObject：物体
    ｘ
    戻値：虚無   //内容のみ記述
    ｘ
    概要：関数記述例
    */
    private int Example(double dDouble, GameObject GameObject)
    {
        //＞変数宣言
        float fFloat = 0.0f;

        //＞算出
        m_nInt = (int)(dDouble * fFloat);

        //＞提供
        return m_nInt;
    }
}