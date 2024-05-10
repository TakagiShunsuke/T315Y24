using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class ExplosionManager : MonoBehaviour
{
    
    public GameObject ExplosionEffectPrefab;
    private GameObject explosionEffect;
    [SerializeField] private double dExplosionRadius = 5.0d;    // 地雷爆発範囲
    private double dExplosionDisplayTime = 0.0d;
    [SerializeField] private double dDisplayTime = 1.0d;    // 地雷爆発範囲


    public static ExplosionManager Instance;
    
    // Start is called before the first frame update
    private void Awake()
    {
        // シングルトンパターンを使用して、ExplosionManagerの唯一のインスタンスを確保する
        if (Instance == null)
        {
            Instance = this;
        }
        
    }
    private void FixedUpdate()
    {
        //＞地雷の再利用カウントダウン
        if (dExplosionDisplayTime > 0.0d)   //クールダウン中
        {
            Debug.LogWarning("カウント");
            dExplosionDisplayTime -= Time.fixedDeltaTime;
            if (dExplosionDisplayTime < 0.0d) { Debug.LogWarning("かうんと2"); Destroy(explosionEffect); }  //地雷利用可能に
        }
    }
    public void CreateExplosion(Vector3 position)
    {
       Debug.LogWarning("マネ1");
        explosionEffect = Instantiate(ExplosionEffectPrefab, position, Quaternion.identity);
       Debug.LogWarning(position);
       Debug.LogWarning(ExplosionEffectPrefab);
        dExplosionDisplayTime = dDisplayTime;
         // 範囲内の敵を検出
         Collider[] colliders = Physics.OverlapSphere(position,(float)dExplosionRadius);
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
}
