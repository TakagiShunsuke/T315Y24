/*=====
<TimeManager.cs> 
└作成者：isi

＞内容
制限時間表示用のスクリプト

＞更新履歴
__Y24   
_M05    
D
15:スクリプト作成:suzumura
30:コメント追加:yamamoto
=====*/
//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//＞クラス定義
public class TimeManager : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] TMP_Text time_txt;   // テキストメッシュプロのテキスト取得用
    [SerializeField] float m_fMaxTime;    // 最大時間
    [SerializeField] float m_fTime;       // 残り時間

    //＞プロパティ定義
    public float currentTime
    {
        get { return m_fTime; }
        private set { m_fTime = value; }
    }


    /*＞初期化関数
  引数１：なし
  ｘ
  戻値：なし
  ｘ
  概要：インスタンス生成時に行う処理
  */
    void Start()
    {
        m_fTime = m_fMaxTime;   // 制限時間設定
    }
    /*＞更新関数
       引数：なし
       ｘ
       戻値：なし
       ｘ
       概要：一定時間ごとに行う更新処理
       */
    void Update()
    {
        if(currentTime > 0.0f) m_fTime -= Time.deltaTime;      // 時間経過処理

        time_txt.SetText("{0}",(int)m_fTime);    // 時間表示
    }
}
