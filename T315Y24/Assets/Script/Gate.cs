/*=====
<Gate.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
��

�����ӎ���
����̃I�u�W�F�N�g�Ɉȉ��̃R���|�[�l���g���Ȃ��Ə\���ȋ@�\�����܂���B
�P.�G�𐶐�����@�\�ł���SpawnEnemy

���X�V����
__Y24
_M05
D
06:�v���O�����쐬:takagi
07:����:takagi
10:����������@�\����:takagi

_M06
D
09:�����@�\�̃N���X�ύX
13:�G�o����SE�ǉ�:nieda
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;  //Unity

//���N���X��`
public class CGate : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] private double m_dSpawnInterval = 3.0d;  //�����Ԋu[s]
    [SerializeField] private uint m_unSpawnMax = 100;  //�������[s]
    private double m_dSpawnCoolTime = 0.0d;   //�����N�[���^�C��[s]
    CSpawnEnemy m_SpawnRandom = null;    //�����@�\
    AudioSource m_audioSource;  // AudioSource��ǉ�
    [SerializeField] public AudioClip SE_Spawn; // �G�o������SE

    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    void Start()
    {
        //��������
        m_SpawnRandom = GetComponent<CSpawnEnemy>();   //���g�̓����擾
        m_audioSource = GetComponent<AudioSource>();
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        if (m_SpawnRandom == null)   //�擾�Ɏ��s������
        {
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�����R���|�[�l���g���ݒ肳��Ă��܂���");    //�x�����O�o��
        }
#endif
    }

    /*�������X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
    */
    private void FixedUpdate()
    {
        //���J�E���g�_�E��
        if (m_dSpawnCoolTime > 0.0d)   //�N�[���_�E����
        {
            m_dSpawnCoolTime -= Time.fixedDeltaTime;    //�J�E���g�_�E���i�s
        }
        else
        {   //������
            if(m_unSpawnMax > CEnemy.ValInstance)
            {
                m_SpawnRandom.Create(); //�C���X�^���X����
                m_audioSource.PlayOneShot(SE_Spawn);    // �G�o����SE�Đ�
            }

            //��������
            m_dSpawnCoolTime = m_dSpawnInterval;    //�N�[���_�E���J�n
        }
    }
}