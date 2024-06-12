/*=====
<SpawnRandomRect.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�����_������(�l�p�`)

���X�V����
__Y24
_M05
D
06:�v���O�����쐬:takagi
07:����:takagi
09:�ϐ����C��:takagi
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;  //Unity
using UseRandom;    //�����_��������

//���N���X��`
public class CSpawnRandomRect : CGetObjects
{
    //���ϐ��錾
    [SerializeField] private Rect m_SpawnRect;  //�����͈�  //TODO:�����ɒl�����Ȃ������玩���̈ʒu�E�T�C�Y����ɂ���悤��
    [SerializeField] private double m_dAltitude;    //����
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
        //�������ʒu�I��
        Vector3 _vSpawnPos = new Vector3(Random.Range(m_SpawnRect.x, m_SpawnRect.x + m_SpawnRect.width), (float)m_dAltitude, Random.Range(m_SpawnRect.y, m_SpawnRect.y + m_SpawnRect.height));  //�������W(x)
        //TODO:�l�p�`���ϑ��Ȍ`�ł��Ή��ł���悤��(�x�N�g��?)

        //������
        if (m_SpawnAssetRef != null && m_SpawnAssetRef.Count > 0)    //���X�g�����݁E��łȂ�
        {
            m_SpawnAssetRef[Random.Range(0, m_SpawnAssetRef.Count)].InstantiateAsync(_vSpawnPos, m_SpawnRotate); //�����_���Ώې���
        }
    }
}