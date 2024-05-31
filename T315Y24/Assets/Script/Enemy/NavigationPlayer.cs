/*=====
<NavigationPlayer.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�G���I�u�W�F�N�g�Ɉ��������炸�ɍŒZ�����Ńv���C���[��ǂ�������

�����ӎ���
Hierarchy�ɁuPlayer�v�̖��O�ȊO�Ńv���C���[��u�����Ƃ��ǂ������Ȃ��Ȃ�̂ŏ����������K�v�ł�

���X�V����
__Y24
_M05
D
22:�v���O�����쐬:suzumura
30:�R�����g�ǉ�:yamamoto
=====*/
//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//���N���X��`
public class NavigationPlayer : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] private GameObject player;
    NavMeshAgent agent;

    /*���������֐�
   �����P�F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�C���X�^���X�������ɍs������
   */
    void Start()
    {
        player = GameObject.Find("Player");//����
        agent = GetComponent<NavMeshAgent>();
    }

    /*���X�V�֐�
   �����F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F��莞�Ԃ��Ƃɍs���X�V����
   */
    void Update()
    {
        agent.destination = player.transform.position;
    }
}
