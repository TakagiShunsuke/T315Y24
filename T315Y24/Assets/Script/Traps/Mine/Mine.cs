/*=====
<Mine.cs> 
���쐬�ҁFyamamoto

�����e
�n���ɕt����X�N���v�g�B
���j�G�t�F�N�g�̐����͂�����

�����ӎ���  
�n����IsTrigger�����Ă���Ɠ��삵�܂���B
Prefab��ݒ肵�Ă��Ȃ��Ɣ����G�t�F�N�g����������Ȃ��B

���X�V����
__Y24   
_M05    
D
08 :�v���O�����쐬:yamamoto 
09 :�d�l�ύX�̈׏�����ύX:yamamoto
10:�R�����g�ǉ�:yamamoto
12:���L���X�g���Ԓǉ�:yamamoto

_M06
D
08�F�e�N���X�ǉ�����ɔ����v���O�������������Fyamamoto
13�F������SE�ǉ��Fnieda
18�FSE�ǉ��Fnieda
26�F�R�����g�ǉ��Fyamamoto
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;

//��Data�N���X��`
public class GameMineData
{
    //���v���p�e�B��`
    public int SetMine { get; set; }    //�u������
    public int UseMine { get; set; }    //�g������
    public int MineKill { get; set; }   //�|������

    /*���R���X�g���N�^
   �����P�Fint _nSetRemoteBomb �F�u������   
   �����Q�Fint _nUseRemoteBomb �F�g������  
   �����R�Fint _nRemoteBombKill�F�|������ 
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�f�[�^�����U���g�ɓn���悤�ɂ܂Ƃ߂�
   */
    public GameMineData(int _nSetMine, int _nUseMine, int _nMineKill)
    {
        //�e�f�[�^���Z�b�g
        SetMine = _nSetMine;     //�u������ 
        UseMine = _nUseMine;     //�g������
        MineKill = _nMineKill;   //�|������ 
    }
}

//���N���X��`
public class Mine : CTrap
{
    //���ϐ��錾
    [Header("�v���n�u")] 
    [SerializeField,Tooltip("���������������v���n�u")] private GameObject m_ExplosionEffectPrefab; // ���������������v���n�u
    private static int m_nSetMine;       //�u������ 
    private static int m_nUseMine;       //�g������
    private static int m_nMineKill;      //�|������ 


    /*���n�������蔻��֐�
    �����P�FCollision _Collision : �������Ă�����̂̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�G���n���ɐG�ꂽ�Ƃ��̂ݏ��������
    */
    private void OnCollisionStay(Collision collision)     //�n���ɉ������������Ă����Ƃ�
    {
        if (Check(collision))  // �N���ł��邩
        {
            m_audioSource.PlayOneShot(SE_ExpTrap);  //����SE�Đ�
            m_nUseMine++;    //�g�����񐔂𑝂₷
            //�����G�t�F�N�g�쐬
            GameObject explosion = Instantiate(m_ExplosionEffectPrefab, transform.position, Quaternion.identity);
            explosion.GetComponent<Explosion>().SetBombType(0);//�i�[���ݒ�
        }
        SetCheck(collision);    //�ݒu�ł��邩�ǂ�������
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

    /*���X�V�֐�
     �����F�Ȃ�
     ��
     �ߒl�F�Ȃ�
     ��
     �T�v�F�X�V
     */
    void Update()
    {
        SetTrap();  //�ݒu�֐��Ăяo��
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
        m_nSetMine++;
    }

    /*���f�[�^�����n���֐�
    �����F�Ȃ�
    ��
    �ߒl�FGameMineData(m_nSetMine, m_nUseMine,m_nMineKill):�f�[�^
    ��
    �T�v�F���U���g�ɓn���p�̃f�[�^���쐬
    */
    public static GameMineData GetGameMineData()
    {
        m_nMineKill=Explosion.m_KillCount[0];       //�Ή������z�񂩂�|���������擾
        Explosion.m_KillCount[0]=0;                 //�z���������
        //���U���g�ɓn���p�̃f�[�^���쐬
        return new GameMineData(m_nSetMine, m_nUseMine,m_nMineKill);
    }

    /*���f�[�^���Z�b�g�֐�
   �����F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F������
   */
    public static void ResetMineData()
    {
        m_nSetMine = 0;     //�u������ ������
        m_nUseMine = 0;     //�g�����񏉊���
        m_nMineKill = 0;    //�|������ ������
    }
}
