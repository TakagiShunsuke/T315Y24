/*cameraSample.cs
//�쐬�ҁ@�Έ�u��
//�����e
//�v���C���[�̈ʒu�擾�ƃv���C���[���痣��鐔�l�Ɖ�]���l�ǉ�
//�J�����ɃA�^�b�`����X�N���v�g

//���X�V����
__Y24
_M05
D
//3�@�J�������� :isii
//8�@�R�����g�A�E�g�ǉ� ���ŃJ�����̍��W�Ɖ�]���l������&�ύX :isii
//10    �R�����g�A�E�g�ǉ��@�ňړ��Ɖ�]�̐��l�ύX�\�� :isii
//11 �R�����g�A�E�g�ǉ� �]���ȕ��폜:isii
 */

//>���O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;



//>�N���X��`
public class cameraSample : MonoBehaviour
{
    //>�ϐ���`
    // Start is called before the first frame update
    private GameObject Player;//�v���C���[�i�[(�I�u�W�F�N�g)�p

    [SerializeField] private GameObject�@target ;//�ǔ�����^�[�Q�b�g
    //�J�����̈ړ��ݒ�
    [SerializeField] private float PosX = 0.0f;//�v���C���[���痣���x�̐��l�ύX
    [SerializeField] private float PosY = 30.0f;//�v���C���[���痣���Y�̐��l�ύX
    [SerializeField] private float PosZ = -40.0f;//�v���C���[���痣���z�̐��l�ύX
    //�J�����̉�]�ݒ�
    [SerializeField] private float angleX = 30.0f;//x��]�̒l��ύX�ł���
    [SerializeField] private float angleY = 0.0f;//y��]�̒l��ύX�ł���
    [SerializeField] private float angleZ = 0.0f;//z��]�̒l��ύX�ł���

    void Start()
    {
        /*���J�����̕ϐ�
   �����P�F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�J�����̕ϐ���O�̂��ߏ�����
   */
        //�J�����̍��W������
        Vector3 pos = this.transform.position;//�ړ��֐��擾
        Debug.Log("X = " + pos.x);//�ړ�x������
        Debug.Log("Y = " + pos.y);//�ړ�y������
        Debug.Log("Z = " + pos.z);//�ړ�z������

        //�J�����̊p�x������
        Vector3 angle = this.transform.localEulerAngles;//��]�֐��̎擾
        Debug.Log("X = " + angle.x);//��]x������
        Debug.Log("Y = " + angle.y);//��]y������
        Debug.Log("Z = " + angle.z);//��]z������
        /*���v���C���[�̈ʒu�擾
   �����P�F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�v���C���[�̈ʒu�̂ݎ擾
   */
        Vector3 a;
        a = new Vector3( +PosX,  +PosY, +PosZ);//�v���C���[�̍��W�{�v���C���[���痣��鐔�l
        //�v���C���[�̒ǔ�
        this.Player =  target;//�ǔ����������O������  
        //�J�����̍��W�ύX
        pos = Player.gameObject.transform.position + a;//�v���C���[���W�擾
      
        this.transform.position = pos;//�J�����̍��W�X�V

    }



    // Update is called once per frame
    void Update()
    {
        /*���J�����̈ʒu�ύX
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�J�����̍��W�ύX
    */


        Vector3 a;
        a = new Vector3(+PosX, +PosY, +PosZ);//�v���C���[�̍��W�{�v���C���[���痣��鐔�l
        //�v���C���[�̒ǔ�
        Vector3 pos =Player.gameObject.transform.position + a ;//pos�֐��擾
        this.gameObject.transform.position = pos;//�J�����̍��W�X�V
        /*���J�����̊p�x�ύX
   �����P�F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�J�����p�x�ύX
   */
        //�J�����̊p�x�ύX
        Vector3 angle = this.transform.localEulerAngles;//��]�֐��擾
       
        
         angle = new Vector3(angleX, angleY, angleZ);//��]���l�ύX
        
        this.transform.localEulerAngles = angle;//�J�����̉�]���l�X�V

      
    }
}
