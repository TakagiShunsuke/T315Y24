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
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;

//＞クラス定義
public class Mine : CTrap
{
    //＞変数宣言
    [SerializeField] private GameObject m_ExplosionEffectPrefab; // 爆発時生成されるプレハブ
    [SerializeField] public AudioClip SE_ExpMine;  // 罠設置時のSE
    AudioSource audioSource;    // AudioSourceを追加
    [SerializeField] public AudioClip SE_explosion; // 爆発時のSE

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
            Instantiate(m_ExplosionEffectPrefab, transform.position, Quaternion.identity);
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
}
