/*=====
<ArrowRain.cs> 
���쐬�ҁFyamamoto

�����e
��𔭎�㩂ɕt����X�N���v�g�B

�����ӎ���  
㩋N���X�C�b�`��IsTrigger�����Ă���Ɠ��삵�܂���B

���X�V����
__Y24   
_M05    
D
16 :�v���O�����쐬:yamamoto 
30 :�R�����g�ǉ�  :yamamoto

=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���N���X��`
public class CArrowRain : MonoBehaviour
{
    //���ϐ��錾
    private double m_dArrowRainCoolTime = 0.0d;                  // �n���N�[���^�C��[s]
    [SerializeField] private double m_dArrowRainInterval = 5.0d; // �n���ė��p�܂ł̎���[s]
    private bool m_bCanArrowRain = true;                         // �n�����p true:�\ false:�s��
    private Text m_CoolDownText;                                 // �N�[���_�E���\���e�L�X�g
    [SerializeField] private int m_nFontSize = 24;               // �N�[���_�E���̃t�H���g�T�C�Y�ύX�p
    [SerializeField] private GameObject arrowPrefab;             // ���Prefab
    [SerializeField] private Transform[] shootPoints;            // ��𔭎˂���ʒu
    [SerializeField] private int arrowsPerPress = 1;             // ��x�ɔ��˂����̖{��
    [SerializeField] private float shootForce = 100f;            // ��̔��ˑ��x

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
        m_CoolDownText.text = m_dArrowRainCoolTime.ToString();// text�̏�����
        m_CoolDownText.fontSize = m_nFontSize;              // �t�H���g�T�C�Y��ύX
        m_CoolDownText.alignment = TextAnchor.MiddleCenter; // text�̕\���ʒu��^�񒆂�
        m_CoolDownText.gameObject.SetActive(false);         // �g�p�O�Ȃ̂Ŕ�\��
    }

    /*���N���X�C�b�`�����蔻��֐�
    �����P�F�����蔻�肪�������I�u�W�F�N�g�̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�G���N���X�C�b�`�ɐG�ꂽ�Ƃ��̂ݏ��������
    */
    private void OnCollisionStay(Collision collision)     //�������������Ă����Ƃ�
    {
        if (collision.gameObject.CompareTag("Enemy") && m_bCanArrowRain)  // Enemy�^�O�����Ă��違㩎g�p�\
        {
            m_bCanArrowRain = false;    // �����ɕ��������ꏊ�ɐ��������̂�h�~
            
            ShootArrows();                                //��ˊ֐��Ăяo��
            m_dArrowRainCoolTime = m_dArrowRainInterval;  //�ė��p���Ԑݒ�
            m_CoolDownText.gameObject.SetActive(true);
        }
    }

    /*����ˊ֐�
   �����P�F�����蔻�肪�������I�u�W�F�N�g�̏��
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�G���N���X�C�b�`�ɐG�ꂽ�Ƃ��̂ݏ��������
   */
    public void ShootArrows()
    {
        for (int i = 0; i < arrowsPerPress; i++)    //�����ꏊ�����΂��{��
        {
            foreach (Transform shootPoint in shootPoints)   //��΂��ꏊ��
            {
                GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);//����
                Rigidbody rb = arrow.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    //��𔭎˓_�̌����ɔ�΂�
                    Vector3 shootDirection = shootPoint.forward;
                    rb.velocity = shootDirection * shootForce;

                    // ��̐i�s�����ɍ��킹�ĉ�]��ݒ�
                    arrow.transform.rotation = Quaternion.LookRotation(shootDirection);
                }
            }
        }
    }
    /*���X�V�֐�
      �����F�Ȃ�
      ��
      �ߒl�F�Ȃ�
      ��
      �T�v�F��莞�Ԃ��Ƃɍs���X�V����
      */
    private void FixedUpdate()
    {
        //���ė��p�J�E���g�_�E��
        if (m_dArrowRainCoolTime > 0.0d)   //�N�[���_�E����
        {
            m_dArrowRainCoolTime -= Time.fixedDeltaTime;
            m_CoolDownText.text = m_dArrowRainCoolTime.ToString("F1");   //�����_�ȉ��ꌅ�܂ŃN�[���_�E���\��
            if (m_dArrowRainCoolTime < 0.0d)
            {
                m_bCanArrowRain = true;
                m_CoolDownText.gameObject.SetActive(false);
            }  //���p�\��
        }
    }

}
