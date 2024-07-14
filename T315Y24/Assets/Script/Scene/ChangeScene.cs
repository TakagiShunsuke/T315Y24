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
17:SE�ǉ�:nieda
18:SE��炷�悤�ɏC��:takagi
21:���t�@�N�^�����O:takagi
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
        [Tooltip("�L�[")] public KeyCode[] TransitionKey; //�V�[���J�ڂ̒��΃L�[
        [Tooltip("�{�^��")] public string[] TransitionButton;   //�V�[���J�ڂ̒��΃{�^��
        [Tooltip("�J�ڐ�V�[��")] public String Nextscene;    //�V�[���̐؊���
    }

    //���ϐ��錾
    [Header("�V�[���̐؂�ւ���")]
    [SerializeField, Tooltip("�Ή��V�[��")] private KeyChangeScene[] m_KeyChangeScenes;    //�V�[���J�ڈꗗ
    [Header("��")]
    [Tooltip("AudioSource��ǉ�")] private AudioSource m_AudioSource;     // AudioSource��ǉ�
    [SerializeField, Tooltip("���莞��SE")] private AudioClip SE_Decide;  // ���莞��SE

    [SerializeField] private Material SceneFadeMaterial;  // �}�e���A��
    [SerializeField] private float fadeTime = 2.0f;       // �t�F�[�h����
    //[SerializeField] private string _propertyName = "_Progress";
    public InkTransition inkTransition;
    //���p�u���b�N�C�x���g
    //public UnityEvent OnFadeDone;

    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    /*���X�V�֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
    */
    private void Update()
    {
        //���ۑS
        if(m_KeyChangeScenes == null)   //�k���`�F�b�N
        {
            //���I��
            return; //�����L�����Z��
        }

        //���V�[���J��
        for(int _nIdx = 0; _nIdx < m_KeyChangeScenes.Length; ++_nIdx)   //�J�ڐ��╪����
        {
            for(int nIdx2 = 0; nIdx2 < m_KeyChangeScenes[_nIdx].TransitionKey.Length; ++nIdx2)    //��t�L�[�����肷��
            {
                if (Input.GetKeyDown(m_KeyChangeScenes[_nIdx].TransitionKey[nIdx2])) //�L�[���͔���
                {
                    StartCoroutine(Change(_nIdx));
                //    m_audioSource.PlayOneShot(SE_Decide);
                //    while(m_audioSource.isPlaying) {}   //�񓯊������FSE��炵�؂�܂őҋ@
                //    SceneManager.LoadScene(m_KeyChangeScenes[nIdx].Nextscene);  //���̃X�e�[�W��
                }
            }
            //�R���g���[���[�p
            for (int _nIdx2 = 0; _nIdx2 < m_KeyChangeScenes[_nIdx].TransitionButton.Length; ++_nIdx2)    //��t�L�[�����肷��
            {
                if (Input.GetButtonDown(m_KeyChangeScenes[_nIdx].TransitionButton[_nIdx2])) //�L�[���͔���
                {
                    StartCoroutine(Change(_nIdx));
                    //    m_audioSource.PlayOneShot(SE_Decide);
                    //    while(m_audioSource.isPlaying) {}   //�񓯊������FSE��炵�؂�܂őҋ@
                    //    SceneManager.LoadScene(m_KeyChangeScenes[nIdx].Nextscene);  //���̃X�e�[�W��
                }
            }
        }
    }

    //�񓯊���SE�Đ��I����҂֐�
    private IEnumerator Change(int _nIdx)
    {
        float currentTime = 0.0f;   // ������

        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            //SceneFadeMaterial.SetFloat(_propertyName, Mathf.Clamp01(currentTime / fadeTime));
            inkTransition.StartTransition();
            yield return null;
        }
        m_AudioSource.PlayOneShot(SE_Decide);
        while (m_AudioSource.isPlaying) { yield return null; }   //�񓯊������FSE��炵�؂�܂őҋ@
        SceneManager.LoadScene(m_KeyChangeScenes[_nIdx].Nextscene);  //���̃X�e�[�W��
    }

    /*���Q�[���I���֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�Q�[�����I�����鏈��
    */
    private void GameEnd()
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