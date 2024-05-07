/*=====
<SpawnRandom.cs> //スクリプト名
└作成者：takagi

＞内容
ランダム生成

＞更新履歴
__Y24
_M05
D
06:プログラム作成:takagi
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;  //Unity
using UseRandom;    //ランダム初期化

//＞クラス定義
public class CSpawnRandom : CGetObjects
{
    /*＞生成関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：関数例
    */
    public void Create()
    {
        //＞生成
        if(m_SpawnAssetRef != null && m_SpawnAssetRef.Count > 0)    //リストが存在・空でない
        {
            m_SpawnAssetRef[Random.Range(0, m_SpawnAssetRef.Count)].InstantiateAsync(); //ランダム対象生成
        }
    }
}