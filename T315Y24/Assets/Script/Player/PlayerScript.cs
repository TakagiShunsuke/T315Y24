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
    float fspeed = 3.0f;    //�v���C���[�̈ړ����x

    void Start()    //�����Œǉ������
    {
        rb = GetComponent<Rigidbody>(); //Rigidbody�R���|�[�l���g��ǉ�
    }

    void Update()   //�L�[�������ꂽ�Ƃ��ɍX�V���s��
    {
        if (Input.GetKey(KeyCode.UpArrow))  //��Arrow�L�[�Ńv���C���[����Ɉړ�������
        {
            rb.velocity = transform.forward * fspeed;
        }
        if (Input.GetKey(KeyCode.DownArrow)) //��Arrow�L�[�Ńv���C���[�����Ɉړ�������
        {
            rb.velocity = -transform.forward * fspeed;
        }
        if (Input.GetKey(KeyCode.RightArrow)) //�EArrow�L�[�Ńv���C���[���E�Ɉړ�������
        {
            rb.velocity = transform.right * fspeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) //��Arrow�L�[�Ńv���C���[�����Ɉړ�������
        {
            rb.velocity = -transform.right * fspeed;
        }
    }
}
