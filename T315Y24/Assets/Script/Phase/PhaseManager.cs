/*=====
<PlayerHPUI.cs> //スクリプト名
└作成者：takagi

＞内容
プレイヤーのHPをUI表示する

＞注意事項
シングルトン
インプット処理や時間計測のためUpdate()を実装できるMonoBehaviorを継承。


＞更新履歴
__Y24
_M06
D
05:プログラム作成:takagi
07:ウェーブ排除・生成パターン追加:takagi
=====*/

//＞名前空間宣言
using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;  //Unity
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

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
        public E_PHASE m_Phase;    //フェーズ
        public List<CEnemyList.SpawnEnemyInfo> m_Enemies;
        public double m_dTime; //そのフェーズの持続時間
    }   //フェーズ定義用の構造体

    //＞定数定義
    const uint INIT_WAVE = 0;   //ウェーブ数カウントの初期値

    //＞変数宣言
    [SerializeField] private List<PhasePatturn> m_Phases;   //フェーズ一覧
    private uint m_unPhase = 0;
    private double m_dCntDwnPhase = 0.0d;

    //＞プロパティ定義
    public bool IsFinPhases { get; private set; } = false;    //フェーズ全終了フラグ
    private double CntDwnWave
    {
        get => m_dCntDwnPhase;   //自身のゲッタ
        set
        {
            m_dCntDwnPhase = value;    //カウントダウン
            if (m_dCntDwnPhase < 0.0d)   //カウントダウン時間超過
            {
                    if (m_Phases.Count > m_unPhase + 1) //フェーズ進行補正
                    {
                        m_unPhase++;    //フェーズ進行
                    CEnemyList.Instance.SpawnInfo = m_Phases[(int)m_unPhase].m_Enemies;
#if UNITY_EDITOR    //エディタ使用中
                    Debug.Log("フェーズ" + m_unPhase);   //フェーズ数出力
#endif
                    }
                    else
                {
                    IsFinPhases = true; //全フェーズ完了
//#if UNITY_EDITOR    //エディタ使用中
//                    //＞エラー出力
//                    UnityEngine.Debug.LogError("フェーズ数が想定を超過しています");  //警告ログ出力
//#endif
                }

                //＞カウントダウン量更新
                m_dCntDwnPhase = m_Phases[(int)m_unPhase].m_dTime + -m_dCntDwnPhase;  //カウント更新(超過分考慮)
            }
        }
    }//ウェーブ用カウントダウン
    public E_PHASE Phase => m_Phases[(int)m_unPhase].m_Phase; //現在のフェーズ


    /*＞初期化関数
     引数１：なし
     ｘ
     戻値：なし
     ｘ
     概要：インスタンス生成時に行う処理
     */
    override protected void CustomAwake()
    {
        CEnemyList.Instance.SpawnInfo = m_Phases[(int)m_unPhase].m_Enemies;
#if UNITY_EDITOR    //エディタ使用中
        Debug.Log("フェーズ" + m_unPhase);   //フェーズ数出力
#endif
        CntDwnWave = m_Phases[(int)m_unPhase].m_dTime;    //カウントダウン初期化
    }

    /*＞物理更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    override protected void Update()
    {
        //＞フェーズ時間更新
        //var SubtractTime = Time.deltaTime;  //時間の減少量
        //if(m_dCntDwnWave - SubtractTime > 0.0d)
        //{
        //    m_dCntDwnWave -= SubtractTime;  //カウントダウン
        //}
        //else
        //{
        //}

        ///または///

        //＞フェーズ時間更新
        //m_dCntDwnWave -= Time.deltaTime;    //カウントダウン
        //if (m_dCntDwnWave < 0.0d)   //カウントダウン時間超過
        //{
        //    //＞ウェーブ・フェーズ管理
        //    m_unWave++; //ウェーブ進行
        //    if( m_unWave >= m_Phases[(int)m_unPhase].m_unPhaseRepeat)   //ウェーブ数超過時
        //    {
        //        m_unPhase++;    //フェーズ進行
        //    }
            
        //    //＞カウントダウン量更新
        //    m_dCntDwnWave = m_Phases[(int)m_unPhase].m_dTime + -m_dCntDwnWave;  //カウント更新
        //}

        ///または///

        CntDwnWave -= Time.deltaTime;    //カウントダウン
        //上記をm_dCntDwnWaveのセッタに纏める(セットするときに発生する処理なため)



        //＞フェーズの終了
        //Todo:終了表現
    }
}