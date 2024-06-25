/*=====
<GameOver.cs>
└作成者：suzumura

＞内容


＞注意事項


＞更新履歴
__Y24
_M05
D
16:プレイヤーのクラスリネームに対応:takagi
31:リファクタリング:takagi
=====*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーン関連のusing

public class GameOver : MonoBehaviour
{
    //＞変数宣言
    [SerializeField]GameObject Player;          // プレイヤーオブジェクト
    CPlayerScript PlayerCom;                     //プレイヤーのスクリプト取得用

    // Start is called before the first frame update
    void Start()
    {
        PlayerCom = Player.GetComponent<CPlayerScript>();   // プレイヤースクリプトを取得
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerCom.HP <= 0)
        {
            SceneManager.LoadScene("ResultScene");    // GameOverSceneへ遷移
        }
    }
}
