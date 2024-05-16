using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrow : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float lifetime = 5f; // ��̎����i�b�j

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    void Update()
    {
        // ��̎�����ݒ肵�A��莞�Ԍ�ɔj��
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        // ��G�ɓ��������ꍇ
        if (other.gameObject.CompareTag("Enemy"))
        {
            //IFeatureMine�����邩�m�F
            if (other.gameObject.TryGetComponent<IFeatureMine>(out var destroy))
                destroy.TakeDestroy();  //�G�폜
               // Destroy(collision.gameObject); // �G��j��
            
        }

        Destroy(gameObject);
    }
}
