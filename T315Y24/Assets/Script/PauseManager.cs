using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class CPauseManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private bool m_bPause;

    [Header("��")]
    [Tooltip("AudioSource��ǉ�")] private AudioSource m_AudioSource;                  // AudioSource��ǉ�
    [SerializeField, Tooltip("�|�[�Y���j���[�\������SE")] private AudioClip SE_Pause;  // �|�[�Y���j���[�\������SE


    // Start is called before the first frame update
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();    // AudioSource�R���|�[�l���g��ǉ�
    }

    // Update is called once per frame
    private void Update()
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
