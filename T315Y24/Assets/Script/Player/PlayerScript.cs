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

//���N���X��`
public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    float speed = 3.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Rigidbody��ǉ�
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))  //Arrow�L�[�Ńv���C���[���ړ�������
        {
            rb.velocity = transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = -transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = transform.right * speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = -transform.right * speed;
        }
    }
}
