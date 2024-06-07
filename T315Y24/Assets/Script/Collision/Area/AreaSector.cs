/*=====
<AreaSector.cs>
���쐬�ҁFtakagi

�����e
�̈攻��(��`)

�����ӎ���
�����蔻����������ϐ�����ĐM���𑗐M���邽�߁A������󂯎���ď������s���Ă��������B

�Ȃ��A�ȉ��̃I�u�W�F�N�g�����݂���K�v������܂��B
�P.m_sTargetName�Œ�`���ꂽ���O�ƈ�v����I�u�W�F�N�g

�܂��A�ȉ��̓_�ɒ��ӂ��Ă�������
�P.m_dResol��0�ȉ��̒l��ݒ肷��Ɩ������[�v���������邽�߃G���[��Ԃ��܂�

���X�V����
__Y24
_M05
D
03:�v���O�����쐬:takagi
04:����:takagi
11:�V�O�i���̕Ԃ�l��bool�����������I�u�W�F�N�g��Ԃ��l�ɁA�ՓˑΏۂ̕ϐ����ύX:takagi
16:����̈����:takagi
31:���t�@�N�^�����O:takagi
=====*/

//�����O��Ԑ錾
using System;
using System.Collections;   //list
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;  //Unity

//���N���X��`
public class CAreaSector : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] private double m_dRadius = 2.0d;   //���a
    [SerializeField] private double m_dSectorAngle = 90.0d; //��`�̊p
    [SerializeField] private double m_dFrontAngle = 90.0d;  //xz���ʏ�Ő��ʕ����̊p�x
    [SerializeField] private List<string> m_sTargetNames;  //���m�Ώۂ̃I�u�W�F�N�g��
    private List<GameObject> m_Targets = new List<GameObject>();    //���m�Ώ�
    [SerializeField] private double m_dResol = 1.0d;   //�̈�\���̉𑜓x
    [SerializeField] private Material m_RangeMaterial;   //�̈�\���p�̃}�e���A��

    //���v���p�e�B��`
    public List<GameObject> SignalCollision { get; private set; } = new List<GameObject>();  //�����蔻��̃V�O�i��


    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    void Start()
    {
        //��������
        if (m_sTargetNames != null)  //�k���`�F�b�N
        {
            for (int nIdx = 0; nIdx < m_sTargetNames.Count; nIdx++) //�Ώۂ̐������I�u�W�F�N�g�����o����
            {
                GameObject Temp = GameObject.Find(m_sTargetNames[nIdx]);    //�I�u�W�F�N�g�擾���s
                if (Temp)   //�擾������
                {
                    m_Targets.Add(Temp);   //�v���C���[�̃C���X�^���X�i�[
                }
#if UNITY_EDITOR    //�G�f�B�^�g�p��
                else    //�擾�Ɏ��s������
                {
                    //���G���[�o��
                    UnityEngine.Debug.LogWarning("�^�[�Q�b�g�F" + m_sTargetNames[nIdx] + "��������܂���");  //�x�����O�o��
                }
#endif
            }
        }
        
        //������̈����
        if(m_dResol > 0)    //�𑜓x������Ȏ�
        {
            //�����ϐ��錾
            GameObject _RangeView = new GameObject(); //�����p�I�u�W�F�N�g�쐬
            List<Vector3> _Vertex = new List<Vector3>(); //���_���
            Vector3 _vDirction = new((float)Math.Cos(Mathf.Deg2Rad * (-transform.eulerAngles.y + m_dFrontAngle)), 0.0f,
                (float)Math.Sin(Mathf.Deg2Rad * (-transform.eulerAngles.y + m_dFrontAngle)));   //���ʂ̃x�N�g��  ��y����]�̕����͍��W�n�Ƌt����
            List<int> _VtxIdx = new List<int>(); //�C���f�b�N�X�o�b�t�@�̏��
            int _nIdx = 1;  //���[�v�p�J�E���^
            Mesh _Mesh = new Mesh();    //���b�V���{��

            //����������
            _RangeView.transform.position = transform.position;    //�ʒu�����킹��
            _RangeView.transform.parent = transform;   //�q�I�u�W�F�N�g�ɒǉ�
            _RangeView.AddComponent<MeshRenderer>().material = m_RangeMaterial;   //�}�e���A���𑀍�\�ɂ��A������
            var _MeshFilter = _RangeView.AddComponent<MeshFilter>();   //���b�V������\�ɂ���
            _Vertex.Add(Vector3.zero);  //���_0�Ԗڂ͌��_
            _vDirction = _vDirction.normalized * (float)m_dRadius;   //�傫��������
            for (double _dAngle = -m_dSectorAngle / 2.0d; _dAngle < m_dSectorAngle / 2.0d; _dAngle += m_dResol)    //�ׂ����|���S���쐬
            {
                _Vertex.Add(Quaternion.Euler(0.0f, (float)(_dAngle), 0.0f) * _vDirction + Vector3.zero);   //���_�ʒu�o�^
                _VtxIdx.AddRange(new int[] { 0, _nIdx - 1, _nIdx });    //�C���f�b�N�X�o�b�t�@�o�^
                _nIdx++;    //�J�E���g�i�s
            }
            _Mesh.vertices = _Vertex.ToArray(); //���_�o�b�t�@�o�^
            _Mesh.SetTriangles(_VtxIdx, 0); //�C���f�b�N�X�o�b�t�@�o�^
            _Mesh.RecalculateNormals(); //�@���̍Čv�Z
            _MeshFilter.sharedMesh = _Mesh; //���b�V�����o�^
        }
#if UNITY_EDITOR    //�G�f�B�^�g�p��
        else    //�������[�v����������𑜓x�Ȏ�
        {
            //���G���[�o��
            Debug.LogError("�������[�v���������܂����B�𑜓x(m_dResol)�ɓ��͂���l��0���傫���K�v������܂�"); ;  //�G���[���O�o��
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
        if (m_Targets == null)   //�K�v�v���̕s����
        {
#if UNITY_EDITOR    //�G�f�B�^�g�p��
            //���G���[�o��
            UnityEngine.Debug.LogWarning("�ǐՑΏۂ����݂��܂���");  //�x�����O�o��
#endif

            //�����f
            return; //�X�V�������f
        }

        //�����m
        for (int nIdx = 0; nIdx < m_Targets.Count; nIdx++) //�Ώۂ̐������I�u�W�F�N�g�����o����
        {
            //���ϐ��錾
            Vector2 m_vDirction = new((float)Math.Cos(Mathf.Deg2Rad * (-transform.eulerAngles.y + m_dFrontAngle)),
                (float)Math.Sin(Mathf.Deg2Rad * (-transform.eulerAngles.y + m_dFrontAngle)));   //���ʂ̃x�N�g��  ��y����]�̕����͍��W�n�Ƌt����
            Vector2 m_vToTarget = new(m_Targets[nIdx].transform.position.x - transform.position.x,
                m_Targets[nIdx].transform.position.z - transform.position.z);  //�v���C���[�����ւ̃x�N�g��

            //��������
            SignalCollision.Clear(); //�����蔻�菉����

            //����`�͈�
            if (m_vToTarget.magnitude <= m_dRadius && Vector2.Angle(m_vToTarget, m_vDirction) <= m_dSectorAngle / 2.0d)   //�x�N�g���̒��������a�ȉ��A���ʂ̃x�N�g���Ɗp�x����`�̊p�̔����ȉ�
            {
                //�������蔻��
                SignalCollision.Add(m_Targets[nIdx]); //�����蔻��X�V
            }

//#if UNITY_EDITOR && DEBUG    //�G�f�B�^�g�p�����f�o�b�O��
//            //���ϐ��錾�E������
//            Vector3 m_vDirctCent = new(m_vDirction.x, 0.0f, m_vDirction.y);  //��`�̒�������
//            m_vDirctCent = m_vDirctCent.normalized * (float)m_dRadius;   //�傫��������
//            Vector3 m_vDirctLeft = Quaternion.Euler(0.0f, (float)(m_dSectorAngle / 2.0d), 0.0f) * m_vDirctCent; //��`�̍��[
//            Vector3 m_vDirctRight = Quaternion.Euler(0.0f, (float)(-m_dSectorAngle / 2.0d), 0.0f) * m_vDirctCent;   //��`�̉E�[

//            //���͈͕\��
//            Debug.DrawRay(transform.position + Vector3.up, m_vDirctCent, Color.blue);   //��`�̒����\��
//            Debug.DrawRay(transform.position + Vector3.up, m_vDirctLeft, Color.blue);   //��`�̍��[�\��
//            Debug.DrawRay(transform.position + Vector3.up, m_vDirctRight, Color.blue);  //��`�̉E�[�\��
//#endif    //�̈�\�������������̂ł������
        }
    }        
}