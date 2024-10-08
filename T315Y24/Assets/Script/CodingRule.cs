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

_M06
D
20:警告文が出る問題を修正・
    インスペクタでの見た目を意識した表記改善・
    メンバでない変数名の記述法変更:takagi
=====*/

//＞名前空間宣言
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;   //名前宣言時にはコメントは不要(勝手にずれるため)

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
        E_ENUM_A,   //列挙名_XXと続ける
        E_ENUM_B,
    }

    //＞構造体定義
    private struct Struct
    {
        GameObject m_Object;    //クラス型のネーミングはハンガリアン記法に従わない
                                //※m_やs_などの型とは関係ない部分では従う
                                //接頭辞の後ろは大文字から始める
        Ray m_Ray;
    }
    [Serializable]
    public struct SerializeStruct
    {
        [Tooltip("簡易説明")] public GameObject m_Member;   //※シリアライズできる場合、その変数が何なのかインスペクタからわかるようにする
    }

    //＞変数宣言
    [Space] //空行を活用し見やすくする
    [Header("初期化")] //変数を分類ごとに分けて記述
    [SerializeField, Tooltip("メンバー変数")] private uint m_uMember;   //属性は記法に影響しない
    private int m_nInt; //通常の型はハンガリアン記法に従う[メンバ変数はm_と付ける]
    [SerializeField, Tooltip("構造体")] private SerializeStruct m_Struct;   //その変数が何なのかインスペクタからわかるようにする
    private static string m_szStr;  //メンバ変数∧静的変数なら融合してms_と記述する

    //＞プロパティ定義
    public double PriProp { get; private set; } //readonlyな形式でも記法は無し

    //初回関数定義前に2行空ける
    /*＞例関数
    引数１：double _dDouble：数値   //引数：内容の形で記述
    引数２：GameObject _GameObject：物体   //引数は_から始める
    ｘ
    戻値：虚無   //内容のみ記述
    ｘ
    概要：関数記述例
    */
    private int Example(double _dDouble, GameObject _GameObject)
    {
        //＞変数宣言
        float _fFloat = 0.0f;    //メンバー変数も_から始める
        GameObject _Object = _GameObject;  //接頭字が無い場合、頭文字を大文字にする

        //＞算出
        m_nInt = (int)((float)_dDouble * _fFloat);  //なるべく全処理にコメントをつける

        //＞提供
        return m_nInt;
    }

    /*＞xx関数
    引数：なし   //引数がない場合は１を省略してもよい
    ｘ
    戻値：なし
    ｘ
    概要：関数例
    */
    public void Function()
    {
    }
}