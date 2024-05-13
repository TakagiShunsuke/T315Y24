/*=====
<Enemy.cs> //スクリプト名
└作成者：takagi

＞内容
敵の親クラス

＞注意事項
敵全体のインスタンス数を管理するため、
継承先のクラスにStart(),OnDestroy()関数を追加するときは必ず呼び出してください。

＞更新履歴
__Y24
_M05
D
07:プログラム作成:takagi
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;  //Unity

//＞クラス定義
public abstract class CEnemy : MonoBehaviour
{
    //＞プロパティ定義
    public uint m_unValInstance { get; private set; } = 0;   //インスタンス数

    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    virtual public void Start()
    {
        //＞カウント
        m_unValInstance++;  //生成数増加
    }

    /*＞終了関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：破棄時に行う処理
    */
    virtual protected void OnDestroy()
    {
        //＞カウント
        m_unValInstance--;  //生成数減少
    }
}