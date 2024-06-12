/*=====
<PlayerDashInterval.cs> //スクリプト名
└作成者：takagi

＞内容
ダッシュのインターバルを表示する

＞注意事項
同一のオブジェクトに以下のコンポーネントがないと十分な機能をしません。
１.プレイヤーの情報が管理されているCPlayerScript


＞更新履歴
__Y24
_M05
D
21:プログラム作成:takagi
25:セマンティクス修正:takagi
31:リファクタリング:takagi

_M06
D
13:脱字修正:takagi
=====*/

//＞名前空間宣言
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;  //Unity

//＞クラス定義
public class CPlayerDashInterval : MonoBehaviour
{
    //＞定数定義
    private const string TEXT_DASH_ABLE = "ダッシュ可能"; //ダッシュ可能時の表示テキスト
    private const string TEXT_DASH_UNABLE = "ダッシュ不可能";   //ダッシュ不可能時の表示テキスト

    //＞変数宣言
    private CPlayerScript m_Player = null;  //プレイヤーの情報
    private double m_dCurData;  //現在管理している情報
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI; //インターバル表示場所

    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    public void Start()
    {
        //＞初期化
        m_Player = GetComponent<CPlayerScript>(); //プレイヤーとしての振る舞い方取得
        if (m_Player != null)   //取得に成功した時
        {
            m_dCurData = m_Player.DashCntDwn;   //データ初期化
            UpdateText();   //テキスト初期化
        }
#if UNITY_EDITOR    //エディタ使用中
        else   //取得に失敗した時
        {
            //＞エラー出力
            UnityEngine.Debug.LogError("プレイヤーに設定されていません");    //警告ログ出力
        }
#endif
#if UNITY_EDITOR    //エディタ使用中
        if (m_TextMeshProUGUI == null)   //取得に失敗した時
        {
            //＞エラー出力
            UnityEngine.Debug.LogWarning("入力先テキストが設定されていません");    //警告ログ出力s
        }
#endif
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
        //＞検査
        if (m_Player == null)   //必要要件の不足時
        {
#if UNITY_EDITOR    //エディタ使用中
            //＞エラー出力
            UnityEngine.Debug.LogWarning("必要な要素が不足しています");  //警告ログ出力
#endif

            //＞中断
            return; //更新処理中断
        }

        //＞更新
        if (m_dCurData != m_Player.DashCntDwn)  //所持データに更新が必要な時
        {
            m_dCurData = m_Player.DashCntDwn;   //データ更新
            UpdateText();    //テキスト更新
        }
    }

    /*＞テキスト更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：テキストの内容更新
    */
    private void UpdateText()
    {
        //＞検査
        if (m_TextMeshProUGUI == null)   //必要要件の不足時
        {
#if UNITY_EDITOR    //エディタ使用中
            //＞エラー出力
            UnityEngine.Debug.LogWarning("必要な要素が不足しています");  //警告ログ出力
#endif

            //＞中断
            return; //更新処理中断
        }

        //＞状態分岐
        if (m_dCurData > 0)   //カウントダウン中
        {
            m_TextMeshProUGUI.text = TEXT_DASH_UNABLE + "\n残り：" + ((uint)m_dCurData).ToString().PadRight(2) + "s"; //ダッシュ不可能
        }
        else
        {   //カウントダウンされていない
            m_TextMeshProUGUI.text = TEXT_DASH_ABLE;    //ダッシュ可能
        }
    }
}