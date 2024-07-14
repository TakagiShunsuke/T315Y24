/*=====
<SceneTransition.cs> //�X�N���v�g��
���쐬�ҁFtei

�����e
�V�[���g�����W�V����

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
using UnityEngine.Events;

//���N���X��`
public class FadeIn : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] private Material SceneTransitionMaterial;  // �}�e���A��
    [SerializeField] private float transitionTime = 1.0f;       // �t�F�[�h����
    [SerializeField] private string propertyName = "_Progress"; // ShaderGraph����`�����ϐ���

    //���p�u���b�N�C�x���g
    public UnityEvent OnTransitionDone;

    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    private void Start()
    {
        StartCoroutine(TransitionCoroutine());
    }

    /*���R���[�`���֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�t�F�C�h���Ԃɍ��킹��X�V����
    */
    private IEnumerator TransitionCoroutine()
    {
        float currentTime = 0.0f;   // ������
        while(currentTime < transitionTime) // �t�F�[�h���Ԃ�菬����������s��
        {
            currentTime += Time.deltaTime;
            SceneTransitionMaterial.SetFloat(propertyName, Mathf.Clamp01(currentTime / transitionTime));    // propertyName�Œ�`�������l�����Ԃ̊����ɍ��킹�ăX���C�h����
            yield return null;
        }
        OnTransitionDone.Invoke();  // �C�x���g�̌Ăяo��
    }
}
