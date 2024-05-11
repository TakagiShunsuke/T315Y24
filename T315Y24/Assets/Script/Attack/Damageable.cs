/*=====
<Damageable.cs>
└作成者：takagi

＞内容
ダメージを受ける機構

＞更新履歴
__Y24
_M05
D
11:プログラム作成:takagi
=====*/

//＞インターフェース定義
public interface IDamageable
{
    //＞プロトタイプ宣言
    public void Damage(double dDamageVal);  //ダメージを受ける
}