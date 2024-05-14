using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCube : MonoBehaviour
{
    [SerializeField] float m_fLifeTime; // ê∂ë∂éûä‘

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_fLifeTime -= Time.deltaTime;

        if(m_fLifeTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
