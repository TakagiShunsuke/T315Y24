using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // �V�[���֘A��using

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
        // �������Ԃ��O�b�Ȃ�
        if (timeMngCom.currentTime <= 0)
        {
            SceneManager.LoadScene("ResultScene");    // ResultScene�֑J��
        }
    }
}
