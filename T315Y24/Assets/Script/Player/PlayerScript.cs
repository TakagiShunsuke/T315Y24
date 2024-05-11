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
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���N���X��`
public class PlayerScript : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    Rigidbody rb;
    float fspeed = 3.0f;    //�v���C���[�̈ړ����x
    [SerializeField] private double m_dHP = 10.0d;   //�̗�


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
        if (Input.GetKey(KeyCode.UpArrow))  //��Arrow�L�[�Ńv���C���[����Ɉړ�������
        {
            rb.velocity = transform.forward * fspeed;
        }
        if (Input.GetKey(KeyCode.DownArrow)) //��Arrow�L�[�Ńv���C���[�����Ɉړ�������
        {
            rb.velocity = -transform.forward * fspeed;
        }
        if (Input.GetKey(KeyCode.RightArrow)) //�EArrow�L�[�Ńv���C���[���E�Ɉړ�������
        {
            rb.velocity = transform.right * fspeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) //��Arrow�L�[�Ńv���C���[�����Ɉړ�������
        {
            rb.velocity = -transform.right * fspeed;
        }
    }

    /*���ړ������֐�
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
