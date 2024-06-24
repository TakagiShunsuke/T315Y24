using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class CPauseManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private bool m_bPause;
    private AudioSource m_AudioSource;
    [SerializeField] private AudioClip SE_Pause;


    // Start is called before the first frame update
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();    // AudioSourceコンポーネントを追加
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            m_AudioSource.PlayOneShot(SE_Pause);    //SE再生
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
