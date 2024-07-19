/*=====
<ResultSet.cs> 
└作成者：yamamoto

＞内容
リザルトに表示するデータを受け取り表示させるスクリプト

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

//＞クラス定義
public class ResultSet : CMonoSingleton<ResultSet>
{
    [Header("テキスト")]
    [Tooltip("表示用のテキスト")] public List<TMP_Text> ResultText;
    private int m_nPage = 0;   //ページ数


    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    override protected void Start()
    {
        //ToDo 一括管理できるようにまとめる
        GameMineData MineResultData = Mine.GetGameMineData();                   //地雷のデータを取得
        GameRemoteBombData RBResultData = RemoteBomb.GetGameRemoteBombData();   //爆弾のデータを取得

        //対応するテキストにセットする
        ResultText[0].SetText($"{MineResultData.SetMine}");         //地雷を置いた数
        ResultText[1].SetText($"{MineResultData.UseMine}");         //地雷を使った数
        ResultText[2].SetText($"{MineResultData.MineKill}");        //地雷で倒した数

        ResultText[3].SetText($"{RBResultData.SetRemoteBomb}");     //爆弾を置いた数
        ResultText[4].SetText($"{RBResultData.UseRemoteBomb}");     //爆弾を使った数
        ResultText[5].SetText($"{RBResultData.RemoteBombKill}");    //爆弾で倒した数

        ResultText[6].SetText($"{MineResultData.SetMine + RBResultData.SetRemoteBomb}");    //置いた数の合計
        ResultText[7].SetText($"{MineResultData.UseMine + RBResultData.UseRemoteBomb}");    //使った回数の合計
        ResultText[8].SetText($"{MineResultData.MineKill + RBResultData.RemoteBombKill}");  //倒した数の合計

        ResultText[9].SetText($"{MineResultData.MineKill + RBResultData.RemoteBombKill}");  //倒した数の合計

        //このタイミングで初期化
        Mine.ResetMineData();                   //地雷のデータをリセット
        RemoteBomb.ResetRemoteBombData();       //爆弾のデータをリセット
    }

    /*＞更新関数
   引数１：なし
   ｘ
   戻値：なし
   ｘ
   概要：更新処理
   */
    override protected void Update()
    {

    }

    /*＞TextActive反転関数
  引数：なし
  ｘ
  戻値：なし
  ｘ
  概要：TextActiveを反転し表示非表示を切り替える
  */
    public void ToggleActive(int nPage)
    {
        //＞ページが切り替わっている
        if (m_nPage == nPage)
        {
            return; //処理しない
        }

        //＞ページ切換
        m_nPage = nPage;    //ページを更新する

        //＞フォント表示切替
        for (int i = 0; i < ResultText.Count; i++) //ResultTextの数繰り返し
        {
            bool currentState = ResultText[i].gameObject.activeSelf;    //現在のactiveを取得
            ResultText[i].gameObject.SetActive(!currentState);          //反転したのをセットする
        }
    }
}
