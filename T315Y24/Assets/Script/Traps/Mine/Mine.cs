/*=====
<Mine.cs> 
└作成者：yamamoto

＞内容
地雷に付けるスクリプト。
爆破エフェクトの生成はここで

＞注意事項  
地雷にIsTriggerがついていると動作しません。
Prefabを設定していないと爆発エフェクトが生成されない。

＞更新履歴
__Y24   
_M05    
D
08 :プログラム作成:yamamoto 
09 :仕様変更の為処理を変更:yamamoto
10:コメント追加:yamamoto
12:リキャスト時間追加:yamamoto

_M06
D
08：親クラス追加それに伴いプログラム書き換え:yamamoto
13：爆発時SE追加:nieda
18：SE追加:nieda
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;

public class GameMineData
{
    public int SetMine { get; set; }
    public int UseMine { get; set; }
    public int MineKill { get; set; }

    // コンストラクタ
    public GameMineData(int setMine, int useMine, int mineKill)
    {
        SetMine = setMine;
        UseMine = useMine;
        MineKill = mineKill;
    }
}

//＞クラス定義
public class Mine : CTrap
{
    //＞変数宣言
    [SerializeField] private GameObject m_ExplosionEffectPrefab; // 爆発時生成されるプレハブ
    [SerializeField] public AudioClip SE_ExpMine;  // 罠設置時のSE
    AudioSource audioSource;    // AudioSourceを追加
    [SerializeField] public AudioClip SE_explosion; // 爆発時のSE

    private static int m_SetMine ;
    private static int m_UseMine ;
    private static int m_MineKill;

    /*＞カウント初期化関数
 引数１：なし
 ｘ
 戻値：なし
 ｘ
 概要：シーンが変わるときに呼ばれる処理
 */
     void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        m_SetMine = 0;
    }

    /*＞地雷当たり判定関数
    引数１：当たり判定があったオブジェクトの情報
    ｘ
    戻値：なし
    ｘ
    概要：敵が地雷に触れたときのみ処理される
    */
    private void OnCollisionStay(Collision collision)     //地雷に何かが当たってきたとき
    {
        if (Check(collision))  // 起爆できるか
        {
            audioSource = GetComponent<AudioSource>();  //AudioSourceコンポーネントを追加
            audioSource.PlayOneShot(SE_explosion);  //爆発SE再生
            m_UseMine++;
            GameObject explosion = Instantiate(m_ExplosionEffectPrefab, transform.position, Quaternion.identity);
            explosion.GetComponent<Explosion>().SetBombType(0);
            Debug.Log("1");
        }
        SetCheck(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        OutCheck(collision);
    }
    void Update()
    {
        aaa();
    }
    public override void SetCount()
    {
        m_SetMine++;
        Debug.Log(m_SetMine);
    }
    public static GameMineData GetGameMineData()
    {
        m_MineKill=Explosion.m_KillCount[0];
        return new GameMineData(m_SetMine, m_UseMine,m_MineKill);
    }
    public static void ResetMineData()
    {
        m_SetMine = 0;
        m_UseMine = 0;
        m_MineKill = 0;
    }
}
