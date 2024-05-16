using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrow : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float lifetime = 5f; // 矢の寿命（秒）

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
        // 矢の寿命を設定し、一定時間後に破棄
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        // 矢が敵に当たった場合
        if (other.gameObject.CompareTag("Enemy"))
        {
            //IFeatureMineがついるか確認
            if (other.gameObject.TryGetComponent<IFeatureMine>(out var destroy))
                destroy.TakeDestroy();  //敵削除
               // Destroy(collision.gameObject); // 敵を破壊
            
        }

        Destroy(gameObject);
    }
}
