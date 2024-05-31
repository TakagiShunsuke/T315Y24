/*=====
<NavigationPlayer.cs> //スクリプト名
└作成者：takagi

＞内容
敵がオブジェクトに引っかからずに最短距離でプレイヤーを追いかける

＞注意事項
Hierarchyに「Player」の名前以外でプレイヤーを置いたとき追いかけなくなるので書き換えが必要です

＞更新履歴
__Y24
_M05
D
22:プログラム作成:suzumura
30:コメント追加:yamamoto
=====*/
//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//＞クラス定義
public class NavigationPlayer : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] private GameObject player;
    NavMeshAgent agent;

    /*＞初期化関数
   引数１：なし
   ｘ
   戻値：なし
   ｘ
   概要：インスタンス生成時に行う処理
   */
    void Start()
    {
        player = GameObject.Find("Player");//検索
        agent = GetComponent<NavMeshAgent>();
    }

    /*＞更新関数
   引数：なし
   ｘ
   戻値：なし
   ｘ
   概要：一定時間ごとに行う更新処理
   */
    void Update()
    {
        agent.destination = player.transform.position;
    }
}
