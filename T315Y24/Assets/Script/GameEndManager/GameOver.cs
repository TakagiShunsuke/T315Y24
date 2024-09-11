/*=====
<GameOver.cs>
└作成者：suzumura

＞内容


＞注意事項


＞更新履歴
__Y24
_M05
D
16: プレイヤーのクラスリネームに対応:takagi
31: リファクタリング:takagi

_M06
D
25: シーン遷移遷移先変更: yamamoto
=====*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CGameOver : MonoBehaviour
{
    //＞変数宣言
    [SerializeField]GameObject Player;          // プレイヤーオブジェクト
    CPlayerScript PlayerCom;                     //プレイヤーのスクリプト取得用
    //public InkTransition inkTransition;
    [SerializeField] private Material SceneFadeMaterial;  // マテリアル
    [SerializeField] private float fadeTime = 2.0f;       // フェード時間
    [SerializeField] private string _propertyName = "_Progress";

    public UnityEvent OnTransitionDone;
    // Start is called before the first frame update
    private void Start()
    {
        PlayerCom = Player.GetComponent<CPlayerScript>();   // プレイヤースクリプトを取得
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (PlayerCom.HP <= 0)
        {
            //float currentTime = 0.0f;   // 現時刻

            //while (currentTime < fadeTime)
            //{
            //    currentTime += Time.deltaTime;
            //    SceneFadeMaterial.SetFloat(_propertyName, Mathf.Clamp01(1 - currentTime / fadeTime));
            //    //inkTransition.StartTransition();

            //}
            TriggerGameOver();
            StartCoroutine(TransitionCoroutine());
            //SceneManager.LoadScene("ResultScene");    // ResultSceneへ遷移
        }
    }
    public void TriggerGameOver()
    {
        IFeatureGameOver[] gameOverObjects = FindObjectsOfType<MonoBehaviour>().OfType<IFeatureGameOver>().ToArray();
        foreach (IFeatureGameOver obj in gameOverObjects)
        {
            obj.OnGameOver();
        }
    }
        private IEnumerator TransitionCoroutine()
    {
        float currentTime = 0.0f;   // 現時刻
        while (currentTime < fadeTime) // フェード時間より小さかったら行う
        {
            currentTime += Time.deltaTime;
            SceneFadeMaterial.SetFloat(_propertyName, Mathf.Clamp01(1 - currentTime / fadeTime));    // propertyNameで定義した数値を時間の割合に合わせてスライドする
            yield return null;
        }
        OnTransitionDone.Invoke();  // イベントの呼び出し
        SceneManager.LoadScene("ResultScene");    // ResultSceneへ遷移
    }
}
