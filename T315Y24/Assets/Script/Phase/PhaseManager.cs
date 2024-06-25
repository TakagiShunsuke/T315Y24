/*=====
<PhaseManager.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�t�F�[�Y�Ǘ�

�����ӎ���
�V���O���g��

���X�V����
__Y24
_M06
D
05:�v���O�����쐬:takagi
07:�E�F�[�u�r���E�����p�^�[���ǉ�:takagi
13:�e�L�X�g�\���@�\�ǉ�:takagi
17:SE�ǉ�:nieda
18:�t�F�[�Y�`���ύX�A���C��:takagi
21:���t�@�N�^�����O:takagi
24:���t�@�N�^�����O:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using static CFeatures;
using static CPhaseManager;

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
        public E_PHASE m_ePhase;    //�t�F�[�Y
        public List<CEnemyList.SpawnEnemyInfo> m_Enemies;   //�o������G
        public uint m_unEnemyVal;   //�Q�[�g��̓G������
        [Min(0.0f)] public double m_dSpawnInterval;  //�����Ԋu[s]  //TODO:double�^
        public double m_dTime; //���̃t�F�[�Y�̎�������
        public uint m_unKillValToNext;  //�t�F�[�Y�؂�ւ��ɕK�v�ȓ�����
    }   //�t�F�[�Y��`�p�̍\����

    //���萔��`
    private const uint INIT_WAVE = 0;   //�E�F�[�u���J�E���g�̏����l

    //���ϐ��錾
    [Header("�t�F�[�Y���")]
    [SerializeField, Tooltip("�t�F�[�Y���\���ӏ�")] private TextMeshProUGUI m_TMP_PhaseVal;    //�t�F�[�Y���\���ꏊ
    //[SerializeField] private TextMeshProUGUI m_TMP_PhaseName;   //�t�F�[�Y���\���ꏊ
    //[SerializeField, CSerializeNamingWithEnum(typeof(E_PHASE)), Tooltip("�t�F�[�Y��")] private string[] m_sPhaseName;    //E_PHASE���Ƃ̃t�F�[�Y��
    [SerializeField, Tooltip("�t�F�[�Y�ڍ�")] private List<PhasePatturn> m_Phases;   //�t�F�[�Y�ꗗ
    private uint m_unPhase = 0; //�t�F�[�Y�ԍ��Ǘ�
    private double m_dCntDwnPhase = 0.0d;   //�t�F�[�Y���ԃJ�E���g
    [Header("��")]
    [SerializeField] private AudioClip SE_Spawn;  // �E�F�[�u�J�n����SE
    private AudioSource m_As; // AudioSource��ǉ�

    //���v���p�e�B��`
    public bool IsFinPhases { get; private set; } = false;    //�t�F�[�Y�S�I���t���O
    private double CntDwnWave
    {
        get => m_dCntDwnPhase;   //���g�̃Q�b�^
        set
        {
            m_dCntDwnPhase = value;    //�J�E���g�_�E��
            //���X�V
            if (m_dCntDwnPhase <= 0.0d)   //�J�E���g�_�E�����Ԓ���
            {
                if (m_Phases.Count > m_unPhase + 1) //�t�F�[�Y�i�s�␳
                {
                    m_unPhase++;    //�t�F�[�Y�i�s
                    CEnemyList.Instance.SpawnInfo = m_Phases[(int)m_unPhase].m_Enemies; //�G�̃��X�g�X�V
                    CGate.SpawnInterval = m_Phases[(int)m_unPhase].m_dSpawnInterval;    //�����Ԋu�X�V
#if UNITY_EDITOR    //�G�f�B�^�g�p��
                    Debug.Log("�t�F�[�Y" + m_unPhase);   //�t�F�[�Y���o��
#endif
                    UpdatePhaseText();  //�t�F�[�Y�\���X�V
                    m_As.PlayOneShot(SE_Spawn);   // SE�Đ�
                }
                else
                {
                    IsFinPhases = true; //�S�t�F�[�Y����
                    //#if UNITY_EDITOR    //�G�f�B�^�g�p��
                    //���G���[�o��
                    //UnityEngine.Debug.LogError("�t�F�[�Y�����z��𒴉߂��Ă��܂�");  //�x�����O�o��
                    //#endif
                }

                //���J�E���g�_�E���ʍX�V
                m_dCntDwnPhase = m_Phases[(int)m_unPhase].m_dTime + -m_dCntDwnPhase;  //�J�E���g�X�V(���ߕ��l��)
            }
        }
    }//�E�F�[�u�p�J�E���g�_�E��
    public E_PHASE Phase => m_Phases[(int)m_unPhase].m_ePhase; //���݂̃t�F�[�Y
    public uint EnemyVal => m_Phases[(int)m_unPhase].m_unEnemyVal;   //��������G��


    /*���������֐�
     �����P�F�Ȃ�
     ��
     �ߒl�F�Ȃ�
     ��
     �T�v�F�C���X�^���X�������ɍs������
     */
    override protected void CustomAwake()
    {
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        if (m_TMP_PhaseVal == null)   //�擾�Ɏ��s������
        {
            //���G���[�o��
            UnityEngine.Debug.LogWarning("���͐�e�L�X�g���ݒ肳��Ă��܂���");    //�x�����O�o��
        }
#endif
//#if UNITY_EDITOR    //�G�f�B�^�g�p��
//        if (m_TMP_PhaseName == null)   //�擾�Ɏ��s������
//        {
//            //���G���[�o��
//            UnityEngine.Debug.LogWarning("���͐�e�L�X�g���ݒ肳��Ă��܂���");    //�x�����O�o��
//        }
//#endif

        //���O��������
        CEnemyList.Instance.SpawnInfo = m_Phases[(int)m_unPhase].m_Enemies; //�G�̃��X�g������
        CGate.SpawnInterval = m_Phases[(int)m_unPhase].m_dSpawnInterval;    //�����Ԋu������
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        Debug.Log("�t�F�[�Y" + m_unPhase);   //�t�F�[�Y���o��
#endif

        //��������
        UpdatePhaseText();  //�t�F�[�Y�\��������
        CntDwnWave = m_Phases[(int)m_unPhase].m_dTime;    //�J�E���g�_�E��������
        m_As = GetComponent<AudioSource>(); // AudioSource�R���|�[�l���g��ǉ�
    }

    /*���X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
    */
    override protected void Update()
    {
        //���t�F�[�Y���ԍX�V
        CntDwnWave -= Time.deltaTime;    //�J�E���g�_�E��

        //���G������
        if(CEnemy.m_nDeadEnemyCount > m_Phases[(int)m_unPhase].m_unKillValToNext)    //���������ڕW�l�ȏ�
        {
            CntDwnWave -= CntDwnWave;   //�J�E���g��0�ɂ���
        }
    }

    /*���t�F�[�Y�p�e�L�X�g�X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�t�F�[�Y�\���ɍX�V���K�v�Ȏ��ɍs������
    */
    private void UpdatePhaseText()
    {
        //���e�L�X�g�C��
        if (m_TMP_PhaseVal != null)   //�t�F�[�Y���\��
        {
            m_TMP_PhaseVal.text = (m_unPhase + 1).ToString().PadRight(2) + " / " + m_Phases.Count.ToString().PadRight(2);    //���݂̃t�F�[�Y�ԍ�/�S�̂̃t�F�[�Y�ԍ�
        }
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        else
        {
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�e�L�X�g���s�����Ă��܂�");  //�x�����O�o��
        }
#endif
//        if (m_TMP_PhaseName != null)   //�t�F�[�Y���\��
//        {
//            if ((int)m_Phases[(int)m_unPhase].m_ePhase < m_sPhaseName.Length && m_sPhaseName[(int)m_Phases[(int)m_unPhase].m_ePhase] != null)
//            {
//                m_TMP_PhaseName.text = m_sPhaseName[(int)m_Phases[(int)m_unPhase].m_ePhase];    //�t�F�[�Y���\��
//            }
//#if UNITY_EDITOR    //�G�f�B�^�g�p��
//            else
//            {
//                //���G���[�o��
//                UnityEngine.Debug.LogWarning("�\������t�F�[�Y�����ݒ肳��Ă��܂���");  //�x�����O�o��
//            }
//#endif
//        }
//#if UNITY_EDITOR    //�G�f�B�^�g�p��
//        else
//        {
//            //���G���[�o��
//            UnityEngine.Debug.LogWarning("�e�L�X�g���s�����Ă��܂�");  //�x�����O�o��
//        }
//#endif
    }
}