/*=====
<Gate.cs> //スクリプト名
└作成者：takagi

＞内容
門

＞更新履歴
__Y24
_M05
D
06:プログラム作成:takagi
07:続き:takagi
10:生成数上限機能実装:takagi
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;  //Unity

//＞クラス定義
public class CGate : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] private double m_dSpawnInterval = 3.0d;  //生成間隔[s]
    [SerializeField] private uint m_unSpawnMax = 100;  //生成上限[s]
    private double m_dSpawnCoolTime = 0.0d;   //生成クールタイム[s]
    CSpawnRandomRect m_SpawnRandom = null;    //生成機構
    
    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    void Start()
    {
        //＞初期化
        m_SpawnRandom = GetComponent<CSpawnRandomRect>();   //自身の特徴取得
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
        {   //＞生成
            if(m_unSpawnMax > CEnemy.ValInstance)
            {
                m_SpawnRandom.Create(); //インスタンス生成
            }

            //＞初期化
            m_dSpawnCoolTime = m_dSpawnInterval;    //クールダウン開始
        }
    }
}