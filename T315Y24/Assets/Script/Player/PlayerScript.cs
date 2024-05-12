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
13:�v���C���[�̈ړ��Ɗp�x�̏C���Aunity��ŃX�s�[�h��ύX�ł���悤�ɕύX
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
    Rigidbody rb;      // Rigidbody��ǉ�
    Rigidbody rb;
    float fspeed = 3.0f;    //�v���C���[�̈ړ����x
    [SerializeField] private double m_dHP = 10.0d;   //�̗�
    [SerializeField] private float fspeed; //�v���C���[�̈ړ����x��ݒ�

    /*�����������֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FRigidbody�R���|�[�l���g��ǉ�
    */
    void Start()    //�����Œǉ������
    {
        rb = GetComponent<Rigidbody>(); //Rigidbody�R���|�[�l���g��ǉ�
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
            transform.Translate(Vector3.forward * Time.deltaTime * fspeed);
        }
    
        // �΂߈ړ�
        if (moveDirection != Vector3.zero)
        {
    
            // ���K�����Ĉړ����x�����ɕۂ�
            moveDirection.Normalize();
            rb.velocity = moveDirection * fspeed;

        }
        else
        {
            // �����L�[��������Ă��Ȃ��ꍇ�͒�~����
            rb.velocity = Vector3.zero;
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
        m_dHP -= dDamageVal;    //HP����
    }
}

