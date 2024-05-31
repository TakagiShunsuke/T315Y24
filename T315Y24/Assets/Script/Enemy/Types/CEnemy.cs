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
10:プロパティ宣言変更:takagi
16:敵討伐数カウントを追加:yamamoto
30:コメント追加:yamamoto
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;  //Unity

//＞クラス定義
public abstract class CEnemy : MonoBehaviour
{
    //＞プロパティ定義
    static public uint ValInstance { get; private set; } = 0;   //インスタンス数
    static public int m_nDeadEnemyCount=0;                      //敵討伐数カウント


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
        ValInstance++;  //生成数増加
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
  
    /*＞カウント初期化関数
  引数１：なし
  ｘ
  戻値：なし
  ｘ
  概要：シーンが変わるときに呼ばれる処理
  */
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        m_nDeadEnemyCount = 0;
    }

    /*＞カウント関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：敵死亡時にカウントを行う処理
    */
    public void counter()
    {
        m_nDeadEnemyCount++;

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
        ValInstance--;  //生成数減少
    }
}