/*=====
<HPBar.cs> 
└作成者：iwamuro

＞内容
HPBarを動かすスプリクト

＞更新履歴
__Y24
_M05
D
04:プログラム作成:iwamuro

=====*/

//＞名前空間宣言
using UnityEngine;
using UnityEngine.UI;

//＞クラス定義
public class HPBar : MonoBehaviour
{
    //＞変数宣言
    [SerializeField] private Image f_hpBarcurrent;   //HPバー
    [SerializeField] private float f_maxHealth;  //プレイヤーの最大HP
    private float f_currentHealth;                //HPバーから減らすHP
    void Awake()        //最大HPからダメージを減らすための関数
    {
        f_currentHealth = f_maxHealth;     //最大HP
    }
    /*＞ダメージ処理関数
    引数：受けたダメージ   //引数がない場合は１を省略してもよい
    ｘ
    戻値：なし
    ｘ
    概要：ダメージ計算を行う
    */

    public void UpdateHP(float damage)  //HPの更新処理を行う
    {
        f_currentHealth = Mathf.Clamp(f_currentHealth - damage, 0, f_maxHealth); //最大HPからダメージ分を引く
        f_hpBarcurrent.fillAmount = f_currentHealth / f_maxHealth;      //HPバーが受けたダメージの分動くように変更
    }
}