/*=====
<Feature.cs>
���쐬�ҁFtakagi

�����e
�G�̓����𑶍݂�����C���^�[�t�F�[�X

���X�V����
__Y24
_M05
D
03:�v���O�����쐬:takagi
04:���l�[��:takagi
=====*/

//���C���^�[�t�F�[�X��`
public interface IFeature
{
    //���v���p�e�B��`
    public double Atk { get; }   //�U����
    public double Move { get; }  //�ړ�����[m/s]
    public string Information {  get; }  //�ڍ׏��
}