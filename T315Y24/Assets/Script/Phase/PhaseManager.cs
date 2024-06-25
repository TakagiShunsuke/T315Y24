/*=====
<PhaseManager.cs> //スクリプト名
└作成者：takagi

＞内容
フェーズ管理

＞注意事項
シングルトン

＞更新履歴
__Y24
_M06
D
05:プログラム作成:takagi
07:ウェーブ排除・生成パターン追加:takagi
13:テキスト表示機構追加:takagi
17:SE追加:nieda
18:フェーズ形式変更、式修正:takagi
21:リファクタリング:takagi
24:リファクタリング:takagi
=====*/

//＞名前空間宣言
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using static CFeatures;
using static CPhaseManager;

//＞クラス定義
public class CPhaseManager : CMonoSingleton<CPhaseManager>
{
    //＞列挙定義
    public enum E_PHASE
    {
        E_PHASE_BATTLE, //戦闘フェーズ
        E_PHASE_SET_UP, //設営フェーズ
    }   //フェーズ分け

    //＞構造体定義
    [Serializable]public struct PhasePatturn
    {
        public E_PHASE m_ePhase;    //フェーズ
        public List<CEnemyList.SpawnEnemyInfo> m_Enemies;   //出現する敵
        public uint m_unEnemyVal;   //ゲート一つの敵生成量
        [Min(0.0f)] public double m_dSpawnInterval;  //生成間隔[s]  //TODO:double型
        public double m_dTime; //そのフェーズの持続時間
        public uint m_unKillValToNext;  //フェーズ切り替えに必要な討伐数
    }   //フェーズ定義用の構造体

    //＞定数定義
    private const uint INIT_WAVE = 0;   //ウェーブ数カウントの初期値

    //＞変数宣言
    [Header("フェーズ情報")]
    [SerializeField, Tooltip("フェーズ数表示箇所")] private TextMeshProUGUI m_TMP_PhaseVal;    //フェーズ数表示場所
    //[SerializeField] private TextMeshProUGUI m_TMP_PhaseName;   //フェーズ名表示場所
    //[SerializeField, CSerializeNamingWithEnum(typeof(E_PHASE)), Tooltip("フェーズ名")] private string[] m_sPhaseName;    //E_PHASEごとのフェーズ名
    [SerializeField, Tooltip("フェーズ詳細")] private List<PhasePatturn> m_Phases;   //フェーズ一覧
    private uint m_unPhase = 0; //フェーズ番号管理
    private double m_dCntDwnPhase = 0.0d;   //フェーズ時間カウント
    [Header("音")]
    [SerializeField] private AudioClip SE_Spawn;  // ウェーブ開始時のSE
    private AudioSource m_As; // AudioSourceを追加

    //＞プロパティ定義
    public bool IsFinPhases { get; private set; } = false;    //フェーズ全終了フラグ
    private double CntDwnWave
    {
        get => m_dCntDwnPhase;   //自身のゲッタ
        set
        {
            m_dCntDwnPhase = value;    //カウントダウン
            //＞更新
            if (m_dCntDwnPhase <= 0.0d)   //カウントダウン時間超過
            {
                if (m_Phases.Count > m_unPhase + 1) //フェーズ進行補正
                {
                    m_unPhase++;    //フェーズ進行
                    CEnemyList.Instance.SpawnInfo = m_Phases[(int)m_unPhase].m_Enemies; //敵のリスト更新
                    CGate.SpawnInterval = m_Phases[(int)m_unPhase].m_dSpawnInterval;    //生成間隔更新
#if UNITY_EDITOR    //エディタ使用中
                    Debug.Log("フェーズ" + m_unPhase);   //フェーズ数出力
#endif
                    UpdatePhaseText();  //フェーズ表示更新
                    m_As.PlayOneShot(SE_Spawn);   // SE再生
                }
                else
                {
                    IsFinPhases = true; //全フェーズ完了
                    //#if UNITY_EDITOR    //エディタ使用中
                    //＞エラー出力
                    //UnityEngine.Debug.LogError("フェーズ数が想定を超過しています");  //警告ログ出力
                    //#endif
                }

                //＞カウントダウン量更新
                m_dCntDwnPhase = m_Phases[(int)m_unPhase].m_dTime + -m_dCntDwnPhase;  //カウント更新(超過分考慮)
            }
        }
    }//ウェーブ用カウントダウン
    public E_PHASE Phase => m_Phases[(int)m_unPhase].m_ePhase; //現在のフェーズ
    public uint EnemyVal => m_Phases[(int)m_unPhase].m_unEnemyVal;   //生成する敵数


    /*＞初期化関数
     引数１：なし
     ｘ
     戻値：なし
     ｘ
     概要：インスタンス生成時に行う処理
     */
    override protected void CustomAwake()
    {
#if UNITY_EDITOR    //エディタ使用中
        if (m_TMP_PhaseVal == null)   //取得に失敗した時
        {
            //＞エラー出力
            UnityEngine.Debug.LogWarning("入力先テキストが設定されていません");    //警告ログ出力
        }
#endif
//#if UNITY_EDITOR    //エディタ使用中
//        if (m_TMP_PhaseName == null)   //取得に失敗した時
//        {
//            //＞エラー出力
//            UnityEngine.Debug.LogWarning("入力先テキストが設定されていません");    //警告ログ出力
//        }
//#endif

        //＞外部初期化
        CEnemyList.Instance.SpawnInfo = m_Phases[(int)m_unPhase].m_Enemies; //敵のリスト初期化
        CGate.SpawnInterval = m_Phases[(int)m_unPhase].m_dSpawnInterval;    //生成間隔初期化
#if UNITY_EDITOR    //エディタ使用中
        Debug.Log("フェーズ" + m_unPhase);   //フェーズ数出力
#endif

        //＞初期化
        UpdatePhaseText();  //フェーズ表示初期化
        CntDwnWave = m_Phases[(int)m_unPhase].m_dTime;    //カウントダウン初期化
        m_As = GetComponent<AudioSource>(); // AudioSourceコンポーネントを追加
    }

    /*＞更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    override protected void Update()
    {
        //＞フェーズ時間更新
        CntDwnWave -= Time.deltaTime;    //カウントダウン

        //＞敵数調査
        if(CEnemy.m_nDeadEnemyCount > m_Phases[(int)m_unPhase].m_unKillValToNext)    //討伐数が目標値以上
        {
            CntDwnWave -= CntDwnWave;   //カウントを0にする
        }
    }

    /*＞フェーズ用テキスト更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：フェーズ表示に更新が必要な時に行う処理
    */
    private void UpdatePhaseText()
    {
        //＞テキスト修正
        if (m_TMP_PhaseVal != null)   //フェーズ数表示
        {
            m_TMP_PhaseVal.text = (m_unPhase + 1).ToString().PadRight(2) + " / " + m_Phases.Count.ToString().PadRight(2);    //現在のフェーズ番号/全体のフェーズ番号
        }
#if UNITY_EDITOR    //エディタ使用中
        else
        {
            //＞エラー出力
            UnityEngine.Debug.LogWarning("テキストが不足しています");  //警告ログ出力
        }
#endif
//        if (m_TMP_PhaseName != null)   //フェーズ名表示
//        {
//            if ((int)m_Phases[(int)m_unPhase].m_ePhase < m_sPhaseName.Length && m_sPhaseName[(int)m_Phases[(int)m_unPhase].m_ePhase] != null)
//            {
//                m_TMP_PhaseName.text = m_sPhaseName[(int)m_Phases[(int)m_unPhase].m_ePhase];    //フェーズ名表示
//            }
//#if UNITY_EDITOR    //エディタ使用中
//            else
//            {
//                //＞エラー出力
//                UnityEngine.Debug.LogWarning("表示するフェーズ名が設定されていません");  //警告ログ出力
//            }
//#endif
//        }
//#if UNITY_EDITOR    //エディタ使用中
//        else
//        {
//            //＞エラー出力
//            UnityEngine.Debug.LogWarning("テキストが不足しています");  //警告ログ出力
//        }
//#endif
    }
}