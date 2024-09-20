using Effekseer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


//��Data�N���X��`
public class GameCutterData
{
    //���v���p�e�B��`
    public int SetCutter { get; set; }    //�u������
    public int UseCutter { get; set; }    //�g������
    public int CutterKill { get; set; }   //�|������

    /*���R���X�g���N�^
   �����P�Fint _nSetRemoteBomb �F�u������   
   �����Q�Fint _nUseRemoteBomb �F�g������  
   �����R�Fint _nRemoteBombKill�F�|������ 
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�f�[�^�����U���g�ɓn���悤�ɂ܂Ƃ߂�
   */
    public GameCutterData(int _nSetCutter, int _nUseCutter, int _nCutterKill)
    {
        //�e�f�[�^���Z�b�g
        SetCutter = _nSetCutter;     //�u������ 
        UseCutter = _nUseCutter;     //�g������
        CutterKill = _nCutterKill;   //�|������ 
    }
}
public class RotaryCutter : CTrap
{
    [SerializeField, Tooltip("UI�\���p�摜")] private /*static*/ AssetReferenceTexture2D m_UIAssetRefCutter; //UI�p�摜�A�Z�b�g
    private static Sprite m_ImageSpriteCutter; //UI�A�Z�b�g�摜
    private static AsyncOperationHandle<Texture2D> m_AssetLoadHandleCutter;   //�A�Z�b�g�����[�h�E�Ǘ�����֐�

    [SerializeField, Tooltip("��]���x")] private float rotationSpeed = 90.0f; // ��]���x (�x/�b)
    private static int m_nSetCutter;       //�u������ 
    private static int m_nUseCutter;       //�g������
    private static int m_nCutterKill;      //�|������ 

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
        MakeSprite();   //�ŏ��ɉ摜�����
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
        if (!m_bMove)
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        SetTrap();  //�ݒu�֐��Ăяo��
    }
  

    /*���摜�ϊ��֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FAdressable�ɓo�^�����摜��Sprite�`���ɕϊ�
    */
    private async void MakeSprite()
    {
        //���ۑS
        if (m_UIAssetRefCutter == null)  //�����A�Z�b�g���Ȃ�
        {
            //�����f
            return; //�������Ȃ�
        }

        //���摜�ǂݍ���
        var _AssetLoadHandle = Addressables.LoadAssetAsync<Texture2D>(m_UIAssetRefCutter);  //�e�N�X�`���f�[�^��ǂݍ��ފ֐��擾
        var _Texture = await _AssetLoadHandle.Task; //�e�N�X�`���ǂݍ��݂�񓯊��Ŏ��s
        m_ImageSpriteCutter = Sprite.Create(_Texture, new Rect(0, 0, _Texture.width, _Texture.height), Vector2.zero);   //�e�N�X�`������摜�f�[�^�쐬

        //���Ǘ�
        m_AssetLoadHandleCutter = _AssetLoadHandle;    //�g�p���Ă���֐����Ǘ�
    }

    /*���n�������蔻��֐�
      �����P�FCollision _Collision : �������Ă�����̂̏��
      ��
      �ߒl�F�Ȃ�
      ��
      �T�v�F�G���n���ɐG�ꂽ�Ƃ��̂ݏ��������
      */
    private void OnCollisionStay(Collision collision)     //�n���ɉ������������Ă����Ƃ�
    {
        Debug.Log("aaa");
        if (Check(collision, true))  // �n�ɓG���������Ă邩
        {
            Debug.Log("bbbb");
            var contactPoints = collision.contacts;
            foreach (var contact in contactPoints)
            {
                var collidingObject = contact.otherCollider.gameObject;
                if (collidingObject.TryGetComponent<IFeatureMine>(out var destroyable))
                {
                    destroyable.TakeDestroy();      //�G�폜
                    m_nCutterKill++;
                }
            }
        }
        if (m_bMove)
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

    /*���ݒu�񐔃J�E���g�֐�
   �����P�FCollision _Collision : �������Ă�����̂̏��
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�ݒu���ꂽ�Ƃ��ɌĂяo�����
   */
    public override void SetCount()
    {
        m_nSetCutter++;
    }


    /*���f�[�^�����n���֐�
  �����F�Ȃ�
  ��
  �ߒl�FGameCutterData(m_nSetCutter, m_nUseCutter,m_nCutterKill):�f�[�^
  ��
  �T�v�F���U���g�ɓn���p�̃f�[�^���쐬
  */
    public static GameCutterData GetGameCutterData()
    {
        //���U���g�ɓn���p�̃f�[�^���쐬
        return new GameCutterData(m_nSetCutter, m_nUseCutter, m_nCutterKill);
    }
    /*���f�[�^���Z�b�g�֐�
 �����F�Ȃ�
 ��
 �ߒl�F�Ȃ�
 ��
 �T�v�F������
 */
    public static void ResetCutterData()
    {
        m_nSetCutter = 0;     //�u������ ������
        m_nUseCutter = 0;     //�g�����񏉊���
        m_nCutterKill = 0;    //�|������ ������
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
        if (m_AssetLoadHandleCutter.IsValid()) //�k���łȂ�
        {
            Addressables.Release(m_AssetLoadHandleCutter); //�Q�Ƃ���߂�
        }
    }
}
