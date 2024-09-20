/*=====
<ResultLight.cs>
���쐬�ҁFtakagi

�����e
���U���g�p���C�g��]

//�����ӎ���
���u���ł���

���X�V����
__Y24
_M06
D
18:�v���O�����쐬:takagi
21:���t�@�N�^�����O:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//���N���X��`
public class CResultLight : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField, Tooltip("������]")] private Vector3 m_vInitShiftRotate;
    [SerializeField, Tooltip("��]��")] private Vector3 m_vRotate;


    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    // Start is called before the first frame update
    private void Start()
    {
        transform.Rotate(m_vInitShiftRotate);
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
        transform.Rotate(m_vRotate);
    }
}