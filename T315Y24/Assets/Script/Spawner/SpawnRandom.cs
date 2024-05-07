/*=====
<SpawnRandom.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�����_������

���X�V����
__Y24
_M05
D
06:�v���O�����쐬:takagi
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;  //Unity
using UseRandom;    //�����_��������

//���N���X��`
public class CSpawnRandom : CGetObjects
{
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
            m_SpawnAssetRef[Random.Range(0, m_SpawnAssetRef.Count)].InstantiateAsync(); //�����_���Ώې���
        }
    }
}