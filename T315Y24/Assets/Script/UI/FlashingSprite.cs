/*=====
<FlashingSprite.cs>
���쐬�ҁFyamamoto

�����e
Sprite��_�ŕ\��������

���X�V����
__Y24
_M09
D
12:�v���O�����쐬:yamamoto

=====*/

//�����O��Ԑ錾
using UnityEngine;

//���N���X��`
public class FlashingSprite : MonoBehaviour
{
    //�ϐ��錾
    [Header("���x�ύX")]
    [SerializeField, Tooltip("�_�ł̑��x")] float fadeSpeed = 1.0f;          // �t�F�[�h���x

    private SpriteRenderer spriteRenderer;  // SpriteRenderer�̎Q��
    private bool fadingOut = true;          // �t�F�[�h�A�E�g�t���O

    /*���������֐�
  �����P�F�Ȃ�
  ��
  �ߒl�F�Ȃ�
  ��
  �T�v�F�C���X�^���X�������ɍs������
  */
    void Start()
    {
        // �����Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă���SpriteRenderer���擾
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /*�������X�V�֐�
   �����F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F��莞�Ԃ��Ƃɍs���X�V����
   */
    void FixedUpdate()
    {
        Color color = spriteRenderer.color;  // ���݂̃X�v���C�g�̐F���擾

        // �A���t�@�l�𒲐����ē_�ł����鏈��
        if (fadingOut)
        {
            color.a -= fadeSpeed * Time.deltaTime;  // �t�F�[�h�A�E�g
            if (color.a <= 0.0f)
            {
                color.a = 0.0f;
                fadingOut = false;  // �t�F�[�h�C���ɐ؂�ւ�
            }
        }
        else
        {
            color.a += fadeSpeed * Time.deltaTime;  // �t�F�[�h�C��
            if (color.a >= 1.0f)
            {
                color.a = 1.0f;
                fadingOut = true;  // �Ăуt�F�[�h�A�E�g�ɐ؂�ւ�
            }
        }

        spriteRenderer.color = color;  // �ύX�����A���t�@�l�𔽉f
    }
}
