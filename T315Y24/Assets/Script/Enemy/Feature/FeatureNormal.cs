/*=====
<FeatureNormal.cs>
└作成者：takagi

＞内容
敵の特徴(普通)

＞更新履歴
__Y24
_M05
D
03:プログラム作成:takagi
04:コメント修正:takagi
11:速度修正:takagi
=====*/

//＞名前空間宣言
using UnityEngine;  //Unity

//＞クラス定義
public class CFeatureNormal : MonoBehaviour, IFeature
{
    //＞プロパティ定義
    public double Atk { get; } = 1.0d;   //攻撃力
    public double Move { get; } = 4.0d;  //移動距離[m/s]
    public string Information { get; } = "もに基準";  //詳細情報
}