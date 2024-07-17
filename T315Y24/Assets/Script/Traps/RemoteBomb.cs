/*=====
<RemoteBome.cs> 
└作成者：yamamoto

＞内容
playerが手動で起動させる罠のスクリプト

＞更新履歴
__Y24   
_M05    
D
31: プログラム作成: yamamoto
_M06    
D
12: リファクタリング: yamamoto
26: コメント追加: yamamoto
=====*/

//＞名前空間宣言
using Effekseer;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using static CCodingRule;

//＞Dataクラス定義
public class GameRemoteBombData
{
    //＞プロパティ定義
    public int SetRemoteBomb { get; set; }  //置いた数
    public int UseRemoteBomb { get; set; }  //使った回数
    public int RemoteBombKill { get; set; } //倒した数


    /*＞コンストラクタ
    引数１：int _nSetRemoteBomb ：置いた数   
    引数２：int _nUseRemoteBomb ：使った回数  
    引数３：int _nRemoteBombKill：倒した数 
    ｘ
    戻値：なし
    ｘ
    概要：データをリザルトに渡すようにまとめる
    */
    public GameRemoteBombData( int _nSetRemoteBomb, int _nUseRemoteBomb,int _nRemoteBombKill)
    {
        //各データをセット
        SetRemoteBomb = _nSetRemoteBomb;        //置いた数 
        UseRemoteBomb = _nUseRemoteBomb;        //使った回数
        RemoteBombKill = _nRemoteBombKill;      //倒した数 
    }
}

//＞クラス定義
public class RemoteBomb : CTrap
{
    //変数宣言
    [Header("プレハブ")]
    //[SerializeField,Tooltip("爆発時生成されるプレハブ")] private GameObject m_ExplosionEffectPrefab; // 爆発時生成されるプレハブ
    [SerializeField, Tooltip("爆発の判定用プレハブ")] private GameObject m_ExplosionCollPrefab; // 爆発時生成されるプレハブ
    [SerializeField, Tooltip("爆発時再生するエフェクト")] private EffekseerEffectAsset m_ExplosionEffect;  // 爆発時再生するエフェクト
    private static int m_nSetRemoteBomb;     //置いた数格納用
    private static int m_nUseRemoteBomb;     //使った回数格納用
    private static int m_nRemoteBombKill; //倒した数格納用


    /*＞更新関数
     引数：なし
     ｘ
     戻値：なし
     ｘ
     概要：更新
     */
    void Update()
    {
        if (!m_bMove)//設置されていたら
        {
            //＞保全
            if (m_ExplosionEffect == null)   //エフェクトがない
            {
#if UNITY_EDITOR    //エディタ使用中
                //＞エラー出力
                UnityEngine.Debug.LogWarning("必要な要素が不足しています");  //警告ログ出力
#endif
                //＞中断
                return; //処理しない
            }

            if ((Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("Explosion")) & m_bUse)
            {//使える状態なら入る
                m_audioSource.PlayOneShot(SE_ExpTrap);  //爆発SE再生
                SetCoolTime();              //クールタイムを設定
                m_nUseRemoteBomb++;          //使った回数を増やす

                //＞爆発エフェクト再生
                EffekseerSystem.PlayEffect(m_ExplosionEffect, transform.position);  //爆発位置に再生

                //爆発判定作成
                GameObject explosion = 
                    Instantiate(m_ExplosionCollPrefab, transform.position, Quaternion.identity);
                explosion.GetComponent<Explosion>().SetBombType(1); //格納先を設定
            }
         }
        SetTrap();//設置関数呼び出し
    }

    /*＞当たり判定関数
    引数１：Collision _Collision : 当たっているものの情報
    ｘ
    戻値：なし
    ｘ
    概要：当たっているとき呼び出される
    */
    private void OnCollisionStay(Collision _Collision)
    {
        SetCheck(_Collision);   //設置できるかどうか判定
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

    /*＞設置回数カウント関数
    引数１：Collision _Collision : 当たっているものの情報
    ｘ
    戻値：なし
    ｘ
    概要：設置されたときに呼び出される
    */
    public override void SetCount()
    {
        m_nSetRemoteBomb++;  //設置回数を増やす
    }

    /*＞データ引き渡し関数
    引数：なし
    ｘ
    戻値： new GameRemoteBombData(m_nSetRemoteBomb, m_nUseRemoteBomb, m_nRemoteBombKill):データ
    ｘ
    概要：リザルトに渡す用のデータを作成
    */
    public static GameRemoteBombData GetGameRemoteBombData()
    {
        m_nRemoteBombKill = Explosion.m_KillCount[1];//対応した配列から倒した数を取得
        Explosion.m_KillCount[1] = 0;               //配列を初期化
        //リザルトに渡す用のデータを作成
        return new GameRemoteBombData(m_nSetRemoteBomb, m_nUseRemoteBomb, m_nRemoteBombKill);
    }

    /*＞データリセット関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：初期化
    */
    public static void ResetRemoteBombData()
    {
        m_nSetRemoteBomb = 0;    //置いた数 初期化
        m_nUseRemoteBomb = 0;    //使った回初期化
        m_nRemoteBombKill = 0;   //倒した数 初期化
    }
}
