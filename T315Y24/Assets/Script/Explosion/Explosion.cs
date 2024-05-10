using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Explosion : MonoBehaviour
{
   private float ObjectRadius;  //�I�u�W�F�N�g�̔��a
   private Vector3 initialPosition; // �����ʒu
   [SerializeField] private double LowerSpeed = 0.1d;
    void Start()
    {
        ObjectRadius = transform.localScale.x / 2.0f; // �I�u�W�F�N�g�̔��a���擾
        initialPosition = transform.position; // �����ʒu��ݒ�
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.LogWarning("�܂�1");
        // �͈͓��̓G�����o
        Collider[] colliders = Physics.OverlapSphere(transform.position, ObjectRadius);
        foreach (Collider collider in colliders)
        {
            Debug.LogWarning("�܂�2");
            if (collider.CompareTag("Enemy"))
            {
                Debug.LogWarning("�܂�3");
                if (collider.gameObject.TryGetComponent<IFeatureMine>(out var destroy))
                    destroy.TakeDestroy();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �I�u�W�F�N�g�����Ɉړ�������
        transform.position -= new Vector3(0f, (float)LowerSpeed * Time.deltaTime, 0f);

        // ���a���������Ɉړ��������ǂ����𔻒f���A�j�󂷂�
        if (transform.position.y <= initialPosition.y - ObjectRadius)
        {
            Destroy(gameObject);
        }
    }
}
