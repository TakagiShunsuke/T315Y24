/*=====
<PlayerScript.cs> 
���쐬�ҁFiwamuro

�����e
Player�𓮂����X�v���N�g

���X�V����
__Y24
_M05
D
04:�v���O�����쐬:iwamuro

=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//���N���X��`
public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    
    [SerializeField] private float fspeed; //�v���C���[�̈ړ����x��ݒ�

    /*�����������֐�
�����F�Ȃ�
��
�ߒl�F�Ȃ�
��
�T�v�FRigidbody�R���|�[�l���g��ǉ�
*/
    void Start()    //�����Œǉ������
    {
      
        rb = GetComponent<Rigidbody>(); //Rigidbody�R���|�[�l���g��ǉ�
    }


    /*���ړ������֐�
�����F�Ȃ�
��
�ߒl�F�Ȃ�
��
�T�v�F�L�[�������ꂽ��ړ������s��
*/
    void Update()
    {
        Vector3 moveDirection = Vector3.zero; // �ړ������̏�����

        if (Input.GetKey(KeyCode.UpArrow))  // ��Arrow�L�[�Ńv���C���[����Ɉړ�������
        {
            moveDirection += transform.forward * fspeed;
      //      transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        }
    
        if (Input.GetKey(KeyCode.DownArrow)) // ��Arrow�L�[�Ńv���C���[�����Ɉړ�������
        {
            moveDirection -= transform.forward * fspeed;
         //   transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        if (Input.GetKey(KeyCode.RightArrow)) // �EArrow�L�[�Ńv���C���[���E�Ɉړ�������
        {
            moveDirection += transform.right * fspeed;
       //    transform.rotation = Quaternion.Euler(0f, 270f, 0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) // ��Arrow�L�[�Ńv���C���[�����Ɉړ�������
        {
            moveDirection -= transform.right * fspeed;
     //       transform.rotation = Quaternion.Euler(0f, 90f, 0f);
          
        }

        // �΂߈ړ�
        if (moveDirection != Vector3.zero)
        {
    
            // ���K�����Ĉړ����x�����ɕۂ�
            moveDirection.Normalize();
            rb.velocity = moveDirection * fspeed;

        }
        else
        {
            // �����L�[��������Ă��Ȃ��ꍇ�͒�~����
            rb.velocity = Vector3.zero;
        }

    }

}

