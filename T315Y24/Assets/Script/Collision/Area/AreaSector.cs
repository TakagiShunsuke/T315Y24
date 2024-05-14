/*=====
<AreaSector.cs>
└作成者：takagi

＞内容
領域判定(扇形)

＞注意事項
当たり判定を取ったら変数を介して信号を送信するため、それを受け取って処理を行ってください。

なお、以下のオブジェクトが存在する必要があります。
１.m_sTargetNameで定義された名前と一致するオブジェクト


＞更新履歴
__Y24
_M05
D
03:プログラム作成:takagi
04:続き:takagi
11:シグナルの返り値をbool→当たったオブジェクトを返す様に、衝突対象の変数名変更:takagi
=====*/

//＞名前空間宣言
using System;
using System.Collections;   //list
using System.Collections.Generic;
using UnityEngine;  //Unity

//＞クラス定義
public class CAreaSector : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] private double m_dRadius = 2.0d;   //半径
    [SerializeField] private double m_dSectorAngle = 90.0d; //扇形の角
    [SerializeField] private double m_dFrontAngle = 90.0d;  //xz平面上で正面方向の角度
    private List<GameObject> m_Targets = new List<GameObject>();    //検知対象
    [SerializeField] private List<string> m_sTargetNames;  //検知対象のオブジェクト名

    //＞プロパティ定義
    //public bool SignalCollision { get; private set; } = false;  //当たり判定のシグナル
    public List<GameObject> SignalCollision { get; private set; } = new List<GameObject>();  //当たり判定のシグナル


    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    // Start is called before the first frame update
    void Start()
    {
        //＞初期化
        if (m_sTargetNames != null)  //ヌルチェック
        {
            for (int nIdx = 0; nIdx < m_sTargetNames.Count; nIdx++) //対象の数だけオブジェクトを検出する
            {
                GameObject Temp = GameObject.Find(m_sTargetNames[nIdx]);
                if (Temp)   //取得成功時
                {
                    m_Targets.Add(Temp);   //プレイヤーのインスタンス格納
                }
#if UNITY_EDITOR    //エディタ使用中
                else    //取得に失敗した時
                {
                    //＞エラー出力
                    UnityEngine.Debug.LogWarning("ターゲット：" + m_sTargetNames[nIdx] + "が見つかりません");  //警告ログ出力
                }
#endif
            }
        }
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
        if (m_Targets == null)   //必要要件の不足時
        {
#if UNITY_EDITOR    //エディタ使用中
            //＞エラー出力
            UnityEngine.Debug.LogWarning("必要な要素が不足しています");  //警告ログ出力
#endif

            //＞中断
            return; //更新処理中断
        }

        //＞検知
        for (int nIdx = 0; nIdx < m_Targets.Count; nIdx++) //対象の数だけオブジェクトを検出する
        {
            //＞変数宣言
            Vector2 m_vDirction = new((float)Math.Cos(Mathf.Deg2Rad * (-transform.eulerAngles.y + m_dFrontAngle)),
                (float)Math.Sin(Mathf.Deg2Rad * (-transform.eulerAngles.y + m_dFrontAngle)));   //正面のベクトル  ※y軸回転の方向は座標系と逆方向
            Vector2 m_vToTarget = new(m_Targets[nIdx].transform.position.x - transform.position.x,
                m_Targets[nIdx].transform.position.z - transform.position.z);  //プレイヤー方向へのベクトル

            //＞初期化
            SignalCollision.Clear(); //当たり判定初期化

            //＞扇形範囲
            if (m_vToTarget.magnitude <= m_dRadius && Vector2.Angle(m_vToTarget, m_vDirction) <= m_dSectorAngle / 2.0d)   //ベクトルの長さが半径以下、正面のベクトルと角度が扇形の角の半分以下
            {
                //＞当たり判定
                SignalCollision.Add(m_Targets[nIdx]); //当たり判定更新
            }

#if UNITY_EDITOR && DEBUG    //エディタ使用中かつデバッグ中
            //＞変数宣言・初期化
            Vector3 m_vDirctCent = new(m_vDirction.x, 0.0f, m_vDirction.y);  //扇形の中央方向
            m_vDirctCent = m_vDirctCent.normalized * (float)m_dRadius;   //大きさ初期化
            Vector3 m_vDirctLeft = Quaternion.Euler(0.0f, (float)(m_dSectorAngle / 2.0d), 0.0f) * m_vDirctCent; //扇形の左端
            Vector3 m_vDirctRight = Quaternion.Euler(0.0f, (float)(-m_dSectorAngle / 2.0d), 0.0f) * m_vDirctCent;   //扇形の右端

            //＞範囲表示
            Debug.DrawRay(transform.position + Vector3.up, m_vDirctCent, Color.blue);   //扇形の中央表示
            Debug.DrawRay(transform.position + Vector3.up, m_vDirctLeft, Color.blue);   //扇形の左端表示
            Debug.DrawRay(transform.position + Vector3.up, m_vDirctRight, Color.blue);  //扇形の右端表示
#endif
        }
    }        
}