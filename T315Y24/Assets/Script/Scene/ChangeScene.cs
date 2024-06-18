/*=====
<ChangeScene.cs> //スクリプト名
└作成者：nieda

＞内容
ボタンを押した時にシーンを切り替える

＞注意事項
1年時に作ったものを完全流用した

＞更新履歴
__Y24
_M05
D
10:プログラム作成:nieda
14:ビルドバグの元を除去:takagi
17:キー入力でシーン遷移実装:nieda

_M06
D
13:シーン遷移ボタン統一、Pでプロトステージ、Oでステージ1に遷移:nieda
13:キー入力の受付を制限+汎化:takagi
17:SE追加:nieda
=====*/

//＞名前空間宣言
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

//＞クラス定義
public class CChangeScene : MonoBehaviour
{
    //＞構造体定義
    [Serializable] public struct KeyChangeScene
    {
        public KeyCode[] TransitionKey; //シーン遷移の着火キー
        public String Nextscene;    //シーンの切換先
    }

    //＞変数宣言
    [SerializeField] private KeyChangeScene[] m_KeyChangeScenes;    //シーン遷移一覧
    [SerializeField] public AudioClip SE_Decide;  // 決定時のSE
    AudioSource m_As; // AudioSourceを追加

    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    void Start()
    {
        m_As = GetComponent<AudioSource>(); // AudioSourceコンポーネントを追加
    }

    /*＞更新関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    void Update()
    {
        //＞保全
        if(m_KeyChangeScenes == null)   //ヌルチェック
        {
            //＞終了
            return; //処理キャンセル
        }

        //＞シーン遷移
        for(int nIdx = 0; nIdx < m_KeyChangeScenes.Length; ++nIdx)   //遷移先候補分判定
        {
            for(int nIdx2 = 0; nIdx2 < m_KeyChangeScenes[nIdx].TransitionKey.Length; ++nIdx2)    //受付キー分判定する
            {
                if (Input.GetKeyDown(m_KeyChangeScenes[nIdx].TransitionKey[nIdx2])) //キー入力判定
                {
                    m_As.PlayOneShot(SE_Decide);   // SE再生
                    SceneManager.LoadScene(m_KeyChangeScenes[nIdx].Nextscene);  //次のステージへ
                }
            }
        }
    }

    /*＞ゲーム終了関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：ゲームを終了する処理
    */
    public void GameEnd()
    {
#if UNITY_EDITOR    //Editor上からの実行時
        //再生モードを解除する
        UnityEditor.EditorApplication.isPlaying = false;
#else               //実行ファイルからの実行時
        //TODO:代替処理
        //Application.Quit();
#endif
    }
}