/*=====
<ChaseTargetWithRigid.cs>
���쐬�ҁFtakagi

�����e
�ΏےǐՎ��ړ�

�����ӎ���
����̃I�u�W�F�N�g�Ɉȉ��̃R���|�[�l���g���K�v�ł��B
�P.�������Z���s��Rigidbody

���X�V����
__Y24
_M05
D
04:�v���O�����쐬:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections;   //list
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;  //Unity

//���N���X��`
public class CChaseTargetWithRigid : MonoBehaviour, IMove
{
    //���ϐ��錾
    private GameObject m_Target = null; //�ǐՑΏ�
    private Rigidbody m_Rigidbody = null;   //�������Z
    [SerializeField] private double m_Speed = 0.0d; //���x
    [SerializeField] private string m_sTargetName = "Player";   //�ǐՖڕW�̃I�u�W�F�N�g��

    //���v���p�e�B��`
    public double Speed { get { return m_Speed; } set { m_Speed = value; } }    //���x

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
        m_Target = GameObject.Find(m_sTargetName);   //�ǐՖڕW�̃C���X�^���X�i�[
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        if (m_Target == null)    //�擾�Ɏ��s������
        {
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�ǐՑΏۂ�������܂���");  //�x�����O�o��
        }
#endif
        m_Rigidbody = GetComponent<Rigidbody>();    //�������Z�擾
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        if (m_Rigidbody == null)    //�擾�Ɏ��s������
        {
            //���G���[�o��
            UnityEngine.Debug.LogWarning("Rigidbody���ݒ肳��Ă��܂���");    //�x�����O�o��
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
        if (m_Target == null)   //�K�v�v���̕s����
        {
#if UNITY_EDITOR    //�G�f�B�^�g�p��
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�K�v�ȗv�f���s�����Ă��܂�");  //�x�����O�o��
#endif

            //�����f
            return; //�X�V�������f
        }

        //���ϐ��錾�E������
        Vector2 m_vToPlayer = new(m_Target.transform.position.x - transform.position.x,
            m_Target.transform.position.z - transform.position.z);  //�v���C���[�����ւ̃x�N�g��
        var ToPlAngle = Vector2.Angle(Vector2.up, m_vToPlayer); //�v���C���[�Ƃ̂Ȃ��p

        //���v���C���[�֌�����
        m_vToPlayer.Set((float)(m_vToPlayer.normalized.x * Speed), (float)(m_vToPlayer.normalized.y * Speed));  //�ړ���
        //m_Rigidbody.AddForce(new Vector3(m_vToPlayer.x, 0.0f, m_vToPlayer.y).normalized 
        //    * (float)(Math.Clamp((Speed - m_Rigidbody.velocity.magnitude), -4.0d, 4.0d)), ForceMode.Acceleration); //�v���C���[�����ֈړ�
        m_Rigidbody.velocity = new Vector3(m_vToPlayer.x, 0.0f, m_vToPlayer.y); //�ړ������ύX
        m_Rigidbody.rotation = Quaternion.Euler(0.0f, Vector2.Angle(Vector2.left, m_vToPlayer) > 90.0f ? ToPlAngle : -ToPlAngle, 0.0f) ;//�v���C���[������                            //�������̃x�N�g���Ƃ̂Ȃ��p��90�x�����Ȃ獶�Ɍ����B�����łȂ���ΉE�Ɍ����BAngle��0to180�Ȃ��߂�0to360�������\�������B

#if UNITY_EDITOR    //�G�f�B�^�g�p��
        //�����O�o��
        UnityEngine.Debug.Log(m_Rigidbody.velocity.magnitude);   //�U������̑���
#endif
    }
}