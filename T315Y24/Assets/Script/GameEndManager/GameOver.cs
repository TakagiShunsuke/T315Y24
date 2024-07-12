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
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGameOver : MonoBehaviour
{
    //＞変数宣言
    [SerializeField]GameObject Player;          // プレイヤーオブジェクト
    CPlayerScript PlayerCom;                     //プレイヤーのスクリプト取得用


    // Start is called before the first frame update
    private void Start()
    {
        PlayerCom = Player.GetComponent<CPlayerScript>();   // プレイヤースクリプトを取得
    }

    // Update is called once per frame
    private void Update()
    {
        if(PlayerCom.HP <= 0)
        {
            SceneManager.LoadScene("ResultScene");    // ResultSceneへ遷移
        }
    }
}
