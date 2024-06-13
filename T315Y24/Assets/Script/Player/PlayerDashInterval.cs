/*=====
<PlayerDashInterval.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�_�b�V���̃C���^�[�o����\������

�����ӎ���
����̃I�u�W�F�N�g�Ɉȉ��̃R���|�[�l���g���Ȃ��Ə\���ȋ@�\�����܂���B
�P.�v���C���[�̏�񂪊Ǘ�����Ă���CPlayerScript


���X�V����
__Y24
_M05
D
21:�v���O�����쐬:takagi
25:�Z�}���e�B�N�X�C��:takagi
31:���t�@�N�^�����O:takagi

_M06
D
13:�E���C��:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;  //Unity

//���N���X��`
public class CPlayerDashInterval : MonoBehaviour
{
    //���萔��`
    private const string TEXT_DASH_ABLE = "�_�b�V���\"; //�_�b�V���\���̕\���e�L�X�g
    private const string TEXT_DASH_UNABLE = "�_�b�V���s�\";   //�_�b�V���s�\���̕\���e�L�X�g

    //���ϐ��錾
    private CPlayerScript m_Player = null;  //�v���C���[�̏��
    private double m_dCurData;  //���݊Ǘ����Ă�����
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI; //�C���^�[�o���\���ꏊ

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
        m_Player = GetComponent<CPlayerScript>(); //�v���C���[�Ƃ��Ă̐U�镑�����擾
        if (m_Player != null)   //�擾�ɐ���������
        {
            m_dCurData = m_Player.DashCntDwn;   //�f�[�^������
            UpdateText();   //�e�L�X�g������
        }
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        else   //�擾�Ɏ��s������
        {
            //���G���[�o��
            UnityEngine.Debug.LogError("�v���C���[�ɐݒ肳��Ă��܂���");    //�x�����O�o��
        }
#endif
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        if (m_TextMeshProUGUI == null)   //�擾�Ɏ��s������
        {
            //���G���[�o��
            UnityEngine.Debug.LogWarning("���͐�e�L�X�g���ݒ肳��Ă��܂���");    //�x�����O�o��s
        }
#endif
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

        //���X�V
        if (m_dCurData != m_Player.DashCntDwn)  //�����f�[�^�ɍX�V���K�v�Ȏ�
        {
            m_dCurData = m_Player.DashCntDwn;   //�f�[�^�X�V
            UpdateText();    //�e�L�X�g�X�V
        }
    }

    /*���e�L�X�g�X�V�֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�e�L�X�g�̓��e�X�V
    */
    private void UpdateText()
    {
        //������
        if (m_TextMeshProUGUI == null)   //�K�v�v���̕s����
        {
#if UNITY_EDITOR    //�G�f�B�^�g�p��
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�K�v�ȗv�f���s�����Ă��܂�");  //�x�����O�o��
#endif

            //�����f
            return; //�X�V�������f
        }

        //����ԕ���
        if (m_dCurData > 0)   //�J�E���g�_�E����
        {
            m_TextMeshProUGUI.text = TEXT_DASH_UNABLE + "\n�c��F" + ((uint)m_dCurData).ToString().PadRight(2) + "s"; //�_�b�V���s�\
        }
        else
        {   //�J�E���g�_�E������Ă��Ȃ�
            m_TextMeshProUGUI.text = TEXT_DASH_ABLE;    //�_�b�V���\
        }
    }
}