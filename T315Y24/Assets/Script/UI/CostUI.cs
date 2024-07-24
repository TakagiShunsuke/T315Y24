/*=====
<CostUI.cs> 
└作成者：yamamoto

＞内容
コストのUI(円ゲージ)

＞更新履歴
__Y24   
_M06    
D
25: プログラム作成: yamamoto
26: コメント追加: yamamoto
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//＞クラス定義
public class CostUI : MonoBehaviour
{
    //＞変数宣言
    [Header("動かすUI")]
    [SerializeField, Tooltip("時計のように動かすUI")] private Image UIobj;   //時計のように動かすUI
    [Header("コスト増加時間")]
    [SerializeField, Tooltip("コストが1増える秒数")] private float countTime = 5.0f; //コストが1増える秒数
    [Header("コスト表示")]
    [SerializeField, Tooltip("数字をだすText")] private TMP_Text Cost_txt; //表示させるテキスト(TMP)


     /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
     */
    void Start()
    {
        UIobj.fillAmount = 0.0f;                        //初期化
        Cost_txt.SetText($"{CTrapSelect.m_nCost}");      //初期化
    }

    /*＞更新関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：更新処理
    */
    void Update()
    {
        //円ゲージを動かす
        UIobj.fillAmount += 1.0f / countTime * Time.deltaTime;  //countTime(秒)の秒数で1になる
        if (UIobj.fillAmount>=1.0f)     //円ゲージが一周したら
        {
            UIobj.fillAmount = 0.0f;    //初期化
            CTrapSelect.m_nCost++;       //コスト増加
        }
        Cost_txt.SetText($"{CTrapSelect.m_nCost}");  //コスト表示
    }
}
