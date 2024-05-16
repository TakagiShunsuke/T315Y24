using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CArrowRain : MonoBehaviour
{
    //���ϐ��錾

    private double m_dArrowRainCoolTime = 0.0d;                  // �n���N�[���^�C��[s]
    [SerializeField] private double m_dArrowRainInterval = 5.0d; // �n���ė��p�܂ł̎���[s]
    private bool m_bCanArrowRain = true;                         // �n�����p true:�\ false:�s��
   // [SerializeField] private GameObject m_ArrowRainEffectPrefab; // ���������������v���n�u
    private Text m_CoolDownText;                                 // �N�[���_�E���\���e�L�X�g
    [SerializeField] private int m_nFontSize = 24;               // �N�[���_�E���̃t�H���g�T�C�Y�ύX�p


    [SerializeField] private GameObject arrowPrefab; // ���Prefab
    [SerializeField] private Transform[] shootPoints; // ��𔭎˂���ʒu
    [SerializeField] private int arrowsPerPress = 1; // ��x�ɔ��˂����̖{��
    [SerializeField] private float shootForce = 100f; // ��̔��ˑ��x

    // Start is called before the first frame update
    void Start()
    {
        m_CoolDownText = GetComponentInChildren<Text>();
        m_CoolDownText.text = m_dArrowRainCoolTime.ToString();   // text�̏�����
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
        if (collision.gameObject.CompareTag("Enemy") && m_bCanArrowRain)  // Enemy�^�O�����Ă��違�n���g�p�\
        {
            m_bCanArrowRain = false;                      // �����ɕ��������ꏊ�ɐ��������̂�h�~
            //�����G�t�F�N�g����
            
            ShootArrows();
            m_dArrowRainCoolTime = m_dArrowRainInterval;  //�n���̍ė��p���Ԍv��
            m_CoolDownText.gameObject.SetActive(true);
        }
    }
    public void ShootArrows()
    {
        for (int i = 0; i < arrowsPerPress; i++)
        {
            foreach (Transform shootPoint in shootPoints)
            {
                GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
                Rigidbody rb = arrow.GetComponent<Rigidbody>();
                if (rb != null)
                {

                    // ��𔭎˓_�̌����ɔ�΂�
                    Vector3 shootDirection = shootPoint.forward;
                    rb.velocity = shootDirection * shootForce;

                    // ��̐i�s�����ɍ��킹�ĉ�]��ݒ�
                    arrow.transform.rotation = Quaternion.LookRotation(shootDirection);
                    //rb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
                }
            }
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
        //���n���̍ė��p�J�E���g�_�E��
        if (m_dArrowRainCoolTime > 0.0d)   //�N�[���_�E����
        {
            m_dArrowRainCoolTime -= Time.fixedDeltaTime;
            m_CoolDownText.text = m_dArrowRainCoolTime.ToString("F1");   //�����_�ȉ��ꌅ�܂ŃN�[���_�E���\��
            if (m_dArrowRainCoolTime < 0.0d)
            {
                m_bCanArrowRain = true;
                m_CoolDownText.gameObject.SetActive(false);
            }  //�n�����p�\��
        }
    }

}
