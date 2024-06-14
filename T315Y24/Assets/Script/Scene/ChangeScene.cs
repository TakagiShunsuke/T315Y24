/*=====
<ChangeScene.cs> //�X�N���v�g��
���쐬�ҁFnieda

�����e
�{�^�������������ɃV�[����؂�ւ���

�����ӎ���
1�N���ɍ�������̂����S���p����

���X�V����
__Y24   //'24�N
_M05    //05��
D       //��
10:�v���O�����쐬:nieda
14:�r���h�o�O�̌�������:takagi
17:�L�[���͂ŃV�[���J�ڎ���:nieda
_M06    //06��
D       //��
13:�V�[���J�ڃ{�^������AP�Ńv���g�X�e�[�W�AO�ŃX�e�[�W1�ɑJ��:nieda
13:���莞SE�ǉ�:nieda
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

//���N���X��`
public class CChangeScene : MonoBehaviour
{
    AudioSource m_audioSource;  // AudioSource��ǉ�
    [SerializeField] public AudioClip SE_Decide;  // �_�b�V������SE

    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    /*���X�V�֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
    */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().name == "TitleScene")
            {
                // �^�C�g�����X�e�[�W�Z���N�g
                m_audioSource.Play();
                SceneManager.LoadScene("SelectScene");
            }
            else if (SceneManager.GetActiveScene().name == "SelectScene")
            {
                // �X�e�[�W�Z���N�g���X�e�[�W1
                m_audioSource.Play();
                SceneManager.LoadScene("Stage01");
            }
            else if (SceneManager.GetActiveScene().name == "ResultScene")
            {
                // ���U���g�A�Q�[���I�[�o�[���^�C�g��
                m_audioSource.Play();
                SceneManager.LoadScene("TitleScene");
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            // �v���g�X�e�[�W�֑J��
            SceneManager.LoadScene("ProtoStage");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            // �X�e�[�W1�֑J��
            SceneManager.LoadScene("Stage01");
        }
    }

    /*���Q�[���I���֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�Q�[�����I�����鏈��
    */
    public void GameEnd()
    {
#if UNITY_EDITOR    //Editor�ォ��̎��s��

        //�Đ����[�h����������
        UnityEditor.EditorApplication.isPlaying = false;

#else               //���s�t�@�C������̎��s��

        //TODO:��֏���
        //Application.Quit();

#endif
    }
}