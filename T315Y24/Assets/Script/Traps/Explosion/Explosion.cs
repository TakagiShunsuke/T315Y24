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
    int m_Setnum;
    public static int[] m_KillCount = new int[2];
    /*���������֐�
  �����P�F�Ȃ�
  ��
  �ߒl�F�Ȃ�
  ��
  �T�v�F�C���X�^���X�������ɍs������
  */
    void Start()
    {
        Debug.Log("2");
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
                    Destroy.TakeDestroy();  //�G�폜
                    m_KillCount[m_Setnum]++;
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
            Destroy(gameObject);
        }
    }
    public void SetBombType(int n)
    {
        m_Setnum = n;
    }
}
