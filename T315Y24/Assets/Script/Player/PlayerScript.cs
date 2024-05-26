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
13:�ϐ����ύX:takagi
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
    [SerializeField] private float m_fSpeed = 6; //�v���C���[�̈ړ����x��ݒ�
    [SerializeField] private double m_dHp = 10;   // HP
    [SerializeField] private double m_unDashInterval =5;   //�_�b�V�����L���X�g����

    //���ϐ��錾
    [SerializeField] private double m_dInvicibleTime = 2.0d;  //���G����[s] :�����l�ς̂��߂ɔ�萔��
    //[SerializeField] private uint m_unFlNu = 5;
    //private bool m_bInvicibleFlag;   //���G��ԃt���O(true�Ŗ��G)
    private double m_dCntDwnInvicibleTime = 0.0d;  //���G���ԃJ�E���g�p
    private double m_dCntDwnDshInterval = 0.0d;  //cd�J�E���g�p
    [SerializeField] private KeyCode m_KeyCode = KeyCode.E;
    [SerializeField] private double m_DashDist = 2.0d;
    //private double m_dashtemp = 0.0d;

    //���v���p�e�B��`
    //public bool IsInvincible { get; private set; } = false; //���G��ԊǗ�
    public bool InvincibleState
    {
        get { return m_dCntDwnInvicibleTime > 0.0f; }   //�t���O�̑���Ɏ��ԂŔ��f����
        set
        {
            //����ԕ���
            if (value == true)  //���G��Ԃɂ���
            {
                m_dCntDwnInvicibleTime = m_dInvicibleTime;  //���G���Ԃ̃J�E���g�����Z�b�g����
            }
            else
            {
                m_dCntDwnInvicibleTime = 0.0f;  //���G���Ԃ𖳂���
            }
        }   //�Z�b�^�œ���ȏ���
    } //���G��ԊǗ�


    //���v���p�e�B��`
    public double HP
    {
        get { return m_dHp; }
        private set { m_dHp = value; }
    }
    //[SerializeField] public uint DashInterval {
    //    //get; private set;
    //    get => m_unDashInterval;  
    //    private set => m_unDashInterval = value;
    //}
    [SerializeField] public double DashCntDwn => m_dCntDwnDshInterval;  //�ǂݎ���p�v���p�e�B
    //{
    //    //get; private set;
    //    get => m_unDashInterval;
    //    private set => m_unDashInterval = value;
    //}

    /*�����������֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FRigidbody�R���|�[�l���g��ǉ�
    */
    void Start()    //�����Œǉ������
    {
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
        if (m_dCntDwnInvicibleTime > 0.0d)   //���G��Ԃ̎�
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
        if (InvincibleState)   //�N�[���_�E����
        {
            //var RadSp = 2.0d * Math.PI * (double)m_unFlNu * m_dCntDwnInvicibleTime / m_dInvicibleTime;
            //��
            //var mr = GetComponent<MeshRenderer>();
            //mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, (float)(Math.Sin(RadSp) * 255.0d));
        }

        if (Input.GetKey(m_KeyCode))
        {
            //Debug.Log("�΂�");
            Dash();
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
        if (InvincibleState)
        {
            return; 
        }

        //���_���[�W�v�Z
        HP -= dDamageVal;    //HP����

        InvincibleState = true;
    }

    public void Dash()
    {
        //������
        if (m_dCntDwnDshInterval > 0.0d)   //dshcd��Ԃ̎�
        {
            return;
        }

        //dash
        
        Vector2 m_vDirction = new((float)Math.Cos(Mathf.Deg2Rad * (-transform.eulerAngles.y + 90.0d/* + m_dFrontAngle*/)),
        (float)Math.Sin(Mathf.Deg2Rad * (-transform.eulerAngles.y + 90.0d/* + m_dFrontAngle*/)));   //���ʂ̃x�N�g��  ��y����]�̕����͍��W�n�Ƌt����

        Vector3 m_vDirctCent = new(m_vDirction.x, 0.0f, m_vDirction.y);  //��`�̒�������

        m_Rb.transform.position += m_vDirctCent.normalized * (float)m_DashDist;
        //m_Rb.velocity += m_vDirctCent.normalized * (float)m_DashDist; //�ړ������ύX
        //m_Rb.AddForce(m_vDirctCent.normalized * (float)m_DashDist, ForceMode.Impulse);



        //���J�E���g�_�E��
        m_dCntDwnDshInterval = m_unDashInterval;   //���Ԃ��J�E���g
        InvincibleState = true;

    }
}