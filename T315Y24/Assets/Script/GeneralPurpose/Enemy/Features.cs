/*=====
<Feature.cs>
¤ì¬ÒFtakagi

àe
GÌÁ¥ðè`Eñ

XVð
__Y24
_M06
D
07:vOì¬:takagi
=====*/

//¼OóÔé¾
using System;
using UnityEngine;

//NXè`
public class CFeatures : CMonoSingleton<CFeatures>
{
    //ñè`
    public enum E_ENEMY_TYPE
    {
        E_ENEMY_TYPE_NORMAL,    //ÊíÌG
        E_ENEMY_TYPE_POWER,    //ÊíÌG
    }   //GÌíÞ

    //\¢Ìè`
    [Serializable] public struct Feature
    {
        //Ïé¾
        [SerializeField] private double m_Atk;   //UÍ
        [SerializeField] private double m_Move;  //Ú®£[m/s]

        //vpeBè`
        public double Atk => m_Atk; //UÍ
        public double Move => m_Move;   //Ú®£[m/s]
    };

    //Ïé¾
    [SerializeField, SerializeNamingWithEnum(typeof(E_ENEMY_TYPE))] private Feature[] m_Feature;
}