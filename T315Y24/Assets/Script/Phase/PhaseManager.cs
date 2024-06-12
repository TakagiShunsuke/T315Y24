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
07:�E�F�[�u�r���E�����p�^�[���ǉ�:takagi
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
        public List<CEnemyList.SpawnEnemyInfo> m_Enemies;
        public double m_dTime; //���̃t�F�[�Y�̎�������
    }   //�t�F�[�Y��`�p�̍\����

    //���萔��`
    const uint INIT_WAVE = 0;   //�E�F�[�u���J�E���g�̏����l

    //���ϐ��錾
    [SerializeField] private List<PhasePatturn> m_Phases;   //�t�F�[�Y�ꗗ
    private uint m_unPhase = 0;
    private double m_dCntDwnPhase = 0.0d;

    //���v���p�e�B��`
    public bool IsFinPhases { get; private set; } = false;    //�t�F�[�Y�S�I���t���O
    private double CntDwnWave
    {
        get => m_dCntDwnPhase;   //���g�̃Q�b�^
        set
        {
            m_dCntDwnPhase = value;    //�J�E���g�_�E��
            if (m_dCntDwnPhase < 0.0d)   //�J�E���g�_�E�����Ԓ���
            {
                    if (m_Phases.Count > m_unPhase + 1) //�t�F�[�Y�i�s�␳
                    {
                        m_unPhase++;    //�t�F�[�Y�i�s
                    CEnemyList.Instance.SpawnInfo = m_Phases[(int)m_unPhase].m_Enemies;
#if UNITY_EDITOR    //�G�f�B�^�g�p��
                    Debug.Log("�t�F�[�Y" + m_unPhase);   //�t�F�[�Y���o��
#endif
                    }
                    else
                {
                    IsFinPhases = true; //�S�t�F�[�Y����
//#if UNITY_EDITOR    //�G�f�B�^�g�p��
//                    //���G���[�o��
//                    UnityEngine.Debug.LogError("�t�F�[�Y�����z��𒴉߂��Ă��܂�");  //�x�����O�o��
//#endif
                }

                //���J�E���g�_�E���ʍX�V
                m_dCntDwnPhase = m_Phases[(int)m_unPhase].m_dTime + -m_dCntDwnPhase;  //�J�E���g�X�V(���ߕ��l��)
            }
        }
    }//�E�F�[�u�p�J�E���g�_�E��
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
        CEnemyList.Instance.SpawnInfo = m_Phases[(int)m_unPhase].m_Enemies;
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        Debug.Log("�t�F�[�Y" + m_unPhase);   //�t�F�[�Y���o��
#endif
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