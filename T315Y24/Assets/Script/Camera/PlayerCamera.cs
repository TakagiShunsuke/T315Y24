/*=====
<PlayerCamera.cs> 
���쐬�ҁFyamamoto

�����e
�v���C���[��Ǐ]����J�����̃X�N���v�g

���X�V����
__Y24   
_M05    
D
11:�X�N���v�g�쐬:yamamoto
=====*/


//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���N���X��`
public class camera : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] private Vector3 pos;   //�J�����̈ʒu
    GameObject Player;                      //�v���C���[�I�u�W�F�N�g�p

    /*���������֐�
   �����P�F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�C���X�^���X�������ɍs������
   */
    void Start()
    {
        Player = GameObject.Find("Player");
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
        var playerPosition = Player.gameObject.transform.position;  //�v���C���[�̍��W�擾

        transform.position = playerPosition + pos;                  //�J�����̍��W���v�Z
    }
}