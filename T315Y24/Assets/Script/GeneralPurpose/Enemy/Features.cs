/*=====
<Features.cs>
¤ì¬ÒFtakagi

àe
GÌÁ¥ðè`Eñ

XVð
__Y24
_M06
D
07:vOì¬:takagi
09:R[hüP:takagi
18:¬x^ÇÁ:takagi
21:t@N^O:takagi
=====*/

//¼OóÔé¾
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

//NXè`
public class CFeatures : CMonoSingleton<CFeatures>
{
    //ñè`
    public enum E_ENEMY_TYPE
    {
        E_ENEMY_TYPE_NORMAL,    //ÊíÌG
        E_ENEMY_TYPE_SPEED, //¬x^
        E_ENEMY_TYPE_POWER, //p[^
    }   //GÌíÞ

    //\¢Ìè`
    [Serializable] public struct FeatureInfo
    {
        //Ïé¾
        [SerializeField, Tooltip("UÍ")] private double m_Atk;   //UÍ
        [SerializeField, Tooltip("¬x[m/s]")] private double m_Move;  //Ú®£[m/s]

        //vpeBè`
        public double Atk => m_Atk; //UÍ
        public double Move => m_Move;   //Ú®£[m/s]
    };

    //Ïé¾
    [SerializeField, CSerializeNamingWithEnum(typeof(E_ENEMY_TYPE)), Tooltip("Á¥ê")] private FeatureInfo[] m_Feature;    //E_ENEMY_TYPEÉÖAÃ¢½Á¥ÌîñQ

    //vpeBè`
    public FeatureInfo[] Feature => m_Feature; //Á¥æ¾
}