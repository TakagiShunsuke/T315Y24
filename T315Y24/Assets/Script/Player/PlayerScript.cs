/*=====
<PlayerScript.cs> 
└作成者：iwamuro

＞内容
Playerを動かすスプリクト

＞更新履歴
__Y24
_M05
D
04:プログラム作成:iwamuro
11:体力・攻撃を受ける処理追加:takagi
13:プレイヤーの移動と角度の修正、unity上でスピードを変更できるように変更:iwamuro
13:変数名変更:takagi
=====*/

//＞名前空間宣言
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//＞クラス定義
public class CPlayerScript : MonoBehaviour, IDamageable
{
    //＞変数宣言
    Rigidbody m_Rb;      // Rigidbodyを追加
    [SerializeField] private float m_fSpeed = 6; //プレイヤーの移動速度を設定
    [SerializeField] private double m_dHp = 10;   // HP
    [SerializeField] private double m_unDashInterval =5;   //ダッシュリキャスト時間

    //＞変数宣言
    [SerializeField] private double m_dInvicibleTime = 2.0d;  //無敵時間[s] :初期値可変のために非定数化
    //[SerializeField] private uint m_unFlNu = 5;
    //private bool m_bInvicibleFlag;   //無敵状態フラグ(trueで無敵)
    private double m_dCntDwnInvicibleTime = 0.0d;  //無敵時間カウント用
    private double m_dCntDwnDshInterval = 0.0d;  //cdカウント用
    [SerializeField] private KeyCode m_KeyCode = KeyCode.E;
    [SerializeField] private double m_DashDist = 2.0d;
    //private double m_dashtemp = 0.0d;

    //＞プロパティ定義
    //public bool IsInvincible { get; private set; } = false; //無敵状態管理
    public bool InvincibleState
    {
        get { return m_dCntDwnInvicibleTime > 0.0f; }   //フラグの代わりに時間で判断する
        set
        {
            //＞状態分岐
            if (value == true)  //無敵状態にする
            {
                m_dCntDwnInvicibleTime = m_dInvicibleTime;  //無敵時間のカウントをリセットする
            }
            else
            {
                m_dCntDwnInvicibleTime = 0.0f;  //無敵時間を無くす
            }
        }   //セッタで特殊な処理
    } //無敵状態管理


    //＞プロパティ定義
    public double HP
    {
        get { return m_dHp; }
        private set { m_dHp = value; }
    }
    //[SerializeField] public uint DashInterval {
    //    //get; private set;
    //    get => m_unDashInterval;  
    //    private set => m_unDashInterval = value;
    //}
    [SerializeField] public double DashCntDwn => m_dCntDwnDshInterval;  //読み取り専用プロパティ
    //{
    //    //get; private set;
    //    get => m_unDashInterval;
    //    private set => m_unDashInterval = value;
    //}

    /*＞初期処理関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：Rigidbodyコンポーネントを追加
    */
    void Start()    //自動で追加される
    {
        m_Rb = GetComponent<Rigidbody>(); //Rigidbodyコンポーネントを追加
    }


    /*＞移動処理関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：キーが押されたら移動をを行う
    */
    void Update()   //キーが押されたときに更新を行う
    {
        Vector3 moveDirection = Vector3.zero; // 移動方向の初期化
        Vector3 target_dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));    //プレイヤーの向きを変えるベクトル

        if (target_dir.magnitude > 0.1) //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える
        {
            //体の向きを変更
            transform.rotation = Quaternion.LookRotation(target_dir);
            //前方へ移動
            transform.Translate(Vector3.forward * Time.deltaTime * m_fSpeed);
        }
    
        // 斜め移動
        if (moveDirection != Vector3.zero)
        {
    
            // 正規化して移動速度を一定に保つ
            moveDirection.Normalize();
            m_Rb.velocity = moveDirection * m_fSpeed;

        }
        else
        {
            // 何もキーが押されていない場合は停止する
            m_Rb.velocity = Vector3.zero;
        }
        
        //＞検査
        if (m_dCntDwnInvicibleTime > 0.0d)   //無敵状態の時
        {
            //＞カウントダウン
            m_dCntDwnInvicibleTime -= Time.deltaTime;   //時間をカウント
        }
        if (m_dCntDwnDshInterval > 0.0d)
        {
            m_dCntDwnDshInterval -= Time.deltaTime;   //時間をカウント
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
        if (InvincibleState)   //クールダウン中
        {
            //var RadSp = 2.0d * Math.PI * (double)m_unFlNu * m_dCntDwnInvicibleTime / m_dInvicibleTime;
            //＞
            //var mr = GetComponent<MeshRenderer>();
            //mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, (float)(Math.Sin(RadSp) * 255.0d));
        }

        if (Input.GetKey(m_KeyCode))
        {
            //Debug.Log("ばつ");
            Dash();
        }
    }

    /*＞被ダメージ関数
    引数：double dDamageVal
    ｘ
    戻値：なし
    ｘ
    概要：ダメージを受ける
    */
    public void Damage(double dDamageVal)
    {
        if (InvincibleState)
        {
            return; 
        }

        //＞ダメージ計算
        HP -= dDamageVal;    //HP減少

        InvincibleState = true;
    }

    public void Dash()
    {
        //＞検査
        if (m_dCntDwnDshInterval > 0.0d)   //dshcd状態の時
        {
            return;
        }

        //dash
        
        Vector2 m_vDirction = new((float)Math.Cos(Mathf.Deg2Rad * (-transform.eulerAngles.y + 90.0d/* + m_dFrontAngle*/)),
        (float)Math.Sin(Mathf.Deg2Rad * (-transform.eulerAngles.y + 90.0d/* + m_dFrontAngle*/)));   //正面のベクトル  ※y軸回転の方向は座標系と逆方向

        Vector3 m_vDirctCent = new(m_vDirction.x, 0.0f, m_vDirction.y);  //扇形の中央方向

        m_Rb.transform.position += m_vDirctCent.normalized * (float)m_DashDist;
        //m_Rb.velocity += m_vDirctCent.normalized * (float)m_DashDist; //移動方向変更
        //m_Rb.AddForce(m_vDirctCent.normalized * (float)m_DashDist, ForceMode.Impulse);



        //＞カウントダウン
        m_dCntDwnDshInterval = m_unDashInterval;   //時間をカウント
        InvincibleState = true;

    }
}