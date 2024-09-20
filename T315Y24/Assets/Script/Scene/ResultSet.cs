/*=====
<ResultSet.cs> 
���쐬�ҁFyamamoto

�����e
���U���g�ɕ\������f�[�^���󂯎��\��������X�N���v�g

���X�V����
__Y24   
_M06    
D
25: �v���O�����쐬: yamamoto
26: �R�����g�ǉ�: yamamoto
=====*/

//�����O��Ԑ錾
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//���N���X��`
public class ResultSet : CMonoSingleton<ResultSet>
{
    [Header("�e�L�X�g")]
    [Tooltip("�\���p�̃e�L�X�g")] public List<TMP_Text> ResultText;
    private int m_nPage = 0;   //�y�[�W��


    /*���������֐�
    �����P�F�Ȃ�
    ��
    �ߒl�F�Ȃ�
    ��
    �T�v�F�C���X�^���X�������ɍs������
    */
    override protected void Start()
    {
        //ToDo �ꊇ�Ǘ��ł���悤�ɂ܂Ƃ߂�
        GameMineData MineResultData = Mine.GetGameMineData();                   //�n���̃f�[�^���擾
        GameRemoteBombData RBResultData = RemoteBomb.GetGameRemoteBombData();   //���e�̃f�[�^���擾

        //�Ή�����e�L�X�g�ɃZ�b�g����
        ResultText[0].SetText($"{MineResultData.SetMine}");         //�n����u������
        ResultText[1].SetText($"{MineResultData.UseMine}");         //�n�����g������
        ResultText[2].SetText($"{MineResultData.MineKill}");        //�n���œ|������

        ResultText[3].SetText($"{RBResultData.SetRemoteBomb}");     //���e��u������
        ResultText[4].SetText($"{RBResultData.UseRemoteBomb}");     //���e���g������
        ResultText[5].SetText($"{RBResultData.RemoteBombKill}");    //���e�œ|������

        ResultText[6].SetText($"{MineResultData.SetMine + RBResultData.SetRemoteBomb}");    //�u�������̍��v
        ResultText[7].SetText($"{MineResultData.UseMine + RBResultData.UseRemoteBomb}");    //�g�����񐔂̍��v
        ResultText[8].SetText($"{MineResultData.MineKill + RBResultData.RemoteBombKill}");  //�|�������̍��v

        ResultText[9].SetText($"{MineResultData.MineKill + RBResultData.RemoteBombKill}");  //�|�������̍��v

        //���̃^�C�~���O�ŏ�����
        Mine.ResetMineData();                   //�n���̃f�[�^�����Z�b�g
        RemoteBomb.ResetRemoteBombData();       //���e�̃f�[�^�����Z�b�g
    }

    /*���X�V�֐�
   �����P�F�Ȃ�
   ��
   �ߒl�F�Ȃ�
   ��
   �T�v�F�X�V����
   */
    override protected void Update()
    {

    }

    /*��TextActive���]�֐�
  �����F�Ȃ�
  ��
  �ߒl�F�Ȃ�
  ��
  �T�v�FTextActive�𔽓]���\����\����؂�ւ���
  */
    public void ToggleActive(int nPage)
    {
        //���y�[�W���؂�ւ���Ă���
        if (m_nPage == nPage)
        {
            return; //�������Ȃ�
        }

        //���y�[�W�؊�
        m_nPage = nPage;    //�y�[�W���X�V����

        //���t�H���g�\���ؑ�
        for (int i = 0; i < ResultText.Count; i++) //ResultText�̐��J��Ԃ�
        {
            bool currentState = ResultText[i].gameObject.activeSelf;    //���݂�active���擾
            ResultText[i].gameObject.SetActive(!currentState);          //���]�����̂��Z�b�g����
        }
    }
}
