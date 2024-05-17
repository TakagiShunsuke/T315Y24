/*=====
<EnemyDeathCounter.cs> 
└作成者：iwamuro

＞内容
死んだ敵の数を2dUI表示させるスクリプト

＞更新履歴
__Y24
_M05
D
09:スクリプト作成:iwamuro
14:プログラム作成:yamamoto
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//＞クラス定義
public class EnemyDeathCounter : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] private TMPro.TMP_Text DeathCount_txt; //表示させるテキスト(TMP)
   

    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    void Start()
    {
        DeathCount_txt.SetText("討伐数:0体 ");     //初期化
    }

    /*＞カウント表示関数
   引数：なし
   ｘ
   戻値：なし
   ｘ
   概要：討伐数が増えたとき表示を更新する処理
   */
    public void DisplayEnemyDeathCounter()
    {
        DeathCount_txt.SetText("討伐数:"+ CEnemy.m_nDeadEnemyCount.ToString() + "体");    // 討伐数表示
    }
}
