/*=====
<Feature.cs>
���쐬�ҁFtakagi

�����e
�G�̓������`�E����

���X�V����
__Y24
_M06
D
07:�v���O�����쐬:takagi
09:�R�[�h���P:takagi
18:���x�^�ǉ�:takagi
=====*/

//�����O��Ԑ錾
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

//���N���X��`
public class CFeatures : CMonoSingleton<CFeatures>
{
    //���񋓒�`
    public enum E_ENEMY_TYPE
    {
        E_ENEMY_TYPE_NORMAL,    //�ʏ�̓G
        E_ENEMY_TYPE_SPEED,    //���x�^
        E_ENEMY_TYPE_POWER,    //�p���[�^
    }   //�G�̎��

    //���\���̒�`
    [Serializable] public struct FeatureInfo
    {
        //���ϐ��錾
        [SerializeField] private double m_Atk;   //�U����
        [SerializeField] private double m_Move;  //�ړ�����[m/s]

        //���v���p�e�B��`
        public double Atk => m_Atk; //�U����
        public double Move => m_Move;   //�ړ�����[m/s]
    };

    //���ϐ��錾
    [SerializeField, SerializeNamingWithEnum(typeof(E_ENEMY_TYPE))] private FeatureInfo[] m_Feature;    //E_ENEMY_TYPE�Ɋ֘A�Â��������̏��Q

    //���v���p�e�B��`
    public FeatureInfo[] Feature => m_Feature; //�����擾
}