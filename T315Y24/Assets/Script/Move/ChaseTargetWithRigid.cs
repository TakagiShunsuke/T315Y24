/*=====
<ChaseTargetWithRigid.cs>
└作成者：takagi

＞内容
対象追跡式移動

＞注意事項
同一のオブジェクトに以下のコンポーネントが必要です。
１.物理演算を行うRigidbody

＞更新履歴
__Y24
_M05
D
04:プログラム作成:takagi
=====*/

//＞名前空間宣言
using System;
using System.Collections;   //list
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;  //Unity

//＞クラス定義
public class CChaseTargetWithRigid : MonoBehaviour, IMove
{
    //＞変数宣言
    private GameObject m_Target = null; //追跡対象
    private Rigidbody m_Rigidbody = null;   //物理演算
    [SerializeField] private double m_Speed = 0.0d; //速度
    [SerializeField] private string m_sTargetName = "Player";   //追跡目標のオブジェクト名

    //＞プロパティ定義
    public double Speed { get { return m_Speed; } set { m_Speed = value; } }    //速度

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
        m_Target = GameObject.Find(m_sTargetName);   //追跡目標のインスタンス格納
#if UNITY_EDITOR    //エディタ使用中
        if (m_Target == null)    //取得に失敗した時
        {
            //＞エラー出力
            UnityEngine.Debug.LogWarning("追跡対象が見つかりません");  //警告ログ出力
        }
#endif
        m_Rigidbody = GetComponent<Rigidbody>();    //物理演算取得
#if UNITY_EDITOR    //エディタ使用中
        if (m_Rigidbody == null)    //取得に失敗した時
        {
            //＞エラー出力
            UnityEngine.Debug.LogWarning("Rigidbodyが設定されていません");    //警告ログ出力
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
        //＞検査
        if (m_Target == null)   //必要要件の不足時
        {
#if UNITY_EDITOR    //エディタ使用中
            //＞エラー出力
            UnityEngine.Debug.LogWarning("必要な要素が不足しています");  //警告ログ出力
#endif

            //＞中断
            return; //更新処理中断
        }

        //＞変数宣言・初期化
        Vector2 m_vToPlayer = new(m_Target.transform.position.x - transform.position.x,
            m_Target.transform.position.z - transform.position.z);  //プレイヤー方向へのベクトル
        var ToPlAngle = Vector2.Angle(Vector2.up, m_vToPlayer); //プレイヤーとのなす角

        //＞プレイヤーへ向かう
        m_vToPlayer.Set((float)(m_vToPlayer.normalized.x * Speed), (float)(m_vToPlayer.normalized.y * Speed));  //移動量
        //m_Rigidbody.AddForce(new Vector3(m_vToPlayer.x, 0.0f, m_vToPlayer.y).normalized 
        //    * (float)(Math.Clamp((Speed - m_Rigidbody.velocity.magnitude), -4.0d, 4.0d)), ForceMode.Acceleration); //プレイヤー方向へ移動
        m_Rigidbody.velocity = new Vector3(m_vToPlayer.x, 0.0f, m_vToPlayer.y); //移動方向変更
        m_Rigidbody.rotation = Quaternion.Euler(0.0f, Vector2.Angle(Vector2.left, m_vToPlayer) > 90.0f ? ToPlAngle : -ToPlAngle, 0.0f) ;//プレイヤーを向く                            //左方向のベクトルとのなす角が90度未満なら左に向く。そうでなければ右に向く。Angleが0to180なために0to360をこう表現した。

#if UNITY_EDITOR    //エディタ使用中
        //＞ログ出力
        UnityEngine.Debug.Log(m_Rigidbody.velocity.magnitude);   //攻撃判定の代わり
#endif
    }
}