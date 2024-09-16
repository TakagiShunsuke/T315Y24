/*=====
<EnemyAttach.cs> //スクリプト名
└作成者：takagi

＞内容
敵(普通)の挙動を統括・管理

＞注意事項
同一のオブジェクトに以下のコンポーネントがないと敵として十分な機能をしません。
１.IFeatureBaseを継承した、特徴を表すコンポーネント
２.攻撃範囲を表す扇形の領域判定AreaSector
３.物理演算を行うRigidbody

また、以下のコンポーネントがある場合は、特徴に応じてその初期値をシリアライズされて実装される値をも無視して初期化します。
１.移動に使用されるコンポーネントNavMeshAgentの変数speed


＞更新履歴
__Y24
_M05
D
03:プログラム作成:takagi
04:続き:takagi
11:プレイヤー削除、AreaSector変更への対応:takagi
31:リファクタリング:takagi

_M06
D
09:特徴改造に対応・リネーム:takagi
21:リファクタリング:takagi
24:リファクタリング:takagi

_M07
D
21:アニメーション追加:nieda
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

//＞クラス定義
public sealed class CEnemyAttach : CEnemy, IFeatureMine, IFeatureGameOver
{
    //＞変数宣言
    [Header("ステータス")]
    [SerializeField, Tooltip("特徴")] private CFeatures.E_ENEMY_TYPE m_eFeatureType;   //特徴の種類
    private CFeatures.FeatureInfo m_FeatureInfo;    //特徴により決定する情報
    [SerializeField, Tooltip("攻撃間隔")] private double m_dAtkInterval;  //攻撃間隔[s]
    private double m_dAtkCoolTime = 0.0d;   //攻撃クールタイム[s]
    [Header("エフェクト")]
    [SerializeField] private GameObject m_EffectCube;       //エフェクトキューブプレハブ
    [SerializeField] private int m_nEffectNum;              //エフェクトキューブ生成数
    [SerializeField] private float m_fPosRandRange;  //エフェクトキューブを生成するポジションをランダムに生成するための範囲
    private CAreaSector m_CAreaSector = null;   //扇形の攻撃範囲
    private Rigidbody m_Rigidbody;
    [Header("アニメーション")]
    private Animator m_Animator;
    private double m_dAnimFinCnt = 0.0d;    // アニメーション終了判定用
    private double m_dAnimFinTime = 0.3d;   // アニメーション終了時間

    private bool isGameOver = false;                        //ゲームオーバー時操作不能にする用


    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    override protected void CustomStart()
    {
        //＞初期化
        if (CFeatures.Instance.Feature.Length > (int)m_eFeatureType)    //自身の列挙数値に対して返答が期待できる時
        {
            //＞数値更新
            m_FeatureInfo = CFeatures.Instance.Feature[(int)m_eFeatureType];    //列挙に対応した情報を取得する

            //＞移動情報書き換え
            var Move = GetComponent<NavMeshAgent>();   //移動コンポーネント取得
            if (Move != null)   //取得成功時
            {
                Move.speed = (float)m_FeatureInfo.Move;    //速度を初期化
            }
        }
#if UNITY_EDITOR    //エディタ使用中
        else
        {
            //＞エラー出力
            UnityEngine.Debug.LogWarning(m_eFeatureType + "の特徴が設定されていません");   //警告ログ出力
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

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();  // Animatorコンポーネントを追加
    }

    /*＞物理更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    override protected void FixedUpdate()
    {
        if (isGameOver)
        {
            m_Animator.SetBool("isAttack", false);
            //待機モーションをここに

            return;
        }
            //＞カウントダウン
            if (m_dAtkCoolTime > 0.0d)   //クールダウン中
        {
            m_dAtkCoolTime -= Time.fixedDeltaTime;  //クールダウン減少
        }

        if(m_dAnimFinCnt > 0.0d)    // 終了カウントダウン中
        {
            m_dAnimFinTime -= Time.fixedDeltaTime;
        }
        else    // 終了カウントダウン終了
        {
            // Attackアニメーションを終了
            m_Animator.SetBool("isAttack", false);
        }

        //＞攻撃
        Attack();   //攻撃を行う

        m_Rigidbody.velocity = Vector3.zero;
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
        if (m_CAreaSector == null)   //必要要件の不足時
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
            m_dAnimFinCnt = m_dAnimFinTime;     //アニメーション終了カウントダウン開始

            //＞ダメージ
            for (int nIdx = 0; nIdx < Hits.Count; nIdx++)   //攻撃範囲全てにダメージ
            {
                //＞変数宣言
                IDamageable Damageable = Hits[nIdx].GetComponent<IDamageable>();    //ダメージを与えて良いか

                //＞当たり判定
                if (Damageable != null)  //ダメージを与えられる相手
                {
                    //＞ダメージ付与
                    Damageable.Damage(m_FeatureInfo.Atk);   //対象にダメージを与える
                }
            }
            
            // Attackアニメーションを再生
            m_Animator.SetBool("isAttack", true);
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
        gameObject.SetActive(false);
        GameObject Text;
        EnemyDeathCounter EnemyDeathCounter;
        Text = GameObject.Find("DeathCounter");
        EnemyDeathCounter = Text.GetComponent<EnemyDeathCounter>();

        EnemyDeathCounter.DisplayEnemyDeathCounter();
    }
    public void OnGameOver()
    {
        isGameOver = true;
    }
}