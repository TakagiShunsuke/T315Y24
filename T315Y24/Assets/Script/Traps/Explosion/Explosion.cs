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
9 : �v���O�����쐬: yamamoto
10: �R�����g�ǉ�: yamamoto

_M06    
D
26: �R�����g�ǉ�: yamamoto
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
    [Header("�X�e�[�^�X")]
    [SerializeField,Tooltip("�����̌����ڂ����������Ă������x")] private double m_LowerSpeed = 0.1d;   //�I�u�W�F�N�g�����ɏ����Ă������x
    public static int[] m_KillCount = new int[2];   //㩂��Ƃɓ|��������ۑ�����z��
    int m_Setnum;   //�i�[�掯�ʔԍ�


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
                {
                    Destroy.TakeDestroy();      //�G�폜
                    m_KillCount[m_Setnum]++;    //�|�������𑝂₷
                }
            }
        }
    }

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
            Destroy(gameObject);    //�j��
        }
    }

    /*���i�[�掯�ʔԍ��Z�b�g�֐�
    �����P�Fint _nNum�F�i�[��̔ԍ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�������̕ۑ��̎w��ꏊ���Z�b�g����
    */
    public void SetBombType(int _nNum)
    {
        m_Setnum = _nNum;   //�i�[����Z�b�g
    }
}
