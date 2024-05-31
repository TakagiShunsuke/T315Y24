/*=====
<Arrow.cs> 
���쐬�ҁFyamamoto

�����e
���˂�����ɕt����X�N���v�g

���X�V����
__Y24   
_M05    
D
16 :�v���O�����쐬:yamamoto 
30 :�R�����g�ǉ�  :yamamoto

=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���N���X��`
public class CArrow : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] private float lifetime = 5f; // ��̎����i�b�j

    /*���������֐�
   �����P�F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�C���X�^���X�������ɍs������
   */
    void Start()
    {
        Rigidbody�@rb = GetComponent<Rigidbody>();

        if (rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);  //�i�s�����ɉ����悤�ɉ�]
        }
    }

    /*���X�V�֐�
     �����F�Ȃ�
     ��
     �ߒl�F�Ȃ�
     ��
     �T�v�F��莞�Ԃ��Ƃɍs���X�V����
     */
    void Update()
    {
        // ��̎�����ݒ肵�A��莞�Ԍ�ɔj��
        Destroy(gameObject, lifetime);
    }

    /*������蔻��֐�
  �����P�F�����蔻�肪�������I�u�W�F�N�g�̏��
  ��
  �ߒl�F�Ȃ�
  ��
  �T�v�F������ɐG�ꂽ�Ƃ��̂ݏ��������
  */
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        // ��G�ɓ��������ꍇ
        if (other.gameObject.CompareTag("Enemy"))
        {
            //IFeatureMine�����邩�m�F
            if (other.gameObject.TryGetComponent<IFeatureMine>(out var destroy))
                destroy.TakeDestroy();  //�G�폜
        }

        Destroy(gameObject);    //�����ɓ�����Ώ��ł�����
    }
}
