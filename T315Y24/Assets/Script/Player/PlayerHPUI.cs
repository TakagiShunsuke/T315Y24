/*=====
<PlayerHPUI.cs> //スクリプト名
└作成者：takagi

＞内容


＞注意事項
同一のオブジェクトに以下のコンポーネントがないと敵として十分な機能をしません。
１.IFeatureBaseを継承した、特徴を表すコンポーネント
２.攻撃範囲を表す扇形の領域判定AreaSector
３.物理演算を行うRigidbody


以下のコンポーネントがある場合はその初期値をシリアライズされて実装される値をも無視して初期化します。
１.IMoveを継承した、移動を行うコンポーネントの変数Speed


＞更新履歴
__Y24
_M05
D
03:プログラム作成:takagi
04:続き:takagi
11:プレイヤー削除、AreaSector変更への対応:takagi
15
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;  //Unity

//＞クラス定義
public class CPlayerHPUI : MonoBehaviour
{
    private Canvas m_Canvas = null;    //検知対象
    [SerializeField] protected AssetReferenceTexture2D m_AssetRef; //生成対象アセット管理
    //[SerializeField] private Sprite m_Sp;   //画像
    [SerializeField] private Vector2 m_First = new Vector2(500.0f, 500.0f);   //1つ目
    [SerializeField] private double m_dInterval;    //生成間隔
    [SerializeField] private float a = 1.6f;
    private GameObject CanvasObj;
    private CPlayerScript Player = null;
    private Sprite sprite;
    private List<AssetReferenceTexture2D> Assets;
    List<AsyncOperationHandle<Texture2D>> op= new List<AsyncOperationHandle<Texture2D>>();
    Sprite sp;


    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    public void Start()
    {
        //HP取得
        Player = GetComponent<CPlayerScript>();
        CanvasObj = new GameObject();
        //キャンバス作り
        CanvasObj.AddComponent<Canvas>();
        m_Canvas = CanvasObj.GetComponent<Canvas>();
        m_Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        m_Canvas.AddComponent<CanvasScaler>();
        m_Canvas.AddComponent<GraphicRaycaster>();
        m_Canvas.name = "PlCanvas";

        //イメージ作り
        //if (m_AssetRef != null)
        //    m_AssetRef.LoadAssetAsync().Completed += (option) =>
        //    {
        //        var tex = option.Result;
        //        sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        //    };
        if (Player != null)
            if (Player.HP > 1.0d)
            {
                k();
            }


        //var Tex2 = await hand.Task;
        //sp = Sprite.Create(Tex2, new Rect(0, 0, Tex2.width, Tex2.height), Vector2.zero);
        //Debug.Log(sp);
        //GameObject ImageObj = new GameObject();
        //ImageObj.AddComponent<Image>();
        //ImageObj.name = "Pl_Heart";
        //ImageObj.GetComponent<Image>().sprite = sp;
        //ImageObj.GetComponent<RectTransform>().position = new Vector2(0.0f + m_First.x,
        //Screen.currentResolution.height - m_First.y);
        //ImageObj.GetComponent<RectTransform>().localScale = new Vector2(a, a);
        ////親子付け
        //ImageObj.transform.parent = CanvasObj.transform;
    }

    /*＞物理更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    private void FixedUpdate()
    {
        for (int i = CanvasObj.transform.childCount; i < Player.HP; i++)
        {
            k(i);
        }


        for (int i = (int)Player.HP; i < CanvasObj.transform.childCount; i++)
        {
            Destroy(CanvasObj.transform.GetChild(i).gameObject);
        }
    }

    private async void k(int g = 0)
    {
        GameObject ImageObj = new GameObject();
        ImageObj.AddComponent<Image>();
        ImageObj.name = "Pl_Heart";
        var qop = Addressables.LoadAssetAsync<Texture2D>(m_AssetRef);
        //var ass = new AssetReferenceTexture2D(m_AssetRef.ToString());
        //if (m_AssetRef != null)
        //    ass.LoadAssetAsync().Completed += (option) =>
        //    {
        //        var tex = option.Result;
        //        ImageObj.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        //    };
        //op = m_AssetRef.LoadAssetAsync();
        var Tex2 = await qop.Task;
        sp = Sprite.Create(Tex2, new Rect(0, 0, Tex2.width, Tex2.height), Vector2.zero);
        ImageObj.GetComponent<Image>().sprite = sp;
            ImageObj.GetComponent<RectTransform>().position = new Vector2(0.0f + m_First.x + (float)m_dInterval * (g % 5),
            Screen.currentResolution.height - m_First.y - (float)m_dInterval * (g / 5));
        ImageObj.GetComponent<RectTransform>().localScale = new Vector2(a, a);
        //親子付け
        ImageObj.transform.parent = CanvasObj.transform;
        //Assets.Add(ass);
        op.Add(qop);
        Debug.Log(sp);
    }

    private void OnDestroy()
    {
        for(var i = 0; i < op.Count; i++)
        {
            //if
            Addressables.Release(op[i]);
        }
    }
}