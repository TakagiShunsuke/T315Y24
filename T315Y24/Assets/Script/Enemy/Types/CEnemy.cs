/*=====
<Enemy.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�G�̐e�N���X

�����ӎ���
�G�S�̂̃C���X�^���X�����Ǘ����邽�߁A
�p����̃N���X��Start(),OnDestroy()�֐���ǉ�����Ƃ��͕K���Ăяo���Ă��������B

���X�V����
__Y24
_M05
D
07:�v���O�����쐬:takagi
10:�v���p�e�B�錾�ύX
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;  //Unity

//���N���X��`
public abstract class CEnemy : MonoBehaviour
{
    //���v���p�e�B��`
    static public uint ValInstance { get; private set; } = 0;   //�C���X�^���X��
    static public int m_nDeadEnemyCount=0;
   
    
    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    virtual public void Start()
    {
        //���J�E���g
        ValInstance++;  //����������
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    //�֐��̒�`
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        m_nDeadEnemyCount = 0;
    }

    public void counter()
    {
        m_nDeadEnemyCount++;

    }
    /*���I���֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�j�����ɍs������
    */
    virtual protected void OnDestroy()
    {
        //���J�E���g
        ValInstance--;  //����������
    }
}