/*=====
<Transparent.cs>
���쐬�ҁFtei

�����e

�����ӎ���


���X�V����
__Y24
_M06
D
21:�X�N���v�g�쐬�Ftei
=====*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    //���ϐ��錾
    private Color color = Color.white;      //color��white���x�[�X�Ɏg�p���܂��B

    //�e �q�I�u�W�F�N�g���i�[�B
    private MeshRenderer[] meshRenderers;
    private MaterialPropertyBlock m_mpb;
    [SerializeField] private string _shaderName = "Unlit/UnlitTransparent";
    [SerializeField] private string _shaderNameChange = "Shader Graphs/ArnoldStandardSurface";
    [SerializeField] private float ObjectAlpha = 1.0f;

    public MaterialPropertyBlock mpb
    {
        get { return m_mpb ?? (m_mpb = new MaterialPropertyBlock()); }
    }

    void Awake()
    {
        //�q�I�u�W�F�N�g�Ɛe�I�u�W�F�N�g��meshrenderer���擾
        meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
    }

    /*�����߃V�F�[�_�[�Ăяo���֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F���ߗp�̃V�F�[�_�[���Ăяo��
    */
    public void ClearMaterialInvoke()
    {
        color.a = ObjectAlpha;
        ObjectAlpha -= Time.deltaTime;
        if (color.a <= 0.3f)
        {
            color.a = 0.3f;
        }
        mpb.SetColor(Shader.PropertyToID("_Color"), color);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find(_shaderName);
            meshRenderers[i].SetPropertyBlock(mpb);
        }
    }

    /*���s���߃V�F�[�_�[�Ăяo���֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��(�s����)�̃V�F�[�_�[���Ăяo��
    */
    public void NotClearMaterialInvoke()
    {
        ObjectAlpha = 1.0f;
        color.a = ObjectAlpha;
        mpb.SetColor(Shader.PropertyToID("_Color"), color);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetComponent<Renderer>().material.shader = Shader.Find(_shaderNameChange);
            meshRenderers[i].SetPropertyBlock(mpb);
        }
    }
}
