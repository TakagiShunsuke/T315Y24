/*=====
<UI3dCamera.cs> 
���쐬�ҁFyamamoto

�����e
�v���C���[��Ǐ]����J�����̃X�N���v�g

���X�V����
__Y24   
_M05    
D
11:�X�N���v�g�쐬:yamamoto
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���N���X��`
public class UI3dCamera : MonoBehaviour
{
    /*���X�V�֐�
  �����P�F�Ȃ�
  ��
  �ߒl�F�Ȃ�
  ��
  �T�v�F�X�V����
  */
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;    //�J�����̊p�x���X�V
    }
}
