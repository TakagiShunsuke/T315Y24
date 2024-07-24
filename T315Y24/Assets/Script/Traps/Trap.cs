/*=====
<Trap.cs> 
���쐬�ҁFyamamoto

�����e
㩂̐e�N���X

���X�V����
__Y24   
_M06
D
08�F�v���O�����쐬�Fyamamoto
13�F������SE�ǉ��Fnieda
18�FSE�ǉ��Fnieda
26�F�R�����g�ǉ��Fyamamoto
27�FSE�֌W���t�@�N�^�����O�Fnieda
=====*/

//�����O��Ԑ錾
using Effekseer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

//���N���X��`
public class CTrap : MonoBehaviour
{
    //�ϐ��錾
    [Header("�X�e�[�^�X")]
    [SerializeField, Tooltip("�Ďg�p�ł���܂ł̎���(�b)")] private double m_dInterval = 5.0d; // �C���^�[�o��
    private double m_dCoolTime = 0.0d;                  // �C���^�[�o���v���p
    [Tooltip("�ݒu���鍂��")] public float m_fPosY;     // �ݒu���鍂��
    [Tooltip("�G��Ȃ���")] public bool m_bMove = true;                         // true:�z�u�� false:�z�u��
    [Tooltip("�G��Ȃ���")] public bool m_bSetting = true;  //true:�ݒu�\�@false:�ݒu�s��
    [Tooltip("�G��Ȃ���")] public bool m_bUse = true;  // ���p true:�\ false:�s��

    [Header("�G�t�F�N�g")]
    [SerializeField, Tooltip("�ݒu���Đ�����G�t�F�N�g")] private EffekseerEffectAsset m_SetEffect;  // �ݒu���Đ�����G�t�F�N�g

    [Header("�e�L�X�g")]
    [SerializeField,Tooltip("�\���ptext")] private Text m_CoolDownText;       // �N�[���_�E���\���e�L�X�g
    [SerializeField,Tooltip("�t�H���g�T�C�Y")] private int m_nFontSize = 24;  // �N�[���_�E���̃t�H���g�T�C�Y�ύX�p
                                                                         
    [Header("��")]
    [Tooltip("AudioSource��ǉ�")] protected AudioSource m_audioSource;    // AudioSource��ǉ�
    [SerializeField,Tooltip("㩐ݒu����SE")] protected AudioClip SE_SetTrap;   // 㩐ݒu����SE
    [SerializeField,Tooltip("㩔�������SE")] protected AudioClip SE_ExpTrap;   // 㩔�������SE

    private GameObject player;  //player�i�[�p
    public Material material; // �������ɂ������}�e���A��

    //���v���p�e�B��`
    virtual public int Cost { get; protected set; }//�R�X�g
    virtual public Sprite ImageSprite { get; protected set; } //UI�A�Z�b�g���摜�ɕϊ���������


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
        m_CoolDownText = GetComponentInChildren<Transform>().GetComponentInChildren<Text>();    // �q��Text�擾
        m_CoolDownText.text = m_dCoolTime.ToString();       // text�̏�����
        m_CoolDownText.fontSize = m_nFontSize;              // �t�H���g�T�C�Y��ύX
        m_CoolDownText.alignment = TextAnchor.MiddleCenter; // text�̕\���ʒu��^�񒆂�
        m_CoolDownText.gameObject.SetActive(false);         // �g�p�O�Ȃ̂Ŕ�\��

        player = GameObject.Find("Player");                 // Player������
        Settings();         // �v���C���[�������Ă��������㩂����Z�b�g
        m_bSetting = true;  // �ݒu�\
        m_audioSource = GetComponent<AudioSource>();        // AudioSource���擾

       Transparent transparent = GetComponent<Transparent>();
        transparent.color.a = 0.8f;
        transparent.ClearMaterialInvoke();
        
    }

    /*��㩔����`�F�b�N�֐�
    �����P�FCollision _Collision : �������Ă�����̂̏��
    ��
    �ߒl�F㩔����\��true �s��false
    ��
    �T�v�F㩔����\���ǂ����̃`�F�b�N
    */
    public bool Check(Collision _collision,bool _Use)
    {
        if (_collision.gameObject.CompareTag("Enemy") && m_bUse && !m_bMove)
        {// Enemy�^�O�����Ă��違�n���g�p�\�Ȃ�
            SetCoolTime();      //�N�[���^�C�����Z�b�g
            m_bUse = _Use;     // �g�p�s��
            return true;
        }
        return false;
    }

    /*���ݒu�s����֐�
    �����P�FCollision _Collision : �������Ă�����̂̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F������㩂ɓ������Ă�����ݒu�s�ɂ���
    */
    public virtual void SetCheck(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Trap"))
        {//���������̂�������㩂�������
            m_bSetting =false;      //�ݒu�s��
        }
        
    }
    /*���ݒu����֐�
    �����P�FCollision _Collision : �������Ă�����̂̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F������㩂ɓ�����Ȃ��Ȃ�����ݒu�\�ɂ���
    */
    public virtual void OutCheck(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Trap"))
        {//������Ȃ��Ȃ����̂�������㩂�������
            m_bSetting = true;  //�ݒu�\
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
            Vector3 vPos = player.transform.forward * 2;    //�v���C���[�̐��ʕ����̃x�N�g�������
            transform.position = player.transform.position + vPos;  //�����ăv���C���[�̏����O��㩂������Ă���
            Settings();     //�����������ɖ߂�
        }
        CooltimeCount();    //�N�[���^�C���̌v�Z
        
    }

    /*���N�[���^�C���v���֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�N�[���^�C�������鎞�̂ݏ��������
    */
    private void CooltimeCount()
    {
        //���ė��p�J�E���g�_�E��
        if (m_dCoolTime > 0.0d)   //�N�[���_�E����
        {
            m_dCoolTime -= Time.fixedDeltaTime;
            m_CoolDownText.text = m_dCoolTime.ToString("F1");   //�����_�ȉ��ꌅ�܂ŃN�[���_�E���\��
            if (m_dCoolTime <= 0.0d)
            {//�N�[���^�C�����I�������
                m_bUse = true;      //�g�p�\
                m_CoolDownText.gameObject.SetActive(false); //Text��\��
            }
        }
    }

    /*��㩂̍���(y)�ύX�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F����(y)�ύX
    */
    public void Settings()
    {
        Vector3 pos = transform.position;   //���݂̈ʒu���擾
        pos.y = m_fPosY;                    //���̂ݕύX
        transform.position = pos;           //�ύX�����

    }

    /*���N�[���^�C���Z�b�g�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�N�[���^�C�����Z�b�g����
    */
    public void SetCoolTime()
    {
        m_dCoolTime = m_dInterval;                  // �N�[���^�C���Z�b�g
        m_CoolDownText.gameObject.SetActive(true);  //Text��\��
        m_bUse=false;                               //�g�p�s��
    }

    /*��㩐ݒu�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F㩂�ݒu����
    */
    public void SetTrap()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Decision")||Input.GetKeyDown(KeyCode.Return)) && m_bSetting && m_bMove)
        {//�u������������������
            m_audioSource.PlayOneShot(SE_SetTrap);  //�z�uSE�Đ�
            m_bMove = false;                        //�ꏊ�Œ�̂���false��

            //�z�u����㩂�I���\��
            GameObject TrapManager;                 //"TrapManager"�i�[�p�@
            CTrapSelect TrapSelect;                 //CTrapSelect�i�[�p
            TrapManager = GameObject.Find("TrapSelect");           //�hTrapManager�h���������擾
            TrapSelect = TrapManager.GetComponent<CTrapSelect>();   //CTrapSelect���擾
            TrapSelect.SetSelect();               //�z�u����㩂�I���\  �ɕύX

            Destroy(GetComponent<Rigidbody>());     //Rigidbody������j��
            SetCount();

            Transparent transparent = GetComponent<Transparent>();
            transparent.NotClearMaterialInvoke();

            //���ۑS
            if (m_SetEffect != null)   //�G�t�F�N�g���Ȃ�
            {
                //���ݒu�G�t�F�N�g�Đ�
                EffekseerSystem.PlayEffect(m_SetEffect, transform.position);  //�ݒu�ʒu�ɍĐ�
            }
#if UNITY_EDITOR    //�G�f�B�^�g�p��
            else
            {
                //���G���[�o��
                UnityEngine.Debug.LogWarning("�K�v�ȗv�f���s�����Ă��܂�");  //�x�����O�o��
            }
#endif
        }
    }

    /*��㩐ݒu�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�e㩂̃X�N���v�g�ŏ����B
    */
    public virtual void SetCount()
    {
        Debug.LogWarning("�K�v�ȗv�f���s�����Ă��܂�");  //�x�����O�o��
    }
}
