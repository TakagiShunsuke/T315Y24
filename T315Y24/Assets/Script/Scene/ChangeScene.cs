/*=====
<ChangeScene.cs> //スクリプト名
└作成者：nieda

＞内容
ボタンを押した時にシーンを切り替える

＞注意事項
1年時に作ったものを完全流用した

＞更新履歴
__Y24   //'24年
_M05    //05月
D       //日
10:プログラム作成:nieda
14:ビルドバグの元を除去:takagi
17:キー入力でシーン遷移実装:nieda
_M06    //06月
D       //日
13:シーン遷移ボタン統一、Pでプロトステージ、Oでステージ1に遷移:nieda
13:決定時SE追加:nieda
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

//＞クラス定義
public class CChangeScene : MonoBehaviour
{
    AudioSource m_audioSource;  // AudioSourceを追加
    [SerializeField] public AudioClip SE_Decide;  // ダッシュ時のSE

    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().name == "TitleScene")
            {
                // タイトル→ステージセレクト
                m_audioSource.Play();
                SceneManager.LoadScene("SelectScene");
            }
            else if (SceneManager.GetActiveScene().name == "SelectScene")
            {
                // ステージセレクト→ステージ1
                m_audioSource.Play();
                SceneManager.LoadScene("Stage01");
            }
            else if (SceneManager.GetActiveScene().name == "ResultScene")
            {
                // リザルト、ゲームオーバー→タイトル
                m_audioSource.Play();
                SceneManager.LoadScene("TitleScene");
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            // プロトステージへ遷移
            SceneManager.LoadScene("ProtoStage");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            // ステージ1へ遷移
            SceneManager.LoadScene("Stage01");
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