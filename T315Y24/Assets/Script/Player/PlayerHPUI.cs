/*=====
<PlayerHPUI.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�v���C���[��HP��UI�\������

�����ӎ���
����̃I�u�W�F�N�g�Ɉȉ��̃R���|�[�l���g���Ȃ��Ə\���ȋ@�\�����܂���B
�P.�v���C���[�̏�񂪊Ǘ�����Ă���CPlayerScript


���X�V����
__Y24
_M05
D
15:�v���O�����쐬:takagi
16:����:takagi
31:���t�@�N�^�����O:takagi
=====*/

//�����O��Ԑ錾
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;  //Unity
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

//���N���X��`
public class CPlayerHPUI : MonoBehaviour
{
    //���萔��`
    private const string CANVAS_NAME = "PlCanvas";  //�L�����o�X��
    private const string IMAGE_NAME = "Pl_Heart";   //�摜�I�u�W�F�N�g��

    //���ϐ��錾    
    [SerializeField] protected AssetReferenceTexture2D m_AssetRef; //�����ΏۃA�Z�b�g
    [SerializeField] private Vector2 m_FirstDrawPos = new Vector2(120.0f, 80.0f);   //UI��ڂ̕\���ʒu
    [SerializeField] private double m_dInterval;    //���������Ԋu
    [SerializeField] private double m_dImageScale = 1.6f;   //�摜�̃X�P�[�����O
    private GameObject m_CanvasObj; //UI�\���̂��߂̃L�����o�X�p�I�u�W�F�N�g
    private CPlayerScript m_Player = null;    //�v���C���[�̏��
    List<AsyncOperationHandle<Texture2D>> m_AssetLoadHandle= new List<AsyncOperationHandle<Texture2D>>();   //�A�Z�b�g�����[�h�E�Ǘ�����֐�


    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    public void Start()
    {
        //��������
        m_CanvasObj = new GameObject(); //�L�����o�X�I�u�W�F�N�g�쐬
        m_Player = GetComponent<CPlayerScript>(); //�v���C���[�Ƃ��Ă̐U�镑�����擾
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        if (m_Player == null)   //�擾�Ɏ��s������
        {
            //���G���[�o��
            UnityEngine.Debug.LogError("�v���C���[�ɐݒ肳��Ă��܂���");    //�x�����O�o��
        }
#endif   

        //���L�����o�X�쐬�E�ݒ�
        Canvas _Canvas = m_CanvasObj.AddComponent<Canvas>(); //�@�\���L�����o�X��
        _Canvas.renderMode = RenderMode.ScreenSpaceOverlay; //UI���őO�ʂɏo��
        _Canvas.AddComponent<CanvasScaler>();   //UI�̃X�P�[������
        _Canvas.AddComponent<GraphicRaycaster>();   //�L�����o�X�ւ̃��C����
        _Canvas.name = CANVAS_NAME;  //���O�t��
        _Canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1;   //�V�F�[�_�[�Z�}���e�B�N�X�F�e�N�X�`�����W
        _Canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.Normal;  //�V�F�[�_�[�Z�}���e�B�N�X�F�@��
        _Canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.Tangent; //�V�F�[�_�[�Z�}���e�B�N�X�F�ڐ�
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
        //������
        if (m_Player == null)   //�K�v�v���̕s����
        {
#if UNITY_EDITOR    //�G�f�B�^�g�p��
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�K�v�ȗv�f���s�����Ă��܂�");  //�x�����O�o��
#endif

            //�����f
            return; //�X�V�������f
        }

        //��HP�����Ή�
        for (int _nIdx = m_CanvasObj.transform.childCount; _nIdx < m_Player.HP; _nIdx++)    //�q�I�u�W�F�N�g�̐���HP�ɍ��킹�đ��₷(�����_�ȉ��؂�̂�)
        {
            //���C���[�W�쐬
            MakeHPUI(_nIdx);    //HP���쐬����
        }

        //��HP�����Ή�
        for (int _nIdx = m_Player.HP >= 0.0d ? (int)m_Player.HP : 0;     //�C���[�W�̓}�C�i�X�ɂł��Ȃ�
            _nIdx < m_CanvasObj.transform.childCount; _nIdx++)   //�q�I�u�W�F�N�g�̐���HP�ɍ��킹�Č��炷(�����_�ȉ��؂�̂�)
        {
            //���I�u�W�F�N�g����
            Destroy(m_CanvasObj.transform.GetChild(_nIdx).gameObject);  //��ԍŌ�ɍ��ꂽ�q�I�u�W�F�N�g���폜
        }
    }

    /*��HPUI�쐬�֐�
    �����Fint HPIdx�F�쐬����UI�����Ԗڂ̂��̂�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FHP��UI�摜���쐬�E�z�u
    */
    private async void MakeHPUI(int HPIdx)
    {
        //���ϐ��錾
        GameObject _ImageObj = new GameObject(); //�摜�\���p�I�u�W�F�N�g

        //��������
        _ImageObj.name = IMAGE_NAME; //���O�t��
        _ImageObj.transform.parent = m_CanvasObj.transform;   //�q�I�u�W�F�N�g�ɒǉ�

        //���摜�ǂݍ���
        var _AssetLoadHandle = Addressables.LoadAssetAsync<Texture2D>(m_AssetRef);  //�e�N�X�`���f�[�^��ǂݍ��ފ֐��擾
        var _Texture = await _AssetLoadHandle.Task; //�e�N�X�`���ǂݍ��݂�񓯊��Ŏ��s
        var _Sprite = Sprite.Create(_Texture, new Rect(0, 0, _Texture.width, _Texture.height), Vector2.zero);   //�e�N�X�`������摜�f�[�^�쐬
        _ImageObj.AddComponent<Image>().sprite = _Sprite; //�摜�\�����@�\�I�Ɏ������A�摜�o�^

        //�����ʃ|���S��
        var _RectTransform = _ImageObj.GetComponent<RectTransform>();   //�擾
        if (_RectTransform != null)   //�擾�ɐ���������
        {
            _RectTransform.position = new Vector2(m_FirstDrawPos.x + (float)m_dInterval * (HPIdx % 5),
                Screen.currentResolution.height - m_FirstDrawPos.y - (float)m_dInterval * (HPIdx / 5)); //�|���S���̈ʒu
            _RectTransform.localScale = new Vector2((float)m_dImageScale, (float)m_dImageScale);    //�|���S���̃X�P�[�����O
        }
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        else
        {
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�摜���o�^�ł��܂���");   //�x�����O�o��
        }
#endif
        
        //���X�V
        m_AssetLoadHandle.Add(_AssetLoadHandle);    //�g�p���Ă���֐����Ǘ�
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
        for (int nIdx = 0; nIdx < m_AssetLoadHandle.Count; nIdx++)  //���������ׂĔj������
        {
            if (m_AssetLoadHandle[nIdx].IsValid()) //�k���łȂ�
            {
                Addressables.Release(m_AssetLoadHandle[nIdx]); //�Q�Ƃ���߂�
            }
        }
    }
}