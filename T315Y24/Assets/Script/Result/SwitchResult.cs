/*=====
<SwitchResult.cs> //スクリプト名
└作成者：takagi

＞内容
リザルトの２画像を切り替える

＞注意事項


＞更新履歴
__Y24
_M06
D
12:プログラム作成:takagi
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
        [Tooltip("キー")] public KeyCode[] m_KeyChangeResult; //画像変更の着火キー
        [Tooltip("画像")] public Image m_NextImag;    //画像の切換先
    }

    //＞変数宣言
    [Header("画像の切り替え方")]
    [SerializeField, Tooltip("対応画像")] private KeyChangeImage[] m_KeyChangeImages;    //シーン遷移一覧


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
        for (int _nIdx = 0; _nIdx < m_KeyChangeImages.Length; ++_nIdx)   //遷移先候補分判定
        {
            for (int _nIdx2 = 0; _nIdx2 < m_KeyChangeImages[_nIdx].m_KeyChangeResult.Length; ++_nIdx2)    //受付キー分判定する
            {
                if (Input.GetKeyDown(m_KeyChangeImages[_nIdx].m_KeyChangeResult[_nIdx2])) //キー入力判定
                {
                    for(int _nIdx3 = 0; _nIdx3 < m_KeyChangeImages.Length; ++_nIdx3)   //自分以外を処理するためにループしなおす
                    {
                        if(_nIdx3 == _nIdx)  //対象が自身
                        {
                            m_KeyChangeImages[_nIdx3].m_NextImag.enabled = true; //可視化
                            continue;   //自分は処理しない
                        }

                        m_KeyChangeImages[_nIdx3].m_NextImag.enabled = false;    //不可視化
                    }
                }
            }
        }
    }
}