/*=====
<PlayerCamera.cs> 
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
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���N���X��`
public class camera : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] private Vector3 m_Pos;             //�J�����̈ʒu
    [SerializeField] private string m_PlayerName;       //�Ǐ]����v���C���[�I�u�W�F�N�g�̖��O
    private GameObject m_Player;                        //�v���C���[�I�u�W�F�N�g�p
    
    /*���������֐�
   �����P�F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�C���X�^���X�������ɍs������
   */
    void Start()
    {
        m_Player = GameObject.Find(m_PlayerName);
        Debug.Log(m_Player);
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
        var PlayerPosition = m_Player.gameObject.transform.position;  //�v���C���[�̍��W�擾

        transform.position = PlayerPosition + m_Pos;                  //�J�����̍��W���v�Z
    }
}