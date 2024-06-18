/*=====
<EnemyList.cs> //スクリプト名
└作成者：takagi

＞内容
敵を羅列して情報管理

＞注意事項
シングルトンである
GetRandomSpawnAssetRefは正しく情報が取得できなかった時ヌルを返すので注意


＞更新履歴
__Y24
_M06
D
05:プログラム作成:takagi
09:コード改善:takagi
13:確率修正:takagi
18:生成比重の最小値設定:takagi
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
    //＞構造体定義
    [Serializable] public struct SpawnEnemyInfo
    {
        public AssetReferenceGameObject m_SpawnAssetRef;   //生成対象アセット
        [Min(0)] public int m_SpawnAmount;    //生成比重
    }   //敵生成用情報

    //＞定数定義
    const string OBJECT_NAME = "EnemyList"; //このオブジェクトが生成されたときの名前

    //＞プロパティ定義
    public List<SpawnEnemyInfo> SpawnInfo { get; set; } = null; //生成対象管理
    public AssetReferenceGameObject GetRandomSpawnAssetRef
    {
        get
        {
            //＞変数宣言
            int _nTotal = 0;    //情報操作用の一時変数

            //＞ヌルチェック
            if (SpawnInfo == null)  //そもそも処理をする相手がいない
            { 
                return null;    //処理中断
            }

            //＞重み付け
            for (int _nIdx = 0; _nIdx < SpawnInfo.Count; _nIdx++)  //生成候補すべて
            {
                //＞乱数最大値決定
                _nTotal += SpawnInfo[_nIdx].m_SpawnAmount;   //重みの総和をとる
            }

            //＞ランダムに生成敵を決定
            var _nRand = UnityEngine.Random.Range(1, _nTotal + 1);  //重みを含めて算出。この時点でnRand > 0である。またRangeにMax値は含まれないため+1。

            //＞重みの定義域層から対象を捜査
            for (int nIdx = 0; nIdx < SpawnInfo.Count; nIdx++)  //生成候補すべて
            {
                if(_nRand <= SpawnInfo[nIdx].m_SpawnAmount) //その定義域内に乱数が収まる
                {
                    //＞重みの層から生成対象を選出・提供する
                    return SpawnInfo[nIdx].m_SpawnAssetRef;     //生成対象確定
                }
                else
                {
                    _nRand -= SpawnInfo[nIdx].m_SpawnAmount;   //添え字を進めるにあたり、重みの定義域層を変更
                }
            }
            
            //＞失敗時対応
            return null;    //_Total == 0またはリストが空であり、失敗扱い。
        }
    }   //ランダムに制定される生成対象のゲッタ


    /*＞初期化関数
     引数１：なし
     ｘ
     戻値：なし
     ｘ
     概要：インスタンス生成時に行う処理
     */
    override protected void CustomAwake()
    {
        //＞リネーム
        gameObject.name = OBJECT_NAME;  //自身のオブジェクト名変更
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
        if(SpawnInfo != null)   //対象が存在する
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