/*=====
<PlayerScript.cs> 
���쐬�ҁFiwamuro

�����e
Player�𓮂����X�v���N�g

���X�V����
__Y24
_M05
D
04:�v���O�����쐬:iwamuro
11:�̗́E�U�����󂯂鏈���ǉ�:takagi
13:�v���C���[�̈ړ��Ɗp�x�̏C���Aunity��ŃX�s�[�h��ύX�ł���悤�ɕύX:iwamuro
23:�_�b�V���ǉ�:takagi
30:�_�b�V�����C�E���G���ԕ���:takagi
31:���t�@�N�^�����O:takagi

_M06
D
13:�_�b�V�����A��_������SE�ǉ�:nieda
17:SE�ǉ�:nieda
21:���t�@�N�^�����O:takagi
27:SE�֌W���t�@�N�^�����O
_M07
D
04:�R���g���[�������ǉ�:iwamuro
=====*/

//�����O��Ԑ錾
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static InputDeviceManager;

//���N���X��`
public class CPlayerScript : MonoBehaviour, IDamageable
{
    //���ϐ��錾
    private Rigidbody m_Rb;      // Rigidbody��ǉ�
    [Header("�X�e�[�^�X")]
    [SerializeField, Tooltip("���ʊp�x[��]")] private double m_dFrontAngle;  //xz���ʏ�Ő��ʕ����̊p�x
    [SerializeField, Tooltip("���x[m/s]")] private float m_fSpeed; //�v���C���[�̈ړ����x��ݒ�
    [SerializeField, Tooltip("HP")] private double m_dHp;   // HP
    [Header("�_�b�V��")]
    [SerializeField, Tooltip("���L���X�g����")] private double m_unDashInterval;   //�_�b�V�����L���X�g����
    private double m_dCntDwnInvicibleTime = 0.0d;  //���G���ԃJ�E���g�p
    private double m_dCntDwnDshInterval = 0.0d;  //�_�b�V���J�E���g�_�E���p
    [SerializeField] private KeyCode m_DushKey = KeyCode.E; //�_�b�V���̃L�[
    [SerializeField] private double m_DashDist;  //�_�b�V�����Ɉړ����鋗��
    [Header("���G")]
    [SerializeField, Tooltip("��U����[s]")] private double m_dDamagedInvicibleTime;  //���G����[s] :�������U�����󂯂��Ƃ��ɔ���
    [SerializeField, Tooltip("�_�b�V����[s]")] private double m_dDushInvicibleTime;  //���G����[s] :�_�b�V�����ɔ���
    [Header("��")]
    [Tooltip("AudioSource��ǉ�")] private AudioSource m_AudioSource;          // AudioSource��ǉ�
    [SerializeField, Tooltip("�_�b�V������SE")] private AudioClip SE_Dash;     // �_�b�V������SE
    [SerializeField, Tooltip("��_������SE")] private AudioClip SE_Damage;     // ��_������SE

    //���v���p�e�B��`
    private double CntDwnInvicibleTime
    {
        get
        {
            return m_dCntDwnInvicibleTime;  //���G���Ԓ�
        }
        set
        {
            if (m_dCntDwnInvicibleTime < value) //���G���ԂɍX�V�̕K�v������
            {
                m_dCntDwnInvicibleTime = value; //���G���ԍX�V
            }
        }
    }   //���G���ԊǗ�
    public bool InvincibleState => CntDwnInvicibleTime > 0.0d;   //���G��ԊǗ�
    //{
    //    get
    //    {
    //        return CntDwnInvicibleTime > 0.0d;   //���G���Ԃɂ���Ĕ��肷��
    //    }
    //}   //���G��ԊǗ�
    [SerializeField] public double HP => m_dHp;  //HP��
    [SerializeField] public double DashCntDwn => m_dCntDwnDshInterval;  //�_�b�V���̃J�E���g�_�E�����Ԓ�


    /*�����������֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FRigidbody�R���|�[�l���g��ǉ�
    */
    private void Start()    //�����Œǉ������
    {
        //��������
        m_Rb = GetComponent<Rigidbody>(); //Rigidbody�R���|�[�l���g��ǉ�
        m_AudioSource = GetComponent<AudioSource>();    //AudioSource�R���|�[�l���g��ǉ�
    
    }

    /*���ړ������֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�L�[�������ꂽ��ړ������s��
    */
    private void Update()   //�L�[�������ꂽ�Ƃ��ɍX�V���s��
    {
        Vector3 moveDirection = Vector3.zero; // �ړ������̏�����
                                              //     Vector3 target_dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));    //�v���C���[�̌�����ς���x�N�g��
                                              //  Vector3 target_dir = new Vector3(Input.GetAxis("Horizontal") + Input.GetAxis("JoystickHorizontal"), 0, Input.GetAxis("Vertical") + Input.GetAxis("JoystickVertical"));    //�v���C���[�̌�����ς���x�N�g��
                                              // �L�[�{�[�h�̓��͂��擾
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �R���g���[���[�̓��͂��擾
        float joystickHorizontal = Input.GetAxis("JoystickHorizontal");
        float joystickVertical = Input.GetAxis("JoystickVertical");

        // ���͂�����
        Vector3 target_dir = new Vector3(horizontalInput + joystickHorizontal, 0, verticalInput + joystickVertical);

        if (target_dir.magnitude > 0.1) //�x�N�g���̒�����0.01f���傫���ꍇ�Ƀv���C���[�̌�����ς���
        {
            //�̂̌�����ύX
            transform.rotation = Quaternion.LookRotation(target_dir);
            //�O���ֈړ�
            transform.Translate(Vector3.forward * Time.deltaTime * m_fSpeed);
        }

        // �΂߈ړ�
        if (moveDirection != Vector3.zero)
        {

            // ���K�����Ĉړ����x�����ɕۂ�
            moveDirection.Normalize();
            m_Rb.velocity = moveDirection * m_fSpeed;

        }
        else
        {
            // �����L�[��������Ă��Ȃ��ꍇ�͒�~����
            m_Rb.velocity = Vector3.zero;
        }

        //������
        if (CntDwnInvicibleTime > 0.0d)   //���G��Ԃ̎�
        {
            //���J�E���g�_�E��
            m_dCntDwnInvicibleTime -= Time.deltaTime;   //���Ԃ��J�E���g
        }
        if (m_dCntDwnDshInterval > 0.0d)
        {
            m_dCntDwnDshInterval -= Time.deltaTime;   //���Ԃ��J�E���g
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
        //�����G�\��
        if (InvincibleState)   //���G��
        {
            //TODO:���ŏ���
            //var RadSp = 2.0d * Math.PI * (double)m_unFlNu * CntDwnInvicibleTime / m_dInvicibleTime;

            //var mr = GetComponent<MeshRenderer>();
            //mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, (float)(Math.Sin(RadSp) * 255.0d));
        }

        if (InputDeviceManager.Instance != null)
        {
            // ���݂̓��̓f�o�C�X�^�C�v���擾
            InputDeviceManager.InputDeviceType currentDeviceType = InputDeviceManager.Instance.CurrentDeviceType;

            //���_�b�V������
            // ���݂̃f�o�C�X�^�C�v�ɉ������������s��
            switch (currentDeviceType)
            {
                case InputDeviceManager.InputDeviceType.Keyboard:
                    Debug.Log("Keyboard���g�p����Ă��܂�");
                    if (Input.GetKeyDown(/*m_DushKey*/KeyCode.Space)) //�_�b�V������
                    {
                        
                        Dash(); //�_�b�V������
                    }
                    break;
                case InputDeviceManager.InputDeviceType.Xbox:
                    Debug.Log("XBOX���g�p����Ă��܂�");
                    if (Input.GetButtonDown("Dash"))
                    {
                        Dash(); //�_�b�V������
                    }
                    break;
                case InputDeviceManager.InputDeviceType.DualShock4:
                    Debug.Log("DualShock4(PS4)���g�p����Ă��܂�");
                    if (Input.GetButtonDown("Dash"))
                    {
                        Dash(); //�_�b�V������
                    }
                    break;
                case InputDeviceManager.InputDeviceType.DualSense:
                    Debug.Log("DualSense(PS5)���g�p����Ă��܂�");
                    if (Input.GetButtonDown("Dash"))
                    {
                        Dash(); //�_�b�V������
                    }
                    break;
                case InputDeviceManager.InputDeviceType.Switch:
                    Debug.Log("Switch��Pro�R���g���[���[���g�p����Ă��܂�");
                    if (Input.GetButtonDown("Dash"))
                    {
                        Dash(); //�_�b�V������
                    }
                    break;
                default:
                    Debug.Log("���m�̓��̓f�o�C�X���g�p����Ă��܂�");
                    break;
            }
        }

    }

    /*����_���[�W�֐�
    �����Fdouble dDamageVal
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�_���[�W���󂯂�
    */
    public void Damage(double _dDamageVal)
    {
        //������������
        if (InvincibleState)    //���G��
        {
            return; //�_���[�W���󂯂Ȃ�
        }

        //���_���[�W�v�Z
        m_dHp -= _dDamageVal;    //HP����
        CntDwnInvicibleTime = m_dDamagedInvicibleTime;  //���G���Ԃ̃J�E���g�����Z�b�g����
        
        m_AudioSource.PlayOneShot(SE_Damage);   // ��_����SE�ǉ�
    }

    /*���_�b�V���֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�_�b�V�����Ĉړ�����
    */
    private void Dash()
    {
        //������
        if (m_dCntDwnDshInterval > 0.0d)   //�N�[���_�E�����̎�
        {
            //���_�b�V���s�\
            return;     //�������f
        }

        //���ϐ��錾�E������
        Vector3 _vFront = new((float)Math.Cos(Mathf.Deg2Rad * (-transform.eulerAngles.y + m_dFrontAngle)), 0.0f,
            (float)Math.Sin(Mathf.Deg2Rad * (-transform.eulerAngles.y + m_dFrontAngle)));  //���ʕ����̃x�N�g��  ��y����]�̕����͍��W�n�Ƌt����
        _vFront = _vFront.normalized * (float)m_DashDist; //�x�N�g���T�C�Y������
        Vector3 _vGo = transform.position + _vFront;    //�s����\��n�̍��W
        Ray _Ray = new Ray(transform.position + Vector3.up, _vFront);   //�O�����C�L���X�g
        RaycastHit _Hit; //���C�L���X�g�œ����铖���蔻��

        //���Փˉ��  //����͂���œG�̑O�Ŏ~�܂邱�Ƃ�...�G�F    //TODO:�^�O
        if (Physics.Raycast(_Ray, out _Hit))
        {
            var cp = GetComponent<CapsuleCollider>();   //�����蔻��擾

            var vBeforeCollide = _Hit.point - transform.localScale * cp.radius; //�����蔻��ɖӖړI�ɕ␳�����ϐ�
            if (Math.Abs(vBeforeCollide.x - transform.position.x) < Math.Abs(_vFront.x))   //��������̃x�N�g���Ƃ���x�l�ɂ��Ĕ�r
            {
                _vGo.x = vBeforeCollide.x;  //x�l��␳�l�ɒu��������
            }
            if (Math.Abs(vBeforeCollide.z - transform.position.z) < Math.Abs(_vFront.z))   //��������̃x�N�g���Ƃ���z�l�ɂ��Ĕ�r
            {
                _vGo.z = vBeforeCollide.z;  //z�l��␳�l�ɒu��������
            }
        }
        if (_vGo != transform.position)  //�ړ���ɑ΂��ĕψڂ�����Ƃ�
        {
            m_Rb.transform.position = _vGo;  //�����Ɉړ����s��

            m_AudioSource.PlayOneShot(SE_Dash); // �_�b�V����SE�Đ�

            //���J�E���g�_�E��
            m_dCntDwnDshInterval = m_unDashInterval;   //���Ԃ��J�E���g
            CntDwnInvicibleTime = m_dDushInvicibleTime;  //���G���Ԃ̃J�E���g�����Z�b�g����
        }
    }
}