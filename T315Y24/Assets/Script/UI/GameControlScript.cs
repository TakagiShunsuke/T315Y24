/*=====
<GameControlScript.cs> 
���쐬�ҁFiwamuro

�����e
�ϐ��̓��e��UI Text�ɕ\������X�v���N�g

���X�V����
__Y24
_M05
D
09:�v���O�����쐬:iwamuro

=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //UI�\���p

//���O��Ԓ�`
namespace UI
{
    public static class CUI
    {
        //���萔��`
        private const uint CONST = 0;    //���u����fps�l

        //���ϐ��錾
        public static readonly double ms_Temp = 0.0d;   //readonly�ȕϐ����������͓���
    }
}

//���N���X��`
public class GameControlScript : MonoBehaviour
{
    // Start is called before the first frame update
    //���萔��`
    public Text TextFrame;  //�e�L�X�g��\������ꏊ
    public Text variable;  //�ϐ�

    //���ϐ��錾
    private int Iframe;     //�\������ϐ�

 
    private int Iframe2;     //�\������ϐ�

    /*���������֐�
   �����F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�\������ϐ�������������
   */
    void Start()
    {
        Iframe = 0; //�l�̏�����
    }
    /*���X�V�֐�
  �����F�Ȃ�
  ��
  �ߒl�F�Ȃ�
  ��
  �T�v�F�w�肵���ϐ���\������
  */

    void Update()
    {
      //  Iframe2 = "������";
        TextFrame.text = string.Format("{0:00000} ������", Iframe);�@//�w�肵���l��\������
        Iframe++;   //�ϐ����v���X����
    }
}
