/*=====
<CodingRule.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�R�[�f�B���O�K����L�q

�����ӎ���   //�Ȃ��Ƃ��͏ȗ�OK
���̋K�񏑂ɋL�q�̂Ȃ����͔̂�������A�K�X�ǉ�����

���X�V����
__Y24   //'24�N
_M04    //04��
D       //��
16:�v���O�����쐬:takagi   //���t:�ύX���e:�{�s��
17:����������:takagi

_M05
D
03:����͂ɂقւ�:takagi
05:����ʂ��:takagi
=====*/

//�����O��Ԓ�`
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���N���X��`
public class CCodingRule : MonoBehaviour    //�N���X�^�̓�������C������
{
    //���񋓒�`
    public enum E_ENUM
    {

    }

    //���\���̒�`
    private struct Struct
    {
        GameObject m_Object;    //�N���X�^�̃l�[�~���O�̓n���K���A���L�@�ɏ]��Ȃ�
                                //��m_��s_�Ȃǂ̌^�Ƃ͊֌W�Ȃ������ł͏]��
        Ray m_Ray;
    }

    //���ϐ��錾
    private int m_nInt; //�ʏ�̌^�̓n���K���A���L�@�ɏ]��
    private static string m_szStr;  //�����o�ϐ��ȐÓI�ϐ��Ȃ�Z������ms_�ƋL�q����

    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�������ɍs������
    */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*���X�V�֐�
    �����F�Ȃ�   //�������Ȃ��ꍇ�͂P���ȗ����Ă��悢
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�t���[���X�V���ɍs����������
    */
    private void FixedUpdate()
    {
    }

    /*��xx�֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�֐���
    */
    public void Function()
    {
    }

    /*����֐�
    �����P�Fdouble dDouble�F���l   //�����F���e�̌`�ŋL�q
    �����Q�FGameObject GameObject�F����
    ��
    �ߒl�F����   //���e�̂݋L�q
    ��
    �T�v�F�֐��L�q��
    */
    private int Example(double dDouble, GameObject GameObject)
    {
        //���ϐ��錾
        float fFloat = 0.0f;

        //���Z�o
        m_nInt = (int)(dDouble * fFloat);

        //����
        return m_nInt;
    }
}
