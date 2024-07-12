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
18:�t�F�[�Y�`���ύX�ɑΉ�:takagi
21:���t�@�N�^�����O:takagi
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

//���N���X��`
public class CGate : MonoBehaviour
{
    //���ϐ��錾
    [Header("�G�������")]
    [SerializeField, Tooltip("�������")] private uint m_unSpawnMax = 100;  //�������[s]
    private double m_dSpawnCoolTime = 0.0d;   //�����N�[���^�C��[s]
    //[SerializeField] private double m_dSpawnInterval = 3.0d;  //�����Ԋu[s]
    private static double m_dSpawnInterval;  //�����Ԋu[s]
    private CSpawnEnemy m_SpawnRandom = null;    //�����@�\

    //���v���p�e�B��`
    public static double SpawnInterval { private get; set; }    //�t�F�[�Y�S�I���t���O


    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    private void Start()
    {
        //��������
        m_SpawnRandom = GetComponent<CSpawnEnemy>();   //���g�̓����擾
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
        {
            //������
            for (uint unIdx = 0; unIdx < CPhaseManager.Instance.EnemyVal && m_unSpawnMax > CEnemy.ValInstance; unIdx++) //�����\�Ȃ�K�v������
            {
                m_SpawnRandom.Create(); //�C���X�^���X����
            }

            //��������
            m_dSpawnCoolTime = m_dSpawnInterval;    //�N�[���_�E���J�n
        }

    }
}