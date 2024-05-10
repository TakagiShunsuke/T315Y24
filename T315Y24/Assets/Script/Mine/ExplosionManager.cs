using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class ExplosionManager : MonoBehaviour
{
    
    public GameObject ExplosionEffectPrefab;
    private GameObject explosionEffect;
    [SerializeField] private double dExplosionRadius = 5.0d;    // �n�������͈�
    private double dExplosionDisplayTime = 0.0d;
    [SerializeField] private double dDisplayTime = 1.0d;    // �n�������͈�


    public static ExplosionManager Instance;
    
    // Start is called before the first frame update
    private void Awake()
    {
        // �V���O���g���p�^�[�����g�p���āAExplosionManager�̗B��̃C���X�^���X���m�ۂ���
        if (Instance == null)
        {
            Instance = this;
        }
        
    }
    private void FixedUpdate()
    {
        //���n���̍ė��p�J�E���g�_�E��
        if (dExplosionDisplayTime > 0.0d)   //�N�[���_�E����
        {
            Debug.LogWarning("�J�E���g");
            dExplosionDisplayTime -= Time.fixedDeltaTime;
            if (dExplosionDisplayTime < 0.0d) { Debug.LogWarning("�������2"); Destroy(explosionEffect); }  //�n�����p�\��
        }
    }
    public void CreateExplosion(Vector3 position)
    {
       Debug.LogWarning("�}�l1");
        explosionEffect = Instantiate(ExplosionEffectPrefab, position, Quaternion.identity);
       Debug.LogWarning(position);
       Debug.LogWarning(ExplosionEffectPrefab);
        dExplosionDisplayTime = dDisplayTime;
         // �͈͓��̓G�����o
         Collider[] colliders = Physics.OverlapSphere(position,(float)dExplosionRadius);
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
}
