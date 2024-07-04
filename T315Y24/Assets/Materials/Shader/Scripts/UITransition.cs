/*=====
<UITransition.cs> //スクリプト名
└作成者：tei

＞内容
UIトランジション

＞注意事項
適当で作ってるから、もっと良い演出が欲しかったら要調整

＞更新履歴
__Y24
_M06
D
12:プログラム作成:tei
=====*/

//＞名前空間宣言
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//＞クラス定義
public class UITransition : MonoBehaviour
{
    //＞変数宣言
    [SerializeField]　private RectTransform rectTransform;   // 移動始点、終点、移動条件
    [SerializeField]　private float transitonTime = 2.0f;    // UI移動タイム
    [SerializeField]　Vector3 startposition; // UI初期位置

    /*＞初期化関数
   引数１：なし
   ｘ
   戻値：なし
   ｘ
   概要：インスタンス生成時に行う処理
   */
    private void Awake()
    {
        startposition = rectTransform.anchoredPosition;
    }

    /*＞初期化関数
   引数１：なし
   ｘ
   戻値：なし
   ｘ
   概要：インスタンス生成時に行う処理
   */
    public void ShowUI()
    {
        StartCoroutine(ShowCoroutine());
    }

    /*＞コルーチン関数
   引数１：なし
   ｘ
   戻値：なし
   ｘ
   概要：UI移動に合わせる更新処理
   */
    private IEnumerator ShowCoroutine()
    {
        float currentTime = 0.0f;   // 現時刻
        while (currentTime < transitonTime) // UI移動時間より小さかったら行う
        {
            currentTime += Time.deltaTime;
            rectTransform.anchoredPosition = Vector3.Lerp(startposition, Vector3.zero, Mathf.Clamp01(currentTime)); // 始点、終点、移動タイム設定
            yield return null;
        }
    }
}
