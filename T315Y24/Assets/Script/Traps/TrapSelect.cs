/*=====
<TrapSelect.cs> 
���쐬�ҁFyamamoto

�����e
�z�u����㩂�I�����邽�߂̃X�N���v�g

���X�V����
__Y24   
_M06    
D
12: �v���O�����쐬: yamamoto
18: SE�ǉ�: nieda
26: �R�����g�ǉ�: yamamoto
27: SE�֌W���t�@�N�^�����O: nieda
=====*/

//�����O��Ԑ錾
using Effekseer;
using EffekseerTool.Data.Value;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static CCodingRule;
using static CFeatures;
using static InputDeviceManager;

//���N���X��`
public class CTrapSelect : CMonoSingleton<CTrapSelect>
{
    //���ϐ��錾
    private GameObject player;

    //���\���̒�`
    //[Serializable]
    //public struct TrapInfo
    //{
    //    [Tooltip("��������I�u�W�F�N�g")]public GameObject m_Trap;
    //    [Tooltip("�\������UI")]public Image m_Image;                  
    //    [Tooltip("�R�X�g")]public int m_Cost;                         // �R�X�g
    //    [Tooltip("�\���p��Text")]public TMP_Text m_CostText;          
    //}
    [Serializable]
    private struct OutputTrapInfo
    {
        [Tooltip("UI�\��")] public Image m_Image;    // UI
        [Tooltip("�R�X�g�\���e�L�X�g")] public TMP_Text m_CostText;  // �R�X�g�e�L�X�g
    }   //㩂̏��\���ꏊ
     
    [Header("㩂̏��")]
    [SerializeField, Tooltip("㩕\��")] private OutputTrapInfo[] m_TrapInfo;   //㩕\���p���

    [Header("�X�e�[�^�X")]
    [SerializeField, Tooltip("�ŏ����玝���Ă���R�X�g")] private int m_FirstCost;
    [Tooltip("�R�X�g�𑝂₷�Ԋu(�b)")]public float m_fIncreaseInterval = 5.0f;  // �R�X�g�𑝂₷�Ԋu�i�b�j
    [SerializeField, Tooltip("�I���m��L�[")] KeyCode m_DecideKey;    //����L�[
    [Tooltip("�G��Ȃ���")] public bool m_bSelect=true;     // true:�I���\�@false:�I��s��
    private int m_nNum;             // ���I��ł���㩂̔ԍ����i�[
    public static int m_nCost;

    [Header("��")]
    [Tooltip("AudioSource��ǉ�")] private AudioSource m_AudioSource;      // AudioSource��ǉ�
    [SerializeField,Tooltip("㩑I������SE")] private AudioClip SE_Select;  // 㩑I������SE
    [SerializeField,Tooltip("㩐ݒu����SE")] private AudioClip SE_Set;     // 㩐ݒu����SE

    //���v���p�e�B��`
    public int HavableTrapNum => m_TrapInfo.Length;    //���Ă�㩐� = �\�����̗p�Ӑ�
    private CTrap[] TrapComps { get; set; }  //㩂̃R���|�[�l���g����


    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    protected override void Start()
    {
        player = GameObject.Find("Player"); // ����
        m_nNum = 0;                         // ������
        //RectTransform���擾
        RectTransform rectTransform = m_TrapInfo[m_nNum].m_Image.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 200);    // �I�𒆂�UI�̑傫����ύX
        m_AudioSource = GetComponent<AudioSource>();        // AudioSource�R���|�[�l���g��ǉ�
        m_bSelect = true;                                   // �I���\
        m_nCost = m_FirstCost;                               // �����R�X�g
        InputDeviceManager.Instance.OnChangeDeviceType.AddListener(OnChangeDeviceTypeHandler);  //�f�o�C�X������

        TrapComps = new CTrap[HavableTrapNum];

        //��㩕\�����铽
        for (int _nIdx = 0; _nIdx < m_TrapInfo.Length; _nIdx++) //���\���ł���͈͓�
        {
            //m_TrapInfo[_nIdx].m_CostText.gameObject.SetActive(false);  //����Text�������Ȃ�
            //m_TrapInfo[_nIdx].m_Image.gameObject.SetActive(false);  //����Image�������Ȃ�
        }
    }

    private void OnChangeDeviceTypeHandler()
    {
        // ���̓f�o�C�X�̎�ʂ��ύX���ꂽ�Ƃ��̏���
        Debug.Log("���̓f�o�C�X�̎�ʂ��ύX����܂����B\n���݂̓��̓f�o�C�X�̎�ʁF" + InputDeviceManager.Instance.CurrentDeviceType);
    }

    /*���X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
    */
    protected override void Update()
    {
        ////���ۑS
        //if(TrapComps != null && m_TrapInfo != null) //�k���`�F�b�N
        //{
        //    //��㩕\��UI�X�V  ���摜��������(������)���񓯊������Ȃ��߂�CTrap����摜�f�[�^�𐳂����󂯎��Ȃ����Ƃ����邽�߂����ɋL��
        //    for (int _nIdx = 0; _nIdx < m_TrapInfo.Length; _nIdx++) //���\���ł���͈͓�
        //    {
        //        if (TrapComps[_nIdx].ImageSprite != null && m_TrapInfo[_nIdx].m_Image.sprite == null)   //�܂��摜�ݒ肳��Ă��炸�A�ݒ肳���ׂ�
        //        {
        //            m_TrapInfo[_nIdx].m_Image.sprite = TrapComps[_nIdx].ImageSprite;   //Image�ɉ摜��ݒ�
        //        }
        //    }
        //}

        //��㩏��\��������
            for (int _nIdx = 0; _nIdx < CTrapManager.Instance.HaveTraps.Count; _nIdx++) //���\���ł���͈͓�
            {
                //������X�V
                if (TrapComps[_nIdx] != null) //�k���`�F�b�N
                {
                    break;  //����ȊO�X�V���Ȃ�
                }

                //���ϐ��錾
                var _Obj = CTrapManager.Instance.HaveTraps[_nIdx]; //�Y��㩎擾
                bool _bMakeObj = false;  //�I�u�W�F�N�g�𐶐�������

                //���ۑS
                if (_Obj == null)  //�k���`�F�b�N
                {
                    //continue;   //�k���A�N�Z�X�h�~
                    Instantiate(_Obj, Vector3.zero, Quaternion.identity);  //�ꎞ�I�Ƀk������Ȃ�����
                    _bMakeObj = true;  //��������
                }

                //���ϐ��錾
                var _Trap = _Obj.GetComponent<CTrap>(); //㩂̃R���|�[�l���g�����o��

                //���ۑS
                if (_Trap == null)  //�Y���R���|�[�l���g���Ȃ�
                {
                    if (_bMakeObj)  //�������Ă�����
                    {
                        Destroy(_Obj);  //���������ɖ߂�
                    }
                    continue;   //�k���A�N�Z�X�h�~
                }

                //��������
                TrapComps[_nIdx] = _Trap;   //�R���|�[�l���g�o�^
                m_TrapInfo[_nIdx].m_CostText.SetText($"{_Trap.Cost}");  //Text�ɃR�X�g�l���Z�b�g
            m_TrapInfo[_nIdx].m_CostText.gameObject.SetActive(true);  //�X�V��Text��������
            m_TrapInfo[_nIdx].m_Image.sprite = _Trap.ImageSprite;   //Image�ɉ摜��ݒ�
            m_TrapInfo[_nIdx].m_Image.gameObject.SetActive(true);  //�X�V��Image��������

            //���Еt��
            if (_bMakeObj)  //�������Ă�����
                {
                    Destroy(_Obj);  //���������ɖ߂�
                }
        }

        //��㩕\��UI�X�V  ���摜��������(������)���񓯊������Ȃ��߂�CTrap����摜�f�[�^�𐳂����󂯎��Ȃ����Ƃ����邽�߂����ɋL��
        for (int _nIdx = 0; _nIdx < m_TrapInfo.Length; _nIdx++) //���\���ł���͈͓�
        {
            //���ۑS
            if (m_TrapInfo[_nIdx].m_Image.sprite != null || TrapComps[_nIdx] == null) //�k���`�F�b�N
            {
                continue;
            }
            if (TrapComps[_nIdx].ImageSprite != null && m_TrapInfo[_nIdx].m_Image.sprite == null)   //�܂��摜�ݒ肳��Ă��炸�A�ݒ肳���ׂ�
            {
                m_TrapInfo[_nIdx].m_Image.sprite = TrapComps[_nIdx].ImageSprite;   //Image�ɉ摜��ݒ�
            }
        }

        if (m_bSelect)
        {//�I���\�Ȃ�
            Select();   //�I��
            
            if (InputDeviceManager.Instance != null)
            {
                //���ϐ��錾
                bool _bDownDesideKey = false;   //����L�[�𗣂�����

                // ���݂̓��̓f�o�C�X�^�C�v���擾
                InputDeviceManager.InputDeviceType currentDeviceType = InputDeviceManager.Instance.CurrentDeviceType;

                //����
                // ���݂̃f�o�C�X�^�C�v�ɉ������������s��
                switch (currentDeviceType)
                {
                    case InputDeviceManager.InputDeviceType.Keyboard:
                        _bDownDesideKey = Input.GetKeyDown(m_DecideKey) && CostCheck(m_nNum); //����L�[���͔���
                        break;
                    case InputDeviceManager.InputDeviceType.Xbox:
                        _bDownDesideKey = Input.GetButtonDown("Decision") && CostCheck(m_nNum); //����L�[���͔���
                        break;
                    case InputDeviceManager.InputDeviceType.DualShock4:
                        _bDownDesideKey = Input.GetButtonDown("Decision") && CostCheck(m_nNum); //����L�[���͔���
                        break;
                    case InputDeviceManager.InputDeviceType.DualSense:
                        _bDownDesideKey = Input.GetButtonDown("Decision") && CostCheck(m_nNum); //����L�[���͔���
                        break;
                    case InputDeviceManager.InputDeviceType.Switch:
                        _bDownDesideKey = Input.GetButtonDown("Decision") && CostCheck(m_nNum); //����L�[���͔���
                        break;
#if UNITY_EDITOR    //�G�f�B�^�g�p��
                    default:
                        Debug.Log("���m�̓��̓f�o�C�X���g�p����Ă��܂�");
                        break;
#endif
                }

                if (_bDownDesideKey) //������͎�
                {//���肵���Ȃ�
                    m_AudioSource.PlayOneShot(SE_Set);   // SE�Đ�
                    Generation(m_nNum);                  //�I�u�W�F�N�g�쐬
                    m_bSelect = false;                   //�I��s��
                    m_bSelect = true;
                }
            }
        }
    }

    /*���I�u�W�F�N�g�����֐�
    �����P�Fint _nNum : �I�����Ă���㩂̔ԍ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�I�u�W�F�N�g�̐���
    */
    private void Generation(int _nNum)
    {
        //�����p
        Vector3 vPos = player.transform.forward * 2;    //�v���C���[�̐��ʕ����̃x�N�g�����擾
                                                        //�I�u�W�F�N�g�̐���
        GameObject TrapObject = Instantiate(CTrapManager.Instance.HaveTraps[_nNum], player.transform.position + vPos, Quaternion.identity);
        TrapObject.SetActive(true); //�R�s�[���̓I�u�W�F�N�g���A�N�e�B�u�łȂ��̂ŃA�N�e�B�u�ɂ���
        TrapObject.GetComponent<CTrap>().m_bSetting = false;    //�ݒu�s��
    }

    /*��㩑I���֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F㩂̑I��
    */
    private void Select()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetButtonDown("Right"))
        {
            m_AudioSource.PlayOneShot(SE_Select);    // SE�Đ�
            ChangeSize(100);                // �T�C�Y��ύX
            m_nNum += 1;                    // ���̔ԍ�
            // 㩂̎�ނ����傫���Ȃ�����ő�ɖ߂�
            if (m_nNum > m_TrapInfo.Length - 1) m_nNum = m_TrapInfo.Length - 1; 
            ChangeSize(200);                // �T�C�Y��ύX
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetButtonDown("Left"))
        {
            m_AudioSource.PlayOneShot(SE_Select);    // SE�Đ�
            ChangeSize(100);                // �T�C�Y��ύX
            m_nNum -= 1;                    // ���̔ԍ�
            if (m_nNum < 0) m_nNum = 0;     // ���̐��ɂȂ�Ȃ��悤��
            ChangeSize(200);                // �T�C�Y��ύX
        }
    }

    /*��UI�T�C�Y�ύX�֐�
    �����P�Fint _nSize : ���̒l�̃T�C�Y�ɕύX
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�FUI�T�C�Y�ύX
    */
    private void ChangeSize(int _nSize)
    {
        // RectTransform�̎擾
        RectTransform rectTransform = m_TrapInfo[m_nNum].m_Image.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(_nSize, _nSize);// �T�C�Y�ύX
    }

    /*���I���\�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�I���\�ɕύX
    */
    public void SetSelect()
    {
        m_bSelect = true;   //�I���\
    }

    /*���I���\�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�I���\�ɕύX
    */
    private bool CostCheck(int _nNum)
    {

        //���ϐ��錾
        var _nCost = m_nCost - TrapComps[_nNum].Cost; //�R�X�g�����̒l

        //����������
        if(_nCost >= 0) //�I���\
        {
            m_nCost = _nCost;   //�l�𔽉f
            return true; // 0�ȏ�Ȃ���Ȃ�
        }
        else
        {//�I��s��
            return false;                   // �I��s��
        }
        //m_Cost -= m_TrapInfo[i].m_Cost;  // ������R�X�g����I������㩂̃R�X�g������
        //if(m_Cost>=0) { return true; }  // 0�ȏ�Ȃ���Ȃ��I���\
        //else
        //{// 0��菬�����Ƃ�
        //    m_Cost += m_TrapInfo[i].m_Cost; // �������R�X�g��߂�
        //    return false;                   // �I��s��
        //}
        
    }
}
