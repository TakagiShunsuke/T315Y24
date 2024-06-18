/*=====
<ChangeScene.cs> //�X�N���v�g��
���쐬�ҁFnieda

�����e
�{�^�������������ɃV�[����؂�ւ���

�����ӎ���
1�N���ɍ�������̂����S���p����

���X�V����
__Y24
_M05
D
10:�v���O�����쐬:nieda
14:�r���h�o�O�̌�������:takagi
17:�L�[���͂ŃV�[���J�ڎ���:nieda

_M06
D
13:�V�[���J�ڃ{�^������AP�Ńv���g�X�e�[�W�AO�ŃX�e�[�W1�ɑJ��:nieda
13:�L�[���͂̎�t�𐧌�+�ĉ�:takagi
13:���莞SE�ǉ�:nieda
18:SE��炷�悤�ɏC��:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

//���N���X��`
public class CChangeScene : MonoBehaviour
{
    //���\���̒�`
    [Serializable] public struct KeyChangeScene
    {
        public KeyCode[] TransitionKey; //�V�[���J�ڂ̒��΃L�[
        public string[] TransitionButton;   //�V�[���J�ڂ̒��΃{�^��
        public String Nextscene;    //�V�[���̐؊���
    }

    //���ϐ��錾
    [SerializeField] private KeyChangeScene[] m_KeyChangeScenes;    //�V�[���J�ڈꗗ
    [SerializeField] public AudioClip SE_Decide;  // ���莞��SE
    AudioSource m_audioSource;  // AudioSource��ǉ�

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
        //���ۑS
        if(m_KeyChangeScenes == null)   //�k���`�F�b�N
        {
            //���I��
            return; //�����L�����Z��
        }

        //���V�[���J��
        for(int nIdx = 0; nIdx < m_KeyChangeScenes.Length; ++nIdx)   //�J�ڐ��╪����
        {
            for(int nIdx2 = 0; nIdx2 < m_KeyChangeScenes[nIdx].TransitionKey.Length; ++nIdx2)    //��t�L�[�����肷��
            {
                if (Input.GetKeyDown(m_KeyChangeScenes[nIdx].TransitionKey[nIdx2])) //�L�[���͔���
                {
                    StartCoroutine( Change(nIdx));
                //    m_audioSource.PlayOneShot(SE_Decide);
                //    while(m_audioSource.isPlaying) {}   //�񓯊������FSE��炵�؂�܂őҋ@
                //    SceneManager.LoadScene(m_KeyChangeScenes[nIdx].Nextscene);  //���̃X�e�[�W��
                }
            }
            //�R���g���[���[�p
            for (int nIdx2 = 0; nIdx2 < m_KeyChangeScenes[nIdx].TransitionButton.Length; ++nIdx2)    //��t�L�[�����肷��
            {
                if (Input.GetButtonDown(m_KeyChangeScenes[nIdx].TransitionButton[nIdx2])) //�L�[���͔���
                {
                    StartCoroutine(Change(nIdx));
                    //    m_audioSource.PlayOneShot(SE_Decide);
                    //    while(m_audioSource.isPlaying) {}   //�񓯊������FSE��炵�؂�܂őҋ@
                    //    SceneManager.LoadScene(m_KeyChangeScenes[nIdx].Nextscene);  //���̃X�e�[�W��
                }
            }
        }
    }

    //�񓯊���SE�Đ��I����҂֐�
    IEnumerator Change(int nIdx)
    {
        m_audioSource.PlayOneShot(SE_Decide);
        while (m_audioSource.isPlaying) { yield return null; }   //�񓯊������FSE��炵�؂�܂őҋ@
        SceneManager.LoadScene(m_KeyChangeScenes[nIdx].Nextscene);  //���̃X�e�[�W��
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