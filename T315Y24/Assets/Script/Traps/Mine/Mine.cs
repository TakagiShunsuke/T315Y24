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
08：親クラス追加それに伴いプログラム書き換え：yamamoto
13：爆発時SE追加：nieda
18：SE追加：nieda
26：コメント追加：yamamoto
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;

//＞Dataクラス定義
public class GameMineData
{
    //＞プロパティ定義
    public int SetMine { get; set; }    //置いた数
    public int UseMine { get; set; }    //使った回数
    public int MineKill { get; set; }   //倒した数

    /*＞コンストラクタ
   引数１：int _nSetRemoteBomb ：置いた数   
   引数２：int _nUseRemoteBomb ：使った回数  
   引数３：int _nRemoteBombKill：倒した数 
   ｘ
   戻値：なし
   ｘ
   概要：データをリザルトに渡すようにまとめる
   */
    public GameMineData(int _nSetMine, int _nUseMine, int _nMineKill)
    {
        //各データをセット
        SetMine = _nSetMine;     //置いた数 
        UseMine = _nUseMine;     //使った回数
        MineKill = _nMineKill;   //倒した数 
    }
}

//＞クラス定義
public class Mine : CTrap
{
    //＞変数宣言
    [Header("プレハブ")] 
    [SerializeField,Tooltip("爆発時生成されるプレハブ")] private GameObject m_ExplosionEffectPrefab; // 爆発時生成されるプレハブ
    private static int m_nSetMine;       //置いた数 
    private static int m_nUseMine;       //使った回数
    private static int m_nMineKill;      //倒した数 


    /*＞地雷当たり判定関数
    引数１：Collision _Collision : 当たっているものの情報
    ｘ
    戻値：なし
    ｘ
    概要：敵が地雷に触れたときのみ処理される
    */
    private void OnCollisionStay(Collision collision)     //地雷に何かが当たってきたとき
    {
        if (Check(collision))  // 起爆できるか
        {
            m_audioSource.PlayOneShot(SE_ExpTrap);  //爆発SE再生
            m_nUseMine++;    //使った回数を増やす
            //爆発エフェクト作成
            GameObject explosion = Instantiate(m_ExplosionEffectPrefab, transform.position, Quaternion.identity);
            explosion.GetComponent<Explosion>().SetBombType(0);//格納先を設定
        }
        SetCheck(collision);    //設置できるかどうか判定
    }

    /*＞当たり判定関数
    引数１：Collision _Collision : 当たっているものの情報
    ｘ
    戻値：なし
    ｘ
    概要：当たっている状態から当たらなくなったら呼び出される
    */
    private void OnCollisionExit(Collision collision)
    {
        OutCheck(collision);    //設置できるかどうか判定
    }

    /*＞更新関数
     引数：なし
     ｘ
     戻値：なし
     ｘ
     概要：更新
     */
    void Update()
    {
        SetTrap();  //設置関数呼び出し
    }

    /*＞設置回数カウント関数
    引数１：Collision _Collision : 当たっているものの情報
    ｘ
    戻値：なし
    ｘ
    概要：設置されたときに呼び出される
    */
    public override void SetCount()
    {
        m_nSetMine++;
    }

    /*＞データ引き渡し関数
    引数：なし
    ｘ
    戻値：GameMineData(m_nSetMine, m_nUseMine,m_nMineKill):データ
    ｘ
    概要：リザルトに渡す用のデータを作成
    */
    public static GameMineData GetGameMineData()
    {
        m_nMineKill=Explosion.m_KillCount[0];       //対応した配列から倒した数を取得
        Explosion.m_KillCount[0]=0;                 //配列を初期化
        //リザルトに渡す用のデータを作成
        return new GameMineData(m_nSetMine, m_nUseMine,m_nMineKill);
    }

    /*＞データリセット関数
   引数：なし
   ｘ
   戻値：なし
   ｘ
   概要：初期化
   */
    public static void ResetMineData()
    {
        m_nSetMine = 0;     //置いた数 初期化
        m_nUseMine = 0;     //使った回初期化
        m_nMineKill = 0;    //倒した数 初期化
    }
}
