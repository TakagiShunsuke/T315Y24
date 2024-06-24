/*=====
<SwitchResult.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
���U���g�̂Q�摜��؂�ւ���

�����ӎ���


���X�V����
__Y24
_M06
D
12:�v���O�����쐬:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//���N���X��`
public class CSwitchResult : MonoBehaviour
{
    //���\���̒�`
    [Serializable]
    public struct KeyChangeImage
    {
        [Tooltip("�L�[")] public KeyCode[] m_KeyChangeResult; //�摜�ύX�̒��΃L�[
        [Tooltip("�摜")] public Image m_NextImag;    //�摜�̐؊���
    }

    //���ϐ��錾
    [Header("�摜�̐؂�ւ���")]
    [SerializeField, Tooltip("�Ή��摜")] private KeyChangeImage[] m_KeyChangeImages;    //�V�[���J�ڈꗗ


    /*���X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
    */
    private void Update()
    {
        //���ۑS
        if (m_KeyChangeImages == null)   //�k���`�F�b�N
        {
            //���I��
            return; //�����L�����Z��
        }

        //���摜�J��
        for (int _nIdx = 0; _nIdx < m_KeyChangeImages.Length; ++_nIdx)   //�J�ڐ��╪����
        {
            for (int _nIdx2 = 0; _nIdx2 < m_KeyChangeImages[_nIdx].m_KeyChangeResult.Length; ++_nIdx2)    //��t�L�[�����肷��
            {
                if (Input.GetKeyDown(m_KeyChangeImages[_nIdx].m_KeyChangeResult[_nIdx2])) //�L�[���͔���
                {
                    for(int _nIdx3 = 0; _nIdx3 < m_KeyChangeImages.Length; ++_nIdx3)   //�����ȊO���������邽�߂Ƀ��[�v���Ȃ���
                    {
                        if(_nIdx3 == _nIdx)  //�Ώۂ����g
                        {
                            m_KeyChangeImages[_nIdx3].m_NextImag.enabled = true; //����
                            continue;   //�����͏������Ȃ�
                        }

                        m_KeyChangeImages[_nIdx3].m_NextImag.enabled = false;    //�s����
                    }
                }
            }
        }
    }
}