/*=====
<UITransition.cs> //�X�N���v�g��
���쐬�ҁFtei

�����e
UI�g�����W�V����

�����ӎ���
�K���ō���Ă邩��A�����Ɨǂ����o���~����������v����

���X�V����
__Y24
_M06
D
12:�v���O�����쐬:tei
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���N���X��`
public class UITransition : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField]�@private RectTransform rectTransform;   // �ړ��n�_�A�I�_�A�ړ�����
    [SerializeField]�@private float transitonTime = 2.0f;    // UI�ړ��^�C��
    [SerializeField]�@Vector3 startposition; // UI�����ʒu

    /*���������֐�
   �����P�F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�C���X�^���X�������ɍs������
   */
    private void Awake()
    {
        startposition = rectTransform.anchoredPosition;
    }

    /*���������֐�
   �����P�F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�C���X�^���X�������ɍs������
   */
    public void ShowUI()
    {
        StartCoroutine(ShowCoroutine());
    }

    /*���R���[�`���֐�
   �����P�F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�FUI�ړ��ɍ��킹��X�V����
   */
    private IEnumerator ShowCoroutine()
    {
        float currentTime = 0.0f;   // ������
        while (currentTime < transitonTime) // UI�ړ����Ԃ�菬����������s��
        {
            currentTime += Time.deltaTime;
            rectTransform.anchoredPosition = Vector3.Lerp(startposition, Vector3.zero, Mathf.Clamp01(currentTime)); // �n�_�A�I�_�A�ړ��^�C���ݒ�
            yield return null;
        }
    }
}
