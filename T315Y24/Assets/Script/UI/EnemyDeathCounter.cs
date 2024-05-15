/*=====
<EnemyDeathCounter.cs> 
���쐬�ҁFiwamuro

�����e
���񂾓G�̐���2dUI�\��������X�N���v�g

���X�V����
__Y24
_M05
D
09:�X�N���v�g�쐬:iwamuro
14:�v���O�����쐬:yamamoto
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//���N���X��`
public class EnemyDeathCounter : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] private TMPro.TMP_Text DeathCount_txt; //�\��������e�L�X�g(TMP)
   

    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    void Start()
    {
        DeathCount_txt.SetText("������:0�� ");     //������
    }

    /*���J�E���g�\���֐�
   �����F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F���������������Ƃ��\�����X�V���鏈��
   */
    public void DisplayEnemyDeathCounter()
    {
        DeathCount_txt.SetText("������:"+ CEnemy.m_nDeadEnemyCount.ToString() + "��");    // �������\��
    }
}
