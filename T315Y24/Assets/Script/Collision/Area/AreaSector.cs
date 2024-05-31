/*=====
<AreaSector.cs>
└作成者：takagi

＞内容
領域判定(扇形)

＞注意事項
当たり判定を取ったら変数を介して信号を送信するため、それを受け取って処理を行ってください。

なお、以下のオブジェクトが存在する必要があります。
１.m_sTargetNameで定義された名前と一致するオブジェクト

また、以下の点に注意してください
１.m_dResolに0以下の値を設定すると無限ループが発生するためエラーを返します

＞更新履歴
__Y24
_M05
D
03:プログラム作成:takagi
04:続き:takagi
11:シグナルの返り値をbool→当たったオブジェクトを返す様に、衝突対象の変数名変更:takagi
16:判定領域可視化:takagi
31:リファクタリング:takagi
=====*/

//＞名前空間宣言
using System;
using System.Collections;   //list
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;  //Unity

//＞クラス定義
public class CAreaSector : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] private double m_dRadius = 2.0d;   //半径
    [SerializeField] private double m_dSectorAngle = 90.0d; //扇形の角
    [SerializeField] private double m_dFrontAngle = 90.0d;  //xz平面上で正面方向の角度
    [SerializeField] private List<string> m_sTargetNames;  //検知対象のオブジェクト名
    private List<GameObject> m_Targets = new List<GameObject>();    //検知対象
    [SerializeField] private double m_dResol = 1.0d;   //領域表示の解像度
    [SerializeField] private Material m_RangeMaterial;   //領域表示用のマテリアル

    //＞プロパティ定義
    public List<GameObject> SignalCollision { get; private set; } = new List<GameObject>();  //当たり判定のシグナル


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
        if (m_sTargetNames != null)  //ヌルチェック
        {
            for (int nIdx = 0; nIdx < m_sTargetNames.Count; nIdx++) //対象の数だけオブジェクトを検出する
            {
                GameObject Temp = GameObject.Find(m_sTargetNames[nIdx]);    //オブジェクト取得試行
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
        
        //＞判定領域可視化
        if(m_dResol > 0)    //解像度が正常な時
        {
            //＞＞変数宣言
            GameObject _RangeView = new GameObject(); //可視化用オブジェクト作成
            List<Vector3> _Vertex = new List<Vector3>(); //頂点情報
            Vector3 _vDirction = new((float)Math.Cos(Mathf.Deg2Rad * (-transform.eulerAngles.y + m_dFrontAngle)), 0.0f,
                (float)Math.Sin(Mathf.Deg2Rad * (-transform.eulerAngles.y + m_dFrontAngle)));   //正面のベクトル  ※y軸回転の方向は座標系と逆方向
            List<int> _VtxIdx = new List<int>(); //インデックスバッファの情報
            int _nIdx = 1;  //ループ用カウンタ
            Mesh _Mesh = new Mesh();    //メッシュ本体

            //＞＞初期化
            _RangeView.transform.position = transform.position;    //位置を合わせる
            _RangeView.transform.parent = transform;   //子オブジェクトに追加
            _RangeView.AddComponent<MeshRenderer>().material = m_RangeMaterial;   //マテリアルを操作可能にし、初期化
            var _MeshFilter = _RangeView.AddComponent<MeshFilter>();   //メッシュ操作可能にする
            _Vertex.Add(Vector3.zero);  //頂点0番目は原点
            _vDirction = _vDirction.normalized * (float)m_dRadius;   //大きさ初期化
            for (double _dAngle = -m_dSectorAngle / 2.0d; _dAngle < m_dSectorAngle / 2.0d; _dAngle += m_dResol)    //細かくポリゴン作成
            {
                _Vertex.Add(Quaternion.Euler(0.0f, (float)(_dAngle), 0.0f) * _vDirction + Vector3.zero);   //頂点位置登録
                _VtxIdx.AddRange(new int[] { 0, _nIdx - 1, _nIdx });    //インデックスバッファ登録
                _nIdx++;    //カウント進行
            }
            _Mesh.vertices = _Vertex.ToArray(); //頂点バッファ登録
            _Mesh.SetTriangles(_VtxIdx, 0); //インデックスバッファ登録
            _Mesh.RecalculateNormals(); //法線の再計算
            _MeshFilter.sharedMesh = _Mesh; //メッシュ情報登録
        }
#if UNITY_EDITOR    //エディタ使用中
        else    //無限ループが発生する解像度な時
        {
            //＞エラー出力
            Debug.LogError("無限ループが発生しました。解像度(m_dResol)に入力する値は0より大きい必要があります"); ;  //エラーログ出力
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
        if (m_Targets == null)   //必要要件の不足時
        {
#if UNITY_EDITOR    //エディタ使用中
            //＞エラー出力
            UnityEngine.Debug.LogWarning("追跡対象が存在しません");  //警告ログ出力
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

//#if UNITY_EDITOR && DEBUG    //エディタ使用中かつデバッグ中
//            //＞変数宣言・初期化
//            Vector3 m_vDirctCent = new(m_vDirction.x, 0.0f, m_vDirction.y);  //扇形の中央方向
//            m_vDirctCent = m_vDirctCent.normalized * (float)m_dRadius;   //大きさ初期化
//            Vector3 m_vDirctLeft = Quaternion.Euler(0.0f, (float)(m_dSectorAngle / 2.0d), 0.0f) * m_vDirctCent; //扇形の左端
//            Vector3 m_vDirctRight = Quaternion.Euler(0.0f, (float)(-m_dSectorAngle / 2.0d), 0.0f) * m_vDirctCent;   //扇形の右端

//            //＞範囲表示
//            Debug.DrawRay(transform.position + Vector3.up, m_vDirctCent, Color.blue);   //扇形の中央表示
//            Debug.DrawRay(transform.position + Vector3.up, m_vDirctLeft, Color.blue);   //扇形の左端表示
//            Debug.DrawRay(transform.position + Vector3.up, m_vDirctRight, Color.blue);  //扇形の右端表示
//#endif    //領域表示を実装したのでお役御免
        }
    }        
}