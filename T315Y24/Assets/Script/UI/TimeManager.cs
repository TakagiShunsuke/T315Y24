using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] TMPro.TMP_Text time_txt;   // �e�L�X�g���b�V���v���̃e�L�X�g�擾�p

    [SerializeField] float m_fMaxTime;    // �ő厞��
    [SerializeField] float m_fTime;       // �c�莞��

    //���v���p�e�B��`
    public float currentTime
    {
        get { return m_fTime; }
        private set { m_fTime = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        m_fTime = m_fMaxTime;   // �������Ԑݒ�
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime > 0.0f) m_fTime -= Time.deltaTime;      // ���Ԍo�ߏ���

        time_txt.SetText("{0}",(int)m_fTime);    // ���ԕ\��
    }
}
