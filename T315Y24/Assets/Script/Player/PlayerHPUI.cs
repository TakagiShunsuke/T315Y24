/*=====
<PlayerHPUI.cs> //スクリプト名
└作成者：takagi

＞内容
プレイヤーのHPをUI表示する

＞注意事項
同一のオブジェクトに以下のコンポーネントがないと十分な機能をしません。
１.プレイヤーの情報が管理されているCPlayerScript


＞更新履歴
__Y24
_M05
D
15:プログラム作成:takagi
16:続き:takagi
31:リファクタリング:takagi
=====*/

//＞名前空間宣言
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;  //Unity
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

//＞クラス定義
public class CPlayerHPUI : MonoBehaviour
{
    //＞定数定義
    private const string CANVAS_NAME = "PlCanvas";  //キャンバス名
    private const string IMAGE_NAME = "Pl_Heart";   //画像オブジェクト名

    //＞変数宣言    
    [SerializeField] protected AssetReferenceTexture2D m_AssetRef; //生成対象アセット
    [SerializeField] private Vector2 m_FirstDrawPos = new Vector2(120.0f, 80.0f);   //UI一つ目の表示位置
    [SerializeField] private double m_dInterval;    //生成距離間隔
    [SerializeField] private double m_dImageScale = 1.6f;   //画像のスケーリング
    private GameObject m_CanvasObj; //UI表示のためのキャンバス用オブジェクト
    private CPlayerScript m_Player = null;    //プレイヤーの情報
    List<AsyncOperationHandle<Texture2D>> m_AssetLoadHandle= new List<AsyncOperationHandle<Texture2D>>();   //アセットをロード・管理する関数


    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    public void Start()
    {
        //＞初期化
        m_CanvasObj = new GameObject(); //キャンバスオブジェクト作成
        m_Player = GetComponent<CPlayerScript>(); //プレイヤーとしての振る舞い方取得
#if UNITY_EDITOR    //エディタ使用中
        if (m_Player == null)   //取得に失敗した時
        {
            //＞エラー出力
            UnityEngine.Debug.LogError("プレイヤーに設定されていません");    //警告ログ出力
        }
#endif   

        //＞キャンバス作成・設定
        Canvas _Canvas = m_CanvasObj.AddComponent<Canvas>(); //機能をキャンバス化
        _Canvas.renderMode = RenderMode.ScreenSpaceOverlay; //UIを最前面に出す
        _Canvas.AddComponent<CanvasScaler>();   //UIのスケール制御
        _Canvas.AddComponent<GraphicRaycaster>();   //キャンバスへのレイ判定
        _Canvas.name = CANVAS_NAME;  //名前付け
        _Canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1;   //シェーダーセマンティクス：テクスチャ座標
        _Canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.Normal;  //シェーダーセマンティクス：法線
        _Canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.Tangent; //シェーダーセマンティクス：接線
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
        //＞検査
        if (m_Player == null)   //必要要件の不足時
        {
#if UNITY_EDITOR    //エディタ使用中
            //＞エラー出力
            UnityEngine.Debug.LogWarning("必要な要素が不足しています");  //警告ログ出力
#endif

            //＞中断
            return; //更新処理中断
        }

        //＞HP増加対応
        for (int _nIdx = m_CanvasObj.transform.childCount; _nIdx < m_Player.HP; _nIdx++)    //子オブジェクトの数をHPに合わせて増やす(小数点以下切り捨て)
        {
            //＞イメージ作成
            MakeHPUI(_nIdx);    //HPを作成する
        }

        //＞HP減少対応
        for (int _nIdx = m_Player.HP >= 0.0d ? (int)m_Player.HP : 0;     //イメージはマイナス個にできない
            _nIdx < m_CanvasObj.transform.childCount; _nIdx++)   //子オブジェクトの数をHPに合わせて減らす(小数点以下切り捨て)
        {
            //＞オブジェクト除去
            Destroy(m_CanvasObj.transform.GetChild(_nIdx).gameObject);  //一番最後に作られた子オブジェクトを削除
        }
    }

    /*＞HPUI作成関数
    引数：int HPIdx：作成するUIが何番目のものか
    ｘ
    戻値：なし
    ｘ
    概要：HPのUI画像を作成・配置
    */
    private async void MakeHPUI(int HPIdx)
    {
        //＞変数宣言
        GameObject _ImageObj = new GameObject(); //画像表示用オブジェクト

        //＞初期化
        _ImageObj.name = IMAGE_NAME; //名前付け
        _ImageObj.transform.parent = m_CanvasObj.transform;   //子オブジェクトに追加

        //＞画像読み込み
        var _AssetLoadHandle = Addressables.LoadAssetAsync<Texture2D>(m_AssetRef);  //テクスチャデータを読み込む関数取得
        var _Texture = await _AssetLoadHandle.Task; //テクスチャ読み込みを非同期で実行
        var _Sprite = Sprite.Create(_Texture, new Rect(0, 0, _Texture.width, _Texture.height), Vector2.zero);   //テクスチャから画像データ作成
        _ImageObj.AddComponent<Image>().sprite = _Sprite; //画像表示を機能的に実装し、画像登録

        //＞平面ポリゴン
        var _RectTransform = _ImageObj.GetComponent<RectTransform>();   //取得
        if (_RectTransform != null)   //取得に成功した時
        {
            _RectTransform.position = new Vector2(m_FirstDrawPos.x + (float)m_dInterval * (HPIdx % 5),
                Screen.currentResolution.height - m_FirstDrawPos.y - (float)m_dInterval * (HPIdx / 5)); //ポリゴンの位置
            _RectTransform.localScale = new Vector2((float)m_dImageScale, (float)m_dImageScale);    //ポリゴンのスケーリング
        }
#if UNITY_EDITOR    //エディタ使用中
        else
        {
            //＞エラー出力
            UnityEngine.Debug.LogWarning("画像が登録できません");   //警告ログ出力
        }
#endif
        
        //＞更新
        m_AssetLoadHandle.Add(_AssetLoadHandle);    //使用している関数を管理
    }

    /*＞破棄関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス破棄時に行う処理
    */
    private void OnDestroy()
    {
        //＞解放
        for (int nIdx = 0; nIdx < m_AssetLoadHandle.Count; nIdx++)  //生成物すべて破棄する
        {
            if (m_AssetLoadHandle[nIdx].IsValid()) //ヌルでない
            {
                Addressables.Release(m_AssetLoadHandle[nIdx]); //参照をやめる
            }
        }
    }
}