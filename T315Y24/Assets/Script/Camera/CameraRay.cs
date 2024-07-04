/*=====
<CameraRay.cs>
���쐬�ҁFtei

�����e
�J�������烌�C���΂��āA�v���C���[�Ƃ̊Ԃɑ��̃I�u�W�F�N�g
����������A�I�u�W�F�N�g�𓧉ߏ������܂��B

�����ӎ���


���X�V����
__Y24
_M06
D
21:�X�N���v�g�쐬�Ftei
25:���t�@�N�^�����O:takagi
=====*/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CCameraRay : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField] private GameObject player;     // �C���X�y�N�^�[��Ńv���C���[��ڑ�
    Vector3 tergetPosition;
    float tergetOffsetYFoot = 0.1f;     // ray���΂������̃I�t�Z�b�g�i�����j
    float tergetOffsetYHead = 17f;      // ray���΂������̃I�t�Z�b�g�i���j

    public GameObject[] prevRaycast;
    public List<GameObject> raycastHitsList_ = new List<GameObject>();

    /*���X�V�����֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�J�������C����̍X�V
    */
    void Update()
    {
        //�J�����������Ԃ̃I�u�W�F�N�g�𔼓��������Ă���
        prevRaycast = raycastHitsList_.ToArray();   //�O�t���[���œ����ɂ��Ă���I�u�W�F�N�g�i���X�g�j��z��prevRayCast�ɏo��
        raycastHitsList_.Clear();                   //�O�t���[���œ����ɂ��Ă���I�u�W�F�N�g�i���X�g�j���������H�����H
        tergetPosition = player.transform.position; //tergetPosition��Player��position���i�[
        tergetPosition.y += tergetOffsetYFoot;      //tergetPosition��y���i���������j�ɃI�t�Z�b�g�𔽉f�B�����ł͑����̍����ɍ��킹�Ă���B�i�����̒l�����̂܂܂����Ɛ^���̏��������ɂȂ邱�Ƃ����������߃I�t�Z�b�g�����B�j
        Vector3 _difference = (tergetPosition - this.transform.position);   //�J�����ʒu��tergetPosition�ւ̃x�N�g�����擾
        RayCastHit(_difference);                    //���̃��\�b�h���Q�ƁBray���΂��ď����ɍ������̂𔼓����ɂ��āAraycastHitList�ɒǉ����Ă���B

        //�J�������������Ԃ̃I�u�W�F�N�g�𔼓��������Ă���
        tergetPosition.y += tergetOffsetYHead;      //tergetPosition��y���i���������j�ɃI�t�Z�b�g�𔽉f�B�����ł͓��̍����ɍ��킹�Ă���B
        _difference = (tergetPosition - this.transform.position);   //�J�����ʒu��tergetPosition�ւ̃x�N�g�����擾
        RayCastHit(_difference);

        //�q�b�g����GameObject�̍��������߂āA�Փ˂��Ȃ������I�u�W�F�N�g��s�����ɖ߂�
        foreach (GameObject _gameObject in prevRaycast.Except<GameObject>(raycastHitsList_))    //prevRaycast��raycastHitList_�Ƃ̍����𒊏o���Ă�B
        {
            Transparent noSampleMaterial = _gameObject.GetComponent<Transparent>();
            if (_gameObject != null)
            {
                noSampleMaterial.NotClearMaterialInvoke();
            }

        }
    }

    /*�����C�֐�
    �����FVector3 _difference
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�J�������C�ƃ��C�̔�����쐬�A���������I�u�W�F�N�g��list�ɒǉ�
    */
    //ray���΂��ď����ɍ������̂𔼓����ɂ��āAraycastHitList�ɒǉ����Ă���B
    public void RayCastHit(Vector3 _difference)
    {
        Vector3 direction = _difference.normalized;           //�J����-�^�[�Q�b�g�Ԃ̃x�N�g���̐��K�x�N�g���𒊏o

        Ray ray = new Ray(this.transform.position, direction);//Ray�𔭎�
        RaycastHit[] rayCastHits = Physics.RaycastAll(ray);    //Ray�ɂ��������I�u�W�F�N�g�����ׂĎ擾

        foreach (RaycastHit hit in rayCastHits)
        {
            float distance = Vector3.Distance(hit.point, transform.position);       //�J����-ray�����������ꏊ�Ԃ̋������擾
            if (distance < _difference.magnitude)      //�J����-ray�����������ꏊ�Ԃ̋����ƃJ����-�^�[�Q�b�g�Ԃ̋������r�B�i���̔�r���s��Ȃ���Player�̉����̃I�u�W�F�N�g�������ɂȂ�B�j
            {
                Transparent transparent = hit.collider.GetComponent<Transparent>();
                if (
                hit.collider.tag == "Map")          //�^�O���m�F
                {
                    transparent.ClearMaterialInvoke();                  //�����ɂ��郁�\�b�h���Ăяo���B
                    raycastHitsList_.Add(hit.collider.gameObject);      //hit����gameobject��ǉ�����
                }
            }
        }
    }
}
