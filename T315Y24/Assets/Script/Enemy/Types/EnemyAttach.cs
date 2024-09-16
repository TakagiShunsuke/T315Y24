/*=====
<EnemyAttach.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�G(����)�̋����𓝊��E�Ǘ�

�����ӎ���
����̃I�u�W�F�N�g�Ɉȉ��̃R���|�[�l���g���Ȃ��ƓG�Ƃ��ď\���ȋ@�\�����܂���B
�P.IFeatureBase���p�������A������\���R���|�[�l���g
�Q.�U���͈͂�\����`�̗̈攻��AreaSector
�R.�������Z���s��Rigidbody

�܂��A�ȉ��̃R���|�[�l���g������ꍇ�́A�����ɉ����Ă��̏����l���V���A���C�Y����Ď��������l�����������ď��������܂��B
�P.�ړ��Ɏg�p�����R���|�[�l���gNavMeshAgent�̕ϐ�speed


���X�V����
__Y24
_M05
D
03:�v���O�����쐬:takagi
04:����:takagi
11:�v���C���[�폜�AAreaSector�ύX�ւ̑Ή�:takagi
31:���t�@�N�^�����O:takagi

_M06
D
09:���������ɑΉ��E���l�[��:takagi
21:���t�@�N�^�����O:takagi
24:���t�@�N�^�����O:takagi

_M07
D
21:�A�j���[�V�����ǉ�:nieda
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

//���N���X��`
public sealed class CEnemyAttach : CEnemy, IFeatureMine, IFeatureGameOver
{
    //���ϐ��錾
    [Header("�X�e�[�^�X")]
    [SerializeField, Tooltip("����")] private CFeatures.E_ENEMY_TYPE m_eFeatureType;   //�����̎��
    private CFeatures.FeatureInfo m_FeatureInfo;    //�����ɂ�茈�肷����
    [SerializeField, Tooltip("�U���Ԋu")] private double m_dAtkInterval;  //�U���Ԋu[s]
    private double m_dAtkCoolTime = 0.0d;   //�U���N�[���^�C��[s]
    [Header("�G�t�F�N�g")]
    [SerializeField] private GameObject m_EffectCube;       //�G�t�F�N�g�L���[�u�v���n�u
    [SerializeField] private int m_nEffectNum;              //�G�t�F�N�g�L���[�u������
    [SerializeField] private float m_fPosRandRange;  //�G�t�F�N�g�L���[�u�𐶐�����|�W�V�����������_���ɐ������邽�߂͈̔�
    private CAreaSector m_CAreaSector = null;   //��`�̍U���͈�
    private Rigidbody m_Rigidbody;
    [Header("�A�j���[�V����")]
    private Animator m_Animator;
    private double m_dAnimFinCnt = 0.0d;    // �A�j���[�V�����I������p
    private double m_dAnimFinTime = 0.3d;   // �A�j���[�V�����I������

    private bool isGameOver = false;                        //�Q�[���I�[�o�[������s�\�ɂ���p


    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    override protected void CustomStart()
    {
        //��������
        if (CFeatures.Instance.Feature.Length > (int)m_eFeatureType)    //���g�̗񋓐��l�ɑ΂��ĕԓ������҂ł��鎞
        {
            //�����l�X�V
            m_FeatureInfo = CFeatures.Instance.Feature[(int)m_eFeatureType];    //�񋓂ɑΉ����������擾����

            //���ړ���񏑂�����
            var Move = GetComponent<NavMeshAgent>();   //�ړ��R���|�[�l���g�擾
            if (Move != null)   //�擾������
            {
                Move.speed = (float)m_FeatureInfo.Move;    //���x��������
            }
        }
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        else
        {
            //���G���[�o��
            UnityEngine.Debug.LogWarning(m_eFeatureType + "�̓������ݒ肳��Ă��܂���");   //�x�����O�o��
        }
#endif
        m_CAreaSector = GetComponent<CAreaSector>();  //�����蔻��擾
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        if (m_CAreaSector == null)   //�擾�Ɏ��s������
        {
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�U���͈͂��ݒ肳��Ă��܂���");    //�x�����O�o��
        }
#endif

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();  // Animator�R���|�[�l���g��ǉ�
    }

    /*�������X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
    */
    override protected void FixedUpdate()
    {
        if (isGameOver)
        {
            m_Animator.SetBool("isAttack", false);
            //�ҋ@���[�V������������

            return;
        }
            //���J�E���g�_�E��
            if (m_dAtkCoolTime > 0.0d)   //�N�[���_�E����
        {
            m_dAtkCoolTime -= Time.fixedDeltaTime;  //�N�[���_�E������
        }

        if(m_dAnimFinCnt > 0.0d)    // �I���J�E���g�_�E����
        {
            m_dAnimFinTime -= Time.fixedDeltaTime;
        }
        else    // �I���J�E���g�_�E���I��
        {
            // Attack�A�j���[�V�������I��
            m_Animator.SetBool("isAttack", false);
        }

        //���U��
        Attack();   //�U�����s��

        m_Rigidbody.velocity = Vector3.zero;
    }

    /*���U���֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�U���͈͂ɂ���v���C���[���U�����鏈��
    */
    private void Attack()
    {
        //������
        if (m_CAreaSector == null)   //�K�v�v���̕s����
        {
#if UNITY_EDITOR    //�G�f�B�^�g�p��
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�K�v�ȗv�f���s�����Ă��܂�");  //�x�����O�o��
#endif

            //�����f
            return; //�X�V�������f
        }

        //���ϐ��錾
        List<GameObject> Hits = m_CAreaSector.SignalCollision;

        //���U������
        if (Hits != null && Hits.Count > 0 && m_dAtkCoolTime <= 0.0d)   //���m�Ώۂ����݂���A�U���N�[���^�C���I��
        {
            //���N�[���_�E��
            m_dAtkCoolTime = m_dAtkInterval;    //�U���Ԋu������
            m_dAnimFinCnt = m_dAnimFinTime;     //�A�j���[�V�����I���J�E���g�_�E���J�n

            //���_���[�W
            for (int nIdx = 0; nIdx < Hits.Count; nIdx++)   //�U���͈͑S�ĂɃ_���[�W
            {
                //���ϐ��錾
                IDamageable Damageable = Hits[nIdx].GetComponent<IDamageable>();    //�_���[�W��^���ėǂ���

                //�������蔻��
                if (Damageable != null)  //�_���[�W��^�����鑊��
                {
                    //���_���[�W�t�^
                    Damageable.Damage(m_FeatureInfo.Atk);   //�ΏۂɃ_���[�W��^����
                }
            }
            
            // Attack�A�j���[�V�������Đ�
            m_Animator.SetBool("isAttack", true);
        }
        
    }

    /*���G�����֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�G���������鏈��
    */
    public void TakeDestroy()
    {
        float x, y, z = 0.0f;

        // �G�t�F�N�g�L���[�u����
        for (int i = 0; i < m_nEffectNum; i++)
        {
            x = Random.Range(-m_fPosRandRange, m_fPosRandRange);
            y = Random.Range(-m_fPosRandRange, m_fPosRandRange);
            z = Random.Range(-m_fPosRandRange, m_fPosRandRange);

            Instantiate(m_EffectCube,new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z), Quaternion.identity);
        }
        counter();
        Destroy(gameObject);    //���̃I�u�W�F�N�g����������
        gameObject.SetActive(false);
        GameObject Text;
        EnemyDeathCounter EnemyDeathCounter;
        Text = GameObject.Find("DeathCounter");
        EnemyDeathCounter = Text.GetComponent<EnemyDeathCounter>();

        EnemyDeathCounter.DisplayEnemyDeathCounter();
    }
    public void OnGameOver()
    {
        isGameOver = true;
    }
}