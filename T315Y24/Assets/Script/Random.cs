/*=====
<Random.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�����_�}�C�Y

���X�V����
__Y24
_M05
D
06:�v���O�����쐬:takagi

_M06
D
21:���t�@�N�^�����O:takagi
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System;

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