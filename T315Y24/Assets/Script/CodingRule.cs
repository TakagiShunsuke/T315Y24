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
04:�������̋L�@�ǉ��E������\�L���C��:takagi
14:�C���^�[�t�F�[�X�ɂ��ċL�q:takagi
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//�����O��Ԓ�`
namespace Space
{
    //���N���X��`
    public static class CSpace
    {
        //���萔��`
        private const uint CONST = 0;    //���u����fps�l

        //���ϐ��錾
        public static readonly double ms_Temp = 0.0d;   //readonly�ȕϐ����������͓���
    }
}

//���C���^�[�t�F�[�X��`
public interface IInterface //�C���^�[�t�F�[�X�̓�������I������
{
    //���v���p�e�B��`
    public double Prop { get; set; }    //���������v���p�e�B�̓n���K���A���L�@�𖳎����Ă悢

    //���v���g�^�C�v�錾
    public void Signaled();
}

//���N���X��`
public class CCodingRule : MonoBehaviour    //�N���X�^�̓�������C������
{
    //���񋓒�`
    public enum E_ENUM  //�񋓂͐ړ�����E_�Ƃ���
    {
        E_ENUM_A,
        E_ENUM_B,
    }

    //���\���̒�`
    private struct Struct
    {
        GameObject m_Object;    //�N���X�^�̃l�[�~���O�̓n���K���A���L�@�ɏ]��Ȃ�
                                //��m_��s_�Ȃǂ̌^�Ƃ͊֌W�Ȃ������ł͏]��
        Ray m_Ray;
    }

    //���ϐ��錾
    private int m_nInt; //�ʏ�̌^�̓n���K���A���L�@�ɏ]��[�����o�ϐ���m_�ƕt����]
    private static string m_szStr;  //�����o�ϐ��ȐÓI�ϐ��Ȃ�Z������ms_�ƋL�q����
    [SerializeField] private uint m_uInner;   //�����͋L�@�ɉe�����Ȃ�

    //���v���p�e�B��`
    public double PriProp { get; private set; } //readonly�Ȍ`���ł��L�@�͖���

    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    void Start()
    {
        
    }

    /*�������X�V�֐�
    �����F�Ȃ�   //�������Ȃ��ꍇ�͂P���ȗ����Ă��悢
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��莞�Ԃ��Ƃɍs���X�V����
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
        GameObject Object;  //�ړ����������ꍇ�A��������啶���ɂ��� 
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