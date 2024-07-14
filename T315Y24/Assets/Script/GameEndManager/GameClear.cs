using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGameClear : MonoBehaviour
{
    [SerializeField] GameObject TimerManagerObj;
    TimeManager timeMngCom;
    public InkTransition inkTransition;

    [SerializeField] private float fadeTime = 2.0f;       // ƒtƒF[ƒhŠÔ

    // Start is called before the first frame update
    private void Start()
    {
        timeMngCom = TimerManagerObj.GetComponent<TimeManager>();    
    }

    // Update is called once per frame
    private void Update()
    {
        // §ŒÀŠÔ‚ª‚O•b‚È‚ç
        //if (timeMngCom.currentTime <= 0)
        if (CPhaseManager.Instance.IsFinPhases)
        {
            float currentTime = 0.0f;   // Œ»

            while (currentTime < fadeTime)
            {
                currentTime += Time.deltaTime;
                //SceneFadeMaterial.SetFloat(_propertyName, Mathf.Clamp01(currentTime / fadeTime));
                inkTransition.StartTransition();
            }
            SceneManager.LoadScene("ResultScene");    // ResultScene‚Ö‘JˆÚ
        }
    }
}
