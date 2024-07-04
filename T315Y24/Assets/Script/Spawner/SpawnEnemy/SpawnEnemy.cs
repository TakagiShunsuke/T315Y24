﻿/*=====
<SpawnEnemy.cs> //スクリプト名
└作成者：takagi

＞内容
ランダム生成

＞注意事項
位置固定(一か所)です。他の範囲を使いたい場合は別コンポーネントで。

＞更新履歴
__Y24
_M05
D
06:プログラム作成:takagi
07:続き:takagi
09:続き:takagi

_M06
D
21:リファクタリング:takagi
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UseRandom;

//＞クラス定義
public class CSpawnEnemy : MonoBehaviour
{
    //＞変数宣言
    [Header("生成範囲")]
    [SerializeField, Tooltip("生成エリア")] private Rect m_SpawnRect;  //生成範囲  //TODO:ここに値を入れなかったら自分の位置・サイズを基準にするように
    [SerializeField, Tooltip("標高")] private double m_dAltitude;    //高さ
    [SerializeField, Tooltip("回転")] private Quaternion m_SpawnRotate;  //生成時回転


    /*＞生成関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：敵生成
    */
    public void Create()
    {
        //＞生成位置選定
        Vector3 _vSpawnPos = new Vector3(Random.Range(m_SpawnRect.x, m_SpawnRect.x + m_SpawnRect.width), (float)m_dAltitude, Random.Range(m_SpawnRect.y, m_SpawnRect.y + m_SpawnRect.height));  //生成座標(x)
        //TODO:四角形が変則な形でも対応できるように(ベクトル?)

        //＞生成
        if (CEnemyList.Instance != null && CEnemyList.Instance.GetRandomSpawnAssetRef != null)    //生成対象が存在・空でない
        {
            CEnemyList.Instance.GetRandomSpawnAssetRef.InstantiateAsync(_vSpawnPos, m_SpawnRotate); //ランダム敵生成
        }
    }
}