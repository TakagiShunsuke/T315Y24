/*=====
<EnemyNormal.cs> //スクリプト名
└作成者：takagi

＞内容
敵(普通)の挙動を統括・管理

＞注意事項
同一のオブジェクトに以下のコンポーネントがないと敵として十分な機能をしません。
１.IFeatureBaseを継承した、特徴を表すコンポーネント
２.攻撃範囲を表す扇形の領域判定AreaSector
３.物理演算を行うRigidbody

また、以下のコンポーネントがある場合はその初期値をシリアライズされて実装される値をも無視して初期化します。
１.IMoveを継承した、移動を行うコンポーネントの変数Speed


＞更新履歴
__Y24
_M05
D
03:プログラム作成:takagi
04:続き:takagi
11:プレイヤー削除、AreaSector変更への対応:takagi
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;  //Unity

//＞クラス定義
public class CEnemyNormal : CEnemy, IFeatureMine
{
    //＞変数宣言
    [SerializeField] private double m_dAtkInterval = 3.0d;  //攻撃間隔[s]
    private double m_dAtkCoolTime = 0.0d;   //攻撃クールタイム[s]
    private IFeature m_Feature = null;  //ステータス特徴
    private CAreaSector m_CAreaSector = null;   //扇形の攻撃範囲

    [SerializeField] GameObject m_EffectCube;       //エフェクトキューブプレハブ
    [SerializeField] int m_nEffectNum;              //エフェクトキューブ生成数
    [SerializeField]float m_fPosRandRange = 0.01f;  //エフェクトキューブを生成するポジションをランダムに生成するための範囲

   

    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    public void Start()
    {
        //＞親関数呼び出し
        transform.GetComponentInParent<CEnemy>().Start(); //親の初期化関数呼び出し

        //＞初期化
        m_Feature = GetComponent<IFeature>();   //自身の特徴取得
        if (m_Feature != null)   //取得に失敗した時
        {
            var Move = GetComponent<IMove>();   //移動コンポーネント取得
            if (Move != null)   //取得成功時
            {
                Move.Speed = m_Feature.Move;    //速度を初期化
            }
        }
#if UNITY_EDITOR    //エディタ使用中
        else
        {
            //＞エラー出力
            UnityEngine.Debug.LogWarning("特徴が設定されていません");   //警告ログ出力
        }
#endif
        m_CAreaSector = GetComponent<CAreaSector>();  //当たり判定取得
#if UNITY_EDITOR    //エディタ使用中
        if (m_CAreaSector == null)   //取得に失敗した時
        {
            //＞エラー出力
            UnityEngine.Debug.LogWarning("攻撃範囲が設定されていません");    //警告ログ出力
        }
#endif
       
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
        //＞カウントダウン
        if(m_dAtkCoolTime > 0.0d)   //クールダウン中
        {
            m_dAtkCoolTime -= Time.fixedDeltaTime;  //クールダウン減少
        }

        //＞攻撃
        Attack();   //攻撃を行う
    }

    /*＞攻撃関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：攻撃範囲にいるプレイヤーを攻撃する処理
    */
    private void Attack()
    {
        //＞検査
        if (m_Feature == null || m_CAreaSector == null)   //必要要件の不足時
        {
#if UNITY_EDITOR    //エディタ使用中
            //＞エラー出力
            UnityEngine.Debug.LogWarning("必要な要素が不足しています");  //警告ログ出力
#endif

            //＞中断
            return; //更新処理中断
        }

        //＞変数宣言
        List<GameObject> Hits = m_CAreaSector.SignalCollision;

        //＞攻撃判定
        if (Hits != null && Hits.Count > 0 && m_dAtkCoolTime <= 0.0d)   //検知対象が存在する、攻撃クールタイム終了
        {
            //＞クールダウン
            m_dAtkCoolTime = m_dAtkInterval;    //攻撃間隔初期化

            //＞ダメージ
            for (int nIdx = 0; nIdx < Hits.Count; nIdx++)   //攻撃範囲全てにダメージ
            {
                //＞変数宣言
                IDamageable Damageable = Hits[nIdx].GetComponent<IDamageable>();    //ダメージを与えて良いか

                //＞当たり判定
                if (Damageable != null)  //ダメージを与えられる相手
                {
                    //＞ダメージ付与
                    Damageable.Damage(m_Feature.Atk);   //対象にダメージを与える
                }
            }
        }
    }
    /*＞敵消去関数
   引数：なし
   ｘ
   戻値：なし
   ｘ
   概要：敵を消去する処理
   */
    public void TakeDestroy()
    {
        float x, y, z = 0.0f;

        // エフェクトキューブ生成
        for (int i = 0; i < m_nEffectNum; i++)
        {
            x = Random.Range(-m_fPosRandRange, m_fPosRandRange);
            y = Random.Range(-m_fPosRandRange, m_fPosRandRange);
            z = Random.Range(-m_fPosRandRange, m_fPosRandRange);

            Instantiate(m_EffectCube,new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z), Quaternion.identity);
        }
        counter();
        Destroy(gameObject);    //このオブジェクトを消去する
        GameObject Text;
        EnemyDeathCounter EnemyDeathCounter;
        Text = GameObject.Find("test");
        EnemyDeathCounter = Text.GetComponent<EnemyDeathCounter>();

        EnemyDeathCounter.DisplayEnemyDeathCounter();
    }
}