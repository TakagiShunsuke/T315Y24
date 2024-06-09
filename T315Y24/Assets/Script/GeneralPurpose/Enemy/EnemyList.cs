/*=====
<EnemyList.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�G�𗅗񂵂ď��Ǘ�

�����ӎ���
�V���O���g��


���X�V����
__Y24
_M06
D
05:�v���O�����쐬:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;  //Unity
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

//���N���X��`
public class CEnemyList : CMonoSingleton<CEnemyList>
{
    [Serializable] public struct SpawnEnemyInfo
    {
        public AssetReferenceGameObject m_SpawnAssetRef;   //�����ΏۃA�Z�b�g
        public int m_SpawnAmount;    //������
    }   //�G�����p���

    //���v���p�e�B��`
    public List<SpawnEnemyInfo> SpawnInfo { get; set; } = null; //�����ΏۊǗ�
    public AssetReferenceGameObject GetRandomSpawnAssetRef
    {
        get
        {
            int _nTotal = 0;

            if(SpawnInfo == null || SpawnInfo.Count == 0) 
            { 
                return null; }

            for (int nIdx = 0; nIdx < SpawnInfo.Count; nIdx++)  //������₷�ׂ�
            {
                _nTotal += SpawnInfo[nIdx].m_SpawnAmount;
            }
            var _nRand = UnityEngine.Random.Range(1, _nTotal);
            //���̎��_��nRand > 0�ł���

            for (int nIdx = 0; nIdx < SpawnInfo.Count; nIdx++)  //������₷�ׂ�
            {
                if(_nRand <= SpawnInfo[nIdx].m_SpawnAmount)
                {
                    return SpawnInfo[nIdx].m_SpawnAssetRef;
                }
                else
                {
                    _nTotal -= SpawnInfo[nIdx].m_SpawnAmount;
                }
            }
            
            return null;    //_Total == 0



            //���݂̏�񂩂烉���_���ɐ����G������
        }
    }

    /*���I���֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�j�����ɍs������
    */
    override protected void CustomOnDestroy()
    {
        //�����
        if(SpawnInfo != null)
        {
            for (int nIdx = 0; nIdx < SpawnInfo.Count; nIdx++)  //���������ׂĔj������
            {
                if (SpawnInfo[nIdx].m_SpawnAssetRef != null && SpawnInfo[nIdx].m_SpawnAssetRef.Asset != null)    //LoadAssetAsync()�֐����g�p����
                {
                    SpawnInfo[nIdx].m_SpawnAssetRef.ReleaseAsset(); //�Q�Ƃ���߂�
                }
            }
        }
    }
}