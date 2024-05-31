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
23:ダッシュ追加:takagi
30:ダッシュ改修・無敵時間分割:takagi
31:リファクタリング:takagi
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
    [SerializeField] private double m_dFrontAngle = 90.0d;  //xz平面上で正面方向の角度
    [SerializeField] private float m_fSpeed = 6; //プレイヤーの移動速度を設定
    [SerializeField] private double m_dHp = 10;   // HP
    [SerializeField] private double m_dDamagedInvicibleTime = 2.0d;  //無敵時間[s] :自分が攻撃を受けたときに発動
    [SerializeField] private double m_unDashInterval = 5;   //ダッシュリキャスト時間
    [SerializeField] private double m_dDushInvicibleTime = 2.0d;  //無敵時間[s] :ダッシュ時に発動
    private double m_dCntDwnInvicibleTime = 0.0d;  //無敵時間カウント用
    private double m_dCntDwnDshInterval = 0.0d;  //ダッシュカウントダウン用
    [SerializeField] private KeyCode m_DushKey = KeyCode.E; //ダッシュのキー
    [SerializeField] private double m_DashDist = 2.0d;  //ダッシュ時に移動する距離

    //＞プロパティ定義
    private double CntDwnInvicibleTime
    {
        get
        {
            return m_dCntDwnInvicibleTime;  //無敵時間提供
        }
        set
        {
            if (m_dCntDwnInvicibleTime < value) //無敵時間に更新の必要がある
            {
                m_dCntDwnInvicibleTime = value; //無敵時間更新
            }
        }
    }   //無敵時間管理
    public bool InvincibleState
    {
        get
        {
            return CntDwnInvicibleTime > 0.0d;   //無敵時間によって判定する
        }
    }   //無敵状態管理
    [SerializeField] public double HP => m_dHp;  //HP提供
    [SerializeField] public double DashCntDwn => m_dCntDwnDshInterval;  //ダッシュのカウントダウン時間提供

    /*＞初期処理関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：Rigidbodyコンポーネントを追加
    */
    void Start()    //自動で追加される
    {
        //＞初期化
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
        if (CntDwnInvicibleTime > 0.0d)   //無敵状態の時
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
        //＞無敵表現
        if (InvincibleState)   //無敵中
        {
            //TODO:明滅処理
            //var RadSp = 2.0d * Math.PI * (double)m_unFlNu * CntDwnInvicibleTime / m_dInvicibleTime;
            
            //var mr = GetComponent<MeshRenderer>();
            //mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, (float)(Math.Sin(RadSp) * 255.0d));
        }

        //＞ダッシュ操作
        if (Input.GetKeyDown(m_DushKey))    //ダッシュ入力
        {
            Dash(); //ダッシュする
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
        //＞処理無効化
        if (InvincibleState)    //無敵中
        {
            return; //ダメージを受けない
        }

        //＞ダメージ計算
        m_dHp -= dDamageVal;    //HP減少
        CntDwnInvicibleTime = m_dDamagedInvicibleTime;  //無敵時間のカウントをリセットする
    }

    /*＞ダッシュ関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：ダッシュして移動する
    */
    public void Dash()
    {
        //＞検査
        if (m_dCntDwnDshInterval > 0.0d)   //クールダウン中の時
        {
            //＞ダッシュ不可能
            return;     //処理中断
        }

        //＞変数宣言・初期化
        Vector3 _vFront = new((float)Math.Cos(Mathf.Deg2Rad * (-transform.eulerAngles.y+ m_dFrontAngle)), 0.0f,
            (float)Math.Sin(Mathf.Deg2Rad * (-transform.eulerAngles.y + m_dFrontAngle)));  //正面方向のベクトル  ※y軸回転の方向は座標系と逆方向
        _vFront = _vFront.normalized * (float)m_DashDist; //ベクトルサイズ初期化
        Vector3 _vGo = transform.position + _vFront;    //行き先予定地の座標
        Ray _Ray = new Ray(transform.position + Vector3.up, _vFront);   //前方レイキャスト
        RaycastHit _Hit; //レイキャストで得られる当たり判定

        //＞衝突回避  //これはこれで敵の前で止まることに...；：    //TODO:タグ
        if (Physics.Raycast(_Ray, out _Hit))
        {
            var cp = GetComponent<CapsuleCollider>();   //当たり判定取得

            var vBeforeCollide = _Hit.point - transform.localScale * cp.radius; //当たり判定に盲目的に補正した変数
            if (Math.Abs(vBeforeCollide.x - transform.position.x) < Math.Abs(_vFront.x))   //自分からのベクトルとしてx値について比較
            {
                _vGo.x = vBeforeCollide.x;  //x値を補正値に置き換える
            }
            if (Math.Abs(vBeforeCollide.z - transform.position.z) < Math.Abs(_vFront.z))   //自分からのベクトルとしてz値について比較
            {
                _vGo.z = vBeforeCollide.z;  //z値を補正値に置き換える
            }
        }
        if (_vGo != transform.position)  //移動先に対して変移があるとき
        {
            m_Rb.transform.position = _vGo;  //即座に移動を行う

            //＞カウントダウン
            m_dCntDwnDshInterval = m_unDashInterval;   //時間をカウント
            CntDwnInvicibleTime = m_dDushInvicibleTime;  //無敵時間のカウントをリセットする
        }
    }
}