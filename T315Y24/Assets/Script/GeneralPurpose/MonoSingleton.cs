/*=====
<MonoSingleton.cs> //�X�N���v�g��
���쐬�ҁFtakagi

�����e
�V���O���g���̎���

�����ӎ���
�W�F�l���b�N�֐��ł��B�p�����Ɍ^�w���Y��Ȃ����ƁB
�C���X�^���X��null�ȏꍇ������܂��B�擾���Ƀk���`�F�b�N���邱�ƁB

���X�V����
__Y24
_M06
D
05:�v���O�����쐬:takagi
06:�����Ftakagi
21:���t�@�N�^�����O:takagi
24:���t�@�N�^�����O:takagi
=====*/

//���N���X��`
using UnityEngine;

public abstract class CMonoSingleton<MonoType> : CVirtualizeMono where MonoType : CMonoSingleton<MonoType>  //where���Ōp���c���[�𖾎��FT��CMonoSingleton<T>��CVirtualizeMono��MonoBehaviour
{
    //���ϐ��錾
    static private MonoType m_Instance; //�C���X�^���X�i�[�p

    //���v���p�e�B��`
    public static MonoType Instance
    {
        get
        {
            if (m_Instance == null) //�k���`�F�b�N
            {
                GameObject _GameObject = new GameObject();   //�C���X�^���X�쐬
                m_Instance = _GameObject.AddComponent<MonoType>();   //���g�̃R���|�[�l���g�o�^
            }
            return m_Instance;  //�C���X�^���X��
        }
    }   //�p����I�u�W�F�N�g�̃C���X�^���X


    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X��������ɍs������
    */
    protected override sealed void Awake()
    {
        //�����g�������ڂ�
        if(m_Instance != null && m_Instance.gameObject != null) //���łɎ��g�Ɠ���̂��̂�����
        {
            //�������L�����Z��
            Destroy(this.gameObject);   //���g�̐������Ȃ��������Ƃɂ���
            return; //�����͏�������Ȃ�
        }

        //���C���X�^���X�o�^
        m_Instance = (MonoType)this;  //���g���C���X�^���X�Ƃ��ēo�^

        //���ǉ��̏���
        CustomAwake();  //�q�N���X�����̃^�C�~���O�ōs����������
    }

    /*���j���֐�
    �����F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�j�����ɍs������
    */
    protected override sealed void OnDestroy()
    {
        //�������L�����Z������
        if(this != m_Instance)    //�����L�����Z���̂��߂ɍs��ꂽ�j���ł���
        {
            //���I��
            return; //����ȍ~�̏����͋����ɍs������̂ł͂Ȃ�
        }

        //���C���X�^���X�j��
        if(m_Instance != null)    //�C���X�^���X�Ƃ��ēo�^����Ă���
        {
            m_Instance = null;    //�C���X�^���X���k���ɏ�����
        }

        //���ǉ��̏���
        CustomOnDestroy();  //�q�N���X�����̃^�C�~���O�ōs����������
    }
}