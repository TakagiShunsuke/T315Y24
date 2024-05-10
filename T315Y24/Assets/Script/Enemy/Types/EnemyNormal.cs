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

�܂��A�ȉ��̃I�u�W�F�N�g�����݂���K�v������܂��B
�P.m_sPlayerName�Œ�`���ꂽ���O�ƈ�v����I�u�W�F�N�g

����ɁA�ȉ��̃R���|�[�l���g������ꍇ�͂��̏����l���V���A���C�Y����Ď��������l�����������ď��������܂��B
�P.IMove���p�������A�ړ����s���R���|�[�l���g�̕ϐ�Speed


���X�V����
__Y24
_M05
D
03:�v���O�����쐬:takagi
04:����:takagi
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;  //Unity

//���N���X��`
public class CEnemyNormal : CEnemy, IFeatureMine
{
    //���ϐ��錾
    [SerializeField] private double m_dAtkInterval = 3.0d;  //�U���Ԋu[s]
    private double m_dAtkCoolTime = 0.0d;   //�U���N�[���^�C��[s]
    private IFeature m_Feature = null;  //�X�e�[�^�X����
    private CAreaSector m_CAreaSector = null;   //��`�̍U���͈�
    private GameObject m_Player = null; //�v���C���[
    [SerializeField] private string m_sPlayerName = "Player";   //�v���C���[�̃I�u�W�F�N�g��

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
        m_Player = GameObject.Find(m_sPlayerName);   //�v���C���[�̃C���X�^���X�i�[
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        if (m_Player == null)    //�擾�Ɏ��s������
        {
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�v���C���[��������܂���");  //�x�����O�o��
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
            m_dAtkCoolTime -= Time.fixedDeltaTime;
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
        if (m_Feature == null || m_CAreaSector == null || m_Player == null)   //�K�v�v���̕s����
        {
#if UNITY_EDITOR    //�G�f�B�^�g�p��
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�K�v�ȗv�f���s�����Ă��܂�");  //�x�����O�o��
#endif

            //�����f
            return; //�X�V�������f
        }

        //�������蔻��
        if (m_dAtkCoolTime <= 0.0d && m_CAreaSector.SignalCollision)  //�U���N�[���^�C���I���A�v���C���[�ւ̍U������
        {
            //��������
            m_dAtkCoolTime = m_dAtkInterval;    //�U���Ԋu������

            //�v���C���[�Ƀ_���[�W��^����
            //m_Player.Attackable.Damage(m_Feature.Atk);

#if UNITY_EDITOR    //�G�f�B�^�g�p��
            //�����O�o��
            UnityEngine.Debug.Log("��������HIT!");   //�U������̑���
#endif
        }
    }
    public void TakeDestroy()
    {
        Debug.LogWarning("�f�X�g���C");
        Destroy(gameObject);
    }
}