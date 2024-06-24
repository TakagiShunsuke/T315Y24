/*=====
<TrackCamera.cs> 
���쐬�ҁFisi

�����e
�v���C���[��Ǐ]����J�����̃X�N���v�g

���X�V����
__Y24   
_M05    
D
10:�X�N���v�g�쐬:isi
11:���^�쐬:takagi
12:�ϐ����ύX:yamamoto

_M06
D
21:���t�@�N�^�����O:takagi
24:���t�@�N�^�����O:takagi
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���N���X��`
public class CTrackCamera : MonoBehaviour
{
    //���ϐ��錾
    [Header("�ǐՏ��")]
    [SerializeField, Tooltip("�ǐՑΏ�")] private GameObject m_Target;  //�Ǐ]����I�u�W�F�N�g
    [SerializeField, Tooltip("���Έʒu")] private Vector3 m_RelativePosition; //�J�����̈ʒu


    /*���X�V�֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�X�V����
    */
    private void Update()
    {
        //���ϐ��錾
        var _PlayerPosition = m_Target.gameObject.transform.position;  //�v���C���[�̍��W�擾

        //���X�V
        transform.position = _PlayerPosition + m_RelativePosition;     //�J�����̍��W���v�Z
    }
}