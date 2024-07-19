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

    [SerializeField] private Material SceneFadeMaterial;  // �}�e���A��
    [SerializeField] private float fadeTime = 2.0f;       // �t�F�[�h����
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
        // �������Ԃ��O�b�Ȃ�
        //if (timeMngCom.currentTime <= 0)
        if (CPhaseManager.Instance.IsFinPhases)
        {
            //float currentTime = 0.0f;   // ������

            //while (currentTime < fadeTime)
            //{
            //    currentTime += Time.deltaTime;
            //    SceneFadeMaterial.SetFloat(_propertyName, Mathf.Clamp01(1 - currentTime / fadeTime));
            //    //inkTransition.StartTransition();
            //}
            StartCoroutine(TransitionCoroutine());
            //SceneManager.LoadScene("ResultScene");    // ResultScene�֑J��
        }
    }

    private IEnumerator TransitionCoroutine()
    {
        float currentTime = 0.0f;   // ������
        while (currentTime < fadeTime) // �t�F�[�h���Ԃ�菬����������s��
        {
            currentTime += Time.deltaTime;
            SceneFadeMaterial.SetFloat(_propertyName, Mathf.Clamp01(1 - currentTime / fadeTime));    // propertyName�Œ�`�������l�����Ԃ̊����ɍ��킹�ăX���C�h����
            yield return null;
        }
        OnTransitionDone.Invoke();  // �C�x���g�̌Ăяo��
        SceneManager.LoadScene("ResultScene");    // ResultScene�֑J��
    }
}
