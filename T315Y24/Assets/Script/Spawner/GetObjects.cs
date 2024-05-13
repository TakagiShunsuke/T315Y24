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
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.AddressableAssets;  //Unity

//＞クラス定義
public abstract class CGetObjects : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] protected List<AssetReferenceGameObject> m_SpawnAssetRef; //生成対象アセット管理

    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    virtual public void Start()
    {
    }

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
        for (int nIdx = 0; nIdx < m_SpawnAssetRef.Count; nIdx++)  //生成物すべて破棄する
        {
            m_SpawnAssetRef[nIdx].ReleaseAsset(); //参照をやめる
        }
    }
}