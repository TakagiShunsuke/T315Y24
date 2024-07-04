using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGameClear : MonoBehaviour
{
    [SerializeField] GameObject TimerManagerObj;
    TimeManager timeMngCom;


    // Start is called before the first frame update
    private void Start()
    {
        timeMngCom = TimerManagerObj.GetComponent<TimeManager>();    
    }

    // Update is called once per frame
    private void Update()
    {
        // êßå¿éûä‘Ç™ÇOïbÇ»ÇÁ
        //if (timeMngCom.currentTime <= 0)
        if (CPhaseManager.Instance.IsFinPhases)
        {
            SceneManager.LoadScene("ResultScene");    // ResultSceneÇ÷ëJà⁄
        }
    }
}
