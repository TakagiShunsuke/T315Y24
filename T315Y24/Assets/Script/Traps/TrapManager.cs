/*=====
<TrapManager.cs> //�X�N���v�g��
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
21:���t�@�N�^�����O:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

//���N���X��`
public class CTrapManager : CMonoSingleton<CTrapManager>
{
    //���萔��`
    private const string OBJECT_NAME = "TrapManager"; //���̃I�u�W�F�N�g���������ꂽ�Ƃ��̖��O

    //���ϐ��錾
    //[Header("�S�Ă��")]
    //[SerializeField, Tooltip("�")] private List<GameObject> AllTrap = null; //�S�Ă�㩊Ǘ�
    private List<GameObject> AllTrap = new List<GameObject>(); //�S�Ă�㩊Ǘ�

    //���v���p�e�B��`
    public List<GameObject> HaveTraps { get; private set; } = new List<GameObject>(); //�����


    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    protected override void CustomAwake()
    {
        //�����l�[��
        gameObject.name = OBJECT_NAME;  //���g�̃I�u�W�F�N�g���ύX

        //���Ǘ��Ώۓo�^
        for (int _nCnt = 0; _nCnt < transform.childCount; _nCnt++)
        {
            var _Obj = transform.GetChild(_nCnt).gameObject;
            _Obj.SetActive(false);
            AllTrap.Add(_Obj);
        }
    }


    protected override void Start()
    {
        //��㩕Ґ���֏���
        int _nIdx = 0;
        while (HaveTraps.Count < CTrapSelect.Instance.HavableTrapNum && _nIdx < AllTrap.Count)  //�������܂�
        {
            HaveTraps.Add(AllTrap[_nIdx]);
            _nIdx++;
        }
    }

    /*���X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
    */
    protected override void Update()
    {
        //TODO:㩕Ґ�����
    }
}