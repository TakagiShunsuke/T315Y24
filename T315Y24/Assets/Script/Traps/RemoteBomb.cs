using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CCodingRule;

public class RemoteBomb : CTrap
{
    [SerializeField] private GameObject m_ExplosionEffectPrefab; // 爆発時生成されるプレハブ
    [SerializeField] private KeyCode m_ExplodingKey = KeyCode.B; //起爆のキー

    // Update is called once per frame
    void Update()
    {
        if (!m_bMove)
        {
           
            if ((Input.GetKeyDown(m_ExplodingKey)|| Input.GetButtonDown("Explosion")) & m_bUse)
            {
                m_audioSource.PlayOneShot(SE_ExpTrap);  //爆発SE再生
                SetCoolTime();
                Instantiate(m_ExplosionEffectPrefab, transform.position, Quaternion.identity);
            }
         }

        aaa();
    }
    private void OnCollisionStay(Collision collision)
    {
        SetCheck(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        OutCheck(collision);
    }
}
