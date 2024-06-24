/*=====
<SpawnRandom.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�����_������

�����ӎ���
�ʒu�Œ�(�ꂩ��)�ł��B���͈̔͂��g�������ꍇ�͕ʃR���|�[�l���g�ŁB

���X�V����
__Y24
_M05
D
06:�v���O�����쐬:takagi
07:����:takagi

_M06
D
21:���t�@�N�^�����O:takagi
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UseRandom;

//���N���X��`
public class CSpawnRandom : CGetObjects
{
    //���ϐ��錾
    [SerializeField] private Vector3 m_SpawnPos;  //�����ʒu
    [SerializeField] private Quaternion m_SpawnRotate;  //�����ʒu


    /*�������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�֐���
    */
    public void Create()
    {
        //������
        if(m_SpawnAssetRef != null && m_SpawnAssetRef.Count > 0)    //���X�g�����݁E��łȂ�
        {
            m_SpawnAssetRef[Random.Range(0, m_SpawnAssetRef.Count)].InstantiateAsync(m_SpawnPos, m_SpawnRotate); //�����_���Ώې���
        }
    }
}