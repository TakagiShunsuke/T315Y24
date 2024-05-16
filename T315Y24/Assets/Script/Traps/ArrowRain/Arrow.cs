using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrow : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float lifetime = 5f; // –î‚Ìõ–½i•bj
    [SerializeField] bool m_bPenetration;   //“G‚ğŠÑ’Ê‚·‚é‚©‚Ç‚¤‚©

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
        // –î‚Ìõ–½‚ğİ’è‚µAˆê’èŠÔŒã‚É”jŠü
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        // –î‚ª“G‚É“–‚½‚Á‚½ê‡
        if (other.gameObject.CompareTag("Enemy"))
        {
            //IFeatureMine‚ª‚Â‚¢‚é‚©Šm”F
            if (other.gameObject.TryGetComponent<IFeatureMine>(out var destroy))
                destroy.TakeDestroy();  //“Gíœ
               // Destroy(collision.gameObject); // “G‚ğ”j‰ó
            
        }

        if(m_bPenetration == false)
             Destroy(gameObject);
    }
}
