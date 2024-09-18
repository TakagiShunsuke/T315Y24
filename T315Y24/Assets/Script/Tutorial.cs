/*=====
<Tutorial.cs> //スクリプト名
└作成者：takagi

＞内容
チュートリアル

＞注意事項
シングルトンである
fixedでないupdateはtimescaleの影響を受けないことを利用している。


＞更新履歴
__Y24
_M08
D
10:プログラム作成:takagi
_M09
D
05:入力で進める/戻す機能追加:takagi
06:背景対応:takagi
=====*/

//＞名前空間宣言
using System;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

//＞クラス定義
public class CTutorial : CMonoSingleton<CTutorial>
{
    //＞列挙定義
    private enum E_STATE
    {
        E_STATE_NONE,   //無し
        E_STATE_WAIT_INPUT, // 入力待ち
        E_STATE_PLAYING,    // チュートリアル中
    }   //状態列挙

    //＞構造体定義
    [Serializable] private struct KeyAndButton
    {
        [Tooltip("キーコード")] public KeyCode m_Key;   //キーの値
        [Tooltip("ボタン名")] public String m_ButtonName;   //ボタンの名前
    }   //入力用データ統括

    //＞定数定義
    private const string OBJECT_NAME = "Tutorial"; //このオブジェクトが生成されたときの名前

    //＞変数宣言
    [Header("チュートリアル事前準備")]
    [SerializeField, Tooltip("要不要確認場所")] private TMP_Text m_TMP; //要不要確認テキスト出力場所
    [SerializeField, Tooltip("要不要確認文")] private String m_Confirm; //要不要確認テキスト
    [SerializeField, Tooltip("入力：はい")] private KeyAndButton m_YesKey;   //はいを選択
    [SerializeField, Tooltip("入力：いいえ")] private KeyAndButton m_NoKey;   //いいえを選択
    [Header("チュートリアル用")]
    [SerializeField, Tooltip("入力：次へ")] private KeyAndButton m_FrontKey; //次のTipsを見る
    [SerializeField, Tooltip("入力：戻る")] private KeyAndButton m_BackKey;  //前のTipsを見る
    [SerializeField, Tooltip("入力：終了")] private KeyAndButton m_FinishKey;   //終了を選択
    [SerializeField, Tooltip("UI")] private GameObject m_UI; //チュートリアル用UI
    [SerializeField, Tooltip("背景")] private GameObject m_BG; //チュートリアル背景
    [SerializeField, Tooltip("ヒント画像")] private List<Sprite> m_Tips = new List<Sprite>(); //チュートリアル画像
    private int m_nTipsIdx = 0;  //チュートリアル画像の何番目を表示しているか
    [SerializeField, Tooltip("画像表示場所")] private Image m_Image; //チュートリアル画像表示場所
    private Image m_ImageClone; //チュートリアル画像の複製
    [SerializeField, Tooltip("表示時間")] private float m_fDrawTime; //一つのチュートリアル画像表示時間
    [SerializeField, Tooltip("表示切替速度")] private float m_fSwitchSpeed; //チュートリアル画像切り替え速度
    private float m_fTimer = 0.0f;   //tips切換用タイマー
    private bool m_bVisiableFrontImage = true;  //手前の画像で表示しているか
    private E_STATE m_eState = E_STATE.E_STATE_NONE;   // 自身の状態
    private bool m_bFading = false; //フェーズ切り替え中か


    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    protected override void CustomAwake()
    {
        //＞リネーム
        gameObject.name = OBJECT_NAME;  //自身のオブジェクト名変更

        //＞可視不可視管理
        if(m_TMP)  //ヌルチェック
        {
            m_TMP.gameObject.SetActive(true);   //初期化のために一時的に使用
            m_TMP.text = m_Confirm; //文字出力
            m_TMP.gameObject.SetActive(false);  //まだ使わない
        }
        if(m_Image)
        {
            m_Image.gameObject.SetActive(true);    //初期化のために一時的に使用
            m_ImageClone = Instantiate(m_Image, m_Image.transform.parent); //画像を同じ階層にクローン体(下に描画されるように)
            m_ImageClone.gameObject.SetActive(false);   //まだ使わない
            if (m_Tips != null && m_Tips.Count > 0)
            {
                m_Image.sprite = m_Tips[0]; //一つ目の画像を登録
                m_Image.SetNativeSize();    //画像サイズに合わせる
            }
            m_Image.gameObject.SetActive(false);    //まだ使わない
        }
        if(m_BG)
        {
            m_BG.gameObject.SetActive(false);   //まだ使わない
        }
        if(m_UI)
        {
            m_UI.gameObject.SetActive(false);   //まだ使わない
        }
    }

    /*＞チュートリアル開始関数(呼出前提)
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：呼び出されたらチュートリアル処理開始。フェードイン用。
    */
    public void StartTutorial()
    {
        //＞保全
        if (m_Image == null || m_TMP == null || m_Tips == null || m_Tips.Count == 0)    //ヌルチェック
        {
            Debug.Log("チュートリアルが実行できません！"); //エラー出力
            Finish();
            return; //処理しない
        }

        //＞時間停止
        Time.timeScale = 0.0f; //時間が進行しない

        //＞チュートリアル確認
        //m_TMP.gameObject.SetActive(true);   //最初に使用する
        //m_eState = E_STATE.E_STATE_WAIT_INPUT; //入力を待つ

        //＞チュートリアル開始
        //Destroy(m_TMP); //テキスト表示は終了
        m_UI.gameObject.SetActive(true);    //UIを表示
        m_Image.gameObject.SetActive(true); //画像を表示
        m_BG.gameObject.SetActive(true);    //背景を表示
        m_eState = E_STATE.E_STATE_PLAYING; //チュートリアル開始
    }

    /*＞更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：時間停止中も一定時間ごとに行う更新処理
    */
    protected override void Update()
    {
        //＞状態分岐
        switch (m_eState)   //処理状態によって分岐
        {
            case E_STATE.E_STATE_WAIT_INPUT:    //入力待ち
                WaitInput(); //専用処理呼出
                break;  //分岐終了
            case E_STATE.E_STATE_PLAYING:   //チュートリアル中
                PlayTutorial(); //専用処理呼出
                break;  //分岐終了
        }
    }

    /*＞入力待機関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：入力を待機し、応じた処理を行う
    */
    private void WaitInput()
    {
        //＞入力待ち
        if (Input.GetKeyUp(m_YesKey.m_Key) || Input.GetButtonUp(m_YesKey.m_ButtonName)) //はいの入力
        {
            Destroy(m_TMP); //テキスト表示は終了
            m_UI.gameObject.SetActive(true);    //UIを表示
            m_Image.gameObject.SetActive(true); //画像を表示
            m_BG.gameObject.SetActive(true);    //背景を表示
            m_eState = E_STATE.E_STATE_PLAYING; //チュートリアル開始
        }
        else if (Input.GetKeyUp(m_NoKey.m_Key) || Input.GetButtonUp(m_NoKey.m_ButtonName))  //いいえの入力
        {
            Finish();   //チュートリアル強制終了
        }
    }

    /*＞チュートリアル処理関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：チュートリアルの基本処理
    */
    private void PlayTutorial()
    {
        //＞入力管理
        if (Input.GetKeyUp(m_FinishKey.m_Key) || Input.GetButtonUp(m_FinishKey.m_ButtonName)) //終了の入力
        {
            Finish();   //強制終了
        }

        //＞カウンタ
        float _OldTime = m_fTimer;  //タイマーのカウント退避
        m_fTimer += Time.unscaledDeltaTime; //タイマー進行

        //＞画像切換
        if (m_bFading)   //初回
        {
            //＞変数宣言
            float _fNewAlpha = 1.0f;    //透明度変更用
            Image _Old = null; //切換前の画像
            Image _New = null; //切換後の画像

            //＞新旧判定
            if (m_bVisiableFrontImage) //上の画像が表示されている
            {
                _Old = m_Image;    //上が古い
                _New = m_ImageClone; //下が新しい
            }
            else
            { //下の画像が表示されている
                _Old = m_ImageClone;    //下が古い
                _New = m_Image; //上が新しい
            }

            _fNewAlpha = Mathf.Max(0.0f, _Old.color.a - Time.unscaledDeltaTime / m_fSwitchSpeed);  //透明度をfpsと速度に比例して減少
            if (Mathf.Approximately(_fNewAlpha, 0.0f))   //透明度がほぼ0
            {
                //＞切換終了
                _fNewAlpha = 0.0f;  //見えなくしておく
                _Old.gameObject.SetActive(false);    //見えなくしておく
                m_fTimer = 0.0f;    //タイマーリセット
                m_bVisiableFrontImage ^= true;  //表示されている画像が逆転した
                m_bFading = false;  // フェーズ切り替えが完了した
            }
            _Old.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, _fNewAlpha);   //透明度更新(減少)
            _New.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 1.0f - _fNewAlpha);   //透明度更新(増加)
        }
        else
        {
            //＞時間による表示切替
            if (m_fTimer > m_fDrawTime) //表示時間を超過している
            {
                //＞初回判定
                if (_OldTime < m_fDrawTime)  //前回は超過が見られない
                {
                    m_nTipsIdx = m_nTipsIdx + 1 < m_Tips.Count ? m_nTipsIdx + 1 : 0;    //画像番号をリスト内で循環
                    ChangeTips();
                }
            }

            //＞入力による表示切替
            if (Input.GetKeyUp(m_FrontKey.m_Key) || Input.GetButtonUp(m_FrontKey.m_ButtonName)) //進める入力
            {
                m_nTipsIdx = m_nTipsIdx + 1 < m_Tips.Count ? m_nTipsIdx + 1 : 0;    //画像番号を進める
                ChangeTips();
            }
            if (Input.GetKeyUp(m_BackKey.m_Key) || Input.GetButtonUp(m_BackKey.m_ButtonName)) //戻る入力
            {
                m_nTipsIdx = m_nTipsIdx - 1 < 0 ? m_Tips.Count - 1 : m_nTipsIdx - 1;    //画像番号を戻す
                ChangeTips();
            }
        }
    }


    /*＞チュートリアル表示切替関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：チュートリアルの表示切替部分
    */
    private void ChangeTips()
    {
        //＞変数宣言
        Image _New = null; //切換後の画像

        //＞新旧判定
        if (m_bVisiableFrontImage) //上の画像が表示されている
        {
            _New = m_ImageClone; //下が新しい
        }
        else
        { //下の画像が表示されている
            _New = m_Image; //上が新しい
        }

        //＞画像切換
        _New.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b,0.0f);
        _New.sprite = m_Tips[m_nTipsIdx];    //次の画像を下に表示
        _New.gameObject.SetActive(true);    //遷移先画像可視化
        
        _New.SetNativeSize();    //画像サイズに合わせる
        
        //＞フェーズ切り替え開始
        m_bFading = true;   //フェードするときに参照される値
    }

/*＞終了関数
引数：なし
ｘ
戻値：なし
ｘ
概要：自身の除去処理
*/
private void Finish()
    {
        //＞時間停止解除
        Time.timeScale = 1.0f; //時間が進行しない

        //＞破棄
        Destroy(gameObject);  // 自身を破壊することでチュートリアルを二度と表示しなくなる
    }
}