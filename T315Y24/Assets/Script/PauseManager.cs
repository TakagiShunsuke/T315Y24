using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] bool m_bPause;
    [SerializeField] public AudioClip SE_Pause;  // ポーズ表示時のSE
    AudioSource m_As; // AudioSourceを追加

    // Start is called before the first frame update
    void Start()
    {
        m_As = GetComponent<AudioSource>(); // AudioSourceコンポーネントを追加
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            m_As.PlayOneShot(SE_Pause);   // SE再生

            if (!m_bPause) m_bPause = true;
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
