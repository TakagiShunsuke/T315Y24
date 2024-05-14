using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // ƒV[ƒ“ŠÖ˜A‚Ìusing

public class GameClear : MonoBehaviour
{
    [SerializeField] GameObject TimerManagerObj;
    TimeManager timeMngCom;

    // Start is called before the first frame update
    void Start()
    {
        timeMngCom = TimerManagerObj.GetComponent<TimeManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // §ŒÀŠÔ‚ª‚O•b‚È‚ç
        if (timeMngCom.currentTime <= 0)
        {
            SceneManager.LoadScene("ResultScene");    // ResultScene‚Ö‘JˆÚ
        }
    }
}
