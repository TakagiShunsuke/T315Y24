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
    private double dTimeToExplosion = 0.0d;                    // �����܂ł̎���[s]
    [SerializeField] private double dExplosionDelay = 2.0d;    // �����܂ł̑ҋ@����[s]
    public double dMineCoolTime = 0.0d;                        // �n���N�[���^�C��[s]
    [SerializeField] public double dMineInterval = 5.0d;       // �n���ė��p�܂ł̎���[s]
    private bool bCanExplode = true;                           // �n�����p true:�\ false:�s��
    public GameObject ExplosionEffectPrefab;                   // ���������������v���n�u
    private Text CoolDownText;                                 // �N�[���_�E���\���e�L�X�g
    [SerializeField] private int nFontSize=24;                 // �N�[���_�E���̃t�H���g�T�C�Y�ύX�p
    /*�t�H���g��ς���Ƃ��͊O���Ă�������
    [SerializeField] private Font customFont;                  // �t�H���g�ύX�p
    */
    void Start()
    {
        CoolDownText = GetComponentInChildren<Text>();
        CoolDownText.text = dMineCoolTime.ToString();   //text�̏�����
        CoolDownText.fontSize = nFontSize;              // �t�H���g�T�C�Y��ύX
        CoolDownText.alignment = TextAnchor.MiddleCenter;
        CoolDownText.gameObject.SetActive(false);       //�g�p�O�Ȃ̂Ŕ�\��

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
        if (collision.gameObject.CompareTag("Enemy") && bCanExplode)  //Enemy�^�O�����Ă��違�n���g�p�\
        {
            bCanExplode = false;                    //�����ɕ��������ꏊ�ɐ��������̂�h�~
            dTimeToExplosion = dExplosionDelay;     //�����܂ł̎��Ԍv��
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
        if (dTimeToExplosion > 0.0d)   //�ҋ@��
        {
            dTimeToExplosion -= Time.fixedDeltaTime;
            if (dTimeToExplosion <= 0.0d)   //�����܂ł̑ҋ@���Ԃ��I�������
            {   //�����G�t�F�N�g����
                Instantiate(ExplosionEffectPrefab, transform.position, Quaternion.identity);
                dMineCoolTime = dMineInterval;  //�n���̍ė��p���Ԍv��
                CoolDownText.gameObject.SetActive(true);
            }
        }
        //���n���̍ė��p�J�E���g�_�E��
        if (dMineCoolTime > 0.0d)   //�N�[���_�E����
        {

            dMineCoolTime -= Time.fixedDeltaTime;
            CoolDownText.text = dMineCoolTime.ToString("F1");   //�����_�ȉ��ꌅ�܂ŃN�[���_�E���\��
            if (dMineCoolTime < 0.0d) 
            { 
                bCanExplode = true;
                CoolDownText.gameObject.SetActive(false);
            }  //�n�����p�\��
        }
    }
}
