/*=====
<FeatureNormal.cs>
���쐬�ҁFtakagi

�����e
�G�̓���(����)

���X�V����
__Y24
_M05
D
03:�v���O�����쐬:takagi
04:�R�����g�C��:takagi
=====*/

//�����O��Ԑ錾
using UnityEngine;  //Unity

//���N���X��`
public class CFeatureNormal : MonoBehaviour, IFeature
{
    //���v���p�e�B��`
    public double Atk { get; } = 40.0d;   //�U����
    public double Move { get; } = 4.0d;  //�ړ�����[m/s]
    public string Information { get; } = "���Ɋ";  //�ڍ׏��
}