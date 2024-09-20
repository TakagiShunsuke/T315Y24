/*=====
<TimeManager.cs> 
���쐬�ҁFisi

�����e
�������ԕ\���p�̃X�N���v�g

���X�V����
__Y24   
_M05    
D
15:�X�N���v�g�쐬:suzumura
30:�R�����g�ǉ�:yamamoto
=====*/
//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//���N���X��`
public class TimeManager : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] TMP_Text time_txt;   // �e�L�X�g���b�V���v���̃e�L�X�g�擾�p
    [SerializeField] float m_fMaxTime;    // �ő厞��
    [SerializeField] float m_fTime;       // �c�莞��

    //���v���p�e�B��`
    public float currentTime
    {
        get { return m_fTime; }
        private set { m_fTime = value; }
    }


    /*���������֐�
  �����P�F�Ȃ�
  ��
  �ߒl�F�Ȃ�
  ��
  �T�v�F�C���X�^���X�������ɍs������
  */
    void Start()
    {
        m_fTime = m_fMaxTime;   // �������Ԑݒ�
    }
    /*���X�V�֐�
       �����F�Ȃ�
       ��
       �ߒl�F�Ȃ�
       ��
       �T�v�F��莞�Ԃ��Ƃɍs���X�V����
       */
    void Update()
    {
        if(currentTime > 0.0f) m_fTime -= Time.deltaTime;      // ���Ԍo�ߏ���

        time_txt.SetText("{0}",(int)m_fTime);    // ���ԕ\��
    }
}
