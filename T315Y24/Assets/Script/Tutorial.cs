/*=====
<Tutorial.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�`���[�g���A��

�����ӎ���
�V���O���g���ł���
fixed�łȂ�update��timescale�̉e�����󂯂Ȃ����Ƃ𗘗p���Ă���B


���X�V����
__Y24
_M08
D
10:�v���O�����쐬:takagi
_M09
D
05:���͂Ői�߂�/�߂��@�\�ǉ�:takagi
06:�w�i�Ή�:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

//���N���X��`
public class CTutorial : CMonoSingleton<CTutorial>
{
    //���񋓒�`
    private enum E_STATE
    {
        E_STATE_NONE,   //����
        E_STATE_WAIT_INPUT, // ���͑҂�
        E_STATE_PLAYING,    // �`���[�g���A����
    }   //��ԗ�

    //���\���̒�`
    [Serializable] private struct KeyAndButton
    {
        [Tooltip("�L�[�R�[�h")] public KeyCode m_Key;   //�L�[�̒l
        [Tooltip("�{�^����")] public String m_ButtonName;   //�{�^���̖��O
    }   //���͗p�f�[�^����

    //���萔��`
    private const string OBJECT_NAME = "Tutorial"; //���̃I�u�W�F�N�g���������ꂽ�Ƃ��̖��O

    //���ϐ��錾
    [Header("�`���[�g���A�����O����")]
    [SerializeField, Tooltip("�v�s�v�m�F�ꏊ")] private TMP_Text m_TMP; //�v�s�v�m�F�e�L�X�g�o�͏ꏊ
    [SerializeField, Tooltip("�v�s�v�m�F��")] private String m_Confirm; //�v�s�v�m�F�e�L�X�g
    [SerializeField, Tooltip("���́F�͂�")] private KeyAndButton m_YesKey;   //�͂���I��
    [SerializeField, Tooltip("���́F������")] private KeyAndButton m_NoKey;   //��������I��
    [Header("�`���[�g���A���p")]
    [SerializeField, Tooltip("���́F����")] private KeyAndButton m_FrontKey; //����Tips������
    [SerializeField, Tooltip("���́F�߂�")] private KeyAndButton m_BackKey;  //�O��Tips������
    [SerializeField, Tooltip("���́F�I��")] private KeyAndButton m_FinishKey;   //�I����I��
    [SerializeField, Tooltip("UI")] private GameObject m_UI; //�`���[�g���A���pUI
    [SerializeField, Tooltip("�w�i")] private GameObject m_BG; //�`���[�g���A���w�i
    [SerializeField, Tooltip("�q���g�摜")] private List<Sprite> m_Tips = new List<Sprite>(); //�`���[�g���A���摜
    private int m_nTipsIdx = 0;  //�`���[�g���A���摜�̉��Ԗڂ�\�����Ă��邩
    [SerializeField, Tooltip("�摜�\���ꏊ")] private Image m_Image; //�`���[�g���A���摜�\���ꏊ
    private Image m_ImageClone; //�`���[�g���A���摜�̕���
    [SerializeField, Tooltip("�\������")] private float m_fDrawTime; //��̃`���[�g���A���摜�\������
    [SerializeField, Tooltip("�\���֑ؑ��x")] private float m_fSwitchSpeed; //�`���[�g���A���摜�؂�ւ����x
    private float m_fTimer = 0.0f;   //tips�؊��p�^�C�}�[
    private bool m_bVisiableFrontImage = true;  //��O�̉摜�ŕ\�����Ă��邩
    private E_STATE m_eState = E_STATE.E_STATE_NONE;   // ���g�̏��
    private bool m_bFading = false; //�t�F�[�Y�؂�ւ�����


    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    protected override void CustomAwake()
    {
        //�����l�[��
        gameObject.name = OBJECT_NAME;  //���g�̃I�u�W�F�N�g���ύX

        //�����s���Ǘ�
        if(m_TMP)  //�k���`�F�b�N
        {
            m_TMP.gameObject.SetActive(true);   //�������̂��߂Ɉꎞ�I�Ɏg�p
            m_TMP.text = m_Confirm; //�����o��
            m_TMP.gameObject.SetActive(false);  //�܂��g��Ȃ�
        }
        if(m_Image)
        {
            m_Image.gameObject.SetActive(true);    //�������̂��߂Ɉꎞ�I�Ɏg�p
            m_ImageClone = Instantiate(m_Image, m_Image.transform.parent); //�摜�𓯂��K�w�ɃN���[����(���ɕ`�悳���悤��)
            m_ImageClone.gameObject.SetActive(false);   //�܂��g��Ȃ�
            if (m_Tips != null && m_Tips.Count > 0)
            {
                m_Image.sprite = m_Tips[0]; //��ڂ̉摜��o�^
                m_Image.SetNativeSize();    //�摜�T�C�Y�ɍ��킹��
            }
            m_Image.gameObject.SetActive(false);    //�܂��g��Ȃ�
        }
        if(m_BG)
        {
            m_BG.gameObject.SetActive(false);   //�܂��g��Ȃ�
        }
        if(m_UI)
        {
            m_UI.gameObject.SetActive(false);   //�܂��g��Ȃ�
        }
    }

    /*���`���[�g���A���J�n�֐�(�ďo�O��)
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�Ăяo���ꂽ��`���[�g���A�������J�n�B�t�F�[�h�C���p�B
    */
    public void StartTutorial()
    {
        //���ۑS
        if (m_Image == null || m_TMP == null || m_Tips == null || m_Tips.Count == 0)    //�k���`�F�b�N
        {
            Debug.Log("�`���[�g���A�������s�ł��܂���I"); //�G���[�o��
            Finish();
            return; //�������Ȃ�
        }

        //�����Ԓ�~
        Time.timeScale = 0.0f; //���Ԃ��i�s���Ȃ�

        //���`���[�g���A���m�F
        //m_TMP.gameObject.SetActive(true);   //�ŏ��Ɏg�p����
        //m_eState = E_STATE.E_STATE_WAIT_INPUT; //���͂�҂�

        //���`���[�g���A���J�n
        //Destroy(m_TMP); //�e�L�X�g�\���͏I��
        m_UI.gameObject.SetActive(true);    //UI��\��
        m_Image.gameObject.SetActive(true); //�摜��\��
        m_BG.gameObject.SetActive(true);    //�w�i��\��
        m_eState = E_STATE.E_STATE_PLAYING; //�`���[�g���A���J�n
    }

    /*���X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F���Ԓ�~������莞�Ԃ��Ƃɍs���X�V����
    */
    protected override void Update()
    {
        //����ԕ���
        switch (m_eState)   //������Ԃɂ���ĕ���
        {
            case E_STATE.E_STATE_WAIT_INPUT:    //���͑҂�
                WaitInput(); //��p�����ďo
                break;  //����I��
            case E_STATE.E_STATE_PLAYING:   //�`���[�g���A����
                PlayTutorial(); //��p�����ďo
                break;  //����I��
        }
    }

    /*�����͑ҋ@�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F���͂�ҋ@���A�������������s��
    */
    private void WaitInput()
    {
        //�����͑҂�
        if (Input.GetKeyUp(m_YesKey.m_Key) || Input.GetButtonUp(m_YesKey.m_ButtonName)) //�͂��̓���
        {
            Destroy(m_TMP); //�e�L�X�g�\���͏I��
            m_UI.gameObject.SetActive(true);    //UI��\��
            m_Image.gameObject.SetActive(true); //�摜��\��
            m_BG.gameObject.SetActive(true);    //�w�i��\��
            m_eState = E_STATE.E_STATE_PLAYING; //�`���[�g���A���J�n
        }
        else if (Input.GetKeyUp(m_NoKey.m_Key) || Input.GetButtonUp(m_NoKey.m_ButtonName))  //�������̓���
        {
            Finish();   //�`���[�g���A�������I��
        }
    }

    /*���`���[�g���A�������֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�`���[�g���A���̊�{����
    */
    private void PlayTutorial()
    {
        //�����͊Ǘ�
        if (Input.GetKeyUp(m_FinishKey.m_Key) || Input.GetButtonUp(m_FinishKey.m_ButtonName)) //�I���̓���
        {
            Finish();   //�����I��
        }

        //���J�E���^
        float _OldTime = m_fTimer;  //�^�C�}�[�̃J�E���g�ޔ�
        m_fTimer += Time.unscaledDeltaTime; //�^�C�}�[�i�s

        //���摜�؊�
        if (m_bFading)   //����
        {
            //���ϐ��錾
            float _fNewAlpha = 1.0f;    //�����x�ύX�p
            Image _Old = null; //�؊��O�̉摜
            Image _New = null; //�؊���̉摜

            //���V������
            if (m_bVisiableFrontImage) //��̉摜���\������Ă���
            {
                _Old = m_Image;    //�オ�Â�
                _New = m_ImageClone; //�����V����
            }
            else
            { //���̉摜���\������Ă���
                _Old = m_ImageClone;    //�����Â�
                _New = m_Image; //�オ�V����
            }

            _fNewAlpha = Mathf.Max(0.0f, _Old.color.a - Time.unscaledDeltaTime / m_fSwitchSpeed);  //�����x��fps�Ƒ��x�ɔ�Ⴕ�Č���
            if (Mathf.Approximately(_fNewAlpha, 0.0f))   //�����x���ق�0
            {
                //���؊��I��
                _fNewAlpha = 0.0f;  //�����Ȃ����Ă���
                _Old.gameObject.SetActive(false);    //�����Ȃ����Ă���
                m_fTimer = 0.0f;    //�^�C�}�[���Z�b�g
                m_bVisiableFrontImage ^= true;  //�\������Ă���摜���t�]����
                m_bFading = false;  // �t�F�[�Y�؂�ւ�����������
            }
            _Old.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, _fNewAlpha);   //�����x�X�V(����)
            _New.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 1.0f - _fNewAlpha);   //�����x�X�V(����)
        }
        else
        {
            //�����Ԃɂ��\���ؑ�
            if (m_fTimer > m_fDrawTime) //�\�����Ԃ𒴉߂��Ă���
            {
                //�����񔻒�
                if (_OldTime < m_fDrawTime)  //�O��͒��߂������Ȃ�
                {
                    m_nTipsIdx = m_nTipsIdx + 1 < m_Tips.Count ? m_nTipsIdx + 1 : 0;    //�摜�ԍ������X�g���ŏz��
                    ChangeTips();
                }
            }

            //�����͂ɂ��\���ؑ�
            if (Input.GetKeyUp(m_FrontKey.m_Key) || Input.GetButtonUp(m_FrontKey.m_ButtonName)) //�i�߂����
            {
                m_nTipsIdx = m_nTipsIdx + 1 < m_Tips.Count ? m_nTipsIdx + 1 : 0;    //�摜�ԍ���i�߂�
                ChangeTips();
            }
            if (Input.GetKeyUp(m_BackKey.m_Key) || Input.GetButtonUp(m_BackKey.m_ButtonName)) //�߂����
            {
                m_nTipsIdx = m_nTipsIdx - 1 < 0 ? m_Tips.Count - 1 : m_nTipsIdx - 1;    //�摜�ԍ���߂�
                ChangeTips();
            }
        }
    }


    /*���`���[�g���A���\���ؑ֊֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�`���[�g���A���̕\���ؑ֕���
    */
    private void ChangeTips()
    {
        //���ϐ��錾
        Image _New = null; //�؊���̉摜

        //���V������
        if (m_bVisiableFrontImage) //��̉摜���\������Ă���
        {
            _New = m_ImageClone; //�����V����
        }
        else
        { //���̉摜���\������Ă���
            _New = m_Image; //�オ�V����
        }

        //���摜�؊�
        _New.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b,0.0f);
        _New.sprite = m_Tips[m_nTipsIdx];    //���̉摜�����ɕ\��
        _New.gameObject.SetActive(true);    //�J�ڐ�摜����
        
        _New.SetNativeSize();    //�摜�T�C�Y�ɍ��킹��
        
        //���t�F�[�Y�؂�ւ��J�n
        m_bFading = true;   //�t�F�[�h����Ƃ��ɎQ�Ƃ����l
    }

/*���I���֐�
�����F�Ȃ�
��
�ߒl�F�Ȃ�
��
�T�v�F���g�̏�������
*/
private void Finish()
    {
        //�����Ԓ�~����
        Time.timeScale = 1.0f; //���Ԃ��i�s���Ȃ�

        //���j��
        Destroy(gameObject);  // ���g��j�󂷂邱�ƂŃ`���[�g���A�����x�ƕ\�����Ȃ��Ȃ�
    }
}