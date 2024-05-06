/*=====
<CMine.cs> 
���쐬�ҁFyamamoto

�����e
�n���I�u�W�F�N�g�ɃA�^�b�`����X�N���v�g

���X�V����
__Y24
_M05
D
03:�v���O�����쐬:yamamoto
06:�C���^�[�t�F�[�X�ǉ�:yamamoto

=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���N���X��`
public class CMine : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] private double ExplosionRadius = 5.0d;  // �n�������͈�
    [SerializeField] private double ExplosionDelay = 2.0d;   // �����܂ł̑ҋ@����
    [SerializeField] private double RechargeDelay = 7.0d;    // �ė��p�܂ł̎���
    private LayerMask EnemyLayer;        // �n���̉e�����󂯂�I�u�W�F�N�g���w��
    private bool CanExplode = true;     //�n�����p true:�\ false:�s��

    /*���n�������蔻��֐�
    �����P�F�����蔻�肪�������I�u�W�F�N�g�̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�G���n���ɐG�ꂽ�Ƃ��̂ݏ��������
    */
    private void OnTriggerEnter(Collider other)     //�n���ɉ������������Ă����Ƃ�
    {
        if (other.CompareTag("Enemy")&&CanExplode)  //Enemy�^�O�����Ă��違�n���g�p�\
        {
            ExplodeAfterDelay();            //�����f�B���C�v��
            StartCoroutine(RechargeMine()); //�ė��p���Ԍv��
        }
    }

    /*���n���ė��p���Ԍv���̊֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�ė��p�̎��Ԃ��J�E���g
    */
    IEnumerator RechargeMine()
    {
        CanExplode = false;     //�n���g�p�s��
        yield return new WaitForSeconds((float)RechargeDelay);      //�������ҋ@����
        CanExplode = true;      //�ҋ@���I���ƒn���g�p�\�ɂ���
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
        yield return new WaitForSeconds((float)ExplosionDelay);     //�������ҋ@����

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
        //�n���͈̔͂ɓ����Ă�G�iEnemyLayer�j�̂ݔz��ɓ����
        Collider[] colliders = Physics.OverlapSphere(transform.position, (float)ExplosionRadius, EnemyLayer);

        foreach (Collider hit in colliders) //colliders�̒��ɓ����Ă�S��for���ŌJ��Ԃ�
        {
            // IDestroy���������Ă�����
            if (hit.gameObject.TryGetComponent<IFeatureMine>(out var destroy))
                if (destroy != null)            //�擾�������Ă��邩
                {
                    destroy.TakeDestroy();      //�j��̏���������
                } 
        }
    }
}

