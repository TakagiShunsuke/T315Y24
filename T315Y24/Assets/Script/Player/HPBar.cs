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
    [SerializeField] private Image _hpBarcurrent;   //
    [SerializeField] private float _maxHealth;  //�ő�HP
    private float currentHealth;                //
    void Awake()        //
    {
        currentHealth = _maxHealth;     //�ő�HP
    }
    /*���_���[�W�����֐�
    �����F�󂯂��_���[�W   //�������Ȃ��ꍇ�͂P���ȗ����Ă��悢
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�_���[�W�v�Z���s��
    */

    public void UpdateHP(float damage)  //HP�̍X�V
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, _maxHealth); //�ő�HP����_���[�W��������
        _hpBarcurrent.fillAmount = currentHealth / _maxHealth;      //
    }
}