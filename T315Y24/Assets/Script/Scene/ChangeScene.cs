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
13:決定時SE追加:nieda
17:SE追加:nieda
18:SEを鳴らすように修正:takagi
21:リファクタリング:takagi
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
        [Tooltip("キー")] public KeyCode[] TransitionKey; //シーン遷移の着火キー
        [Tooltip("ボタン")] public string[] TransitionButton;   //シーン遷移の着火ボタン
        [Tooltip("遷移先シーン")] public String Nextscene;    //シーンの切換先
    }

    //＞変数宣言
    [Header("シーンの切り替え方")]
    [SerializeField, Tooltip("対応シーン")] private KeyChangeScene[] m_KeyChangeScenes;    //シーン遷移一覧
    [Header("音")]
    [Tooltip("AudioSourceを追加")] private AudioSource m_AudioSource;     // AudioSourceを追加
    [SerializeField, Tooltip("決定時のSE")] private AudioClip SE_Decide;  // 決定時のSE

    [SerializeField] private Material SceneFadeMaterial;  // マテリアル
    [SerializeField] private float fadeTime = 2.0f;       // フェード時間
    //[SerializeField] private string _propertyName = "_Progress";
    public InkTransition inkTransition;
    //＞パブリックイベント
    //public UnityEvent OnFadeDone;

    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    /*＞更新関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    private void Update()
    {
        //＞保全
        if(m_KeyChangeScenes == null)   //ヌルチェック
        {
            //＞終了
            return; //処理キャンセル
        }

        //＞シーン遷移
        for(int _nIdx = 0; _nIdx < m_KeyChangeScenes.Length; ++_nIdx)   //遷移先候補分判定
        {
            for(int nIdx2 = 0; nIdx2 < m_KeyChangeScenes[_nIdx].TransitionKey.Length; ++nIdx2)    //受付キー分判定する
            {
                if (Input.GetKeyDown(m_KeyChangeScenes[_nIdx].TransitionKey[nIdx2])) //キー入力判定
                {
                    StartCoroutine(Change(_nIdx));
                //    m_audioSource.PlayOneShot(SE_Decide);
                //    while(m_audioSource.isPlaying) {}   //非同期処理：SEを鳴らし切るまで待機
                //    SceneManager.LoadScene(m_KeyChangeScenes[nIdx].Nextscene);  //次のステージへ
                }
            }
            //コントローラー用
            for (int _nIdx2 = 0; _nIdx2 < m_KeyChangeScenes[_nIdx].TransitionButton.Length; ++_nIdx2)    //受付キー分判定する
            {
                if (Input.GetButtonDown(m_KeyChangeScenes[_nIdx].TransitionButton[_nIdx2])) //キー入力判定
                {
                    StartCoroutine(Change(_nIdx));
                    //    m_audioSource.PlayOneShot(SE_Decide);
                    //    while(m_audioSource.isPlaying) {}   //非同期処理：SEを鳴らし切るまで待機
                    //    SceneManager.LoadScene(m_KeyChangeScenes[nIdx].Nextscene);  //次のステージへ
                }
            }
        }
    }

    //非同期でSE再生終了を待つ関数
    private IEnumerator Change(int _nIdx)
    {
        float currentTime = 0.0f;   // 現時刻

        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            //SceneFadeMaterial.SetFloat(_propertyName, Mathf.Clamp01(currentTime / fadeTime));
            inkTransition.StartTransition();
            yield return null;
        }
        m_AudioSource.PlayOneShot(SE_Decide);
        while (m_AudioSource.isPlaying) { yield return null; }   //非同期処理：SEを鳴らし切るまで待機
        SceneManager.LoadScene(m_KeyChangeScenes[_nIdx].Nextscene);  //次のステージへ
    }

    /*＞ゲーム終了関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：ゲームを終了する処理
    */
    private void GameEnd()
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