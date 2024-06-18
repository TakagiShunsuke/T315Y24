/*=====
<SwitchResult.cs> //スクリプト名
└作成者：takagi

＞内容
シーントランジション

＞注意事項
適当で作ってるから、もっと良い演出が欲しかったら要調整

＞更新履歴
__Y24
_M06
D
12:プログラム作成:tei
=====*/

//＞名前空間宣言
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//＞クラス定義
public class CSwitchResult : MonoBehaviour
{
    //＞構造体定義
    [Serializable]
    public struct KeyChangeImage
    {
        public KeyCode[] m_KeyChangeResult; //画像変更の着火キー
        public Image m_NextImag;    //画像の切換先
    }

    //＞変数宣言
    [SerializeField] private KeyChangeImage[] m_KeyChangeImages;    //シーン遷移一覧

    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    private void Start()
    {
    }

    /*＞更新関数
    引数：なし
    ｘ
    戻値：なし
    ｘ
    概要：一定時間ごとに行う更新処理
    */
    private void Update()
    {
        //＞保全
        if (m_KeyChangeImages == null)   //ヌルチェック
        {
            //＞終了
            return; //処理キャンセル
        }

        //＞画像遷移
        for (int nIdx = 0; nIdx < m_KeyChangeImages.Length; ++nIdx)   //遷移先候補分判定
        {
            for (int nIdx2 = 0; nIdx2 < m_KeyChangeImages[nIdx].m_KeyChangeResult.Length; ++nIdx2)    //受付キー分判定する
            {
                if (Input.GetKeyDown(m_KeyChangeImages[nIdx].m_KeyChangeResult[nIdx2])) //キー入力判定
                {
                    for(int nIdx3 = 0; nIdx3 < m_KeyChangeImages.Length; ++nIdx3)   //自分以外を処理するためにループしなおす
                    {
                        if(nIdx3 == nIdx)  //対象が自身
                        {
                            m_KeyChangeImages[nIdx3].m_NextImag.enabled = true; //可視化
                            continue;   //自分は処理しない
                        }

                        m_KeyChangeImages[nIdx3].m_NextImag.enabled = false;    //不可視化
                    }
                }
            }
        }
    }
}
