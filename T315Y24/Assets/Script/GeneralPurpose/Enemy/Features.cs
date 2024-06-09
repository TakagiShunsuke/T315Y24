/*=====
<Feature.cs>
└作成者：takagi

＞内容
敵の特徴を定義・羅列

＞更新履歴
__Y24
_M06
D
07:プログラム作成:takagi
=====*/

//＞名前空間宣言
using System;
using UnityEngine;

//＞クラス定義
public class CFeatures : CMonoSingleton<CFeatures>
{
    //＞列挙定義
    public enum E_ENEMY_TYPE
    {
        E_ENEMY_TYPE_NORMAL,    //通常の敵
        E_ENEMY_TYPE_POWER,    //通常の敵
    }   //敵の種類

    //＞構造体定義
    [Serializable] public struct Feature
    {
        //＞変数宣言
        [SerializeField] private double m_Atk;   //攻撃力
        [SerializeField] private double m_Move;  //移動距離[m/s]

        //＞プロパティ定義
        public double Atk => m_Atk; //攻撃力
        public double Move => m_Move;   //移動距離[m/s]
    };

    //＞変数宣言
    [SerializeField, SerializeNamingWithEnum(typeof(E_ENEMY_TYPE))] private Feature[] m_Feature;
}