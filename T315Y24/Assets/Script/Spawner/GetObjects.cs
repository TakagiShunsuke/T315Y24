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
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.AddressableAssets;  //Unity

//���N���X��`
public abstract class CGetObjects : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] protected List<AssetReferenceGameObject> m_SpawnAssetRef; //�����ΏۃA�Z�b�g�Ǘ�

    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    virtual public void Start()
    {
    }

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
        for (int nIdx = 0; nIdx < m_SpawnAssetRef.Count; nIdx++)  //���������ׂĔj������
        {
            m_SpawnAssetRef[nIdx].ReleaseAsset(); //�Q�Ƃ���߂�
        }
    }
}