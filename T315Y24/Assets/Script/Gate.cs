/*=====
<Gate.cs> //スクリプト名
└作成者：takagi

＞内容
門

＞注意事項
同一のオブジェクトに以下のコンポーネントがないと十分な機能をしません。
１.敵を生成する機構であるSpawnEnemy

＞更新履歴
__Y24
_M05
D
06:プログラム作成:takagi
07:続き:takagi
10:生成数上限機能実装:takagi

_M06
D
09:生成機構のクラス変更
13:敵出現時SE追加:nieda
18:フェーズ形式変更に対応:takagi
21:リファクタリング:takagi
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

//＞クラス定義
public class CGate : MonoBehaviour
{
    //＞変数宣言
    [Header("敵生成情報")]
    [SerializeField, Tooltip("生成上限")] private uint m_unSpawnMax = 100;  //生成上限[s]
    private double m_dSpawnCoolTime = 0.0d;   //生成クールタイム[s]
    //[SerializeField] private double m_dSpawnInterval = 3.0d;  //生成間隔[s]
    private static double m_dSpawnInterval;  //生成間隔[s]
    private CSpawnEnemy m_SpawnRandom = null;    //生成機構

    //＞プロパティ定義
    public static double SpawnInterval { private get; set; }    //フェーズ全終了フラグ


    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    private void Start()
    {
        //＞初期化
        m_SpawnRandom = GetComponent<CSpawnEnemy>();   //自身の特徴取得
#if UNITY_EDITOR    //エディタ使用中
        if (m_SpawnRandom == null)   //取得に失敗した時
        {
            //＞エラー出力
            UnityEngine.Debug.LogWarning("生成コンポーネントが設定されていません");    //警告ログ出力
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
        if (m_dSpawnCoolTime > 0.0d)   //クールダウン中
        {
            m_dSpawnCoolTime -= Time.fixedDeltaTime;    //カウントダウン進行
        }
        else
        {
            //＞生成
            for (uint unIdx = 0; unIdx < CPhaseManager.Instance.EnemyVal && m_unSpawnMax > CEnemy.ValInstance; unIdx++) //生成可能なら必要数生成
            {
                m_SpawnRandom.Create(); //インスタンス生成
            }

            //＞初期化
            m_dSpawnCoolTime = m_dSpawnInterval;    //クールダウン開始
        }

    }
}