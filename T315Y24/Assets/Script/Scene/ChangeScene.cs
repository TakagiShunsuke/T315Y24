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
    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    // Start is called before the first frame update
    void Start()
    {

    }

    /*＞更新関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //シーンを読み込む
            SceneManager.LoadScene("SelectScene");
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            //シーンを読み込む
            SceneManager.LoadScene("ProtoStage");
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            //シーンを読み込む
            SceneManager.LoadScene("TitleScene");
        }
    }

    /*＞ロードシーン関数
    引数１：string str：シーン名
    ｘ
    戻値：なし
    ｘ
    概要：シーンをロードする
    */
    //public void LoadScene(string str)
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        //シーンを読み込む
    //        SceneManager.LoadScene(str);
    //    }
    //}

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