using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CCodingRule;

public class CTrap : MonoBehaviour
{
    [SerializeField] private double m_dInterval = 5.0d; // �C���^�[�o��
    private double m_dCoolTime = 0.0d;                  // �C���^�[�o���v���p
    public bool m_bUse = true;                         // ���p true:�\ false:�s��
    public Text m_CoolDownText;                         // �N�[���_�E���\���e�L�X�g
    [SerializeField] private int m_nFontSize = 24;      // �N�[���_�E���̃t�H���g�T�C�Y�ύX�p
    public bool m_bMove = true;                        // true:�z�u�� false:�z�u��
    public float m_fPosY;
    public bool m_bSetting = true;
    private GameObject player;

 /*���������֐�
 �����P�F�Ȃ�
 ��
 �ߒl�F�Ȃ�
 ��
 �T�v�F�C���X�^���X�������ɍs������
 */
    void Start()
    {
        // �e�L�X�g�̏�����
        m_CoolDownText = GetComponentInChildren<Text>();
        m_CoolDownText.text = m_dCoolTime.ToString();   // text�̏�����
        m_CoolDownText.fontSize = m_nFontSize;              // �t�H���g�T�C�Y��ύX
        m_CoolDownText.alignment = TextAnchor.MiddleCenter; // text�̕\���ʒu��^�񒆂�
        m_CoolDownText.gameObject.SetActive(false);         // �g�p�O�Ȃ̂Ŕ�\��

        player = GameObject.Find("Player");//����
        Settings();
    }

/*��㩔����`�F�b�N�֐�
�����P�F�����蔻�肪�������I�u�W�F�N�g�̏��
��
�ߒl�F㩔����\��true �s��false
��
�T�v�F㩔����\���ǂ����̃`�F�b�N
*/
    public bool Check(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && m_bUse && !m_bMove)  // Enemy�^�O�����Ă��違�n���g�p�\
        {
            m_bUse = false;                     // �g�p�s��
            SetCoolTime();
           
            return true;
        }
        return false;
    }
    public virtual void SetCheck(Collision collision)
    {
        if (/*collision.gameObject.CompareTag("Map") || */collision.gameObject.CompareTag("Trap"))
        {
           m_bSetting=false;
        }
        
    }


    /*�������X�V�֐�
�����F�Ȃ�
��
�ߒl�F�Ȃ�
��
�T�v�F��莞�Ԃ��Ƃɍs���X�V����
*/
    private void FixedUpdate()
    {
        if(m_bMove)
        {
            Vector3 p = player.transform.forward * 2;
            transform.position = player.transform.position + p;
            Settings();
           // if (Input.GetKeyDown(KeyCode.R)&& m_bSetting)
           // {
           //     Debug.Log("dddd");
           //     m_bMove = false;
           //     GameObject A;
           //     CTrapSelect T;
           //    A = GameObject.Find("TrapManager");
           //     T = A.GetComponent<CTrapSelect>();
           //     T.SetSelect();
           // }
           // m_bSetting = true;
        }
        CooltimeCount();
        
    }
    private void CooltimeCount()
    {
        //���ė��p�J�E���g�_�E��
        if (m_dCoolTime > 0.0d)   //�N�[���_�E����
        {
            m_dCoolTime -= Time.fixedDeltaTime;
            m_CoolDownText.text = m_dCoolTime.ToString("F1");   //�����_�ȉ��ꌅ�܂ŃN�[���_�E���\��
            if (m_dCoolTime <= 0.0d)
            {
                m_bUse = true;
                m_CoolDownText.gameObject.SetActive(false);
            }  //���p�\��
        }
    }
    
    public  void Settings()
    {
        Vector3 pos = transform.position;
        pos.y = m_fPosY;
        transform.position = pos;

    }
    public void SetCoolTime()
    {
        m_dCoolTime = m_dInterval;          // �ė��p���Ԍv��
        m_CoolDownText.gameObject.SetActive(true);
        m_bUse=false;
    }

    public void aaa()
    {
        if (Input.GetKeyDown(KeyCode.R) && m_bSetting)
        {
            Debug.Log("dddd");
            m_bMove = false;
            GameObject A;
            CTrapSelect T;
            A = GameObject.Find("TrapManager");
            T = A.GetComponent<CTrapSelect>();
            T.SetSelect();
        }
        m_bSetting = true;
    }
}
