using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Explosion : MonoBehaviour
{
   private float ObjectRadius;  //オブジェクトの半径
   private Vector3 initialPosition; // 初期位置
   [SerializeField] private double LowerSpeed = 0.1d;
    void Start()
    {
        ObjectRadius = transform.localScale.x / 2.0f; // オブジェクトの半径を取得
        initialPosition = transform.position; // 初期位置を設定
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.LogWarning("まね1");
        // 範囲内の敵を検出
        Collider[] colliders = Physics.OverlapSphere(transform.position, ObjectRadius);
        foreach (Collider collider in colliders)
        {
            Debug.LogWarning("まね2");
            if (collider.CompareTag("Enemy"))
            {
                Debug.LogWarning("まね3");
                if (collider.gameObject.TryGetComponent<IFeatureMine>(out var destroy))
                    destroy.TakeDestroy();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // オブジェクトを下に移動させる
        transform.position -= new Vector3(0f, (float)LowerSpeed * Time.deltaTime, 0f);

        // 半径分だけ下に移動したかどうかを判断し、破壊する
        if (transform.position.y <= initialPosition.y - ObjectRadius)
        {
            Destroy(gameObject);
        }
    }
}
