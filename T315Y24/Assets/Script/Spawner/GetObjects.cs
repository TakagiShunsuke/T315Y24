/*=====
<GetObjects.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�I�u�W�F�N�g�擾

���X�V����
__Y24
_M05
D
06:�v���O�����쐬:takagi
07:���[�h���@�ύX:takagi
10:�x�����o�錴��������:takagi

_M06
D
21:���t�@�N�^�����O:takagi
24:���t�@�N�^�����O:takagi
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.AddressableAssets;

//���N���X��`
public abstract class CGetObjects : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] protected List<AssetReferenceGameObject> m_SpawnAssetRef; //�����ΏۃA�Z�b�g�Ǘ�


    /*���I���֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�j�����ɍs������
    */
    virtual protected void OnDestroy()
    {
        //�����
        for (int _nIdx = 0; _nIdx < m_SpawnAssetRef.Count; _nIdx++)  //���������ׂĔj������
        {
            if (m_SpawnAssetRef[_nIdx].Asset != null)    //LoadAssetAsync()�֐����g�p����
            {
                m_SpawnAssetRef[_nIdx].ReleaseAsset(); //�Q�Ƃ���߂�
            }
        }
    }
}