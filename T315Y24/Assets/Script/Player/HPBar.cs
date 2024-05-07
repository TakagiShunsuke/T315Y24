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
    [SerializeField] private Image _hpBarcurrent;   //
    [SerializeField] private float _maxHealth;  //最大HP
    private float currentHealth;                //
    void Awake()        //
    {
        currentHealth = _maxHealth;     //最大HP
    }
    /*＞ダメージ処理関数
    引数：受けたダメージ   //引数がない場合は１を省略してもよい
    ｘ
    戻値：なし
    ｘ
    概要：ダメージ計算を行う
    */

    public void UpdateHP(float damage)  //HPの更新
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, _maxHealth); //最大HPからダメージ分を引く
        _hpBarcurrent.fillAmount = currentHealth / _maxHealth;      //
    }
}