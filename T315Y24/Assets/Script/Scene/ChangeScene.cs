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

_M06
D
13:�L�[���͂̎�t�𐧌�+�ĉ�:takagi
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
        public String Nextscene;    //�V�[���̐؊���
    }

    //���ϐ��錾
    [SerializeField] private KeyChangeScene[] m_KeyChangeScenes;    //�V�[���J�ڈꗗ


    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    // Start is called before the first frame update
    void Start()
    {

    }

    /*���X�V�֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
    */
    // Update is called once per frame
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
                    SceneManager.LoadScene(m_KeyChangeScenes[nIdx].Nextscene);  //���̃X�e�[�W��
                }
            }
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