/*=====
<Mine.cs> 
���쐬�ҁFyamamoto

�����e
�n���ɕt����X�N���v�g�B
���j�G�t�F�N�g�̐����͂�����

�����ӎ���  
�n����IsTrigger�����Ă���Ɠ��삵�܂���B
Prefab��ݒ肵�Ă��Ȃ��Ɣ����G�t�F�N�g����������Ȃ��B

���X�V����
__Y24   
_M05    
D
8 :�v���O�����쐬:yamamoto 
9 :�d�l�ύX�̈׏�����ύX:yamamoto
10:�R�����g�ǉ�:yamamoto
12:���L���X�g���Ԓǉ�:yamamoto
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;

//���N���X��`
public class Mine : MonoBehaviour
{
    //���ϐ��錾
    private double m_dTimeToExplosion = 0.0d;                    // �����܂ł̎���[s]
    [SerializeField] private double m_dExplosionDelay = 2.0d;    // �����܂ł̑ҋ@����[s]
    private double m_dMineCoolTime = 0.0d;                       // �n���N�[���^�C��[s]
    [SerializeField] private double m_dMineInterval = 5.0d;      // �n���ė��p�܂ł̎���[s]
    private bool m_bCanExplode = true;                           // �n�����p true:�\ false:�s��
    [SerializeField] private GameObject m_ExplosionEffectPrefab; // ���������������v���n�u
    private Text m_CoolDownText;                                 // �N�[���_�E���\���e�L�X�g
    [SerializeField] private int m_nFontSize=24;                 // �N�[���_�E���̃t�H���g�T�C�Y�ύX�p

    /*�t�H���g��ς���Ƃ��͊O���Ă�������
    [SerializeField] private Font m_CustomFont;                  // �t�H���g�ύX�p
    */

    /*���������֐�
  �����P�F�Ȃ�
  ��
  �ߒl�F�Ȃ�
  ��
  �T�v�F�C���X�^���X�������ɍs������
  */
    void Start()
    {
        m_CoolDownText = GetComponentInChildren<Text>();
        m_CoolDownText.text = m_dMineCoolTime.ToString();   // text�̏�����
        m_CoolDownText.fontSize = m_nFontSize;              // �t�H���g�T�C�Y��ύX
        m_CoolDownText.alignment = TextAnchor.MiddleCenter; // text�̕\���ʒu��^�񒆂�
        m_CoolDownText.gameObject.SetActive(false);         // �g�p�O�Ȃ̂Ŕ�\��
    }

    /*���n�������蔻��֐�
    �����P�F�����蔻�肪�������I�u�W�F�N�g�̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�G���n���ɐG�ꂽ�Ƃ��̂ݏ��������
    */
    private void OnCollisionStay(Collision collision)     //�n���ɉ������������Ă����Ƃ�
    {
        if (collision.gameObject.CompareTag("Enemy") && m_bCanExplode)  // Enemy�^�O�����Ă��違�n���g�p�\
        {
            m_bCanExplode = false;                      // �����ɕ��������ꏊ�ɐ��������̂�h�~
            m_dTimeToExplosion = m_dExplosionDelay;     // �����܂ł̎��Ԍv��
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
        //�������J�E���g�_�E��
        if (m_dTimeToExplosion > 0.0d)   //�ҋ@��
        {
            m_dTimeToExplosion -= Time.fixedDeltaTime;
            if (m_dTimeToExplosion <= 0.0d)   //�����܂ł̑ҋ@���Ԃ��I�������
            {   //�����G�t�F�N�g����
                Instantiate(m_ExplosionEffectPrefab, transform.position, Quaternion.identity);
                m_dMineCoolTime = m_dMineInterval;  //�n���̍ė��p���Ԍv��
                m_CoolDownText.gameObject.SetActive(true);
            }
        }

        //���n���̍ė��p�J�E���g�_�E��
        if (m_dMineCoolTime > 0.0d)   //�N�[���_�E����
        {
            m_dMineCoolTime -= Time.fixedDeltaTime;
            m_CoolDownText.text = m_dMineCoolTime.ToString("F1");   //�����_�ȉ��ꌅ�܂ŃN�[���_�E���\��
            if (m_dMineCoolTime < 0.0d) 
            { 
                m_bCanExplode = true;
                m_CoolDownText.gameObject.SetActive(false);
            }  //�n�����p�\��
        }
    }
}
