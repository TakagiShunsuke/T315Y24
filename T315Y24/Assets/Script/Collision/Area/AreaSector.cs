/*=====
<AreaSector.cs>
���쐬�ҁFtakagi

�����e
�̈攻��(��`)

�����ӎ���
�����蔻����������ϐ�����ĐM���𑗐M���邽�߁A������󂯎���ď������s���Ă��������B

�Ȃ��A�ȉ��̃I�u�W�F�N�g�����݂���K�v������܂��B
�P.m_sPlayerName�Œ�`���ꂽ���O�ƈ�v����I�u�W�F�N�g


���X�V����
__Y24
_M05
D
03:�v���O�����쐬:takagi
04:����:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections;   //list
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;  //Unity

//���N���X��`
public class CAreaSector : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] private double m_dRadius = 2.0d;   //���a
    [SerializeField] private double m_dSectorAngle = 90.0d; //��`�̊p
    [SerializeField] private double m_dFrontAngle = 90.0d;  //xz���ʏ�Ő��ʕ����̊p�x
    private GameObject m_Player = null; //�v���C���[
    [SerializeField] private string m_sPlayerName = "Player";   //�v���C���[�̃I�u�W�F�N�g��

    //���v���p�e�B��`
    public bool SignalCollision { get; private set; } = false;  //�����蔻��̃V�O�i��

    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    // Start is called before the first frame update
    void Start()
    {
        //��������
        m_Player = GameObject.Find(m_sPlayerName);   //�v���C���[�̃C���X�^���X�i�[
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        if (m_Player == null)    //�擾�Ɏ��s������
        {
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�v���C���[��������܂���");  //�x�����O�o��
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

        //���ϐ��錾
        Vector2 m_vDirction = new((float)Math.Cos(Mathf.Deg2Rad * (-transform.eulerAngles.y + m_dFrontAngle)),�@
            (float)Math.Sin(Mathf.Deg2Rad * (-transform.eulerAngles.y + m_dFrontAngle)));   //���ʂ̃x�N�g��  ��y����]�̕����͍��W�n�Ƌt����
        Vector2 m_vToPlayer = new(m_Player.transform.position.x - transform.position.x,
            m_Player.transform.position.z - transform.position.z);  //�v���C���[�����ւ̃x�N�g��

        //��������
        SignalCollision = false; //�����蔻�菉����

        //����`�͈�
        if (m_vToPlayer.magnitude <= m_dRadius && Vector2.Angle(m_vToPlayer, m_vDirction) <= m_dSectorAngle / 2.0d)   //�x�N�g���̒��������a�ȉ��A���ʂ̃x�N�g���Ɗp�x����`�̊p�̔����ȉ�
        {
            //�������蔻��
            SignalCollision = true; //�����蔻��X�V
        }

#if UNITY_EDITOR && DEBUG    //�G�f�B�^�g�p�����f�o�b�O��
        //���ϐ��錾�E������
        Vector3 m_vDirctCent = new(m_vDirction.x, 0.0f, m_vDirction.y);  //��`�̒�������
        m_vDirctCent = m_vDirctCent.normalized *(float)m_dRadius;   //�傫��������
        Vector3 m_vDirctLeft = Quaternion.Euler(0.0f, (float)(m_dSectorAngle / 2.0d), 0.0f) * m_vDirctCent; //��`�̍��[
        Vector3 m_vDirctRight = Quaternion.Euler(0.0f, (float)(-m_dSectorAngle / 2.0d), 0.0f) * m_vDirctCent;   //��`�̉E�[

        //���͈͕\��
        Debug.DrawRay(transform.position + Vector3.up, m_vDirctCent, Color.blue);   //��`�̒����\��
        Debug.DrawRay(transform.position + Vector3.up, m_vDirctLeft, Color.blue);   //��`�̍��[�\��
        Debug.DrawRay(transform.position + Vector3.up, m_vDirctRight, Color.blue);  //��`�̉E�[�\��
#endif
    }
}