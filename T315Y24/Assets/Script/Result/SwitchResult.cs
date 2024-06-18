/*=====
<SwitchResult.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�V�[���g�����W�V����

�����ӎ���
�K���ō���Ă邩��A�����Ɨǂ����o���~����������v����

���X�V����
__Y24
_M06
D
12:�v���O�����쐬:tei
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
        public KeyCode[] m_KeyChangeResult; //�摜�ύX�̒��΃L�[
        public Image m_NextImag;    //�摜�̐؊���
    }

    //���ϐ��錾
    [SerializeField] private KeyChangeImage[] m_KeyChangeImages;    //�V�[���J�ڈꗗ

    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    private void Start()
    {
    }

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
        for (int nIdx = 0; nIdx < m_KeyChangeImages.Length; ++nIdx)   //�J�ڐ��╪����
        {
            for (int nIdx2 = 0; nIdx2 < m_KeyChangeImages[nIdx].m_KeyChangeResult.Length; ++nIdx2)    //��t�L�[�����肷��
            {
                if (Input.GetKeyDown(m_KeyChangeImages[nIdx].m_KeyChangeResult[nIdx2])) //�L�[���͔���
                {
                    for(int nIdx3 = 0; nIdx3 < m_KeyChangeImages.Length; ++nIdx3)   //�����ȊO���������邽�߂Ƀ��[�v���Ȃ���
                    {
                        if(nIdx3 == nIdx)  //�Ώۂ����g
                        {
                            m_KeyChangeImages[nIdx3].m_NextImag.enabled = true; //����
                            continue;   //�����͏������Ȃ�
                        }

                        m_KeyChangeImages[nIdx3].m_NextImag.enabled = false;    //�s����
                    }
                }
            }
        }
    }
}
