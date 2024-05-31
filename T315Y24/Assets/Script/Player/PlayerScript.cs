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
=====*/

//�����O��Ԑ錾
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//���N���X��`
public class CPlayerScript : MonoBehaviour, IDamageable
{
    //���ϐ��錾
    Rigidbody m_Rb;      // Rigidbody��ǉ�
    [SerializeField] private double m_dFrontAngle = 90.0d;  //xz���ʏ�Ő��ʕ����̊p�x
    [SerializeField] private float m_fSpeed = 6; //�v���C���[�̈ړ����x��ݒ�
    [SerializeField] private double m_dHp = 10;   // HP
    [SerializeField] private double m_dDamagedInvicibleTime = 2.0d;  //���G����[s] :�������U�����󂯂��Ƃ��ɔ���
    [SerializeField] private double m_unDashInterval = 5;   //�_�b�V�����L���X�g����
    [SerializeField] private double m_dDushInvicibleTime = 2.0d;  //���G����[s] :�_�b�V�����ɔ���
    private double m_dCntDwnInvicibleTime = 0.0d;  //���G���ԃJ�E���g�p
    private double m_dCntDwnDshInterval = 0.0d;  //�_�b�V���J�E���g�_�E���p
    [SerializeField] private KeyCode m_DushKey = KeyCode.E; //�_�b�V���̃L�[
    [SerializeField] private double m_DashDist = 2.0d;  //�_�b�V�����Ɉړ����鋗��

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
    public bool InvincibleState
    {
        get
        {
            return CntDwnInvicibleTime > 0.0d;   //���G���Ԃɂ���Ĕ��肷��
        }
    }   //���G��ԊǗ�
    [SerializeField] public double HP => m_dHp;  //HP��
    [SerializeField] public double DashCntDwn => m_dCntDwnDshInterval;  //�_�b�V���̃J�E���g�_�E�����Ԓ�

    /*�����������֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FRigidbody�R���|�[�l���g��ǉ�
    */
    void Start()    //�����Œǉ������
    {
        //��������
        m_Rb = GetComponent<Rigidbody>(); //Rigidbody�R���|�[�l���g��ǉ�
    }

    /*���ړ������֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�L�[�������ꂽ��ړ������s��
    */
    void Update()   //�L�[�������ꂽ�Ƃ��ɍX�V���s��
    {
        Vector3 moveDirection = Vector3.zero; // �ړ������̏�����
        Vector3 target_dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));    //�v���C���[�̌�����ς���x�N�g��

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

        //���_�b�V������
        if (Input.GetKeyDown(m_DushKey))    //�_�b�V������
        {
            Dash(); //�_�b�V������
        }
    }

    /*����_���[�W�֐�
    �����Fdouble dDamageVal
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�_���[�W���󂯂�
    */
    public void Damage(double dDamageVal)
    {
        //������������
        if (InvincibleState)    //���G��
        {
            return; //�_���[�W���󂯂Ȃ�
        }

        //���_���[�W�v�Z
        m_dHp -= dDamageVal;    //HP����
        CntDwnInvicibleTime = m_dDamagedInvicibleTime;  //���G���Ԃ̃J�E���g�����Z�b�g����
    }

    /*���_�b�V���֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�_�b�V�����Ĉړ�����
    */
    public void Dash()
    {
        //������
        if (m_dCntDwnDshInterval > 0.0d)   //�N�[���_�E�����̎�
        {
            //���_�b�V���s�\
            return;     //�������f
        }

        //���ϐ��錾�E������
        Vector3 _vFront = new((float)Math.Cos(Mathf.Deg2Rad * (-transform.eulerAngles.y+ m_dFrontAngle)), 0.0f,
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

            //���J�E���g�_�E��
            m_dCntDwnDshInterval = m_unDashInterval;   //���Ԃ��J�E���g
            CntDwnInvicibleTime = m_dDushInvicibleTime;  //���G���Ԃ̃J�E���g�����Z�b�g����
        }
    }
}