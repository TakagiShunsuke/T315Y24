using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] TMPro.TMP_Text time_txt;   // テキストメッシュプロのテキスト取得用

    [SerializeField] float m_fMaxTime;    // 最大時間
    [SerializeField] float m_fTime;       // 残り時間

    //＞プロパティ定義
    public float currentTime
    {
        get { return m_fTime; }
        private set { m_fTime = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        m_fTime = m_fMaxTime;   // 制限時間設定
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime > 0.0f) m_fTime -= Time.deltaTime;      // 時間経過処理

        time_txt.SetText("{0}",(int)m_fTime);    // 時間表示
    }
}
