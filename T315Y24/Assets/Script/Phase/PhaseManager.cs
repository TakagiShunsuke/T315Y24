/*=====
<PlayerHPUI.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�v���C���[��HP��UI�\������

�����ӎ���
�V���O���g��
�C���v�b�g�����⎞�Ԍv���̂���Update()�������ł���MonoBehavior���p���B


���X�V����
__Y24
_M06
D
05:�v���O�����쐬:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;  //Unity
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

//���N���X��`
public class CPhaseManager : CMonoSingleton<CPhaseManager>
{
    //���񋓒�`
    public enum E_PHASE
    {
        E_PHASE_BATTLE, //�퓬�t�F�[�Y
        E_PHASE_SET_UP, //�݉c�t�F�[�Y
    }   //�t�F�[�Y����

    //���\���̒�`
    [Serializable]public struct PhasePatturn
    {
        public E_PHASE m_Phase;    //�t�F�[�Y
        public uint m_unPhaseRepeat; //�t�F�[�Y�̌J��Ԃ���
        public double m_dTime; //���̃t�F�[�Y�̎�������
    }   //�t�F�[�Y��`�p�̍\����

    //���萔��`
    const uint INIT_WAVE = 0;   //�E�F�[�u���J�E���g�̏����l

    //���ϐ��錾
    [SerializeField] private List<PhasePatturn> m_Phases;   //�t�F�[�Y�ꗗ
    private uint m_unWave = 0;  //�E�F�[�u���J�E���^
    private uint m_unPhase = 0;
    private double m_dCntDwnWave = 0.0d;

    //���v���p�e�B��`
    private double CntDwnWave
    {
        get => m_dCntDwnWave;   //���g�̃Q�b�^
        set
        {
            m_dCntDwnWave = value;    //�J�E���g�_�E��
            if (m_dCntDwnWave < 0.0d)   //�J�E���g�_�E�����Ԓ���
            {
                //���E�F�[�u�E�t�F�[�Y�Ǘ�
                m_unWave++; //�E�F�[�u�i�s
#if UNITY_EDITOR    //�G�f�B�^�g�p��
                Debug.Log("�E�F�[�u" + m_unWave);   //�E�F�[�u���o��
#endif
                if (m_unWave >= m_Phases[(int)m_unPhase].m_unPhaseRepeat)   //�E�F�[�u�����ߎ�
                {
                    if (m_Phases.Count > m_unPhase + 1) //�t�F�[�Y�i�s�␳
                    {
                        m_unPhase++;    //�t�F�[�Y�i�s
#if UNITY_EDITOR    //�G�f�B�^�g�p��
                        Debug.Log("�t�F�[�Y" + m_unWave);   //�t�F�[�Y���o��
#endif
                    }
#if UNITY_EDITOR    //�G�f�B�^�g�p��
                    else
                    {
                        //���G���[�o��
                        UnityEngine.Debug.LogError("�t�F�[�Y�����z��𒴉߂��Ă��܂�");  //�x�����O�o��
                    }
#endif
                }

                //���J�E���g�_�E���ʍX�V
                m_dCntDwnWave = m_Phases[(int)m_unPhase].m_dTime + -m_dCntDwnWave;  //�J�E���g�X�V(���ߕ��l��)
            }
        }
    }//�E�F�[�u�p�J�E���g�_�E��
    private uint unPhase
    {
        get => m_unPhase;   //���g�̃Q�b�^
        set 
        {
            m_unPhase = value;  //�Z�b�^�Ƃ��Ă̋@�\
            m_unWave = INIT_WAVE;   //�t�F�[�Y���ς�������΂ɃE�F�[�u�͐��������ɂȂ�
        }
    } //�t�F�[�Y���J�E���^
    public E_PHASE Phase => m_Phases[(int)m_unPhase].m_Phase; //���݂̃t�F�[�Y

    /*���������֐�
     �����P�F�Ȃ�
     ��
     �ߒl�F�Ȃ�
     ��
     �T�v�F�C���X�^���X�������ɍs������
     */
    override protected void CustomAwake()
    {
        CntDwnWave = m_Phases[(int)m_unPhase].m_dTime;    //�J�E���g�_�E��������

    }

    /*�������X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
    */
    override protected void Update()
    {
        //���t�F�[�Y���ԍX�V
        //var SubtractTime = Time.deltaTime;  //���Ԃ̌�����
        //if(m_dCntDwnWave - SubtractTime > 0.0d)
        //{
        //    m_dCntDwnWave -= SubtractTime;  //�J�E���g�_�E��
        //}
        //else
        //{
        //}

        ///�܂���///

        //���t�F�[�Y���ԍX�V
        //m_dCntDwnWave -= Time.deltaTime;    //�J�E���g�_�E��
        //if (m_dCntDwnWave < 0.0d)   //�J�E���g�_�E�����Ԓ���
        //{
        //    //���E�F�[�u�E�t�F�[�Y�Ǘ�
        //    m_unWave++; //�E�F�[�u�i�s
        //    if( m_unWave >= m_Phases[(int)m_unPhase].m_unPhaseRepeat)   //�E�F�[�u�����ߎ�
        //    {
        //        m_unPhase++;    //�t�F�[�Y�i�s
        //    }
            
        //    //���J�E���g�_�E���ʍX�V
        //    m_dCntDwnWave = m_Phases[(int)m_unPhase].m_dTime + -m_dCntDwnWave;  //�J�E���g�X�V
        //}

        ///�܂���///

        CntDwnWave -= Time.deltaTime;    //�J�E���g�_�E��
        //��L��m_dCntDwnWave�̃Z�b�^�ɓZ�߂�(�Z�b�g����Ƃ��ɔ������鏈���Ȃ���)



        //���t�F�[�Y�̏I��
        //Todo:�I���\��
    }
}