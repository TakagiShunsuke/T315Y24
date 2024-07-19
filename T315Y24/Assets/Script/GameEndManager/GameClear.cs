using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class CGameClear : MonoBehaviour
{
    [SerializeField] GameObject TimerManagerObj;
    TimeManager timeMngCom;
    //public InkTransition inkTransition;

    [SerializeField] private Material SceneFadeMaterial;  // マテリアル
    [SerializeField] private float fadeTime = 2.0f;       // フェード時間
    [SerializeField] private string _propertyName = "_Progress";

    public UnityEvent OnTransitionDone;

    // Start is called before the first frame update
    private void Start()
    {
        
        timeMngCom = TimerManagerObj.GetComponent<TimeManager>();    
    }

    // Update is called once per frame
    private void Update()
    {
        // 制限時間が０秒なら
        //if (timeMngCom.currentTime <= 0)
        if (CPhaseManager.Instance.IsFinPhases)
        {
            //float currentTime = 0.0f;   // 現時刻

            //while (currentTime < fadeTime)
            //{
            //    currentTime += Time.deltaTime;
            //    SceneFadeMaterial.SetFloat(_propertyName, Mathf.Clamp01(1 - currentTime / fadeTime));
            //    //inkTransition.StartTransition();
            //}
            StartCoroutine(TransitionCoroutine());
            //SceneManager.LoadScene("ResultScene");    // ResultSceneへ遷移
        }
    }

    private IEnumerator TransitionCoroutine()
    {
        float currentTime = 0.0f;   // 現時刻
        while (currentTime < fadeTime) // フェード時間より小さかったら行う
        {
            currentTime += Time.deltaTime;
            SceneFadeMaterial.SetFloat(_propertyName, Mathf.Clamp01(1 - currentTime / fadeTime));    // propertyNameで定義した数値を時間の割合に合わせてスライドする
            yield return null;
        }
        OnTransitionDone.Invoke();  // イベントの呼び出し
        SceneManager.LoadScene("ResultScene");    // ResultSceneへ遷移
    }
}
