using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] bool m_bPause;
    AudioSource m_AudioSource;
    [SerializeField] public AudioClip SE_Pause;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();    // AudioSource�R���|�[�l���g��ǉ�
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            m_AudioSource.PlayOneShot(SE_Pause);    //SE�Đ�
            if(!m_bPause) m_bPause = true;
            else m_bPause = false;
        }

        if (m_bPause == true)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            panel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
