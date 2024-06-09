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
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;  //Unity
using UnityEngine.AddressableAssets;
using UseRandom;    //ランダム初期化

//＞クラス定義
public class CSpawnEnemy : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] protected AssetReferenceGameObject m_SpawnAssetRef; //生成対象アセット管理

    //＞変数宣言
    [SerializeField] private Rect m_SpawnRect;  //生成範囲  //TODO:ここに値を入れなかったら自分の位置・サイズを基準にするように
    [SerializeField] private double m_dAltitude;    //高さ
    [SerializeField] private Quaternion m_SpawnRotate;  //生成位置

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