/*=====
<CostUI.cs> 
���쐬�ҁFyamamoto

�����e
�R�X�g��UI(�~�Q�[�W)

���X�V����
__Y24   
_M06    
D
25: �v���O�����쐬: yamamoto
26: �R�����g�ǉ�: yamamoto
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//���N���X��`
public class CostUI : MonoBehaviour
{
    //���ϐ��錾
    [Header("������UI")]
    [SerializeField, Tooltip("���v�̂悤�ɓ�����UI")] private Image UIobj;   //���v�̂悤�ɓ�����UI
    [Header("�R�X�g��������")]
    [SerializeField, Tooltip("�R�X�g��1������b��")] private float countTime = 5.0f; //�R�X�g��1������b��
    [Header("�R�X�g�\��")]
    [SerializeField, Tooltip("����������Text")] private TMP_Text Cost_txt; //�\��������e�L�X�g(TMP)


     /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
     */
    void Start()
    {
        UIobj.fillAmount = 0.0f;                        //������
        Cost_txt.SetText($"{CTrapSelect.m_nCost}");      //������
    }

    /*���X�V�֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�X�V����
    */
    void Update()
    {
        //�~�Q�[�W�𓮂���
        UIobj.fillAmount += 1.0f / countTime * Time.deltaTime;  //countTime(�b)�̕b����1�ɂȂ�
        if (UIobj.fillAmount>=1.0f)     //�~�Q�[�W�����������
        {
            UIobj.fillAmount = 0.0f;    //������
            CTrapSelect.m_nCost++;       //�R�X�g����
        }
        Cost_txt.SetText($"{CTrapSelect.m_nCost}");  //�R�X�g�\��
    }
}
