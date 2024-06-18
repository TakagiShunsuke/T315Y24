/*=====
<EnemyList.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�G�𗅗񂵂ď��Ǘ�

�����ӎ���
�V���O���g���ł���
GetRandomSpawnAssetRef�͐�������񂪎擾�ł��Ȃ��������k����Ԃ��̂Œ���


���X�V����
__Y24
_M06
D
05:�v���O�����쐬:takagi
09:�R�[�h���P:takagi
13:�m���C��:takagi
18:������d�̍ŏ��l�ݒ�:takagi
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
    //���\���̒�`
    [Serializable] public struct SpawnEnemyInfo
    {
        public AssetReferenceGameObject m_SpawnAssetRef;   //�����ΏۃA�Z�b�g
        [Min(0)] public int m_SpawnAmount;    //������d
    }   //�G�����p���

    //���萔��`
    const string OBJECT_NAME = "EnemyList"; //���̃I�u�W�F�N�g���������ꂽ�Ƃ��̖��O

    //���v���p�e�B��`
    public List<SpawnEnemyInfo> SpawnInfo { get; set; } = null; //�����ΏۊǗ�
    public AssetReferenceGameObject GetRandomSpawnAssetRef
    {
        get
        {
            //���ϐ��錾
            int _nTotal = 0;    //��񑀍�p�̈ꎞ�ϐ�

            //���k���`�F�b�N
            if (SpawnInfo == null)  //�����������������鑊�肪���Ȃ�
            { 
                return null;    //�������f
            }

            //���d�ݕt��
            for (int _nIdx = 0; _nIdx < SpawnInfo.Count; _nIdx++)  //������₷�ׂ�
            {
                //�������ő�l����
                _nTotal += SpawnInfo[_nIdx].m_SpawnAmount;   //�d�݂̑��a���Ƃ�
            }

            //�������_���ɐ����G������
            var _nRand = UnityEngine.Random.Range(1, _nTotal + 1);  //�d�݂��܂߂ĎZ�o�B���̎��_��nRand > 0�ł���B�܂�Range��Max�l�͊܂܂�Ȃ�����+1�B

            //���d�݂̒�`��w����Ώۂ�{��
            for (int nIdx = 0; nIdx < SpawnInfo.Count; nIdx++)  //������₷�ׂ�
            {
                if(_nRand <= SpawnInfo[nIdx].m_SpawnAmount) //���̒�`����ɗ��������܂�
                {
                    //���d�݂̑w���琶���Ώۂ�I�o�E�񋟂���
                    return SpawnInfo[nIdx].m_SpawnAssetRef;     //�����Ώۊm��
                }
                else
                {
                    _nRand -= SpawnInfo[nIdx].m_SpawnAmount;   //�Y������i�߂�ɂ�����A�d�݂̒�`��w��ύX
                }
            }
            
            //�����s���Ή�
            return null;    //_Total == 0�܂��̓��X�g����ł���A���s�����B
        }
    }   //�����_���ɐ��肳��鐶���Ώۂ̃Q�b�^


    /*���������֐�
     �����P�F�Ȃ�
     ��
     �ߒl�F�Ȃ�
     ��
     �T�v�F�C���X�^���X�������ɍs������
     */
    override protected void CustomAwake()
    {
        //�����l�[��
        gameObject.name = OBJECT_NAME;  //���g�̃I�u�W�F�N�g���ύX
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
        if(SpawnInfo != null)   //�Ώۂ����݂���
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