/*=====
<TrapManager.cs> //スクリプト名
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
21:リファクタリング:takagi
=====*/

//＞名前空間宣言
using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

//＞クラス定義
public class CTrapManager : CMonoSingleton<CTrapManager>
{
    //＞定数定義
    private const string OBJECT_NAME = "TrapManager"; //このオブジェクトが生成されたときの名前

    //＞変数宣言
    //[Header("全ての罠")]
    //[SerializeField, Tooltip("罠")] private List<GameObject> AllTrap = null; //全ての罠管理
    private List<GameObject> AllTrap = new List<GameObject>(); //全ての罠管理

    //＞プロパティ定義
    public List<GameObject> HaveTraps { get; private set; } = new List<GameObject>(); //所持罠


    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    protected override void CustomAwake()
    {
        //＞リネーム
        gameObject.name = OBJECT_NAME;  //自身のオブジェクト名変更

        //＞管理対象登録
        for (int _nCnt = 0; _nCnt < transform.childCount; _nCnt++)
        {
            var _Obj = transform.GetChild(_nCnt).gameObject;
            _Obj.SetActive(false);
            AllTrap.Add(_Obj);
        }
    }


    protected override void Start()
    {
        //＞罠編成代替処理
        int _nIdx = 0;
        while (HaveTraps.Count < CTrapSelect.Instance.HavableTrapNum && _nIdx < AllTrap.Count)  //所持数まで
        {
            HaveTraps.Add(AllTrap[_nIdx]);
            _nIdx++;
        }
    }

    /*＞更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    protected override void Update()
    {
        //TODO:罠編成処理
    }
}