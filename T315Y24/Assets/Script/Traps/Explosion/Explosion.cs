/*=====
<Explosion.cs> 
└作成者：yamamoto

＞内容
地雷エフェクト用に付けるスクリプト

＞注意事項  
地雷エフェクト用にIsTriggerを付けないと動作しません。

＞更新履歴
__Y24   
_M05    
D
9 :プログラム作成:yamamoto
10:コメント追加:yamamoto
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

//＞クラス定義
public class Explosion : MonoBehaviour
{
    //＞変数宣言
    private float m_ObjectRadius;      //オブジェクトの半径
    private Vector3 m_InitialObjectPos; // オブジェクトの初期位置
    [SerializeField] private double m_LowerSpeed = 0.1d;   //オブジェクトが下に消えていく速度
    int m_Setnum;
    public static int[] m_KillCount = new int[2];
    /*＞初期化関数
  引数１：なし
  ｘ
  戻値：なし
  ｘ
  概要：インスタンス生成時に行う処理
  */
    void Start()
    {
        Debug.Log("2");
        m_ObjectRadius = transform.localScale.x / 2.0f;   // オブジェクトの半径を取得
        m_InitialObjectPos = transform.position;          // 初期位置を設定

        // 範囲内の敵を検出
        Collider[] Colliders = Physics.OverlapSphere(transform.position, m_ObjectRadius);
        foreach (Collider Collider in Colliders)    //Collider[]の中に入っているだけループする
        {
            if (Collider.CompareTag("Enemy"))   //当たり判定の中にあるものに敵タグがついてるか確認
            {
                //IFeatureMineがついるか確認
                if (Collider.gameObject.TryGetComponent<IFeatureMine>(out var Destroy))
                {
                    Destroy.TakeDestroy();  //敵削除
                    m_KillCount[m_Setnum]++;
                }
            }
        }
    }

    /*＞更新関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：更新処理
    */
    void Update()
    {
        // オブジェクトを下に移動させる
        transform.position -= new Vector3(0.0f, (float)m_LowerSpeed * Time.deltaTime, 0.0f);

        // 半径分だけ下に移動したかどうかを判断し、破壊する
        if (transform.position.y <= m_InitialObjectPos.y - m_ObjectRadius)
        {
            Destroy(gameObject);
        }
    }
    public void SetBombType(int n)
    {
        m_Setnum = n;
    }
}
