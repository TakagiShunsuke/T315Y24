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
08�F�e�N���X�ǉ�����ɔ����v���O������������:yamamoto
13�F������SE�ǉ�:nieda
18�FSE�ǉ�:nieda
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;

public class GameMineData
{
    public int SetMine { get; set; }
    public int UseMine { get; set; }
    public int MineKill { get; set; }

    // �R���X�g���N�^
    public GameMineData(int setMine, int useMine, int mineKill)
    {
        SetMine = setMine;
        UseMine = useMine;
        MineKill = mineKill;
    }
}

//���N���X��`
public class Mine : CTrap
{
    //���ϐ��錾
    [SerializeField] private GameObject m_ExplosionEffectPrefab; // ���������������v���n�u
    [SerializeField] public AudioClip SE_ExpMine;  // 㩐ݒu����SE
    AudioSource audioSource;    // AudioSource��ǉ�
    [SerializeField] public AudioClip SE_explosion; // ��������SE

    private static int m_SetMine ;
    private static int m_UseMine ;
    private static int m_MineKill;

    /*���J�E���g�������֐�
 �����P�F�Ȃ�
 ��
 �ߒl�F�Ȃ�
 ��
 �T�v�F�V�[�����ς��Ƃ��ɌĂ΂�鏈��
 */
     void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        m_SetMine = 0;
    }

    /*���n�������蔻��֐�
    �����P�F�����蔻�肪�������I�u�W�F�N�g�̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�G���n���ɐG�ꂽ�Ƃ��̂ݏ��������
    */
    private void OnCollisionStay(Collision collision)     //�n���ɉ������������Ă����Ƃ�
    {
        if (Check(collision))  // �N���ł��邩
        {
            audioSource = GetComponent<AudioSource>();  //AudioSource�R���|�[�l���g��ǉ�
            audioSource.PlayOneShot(SE_explosion);  //����SE�Đ�
            m_UseMine++;
            GameObject explosion = Instantiate(m_ExplosionEffectPrefab, transform.position, Quaternion.identity);
            explosion.GetComponent<Explosion>().SetBombType(0);
            Debug.Log("1");
        }
        SetCheck(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        OutCheck(collision);
    }
    void Update()
    {
        aaa();
    }
    public override void SetCount()
    {
        m_SetMine++;
        Debug.Log(m_SetMine);
    }
    public static GameMineData GetGameMineData()
    {
        m_MineKill=Explosion.m_KillCount[0];
        return new GameMineData(m_SetMine, m_UseMine,m_MineKill);
    }
    public static void ResetMineData()
    {
        m_SetMine = 0;
        m_UseMine = 0;
        m_MineKill = 0;
    }
}
