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
=====*/

//�����O��Ԑ錾
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CCodingRule;

//���N���X��`
public class CTrapSelect : MonoBehaviour
{
    //�ϐ��錾
    private GameObject player;

    //���\���̒�`
    [Serializable]
    public struct TrapInfo
    {
        [Tooltip("��������I�u�W�F�N�g")]public GameObject m_Trap;    // �I�u�W�F�N�g
        [Tooltip("�\������UI")]public Image m_Image;                  // UI
        [Tooltip("�R�X�g")]public int m_Cost;                         // �R�X�g
        [Tooltip("�\���p��Text")]public TMP_Text m_CostText;          // �R�X�g�e�L�X�g
    }
    [Header("㩂̏��")]
    [SerializeField,Tooltip("㩂̏��")] private TrapInfo[] m_TrapInfo;    //㩂̏��

    [Header("�X�e�[�^�X")]
    [SerializeField, Tooltip("�ŏ����玝���Ă���R�X�g")] private int m_FirstCost;
    [Tooltip("�R�X�g�𑝂₷�Ԋu(�b)")]public float m_fIncreaseInterval = 5.0f;  // �R�X�g�𑝂₷�Ԋu�i�b�j
    [Tooltip("�G��Ȃ���")] public bool m_bSelect=true;     // true:�I���\�@false:�I��s��
    private int m_nNum;             // ���I��ł���㩂̔ԍ����i�[
    public static int m_Cost;

    [Header("��")]
    [SerializeField,Tooltip("㩑I������SE")] public AudioClip SE_Select;  // 㩑I������SE
    [SerializeField,Tooltip("㩐ݒu����SE")] public AudioClip SE_Set;  // 㩐ݒu����SE
    AudioSource m_As; // AudioSource��ǉ�


    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    void Start()
    {
        player = GameObject.Find("Player"); // ����
        m_nNum = 0;                         // ������
        //RectTransform���擾
        RectTransform rectTransform = m_TrapInfo[m_nNum].m_Image.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 200);    // �I�𒆂�UI�̑傫����ύX
        m_As = GetComponent<AudioSource>();                 // AudioSource�R���|�[�l���g��ǉ�
        m_bSelect = true;                                   // �I���\
        m_Cost = m_FirstCost;                               // �����R�X�g
        m_TrapInfo[0].m_CostText.SetText($"{m_TrapInfo[0].m_Cost}");  //Text���Z�b�g
        m_TrapInfo[1].m_CostText.SetText($"{m_TrapInfo[1].m_Cost}");  //Text���Z�b�g
    }

    /*���X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
    */
    void Update()
    {
        if (m_bSelect)
        {//�I���\�Ȃ�
            Select();   //�I��
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Decision"))&& CostCheck(m_nNum))
            {//���肵���Ȃ�
                m_As.PlayOneShot(SE_Set);   // SE�Đ�
                Generation(m_nNum);         //�I�u�W�F�N�g�쐬
                m_bSelect = false;          //�I��s��
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
        GameObject TrapObject= Instantiate(m_TrapInfo[_nNum].m_Trap, player.transform.position + vPos, Quaternion.identity);
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
            m_As.PlayOneShot(SE_Select);    // SE�Đ�
            ChangeSize(100);                // �T�C�Y��ύX
            m_nNum += 1;                    // ���̔ԍ�
            // 㩂̎�ނ����傫���Ȃ�����ő�ɖ߂�
            if (m_nNum > m_TrapInfo.Length - 1) m_nNum = m_TrapInfo.Length - 1; 
            ChangeSize(200);                // �T�C�Y��ύX
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetButtonDown("Left"))
        {
            m_As.PlayOneShot(SE_Select);    // SE�Đ�
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
    private bool CostCheck(int i)
    {
        m_Cost-= m_TrapInfo[i].m_Cost;  // ������R�X�g����I������㩂̃R�X�g������
        if(m_Cost>=0) { return true; }  // 0�ȏ�Ȃ���Ȃ��I���\
        else
        {// 0��菬�����Ƃ�
            m_Cost += m_TrapInfo[i].m_Cost; // �������R�X�g��߂�
            return false;                   // �I��s��
        }
        
    }
}
