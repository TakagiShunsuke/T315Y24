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
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
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
    [Header("�X�e�[�^�X")]
    [SerializeField, Tooltip("�R�X�g")] private /*static*/ int m_nCostRemoteBomb; // �R�X�g
    //[Header("UI�C���[�W")]
    //[SerializeField, Tooltip("UI�\���p�摜")] private /*static*/ AssetReferenceTexture2D m_UIAssetRefRemoteBomb; //UI�p�摜�A�Z�b�g
    private static AsyncOperationHandle<Texture2D> m_AssetLoadHandleRemoteBomb;   //�A�Z�b�g�����[�h�E�Ǘ�����֐�
    private static int m_nSetRemoteBomb;     //�u�������i�[�p
    private static int m_nUseRemoteBomb;     //�g�����񐔊i�[�p
    private static int m_nRemoteBombKill; //�|�������i�[�p
    [Header("UI�C���[�W")]
    [SerializeField, Tooltip("UI�\���p�摜")] private Texture2D m_ImageSpriteRemoteBomb; //UI�A�Z�b�g�摜

    //���v���p�e�B��`
    public override int Cost => m_nCostRemoteBomb; //�R�X�g
    public override Sprite ImageSprite => Sprite.Create(m_ImageSpriteRemoteBomb, new Rect(0, 0, m_ImageSpriteRemoteBomb.width, m_ImageSpriteRemoteBomb.height), Vector2.zero); //UI�A�Z�b�g���摜�ɕϊ���������


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
    //    if (m_UIAssetRefRemoteBomb == null)  //�����A�Z�b�g���Ȃ�
    //    {
    //        //�����f
    //        return; //�������Ȃ�
    //    }

    //    //���摜�ǂݍ���
    //    var _AssetLoadHandle = Addressables.LoadAssetAsync<Texture2D>(m_UIAssetRefRemoteBomb);  //�e�N�X�`���f�[�^��ǂݍ��ފ֐��擾
    //    var _Texture = await _AssetLoadHandle.Task; //�e�N�X�`���ǂݍ��݂�񓯊��Ŏ��s
    //    m_ImageSpriteRemoteBomb = Sprite.Create(_Texture, new Rect(0, 0, _Texture.width, _Texture.height), Vector2.zero);   //�e�N�X�`������摜�f�[�^�쐬

    //    //���Ǘ�
    //    m_AssetLoadHandleRemoteBomb = _AssetLoadHandle;    //�g�p���Ă���֐����Ǘ�
    //}

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
        if (m_AssetLoadHandleRemoteBomb.IsValid()) //�k���łȂ�
        {
            Addressables.Release(m_AssetLoadHandleRemoteBomb); //�Q�Ƃ���߂�
        }
    }
}
