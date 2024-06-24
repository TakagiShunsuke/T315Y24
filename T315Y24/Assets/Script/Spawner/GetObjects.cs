/*=====
<GetObjects.cs> //スクリプト名
└作成者：takagi

＞内容
オブジェクト取得

＞更新履歴
__Y24
_M05
D
06:プログラム作成:takagi
07:ロード方法変更:takagi
10:警告が出る原因を除去:takagi

_M06
D
21:リファクタリング:takagi
24:リファクタリング:takagi
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.AddressableAssets;

//＞クラス定義
public abstract class CGetObjects : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] protected List<AssetReferenceGameObject> m_SpawnAssetRef; //生成対象アセット管理


    /*＞終了関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：破棄時に行う処理
    */
    virtual protected void OnDestroy()
    {
        //＞解放
        for (int _nIdx = 0; _nIdx < m_SpawnAssetRef.Count; _nIdx++)  //生成物すべて破棄する
        {
            if (m_SpawnAssetRef[_nIdx].Asset != null)    //LoadAssetAsync()関数を使用した
            {
                m_SpawnAssetRef[_nIdx].ReleaseAsset(); //参照をやめる
            }
        }
    }
}