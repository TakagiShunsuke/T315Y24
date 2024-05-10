using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mine : MonoBehaviour
{
    //���ϐ��錾
    private double dTimeToExplosion = 0.0d;                     //�����܂ł̎���[s]
    [SerializeField] private double dExplosionDelay = 2.0d;     // �����܂ł̑ҋ@����[s]
    private double dMineCoolTime = 0.0d;                        //�n���N�[���^�C��[s]
    [SerializeField] private double dMineInterval = 5.0d;       // �n���ė��p�܂ł̎���[s]
    private bool bCanExplode = true;                            //�n�����p true:�\ false:�s��
    public GameObject ExplosionEffectPrefab;

    /*���n�������蔻��֐�
    �����P�F�����蔻�肪�������I�u�W�F�N�g�̏��
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�G���n���ɐG�ꂽ�Ƃ��̂ݏ��������
    */
    private void OnCollisionEnter(Collision collision)     //�n���ɉ������������Ă����Ƃ�
    {
        if (collision.gameObject.CompareTag("Enemy") && bCanExplode)  //Enemy�^�O�����Ă��違�n���g�p�\
        {
            Debug.LogWarning("�ŏ�");
            bCanExplode = false;
            dTimeToExplosion = dExplosionDelay;     //�����܂ł̎��Ԍv��
        }
    }

    private void FixedUpdate()
    {
        //�������J�E���g�_�E��
        if (dTimeToExplosion > 0.0d)   //�ҋ@��
        {
            dTimeToExplosion -= Time.fixedDeltaTime;
            if (dTimeToExplosion <= 0.0d)
            {
                //ExplosionManager.Instance.CreateExplosion(transform.position);
                Instantiate(ExplosionEffectPrefab, transform.position, Quaternion.identity);
                dMineCoolTime = dMineInterval;
            }
        }

        //���n���̍ė��p�J�E���g�_�E��
        if (dMineCoolTime > 0.0d)   //�N�[���_�E����
        {
            dMineCoolTime -= Time.fixedDeltaTime;
            if (dMineCoolTime < 0.0d) { bCanExplode = true; Debug.LogWarning("�ė��p"); }  //�n�����p�\��
        }
    }
}
