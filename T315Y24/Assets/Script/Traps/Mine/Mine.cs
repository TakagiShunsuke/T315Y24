/*=====
<Mine.cs> 
���쐬�ҁFyamamoto

�����e
�n���ɕt����X�N���v�g�B
���j�G�t�F�N�g�̐����͂�����

�����ӎ���  
�n����IsTrigger�����Ă���Ɠ��삵�܂���B
Prefab��ݒ肵�Ă��Ȃ��Ɣ����G�t�F�N�g����������Ȃ��B

���X�V����
__Y24   
_M05    
D
08 :�v���O�����쐬:yamamoto 
09 :�d�l�ύX�̈׏�����ύX:yamamoto
10:�R�����g�ǉ�:yamamoto
12:���L���X�g���Ԓǉ�:yamamoto

_M06
D
08�F�e�N���X�ǉ�����ɔ����v���O������������:yamamoto
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;

//���N���X��`
public class Mine : CTrap
{
    //���ϐ��錾
    [SerializeField] private GameObject m_ExplosionEffectPrefab; // ���������������v���n�u

    /*���n�������蔻��֐�
    �����P�F�����蔻�肪�������I�u�W�F�N�g�̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�G���n���ɐG�ꂽ�Ƃ��̂ݏ��������
    */
    private void OnCollisionStay(Collision collision)     //�n���ɉ������������Ă����Ƃ�
    {
        if (Check(collision))  // �N���ł��邩
        {
            Instantiate(m_ExplosionEffectPrefab, transform.position, Quaternion.identity);
        }

        SetCheck(collision);
    }

}
