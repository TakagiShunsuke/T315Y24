/*=====
<Feature.cs>
└作成者：takagi

＞内容
敵の特徴を存在させるインターフェース

＞更新履歴
__Y24
_M05
D
03:プログラム作成:takagi
04:リネーム:takagi
=====*/

//＞インターフェース定義
public interface IFeature
{
    //＞プロパティ定義
    public double Atk { get; }   //攻撃力
    public double Move { get; }  //移動距離[m/s]
    public string Information {  get; }  //詳細情報
}