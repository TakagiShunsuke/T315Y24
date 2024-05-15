/*=====
<EnemyNormal.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�G(����)�̋����𓝊��E�Ǘ�

�����ӎ���
����̃I�u�W�F�N�g�Ɉȉ��̃R���|�[�l���g���Ȃ��ƓG�Ƃ��ď\���ȋ@�\�����܂���B
�P.IFeatureBase���p�������A������\���R���|�[�l���g
�Q.�U���͈͂�\����`�̗̈攻��AreaSector
�R.�������Z���s��Rigidbody

�܂��A�ȉ��̃R���|�[�l���g������ꍇ�͂��̏����l���V���A���C�Y����Ď��������l�����������ď��������܂��B
�P.IMove���p�������A�ړ����s���R���|�[�l���g�̕ϐ�Speed


���X�V����
__Y24
_M05
D
03:�v���O�����쐬:takagi
04:����:takagi
11:�v���C���[�폜�AAreaSector�ύX�ւ̑Ή�:takagi
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;  //Unity

//���N���X��`
public class CEnemyNormal : CEnemy, IFeatureMine
{
    //���ϐ��錾
    [SerializeField] private double m_dAtkInterval = 3.0d;  //�U���Ԋu[s]
    private double m_dAtkCoolTime = 0.0d;   //�U���N�[���^�C��[s]
    private IFeature m_Feature = null;  //�X�e�[�^�X����
    private CAreaSector m_CAreaSector = null;   //��`�̍U���͈�

    [SerializeField] GameObject m_EffectCube;       //�G�t�F�N�g�L���[�u�v���n�u
    [SerializeField] int m_nEffectNum;              //�G�t�F�N�g�L���[�u������
    [SerializeField]float m_fPosRandRange = 0.01f;  //�G�t�F�N�g�L���[�u�𐶐�����|�W�V�����������_���ɐ������邽�߂͈̔�

   

    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    public void Start()
    {
        //���e�֐��Ăяo��
        transform.GetComponentInParent<CEnemy>().Start(); //�e�̏������֐��Ăяo��

        //��������
        m_Feature = GetComponent<IFeature>();   //���g�̓����擾
        if (m_Feature != null)   //�擾�Ɏ��s������
        {
            var Move = GetComponent<IMove>();   //�ړ��R���|�[�l���g�擾
            if (Move != null)   //�擾������
            {
                Move.Speed = m_Feature.Move;    //���x��������
            }
        }
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        else
        {
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�������ݒ肳��Ă��܂���");   //�x�����O�o��
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
        if(m_dAtkCoolTime > 0.0d)   //�N�[���_�E����
        {
            m_dAtkCoolTime -= Time.fixedDeltaTime;  //�N�[���_�E������
        }

        //���U��
        Attack();   //�U�����s��
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
        if (m_Feature == null || m_CAreaSector == null)   //�K�v�v���̕s����
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

            //���_���[�W
            for (int nIdx = 0; nIdx < Hits.Count; nIdx++)   //�U���͈͑S�ĂɃ_���[�W
            {
                //���ϐ��錾
                IDamageable Damageable = Hits[nIdx].GetComponent<IDamageable>();    //�_���[�W��^���ėǂ���

                //�������蔻��
                if (Damageable != null)  //�_���[�W��^�����鑊��
                {
                    //���_���[�W�t�^
                    Damageable.Damage(m_Feature.Atk);   //�ΏۂɃ_���[�W��^����
                }
            }
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
        GameObject Text;
        EnemyDeathCounter EnemyDeathCounter;
        Text = GameObject.Find("test");
        EnemyDeathCounter = Text.GetComponent<EnemyDeathCounter>();

        EnemyDeathCounter.DisplayEnemyDeathCounter();
    }
}