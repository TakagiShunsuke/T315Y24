using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using static CCodingRule;

//[System.Serializable]
public class GameRemoteBombData
{ 
    public int SetRemoteBomb { get; set; }
    public int UseRemoteBomb { get; set; }
    public int RemoteBombKill { get; set; }

    // コンストラクタ
    public GameRemoteBombData( int setRemoteBomb, int useRemoteBomb,int remoteBombKill)
    {
        SetRemoteBomb = setRemoteBomb;
        UseRemoteBomb = useRemoteBomb;
        RemoteBombKill = remoteBombKill;
    }
}

public class RemoteBomb : CTrap
{
    [SerializeField] private GameObject m_ExplosionEffectPrefab; // 爆発時生成されるプレハブ
    [SerializeField] private KeyCode m_ExplodingKey = KeyCode.B; //起爆のキー

    private static int m_SetRemoteBomb;
    private static int m_UseRemoteBomb;
    private static int m_RemoteBombKill;
    // Update is called once per frame
    void Update()
    {
        if (!m_bMove)
        {
           
            if ((Input.GetKeyDown(m_ExplodingKey)|| Input.GetButtonDown("Explosion")) & m_bUse)
            {
                m_audioSource.PlayOneShot(SE_ExpTrap);  //爆発SE再生
                SetCoolTime();
                m_UseRemoteBomb++;
                GameObject explosion = Instantiate(m_ExplosionEffectPrefab, transform.position, Quaternion.identity);
                Debug.Log("1");
                explosion.GetComponent<Explosion>().SetBombType(1);

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
    public override void SetCount()
    {
        m_SetRemoteBomb++;
        Debug.Log(m_SetRemoteBomb);
    }
    public static GameRemoteBombData GetGameRemoteBombData()
    {
        m_RemoteBombKill = Explosion.m_KillCount[1];
        return new GameRemoteBombData(m_SetRemoteBomb, m_UseRemoteBomb, m_RemoteBombKill);
    }
    public static void ResetRemoteBombData()
    {
        m_SetRemoteBomb = 0;
        m_UseRemoteBomb = 0;
        m_RemoteBombKill = 0;
    }
}
