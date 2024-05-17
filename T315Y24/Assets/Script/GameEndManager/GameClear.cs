using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーン関連のusing

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
        // 制限時間が０秒なら
        if (timeMngCom.currentTime <= 0)
        {
            SceneManager.LoadScene("ResultScene");    // ResultSceneへ遷移
        }
    }
}
