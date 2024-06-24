/*=====
<SpawnRandom.cs> //スクリプト名
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

_M06
D
21:リファクタリング:takagi
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UseRandom;

//＞クラス定義
public class CSpawnRandom : CGetObjects
{
    //＞変数宣言
    [SerializeField] private Vector3 m_SpawnPos;  //生成位置
    [SerializeField] private Quaternion m_SpawnRotate;  //生成位置


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
            m_SpawnAssetRef[Random.Range(0, m_SpawnAssetRef.Count)].InstantiateAsync(m_SpawnPos, m_SpawnRotate); //ランダム対象生成
        }
    }
}