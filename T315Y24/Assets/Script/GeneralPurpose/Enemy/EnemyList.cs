/*=====
<EnemyList.cs> //スクリプト名
└作成者：takagi

＞内容
敵を羅列して情報管理

＞注意事項
シングルトン


＞更新履歴
__Y24
_M06
D
05:プログラム作成:takagi
=====*/

//＞名前空間宣言
using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;  //Unity
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

//＞クラス定義
public class CEnemyList : CMonoSingleton<CEnemyList>
{
    [Serializable] public struct SpawnEnemyInfo
    {
        public AssetReferenceGameObject m_SpawnAssetRef;   //生成対象アセット
        public int m_SpawnAmount;    //生成量
    }   //敵生成用情報

    //＞プロパティ定義
    public List<SpawnEnemyInfo> SpawnInfo { get; set; } = null; //生成対象管理
    public AssetReferenceGameObject GetRandomSpawnAssetRef
    {
        get
        {
            int _nTotal = 0;

            if(SpawnInfo == null || SpawnInfo.Count == 0) 
            { 
                return null; }

            for (int nIdx = 0; nIdx < SpawnInfo.Count; nIdx++)  //生成候補すべて
            {
                _nTotal += SpawnInfo[nIdx].m_SpawnAmount;
            }
            var _nRand = UnityEngine.Random.Range(1, _nTotal);
            //この時点でnRand > 0である

            for (int nIdx = 0; nIdx < SpawnInfo.Count; nIdx++)  //生成候補すべて
            {
                if(_nRand <= SpawnInfo[nIdx].m_SpawnAmount)
                {
                    return SpawnInfo[nIdx].m_SpawnAssetRef;
                }
                else
                {
                    _nTotal -= SpawnInfo[nIdx].m_SpawnAmount;
                }
            }
            
            return null;    //_Total == 0



            //現在の情報からランダムに生成敵を決定
        }
    }

    /*＞終了関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：破棄時に行う処理
    */
    override protected void CustomOnDestroy()
    {
        //＞解放
        if(SpawnInfo != null)
        {
            for (int nIdx = 0; nIdx < SpawnInfo.Count; nIdx++)  //生成物すべて破棄する
            {
                if (SpawnInfo[nIdx].m_SpawnAssetRef != null && SpawnInfo[nIdx].m_SpawnAssetRef.Asset != null)    //LoadAssetAsync()関数を使用した
                {
                    SpawnInfo[nIdx].m_SpawnAssetRef.ReleaseAsset(); //参照をやめる
                }
            }
        }
    }
}