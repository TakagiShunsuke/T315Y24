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
09:コード改善:takagi
18:速度型追加:takagi
=====*/

//＞名前空間宣言
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

//＞クラス定義
public class CFeatures : CMonoSingleton<CFeatures>
{
    //＞列挙定義
    public enum E_ENEMY_TYPE
    {
        E_ENEMY_TYPE_NORMAL,    //通常の敵
        E_ENEMY_TYPE_SPEED,    //速度型
        E_ENEMY_TYPE_POWER,    //パワー型
    }   //敵の種類

    //＞構造体定義
    [Serializable] public struct FeatureInfo
    {
        //＞変数宣言
        [SerializeField] private double m_Atk;   //攻撃力
        [SerializeField] private double m_Move;  //移動距離[m/s]

        //＞プロパティ定義
        public double Atk => m_Atk; //攻撃力
        public double Move => m_Move;   //移動距離[m/s]
    };

    //＞変数宣言
    [SerializeField, SerializeNamingWithEnum(typeof(E_ENEMY_TYPE))] private FeatureInfo[] m_Feature;    //E_ENEMY_TYPEに関連づいた特徴の情報群

    //＞プロパティ定義
    public FeatureInfo[] Feature => m_Feature; //特徴取得
}