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
using Effekseer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
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
    //[SerializeField,Tooltip("���������������v���n�u")] private GameObject m_ExplosionEffectPrefab; // ���������������v���n�u
    [SerializeField,Tooltip("�����̔���p�v���n�u")] private GameObject m_ExplosionCollPrefab; // ���������������v���n�u
    [SerializeField, Tooltip("�������Đ�����G�t�F�N�g")] private  EffekseerEffectAsset m_ExplosionEffect;  // �������Đ�����G�t�F�N�g
    [Header("�X�e�[�^�X")]
    [SerializeField, Tooltip("�R�X�g")] private /*static*/ int m_nCostMine; // �R�X�g //static���ƃC���X�y�N�^�ɕ\������Ȃ�
    //[Header("UI�C���[�W")]
    //[SerializeField, Tooltip("UI�\���p�摜")] private /*static*/ AssetReferenceTexture2D m_UIAssetRefMine; //UI�p�摜�A�Z�b�g
    private static AsyncOperationHandle<Texture2D> m_AssetLoadHandleMine;   //�A�Z�b�g�����[�h�E�Ǘ�����֐�
    private static int m_nSetMine;       //�u������ 
    private static int m_nUseMine;       //�g������
    private static int m_nMineKill;      //�|������ 
    //private static Sprite m_ImageSpriteMine; //UI�A�Z�b�g�摜
    [Header("UI�C���[�W")]
    [SerializeField, Tooltip("UI�\���p�摜")] private Texture2D m_ImageSpriteMine; //UI�A�Z�b�g�摜

    //���v���p�e�B��`
    public override int Cost => m_nCostMine; //�R�X�g
    //public override Sprite ImageSprite => m_ImageSpriteMine; //UI�A�Z�b�g���摜�ɕϊ���������
    public override Sprite ImageSprite => Sprite.Create(m_ImageSpriteMine, new Rect(0, 0, m_ImageSpriteMine.width, m_ImageSpriteMine.height), Vector2.zero); //UI�A�Z�b�g���摜�ɕϊ���������


    /*���������֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X��������ɍs������
    */
    private void Awake()
    {
        //��������
        //MakeSprite();   //�ŏ��ɉ摜�����
    }

    /*���摜�ϊ��֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FAdressable�ɓo�^�����摜��Sprite�`���ɕϊ�
    */
    //private async void MakeSprite()
    //{
    //    //���ۑS
    //    if (m_UIAssetRefMine == null)  //�����A�Z�b�g���Ȃ�
    //    {
    //        //�����f
    //        return; //�������Ȃ�
    //    }

    //    //���摜�ǂݍ���
    //    var _AssetLoadHandle = Addressables.LoadAssetAsync<Texture2D>(m_UIAssetRefMine);  //�e�N�X�`���f�[�^��ǂݍ��ފ֐��擾
    //    var _Texture = await _AssetLoadHandle.Task; //�e�N�X�`���ǂݍ��݂�񓯊��Ŏ��s
    //    m_ImageSpriteMine = Sprite.Create(_Texture, new Rect(0, 0, _Texture.width, _Texture.height), Vector2.zero);   //�e�N�X�`������摜�f�[�^�쐬

    //    //���Ǘ�
    //    m_AssetLoadHandleMine = _AssetLoadHandle;    //�g�p���Ă���֐����Ǘ�
    //}

    /*���n�������蔻��֐�
    �����P�FCollision _Collision : �������Ă�����̂̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�G���n���ɐG�ꂽ�Ƃ��̂ݏ��������
    */
    private void OnCollisionStay(Collision collision)     //�n���ɉ������������Ă����Ƃ�
    {
        //���ۑS
        if(m_ExplosionEffect == null)   //�G�t�F�N�g���Ȃ�
        {
#if UNITY_EDITOR    //�G�f�B�^�g�p��
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�K�v�ȗv�f���s�����Ă��܂�");  //�x�����O�o��
#endif
            //�����f
            return; //�������Ȃ�
        }

        if (Check(collision,false))  // �N���ł��邩
        {
            m_audioSource.PlayOneShot(SE_ExpTrap);  //����SE�Đ�
            m_nUseMine++;    //�g�����񐔂𑝂₷

            //�������G�t�F�N�g�Đ�
            EffekseerSystem.PlayEffect(m_ExplosionEffect, transform.position);  //�����ʒu�ɍĐ�

            //��������쐬
            GameObject explosion = Instantiate(m_ExplosionCollPrefab, transform.position, Quaternion.identity);
            explosion.GetComponent<Explosion>().SetBombType(0);//�i�[���ݒ�
        }
        if(m_bMove)
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

    /*���j���֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�j�����ɍs������
    */
    private void OnDestroy()
    {
        //�����
        if (m_AssetLoadHandleMine.IsValid()) //�k���łȂ�
        {
            Addressables.Release(m_AssetLoadHandleMine); //�Q�Ƃ���߂�
        }
    }
}
