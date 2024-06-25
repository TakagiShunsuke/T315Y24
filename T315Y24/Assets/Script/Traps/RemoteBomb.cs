using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CCodingRule;

public class RemoteBomb : CTrap
{
    [SerializeField] private GameObject m_ExplosionEffectPrefab; // ���������������v���n�u
    [SerializeField] private KeyCode m_ExplodingKey = KeyCode.B; //�N���̃L�[

    // Update is called once per frame
    void Update()
    {
        if (!m_bMove)
        {
           
            if ((Input.GetKeyDown(m_ExplodingKey)|| Input.GetButtonDown("Explosion")) & m_bUse)
            {
                m_audioSource.PlayOneShot(SE_ExpTrap);  //����SE�Đ�
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
