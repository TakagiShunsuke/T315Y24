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
using System;   //�V�X�e��(���Ԏ擾)

//�����O��Ԓ�`
namespace UseRandom
{
    //���N���X��`
    static class CUseRandom
    {
        /*���R���X�g���N�^
        �����P�F�Ȃ�
        ��
        �ߒl�F�Ȃ�
        ��
        �T�v�F�������ɍs������
        */
        static CUseRandom()
        {
            //�������V�[�h������
            UnityEngine.Random.InitState(DateTime.Now.Millisecond); //����(���`�����@)

#if UNITY_EDITOR    //�G�f�B�^�g�p��
            //���G���[�o��
            UnityEngine.Debug.Log("�����_��������");   //���O�o��
#endif
        }
    }
}