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
06:�s�v�ȃ��O�폜:takagi

_M06
D
21:���t�@�N�^�����O:takagi
24:���t�@�N�^�����O:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���N���X��`
public class CChaseTargetWithRigid : MonoBehaviour, IMove
{
    //���ϐ��錾
    private Rigidbody m_Rigidbody = null;   //�������Z
    [SerializeField, Tooltip("���x[m/s]")] private double m_Speed; //���x
    [SerializeField, Tooltip("�ǐՑΏ�")] private GameObject m_Target; //�ǐՑΏ�

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
    private void Start()
    {
//        //��������
//        m_Target = GameObject.Find(m_sTargetName);   //�ǐՖڕW�̃C���X�^���X�i�[
//#if UNITY_EDITOR    //�G�f�B�^�g�p��
//        if (m_Target == null)    //�擾�Ɏ��s������
//        {
//            //���G���[�o��
//            UnityEngine.Debug.LogWarning("�ǐՑΏۂ�������܂���");  //�x�����O�o��
//        }
//#endif
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
        Vector2 _vToPlayer = new(m_Target.transform.position.x - transform.position.x,
            m_Target.transform.position.z - transform.position.z);  //�v���C���[�����ւ̃x�N�g��
        var _ToPlAngle = Vector2.Angle(Vector2.up, _vToPlayer); //�v���C���[�Ƃ̂Ȃ��p

        //���v���C���[�֌�����
        _vToPlayer.Set((float)(_vToPlayer.normalized.x * Speed), (float)(_vToPlayer.normalized.y * Speed));  //�ړ���
        //m_Rigidbody.AddForce(new Vector3(m_vToPlayer.x, 0.0f, m_vToPlayer.y).normalized 
        //    * (float)(Math.Clamp((Speed - m_Rigidbody.velocity.magnitude), -4.0d, 4.0d)), ForceMode.Acceleration); //�v���C���[�����ֈړ�
        m_Rigidbody.velocity = new Vector3(_vToPlayer.x, 0.0f, _vToPlayer.y); //�ړ������ύX
        m_Rigidbody.rotation = Quaternion.Euler(0.0f, Vector2.Angle(Vector2.left, _vToPlayer) > 90.0f ? _ToPlAngle : -_ToPlAngle, 0.0f) ;//�v���C���[������
                                        //�������̃x�N�g���Ƃ̂Ȃ��p��90�x�����Ȃ獶�Ɍ����B�����łȂ���ΉE�Ɍ����BAngle��0to180�Ȃ��߂�0to360�������\�������B
    }
}