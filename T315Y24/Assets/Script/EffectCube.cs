using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEffectCube : MonoBehaviour
{
    [SerializeField] private float m_fLifeTime; // ê∂ë∂éûä‘


    // Update is called once per frame
    private void Update()
    {
        m_fLifeTime -= Time.deltaTime;

        if(m_fLifeTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
