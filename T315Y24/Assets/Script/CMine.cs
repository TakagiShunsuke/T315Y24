/*=====
<Mine.cs> 
���쐬�ҁFyamamoto

�����e
�n���I�u�W�F�N�g�ɃA�^�b�`����X�N���v�g

�����ӎ���   //�Ȃ��Ƃ��͏ȗ�OK
���̋K�񏑂ɋL�q�̂Ȃ����͔̂�������A�K�X�ǉ�����

���X�V����
__Y24   //'24�N
_M05    //05��
D       //��
03:�v���O�����쐬:yamamoto
06:�C���^�[�t�F�[�X�ǉ�:yamamoto

=====*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMine : MonoBehaviour
{
    public float ExplosionRadius = 5f;  // �n�������͈�
    public float ExplosionDelay = 2f;   // �����܂ł̑ҋ@����
    public float RechargeDelay = 7f;    // �ď[�d�܂ł̑ҋ@����
    public LayerMask EnemyLayer;        // �n���̉e�����󂯂�I�u�W�F�N�g���w��

    private bool CanExplode = true;

    public interface IDestroy
    { 
        void TakeDestroy();
    }

    /*���n�������蔻��֐�
    �����P�F�����蔻�肪�������I�u�W�F�N�g�̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�G���n���ɐG�ꂽ�Ƃ��̂ݔ�������
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")&&CanExplode)  //Enemy�^�O�����Ă��違�n���g�p�\
        {
            ExplodeAfterDelay();            //�����f�B���C�v��
            StartCoroutine(RechargeMine()); //�ė��p���Ԍv��
        }
    }

    /*���n���ė��p�̔���̊֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�ė��p�̎��Ԃ��J�E���g
    */
    IEnumerator RechargeMine()
    {
        CanExplode = false;
        yield return new WaitForSeconds(RechargeDelay);
        CanExplode = true;
    }

    /*�����������֐�
   �����P�F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�����܂ł̎��Ԃ��J�E���g
   */
    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(ExplosionDelay);

        // ���������������ɋL�q
        Explode();
    }

    /*�����������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F��������
    */
    void Explode()
    {
        //�n���͈̔͂ɓ����Ă�G�̂ݔz��ɓ����
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, EnemyLayer);

        foreach (Collider hit in colliders) //colliders�̒��ɓ����Ă�S��for���ŌJ��Ԃ�
        {
            // IDestroy���������Ă�����
            if (hit.gameObject.TryGetComponent<IDestroy>(out var destroy))
                if (destroy != null)
                {
                    destroy.TakeDestroy();
                } 
        }
    }
}

