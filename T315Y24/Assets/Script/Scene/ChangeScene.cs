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

    }

    /*�����[�h�V�[���֐�
    �����P�Fstring str�F�V�[����
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�V�[�������[�h����
    */
    public void LoadScene(string str)
    {
        //�V�[����ǂݍ���
        SceneManager.LoadScene(str);
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

        Application.Quit();

#endif
    }
}