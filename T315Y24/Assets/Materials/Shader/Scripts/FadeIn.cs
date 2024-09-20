/*=====
<SceneTransition.cs> //スクリプト名
└作成者：tei

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//＞クラス定義
public class FadeIn : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] private Material SceneTransitionMaterial;  // マテリアル
    [SerializeField] private float transitionTime = 1.0f;       // フェード時間
    [SerializeField] private string propertyName = "_Progress"; // ShaderGraph内定義した変数名

    //＞パブリックイベント
    public UnityEvent OnTransitionDone;

    /*＞初期化関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：インスタンス生成時に行う処理
    */
    private void Start()
    {
        StartCoroutine(TransitionCoroutine());
    }

    /*＞コルーチン関数
    引数１：なし
    ｘ
    戻値：なし
    ｘ
    概要：フェイド時間に合わせる更新処理
    */
    private IEnumerator TransitionCoroutine()
    {
        float currentTime = 0.0f;   // 現時刻
        while(currentTime < transitionTime) // フェード時間より小さかったら行う
        {
            currentTime += Time.deltaTime;
            SceneTransitionMaterial.SetFloat(propertyName, Mathf.Clamp01(currentTime / transitionTime));    // propertyNameで定義した数値を時間の割合に合わせてスライドする
            yield return null;
        }
        OnTransitionDone.Invoke();  // イベントの呼び出し
    }
}
