/*=====
<HPBar.cs> 
���쐬�ҁFiwamuro

�����e
HPBar�𓮂����X�v���N�g

���X�V����
__Y24
_M05
D
04:�v���O�����쐬:iwamuro

=====*/

//�����O��Ԑ錾
using UnityEngine;
using UnityEngine.UI;

//���N���X��`
public class HPBar : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] private Image f_hpBarcurrent;   //HP�o�[
    [SerializeField] private float f_maxHealth;  //�v���C���[�̍ő�HP
    private float f_currentHealth;                //HP�o�[���猸�炷HP
    void Awake()        //�ő�HP����_���[�W�����炷���߂̊֐�
    {
        f_currentHealth = f_maxHealth;     //�ő�HP
    }
    /*���_���[�W�����֐�
    �����F�󂯂��_���[�W   //�������Ȃ��ꍇ�͂P���ȗ����Ă��悢
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�_���[�W�v�Z���s��
    */

    public void UpdateHP(float damage)  //HP�̍X�V�������s��
    {
        f_currentHealth = Mathf.Clamp(f_currentHealth - damage, 0, f_maxHealth); //�ő�HP����_���[�W��������
        f_hpBarcurrent.fillAmount = f_currentHealth / f_maxHealth;      //HP�o�[���󂯂��_���[�W�̕������悤�ɕύX
    }
}