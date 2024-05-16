/*=====
<Explosion.cs> 
���쐬�ҁFyamamoto

�����e
�n���G�t�F�N�g�p�ɕt����X�N���v�g

�����ӎ���  
�n���G�t�F�N�g�p��IsTrigger��t���Ȃ��Ɠ��삵�܂���B

���X�V����
__Y24   
_M05    
D
9 :�v���O�����쐬:yamamoto
10:�R�����g�ǉ�:yamamoto
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

//���N���X��`
public class Explosion : MonoBehaviour
{
    //���ϐ��錾
    private float m_ObjectRadius;      //�I�u�W�F�N�g�̔��a
    private Vector3 m_InitialObjectPos; // �I�u�W�F�N�g�̏����ʒu
    [SerializeField] private double m_LowerSpeed = 0.1d;   //�I�u�W�F�N�g�����ɏ����Ă������x

    /*���������֐�
  �����P�F�Ȃ�
  ��
  �ߒl�F�Ȃ�
  ��
  �T�v�F�C���X�^���X�������ɍs������
  */
    void Start()
    {
        m_ObjectRadius = transform.localScale.x / 2.0f;   // �I�u�W�F�N�g�̔��a���擾
        m_InitialObjectPos = transform.position;          // �����ʒu��ݒ�

        // �͈͓��̓G�����o
        Collider[] Colliders = Physics.OverlapSphere(transform.position, m_ObjectRadius);
        foreach (Collider Collider in Colliders)    //Collider[]�̒��ɓ����Ă��邾�����[�v����
        {
            if (Collider.CompareTag("Enemy"))   //�����蔻��̒��ɂ�����̂ɓG�^�O�����Ă邩�m�F
            {
                //IFeatureMine�����邩�m�F
                if (Collider.gameObject.TryGetComponent<IFeatureMine>(out var Destroy))
                    Destroy.TakeDestroy();  //�G�폜
            }
        }
    }

    //�d�l�ύX�����邩��������ꉞ�c��

    /*���Gto���j�����蔻��֐�
  �����P�F�����蔻�肪�������I�u�W�F�N�g�̏��
  ��
  �ߒl�F�Ȃ�
  ��
  �T�v�F���������Ƃ��G���폜����֐�
  */
    /*private void OnTriggerStay(Collider other)
    {
        // �͈͓��̓G�����o
        Collider[] colliders = Physics.OverlapSphere(transform.position, ObjectRadius);
        foreach (Collider collider in colliders)    //Collider[]�̒��ɓ����Ă��邾�����[�v����
        {
            if (collider.CompareTag("Enemy"))   //�����蔻��̒��ɂ�����̂ɓG�^�O�����Ă邩�m�F
            {
                //IFeatureMine�����邩�m�F
                if (collider.gameObject.TryGetComponent<IFeatureMine>(out var destroy))
                    destroy.TakeDestroy();  //�G�폜
            }
        }
    }*/

    /*���X�V�֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�X�V����
    */
    void Update()
    {
        // �I�u�W�F�N�g�����Ɉړ�������
        transform.position -= new Vector3(0.0f, (float)m_LowerSpeed * Time.deltaTime, 0.0f);

        // ���a���������Ɉړ��������ǂ����𔻒f���A�j�󂷂�
        if (transform.position.y <= m_InitialObjectPos.y - m_ObjectRadius)
        {
            Destroy(gameObject);
        }
    }
}
