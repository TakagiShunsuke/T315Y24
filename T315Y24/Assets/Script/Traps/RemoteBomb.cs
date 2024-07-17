/*=====
<RemoteBome.cs> 
���쐬�ҁFyamamoto

�����e
player���蓮�ŋN��������㩂̃X�N���v�g

���X�V����
__Y24   
_M05    
D
31: �v���O�����쐬: yamamoto
_M06    
D
12: ���t�@�N�^�����O: yamamoto
26: �R�����g�ǉ�: yamamoto
=====*/

//�����O��Ԑ錾
using Effekseer;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using static CCodingRule;

//��Data�N���X��`
public class GameRemoteBombData
{
    //���v���p�e�B��`
    public int SetRemoteBomb { get; set; }  //�u������
    public int UseRemoteBomb { get; set; }  //�g������
    public int RemoteBombKill { get; set; } //�|������


    /*���R���X�g���N�^
    �����P�Fint _nSetRemoteBomb �F�u������   
    �����Q�Fint _nUseRemoteBomb �F�g������  
    �����R�Fint _nRemoteBombKill�F�|������ 
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�f�[�^�����U���g�ɓn���悤�ɂ܂Ƃ߂�
    */
    public GameRemoteBombData( int _nSetRemoteBomb, int _nUseRemoteBomb,int _nRemoteBombKill)
    {
        //�e�f�[�^���Z�b�g
        SetRemoteBomb = _nSetRemoteBomb;        //�u������ 
        UseRemoteBomb = _nUseRemoteBomb;        //�g������
        RemoteBombKill = _nRemoteBombKill;      //�|������ 
    }
}

//���N���X��`
public class RemoteBomb : CTrap
{
    //�ϐ��錾
    [Header("�v���n�u")]
    //[SerializeField,Tooltip("���������������v���n�u")] private GameObject m_ExplosionEffectPrefab; // ���������������v���n�u
    [SerializeField, Tooltip("�����̔���p�v���n�u")] private GameObject m_ExplosionCollPrefab; // ���������������v���n�u
    [SerializeField, Tooltip("�������Đ�����G�t�F�N�g")] private EffekseerEffectAsset m_ExplosionEffect;  // �������Đ�����G�t�F�N�g
    private static int m_nSetRemoteBomb;     //�u�������i�[�p
    private static int m_nUseRemoteBomb;     //�g�����񐔊i�[�p
    private static int m_nRemoteBombKill; //�|�������i�[�p


    /*���X�V�֐�
     �����F�Ȃ�
     ��
     �ߒl�F�Ȃ�
     ��
     �T�v�F�X�V
     */
    void Update()
    {
        if (!m_bMove)//�ݒu����Ă�����
        {
            //���ۑS
            if (m_ExplosionEffect == null)   //�G�t�F�N�g���Ȃ�
            {
#if UNITY_EDITOR    //�G�f�B�^�g�p��
                //���G���[�o��
                UnityEngine.Debug.LogWarning("�K�v�ȗv�f���s�����Ă��܂�");  //�x�����O�o��
#endif
                //�����f
                return; //�������Ȃ�
            }

            if ((Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("Explosion")) & m_bUse)
            {//�g�����ԂȂ����
                m_audioSource.PlayOneShot(SE_ExpTrap);  //����SE�Đ�
                SetCoolTime();              //�N�[���^�C����ݒ�
                m_nUseRemoteBomb++;          //�g�����񐔂𑝂₷

                //�������G�t�F�N�g�Đ�
                EffekseerSystem.PlayEffect(m_ExplosionEffect, transform.position);  //�����ʒu�ɍĐ�

                //��������쐬
                GameObject explosion = 
                    Instantiate(m_ExplosionCollPrefab, transform.position, Quaternion.identity);
                explosion.GetComponent<Explosion>().SetBombType(1); //�i�[���ݒ�
            }
         }
        SetTrap();//�ݒu�֐��Ăяo��
    }

    /*�������蔻��֐�
    �����P�FCollision _Collision : �������Ă�����̂̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�������Ă���Ƃ��Ăяo�����
    */
    private void OnCollisionStay(Collision _Collision)
    {
        SetCheck(_Collision);   //�ݒu�ł��邩�ǂ�������
    }

    /*�������蔻��֐�
    �����P�FCollision _Collision : �������Ă�����̂̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�������Ă����Ԃ��瓖����Ȃ��Ȃ�����Ăяo�����
    */
    private void OnCollisionExit(Collision collision)
    {
        OutCheck(collision);    //�ݒu�ł��邩�ǂ�������
    }

    /*���ݒu�񐔃J�E���g�֐�
    �����P�FCollision _Collision : �������Ă�����̂̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�ݒu���ꂽ�Ƃ��ɌĂяo�����
    */
    public override void SetCount()
    {
        m_nSetRemoteBomb++;  //�ݒu�񐔂𑝂₷
    }

    /*���f�[�^�����n���֐�
    �����F�Ȃ�
    ��
    �ߒl�F new GameRemoteBombData(m_nSetRemoteBomb, m_nUseRemoteBomb, m_nRemoteBombKill):�f�[�^
    ��
    �T�v�F���U���g�ɓn���p�̃f�[�^���쐬
    */
    public static GameRemoteBombData GetGameRemoteBombData()
    {
        m_nRemoteBombKill = Explosion.m_KillCount[1];//�Ή������z�񂩂�|���������擾
        Explosion.m_KillCount[1] = 0;               //�z���������
        //���U���g�ɓn���p�̃f�[�^���쐬
        return new GameRemoteBombData(m_nSetRemoteBomb, m_nUseRemoteBomb, m_nRemoteBombKill);
    }

    /*���f�[�^���Z�b�g�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F������
    */
    public static void ResetRemoteBombData()
    {
        m_nSetRemoteBomb = 0;    //�u������ ������
        m_nUseRemoteBomb = 0;    //�g�����񏉊���
        m_nRemoteBombKill = 0;   //�|������ ������
    }
}
