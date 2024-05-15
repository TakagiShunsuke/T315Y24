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
public class PlayerScript : MonoBehaviour, IDamageable
{
    //���ϐ��錾
    Rigidbody m_Rb;      // Rigidbody��ǉ�
    [SerializeField] private float m_fSpeed; //�v���C���[�̈ړ����x��ݒ�
    [SerializeField] private double m_dHp;   // HP

    //���v���p�e�B��`
    public double HP
    {
        get { return m_dHp; }
        private set { m_dHp = value; }
    }

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
        //���_���[�W�v�Z
        HP -= dDamageVal;    //HP����
    }
}

