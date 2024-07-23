using Effekseer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


//＞Dataクラス定義
public class GameCutterData
{
    //＞プロパティ定義
    public int SetCutter { get; set; }    //置いた数
    public int UseCutter { get; set; }    //使った回数
    public int CutterKill { get; set; }   //倒した数

    /*＞コンストラクタ
   引数１：int _nSetRemoteBomb ：置いた数   
   引数２：int _nUseRemoteBomb ：使った回数  
   引数３：int _nRemoteBombKill：倒した数 
   ｘ
   戻値：なし
   ｘ
   概要：データをリザルトに渡すようにまとめる
   */
    public GameCutterData(int _nSetCutter, int _nUseCutter, int _nCutterKill)
    {
        //各データをセット
        SetCutter = _nSetCutter;     //置いた数 
        UseCutter = _nUseCutter;     //使った回数
        CutterKill = _nCutterKill;   //倒した数 
    }
}
public class RotaryCutter : CTrap
{
    [SerializeField, Tooltip("UI表示用画像")] private /*static*/ AssetReferenceTexture2D m_UIAssetRefCutter; //UI用画像アセット
    private static Sprite m_ImageSpriteCutter; //UIアセット画像
    private static AsyncOperationHandle<Texture2D> m_AssetLoadHandleCutter;   //アセットをロード・管理する関数

    [SerializeField, Tooltip("回転速度")] private float rotationSpeed = 90.0f; // 回転速度 (度/秒)
    private static int m_nSetCutter;       //置いた数 
    private static int m_nUseCutter;       //使った回数
    private static int m_nCutterKill;      //倒した数 

    /*＞初期化関数
       引数：なし
       ｘ
       戻値：なし
       ｘ
       概要：インスタンス生成直後に行う処理
       */
    private void Awake()
    {
        //＞初期化
        MakeSprite();   //最初に画像を作る
    }

    /*＞更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：更新
    */
    void Update()
    {
        if (!m_bMove)
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        SetTrap();  //設置関数呼び出し
    }
  

    /*＞画像変換関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：Adressableに登録した画像をSprite形式に変換
    */
    private async void MakeSprite()
    {
        //＞保全
        if (m_UIAssetRefCutter == null)  //扱うアセットがない
        {
            //＞中断
            return; //処理しない
        }

        //＞画像読み込み
        var _AssetLoadHandle = Addressables.LoadAssetAsync<Texture2D>(m_UIAssetRefCutter);  //テクスチャデータを読み込む関数取得
        var _Texture = await _AssetLoadHandle.Task; //テクスチャ読み込みを非同期で実行
        m_ImageSpriteCutter = Sprite.Create(_Texture, new Rect(0, 0, _Texture.width, _Texture.height), Vector2.zero);   //テクスチャから画像データ作成

        //＞管理
        m_AssetLoadHandleCutter = _AssetLoadHandle;    //使用している関数を管理
    }

    /*＞地雷当たり判定関数
      引数１：Collision _Collision : 当たっているものの情報
      ｘ
      戻値：なし
      ｘ
      概要：敵が地雷に触れたときのみ処理される
      */
    private void OnCollisionStay(Collision collision)     //地雷に何かが当たってきたとき
    {
        Debug.Log("aaa");
        if (Check(collision, true))  // 刃に敵が当たってるか
        {
            Debug.Log("bbbb");
            var contactPoints = collision.contacts;
            foreach (var contact in contactPoints)
            {
                var collidingObject = contact.otherCollider.gameObject;
                if (collidingObject.TryGetComponent<IFeatureMine>(out var destroyable))
                {
                    destroyable.TakeDestroy();      //敵削除
                    m_nCutterKill++;
                }
            }
        }
        if (m_bMove)
          SetCheck(collision);    //設置できるかどうか判定
        
    }

    /*＞当たり判定関数
    引数１：Collision _Collision : 当たっているものの情報
    ｘ
    戻値：なし
    ｘ
    概要：当たっている状態から当たらなくなったら呼び出される
    */
    private void OnCollisionExit(Collision collision)
    {
        
        OutCheck(collision);    //設置できるかどうか判定
    }

    /*＞設置回数カウント関数
   引数１：Collision _Collision : 当たっているものの情報
   ｘ
   戻値：なし
   ｘ
   概要：設置されたときに呼び出される
   */
    public override void SetCount()
    {
        m_nSetCutter++;
    }


    /*＞データ引き渡し関数
  引数：なし
  ｘ
  戻値：GameCutterData(m_nSetCutter, m_nUseCutter,m_nCutterKill):データ
  ｘ
  概要：リザルトに渡す用のデータを作成
  */
    public static GameCutterData GetGameCutterData()
    {
        //リザルトに渡す用のデータを作成
        return new GameCutterData(m_nSetCutter, m_nUseCutter, m_nCutterKill);
    }
    /*＞データリセット関数
 引数：なし
 ｘ
 戻値：なし
 ｘ
 概要：初期化
 */
    public static void ResetCutterData()
    {
        m_nSetCutter = 0;     //置いた数 初期化
        m_nUseCutter = 0;     //使った回初期化
        m_nCutterKill = 0;    //倒した数 初期化
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
        if (m_AssetLoadHandleCutter.IsValid()) //ヌルでない
        {
            Addressables.Release(m_AssetLoadHandleCutter); //参照をやめる
        }
    }
}
