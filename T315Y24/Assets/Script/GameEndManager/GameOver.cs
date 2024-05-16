using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // �V�[���֘A��using

public class GameOver : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField]GameObject Player;          // �v���C���[�I�u�W�F�N�g
    CPlayerScript PlayerCom;                     //�v���C���[�̃X�N���v�g�擾�p

    // Start is called before the first frame update
    void Start()
    {
        PlayerCom = Player.GetComponent<CPlayerScript>();   // �v���C���[�X�N���v�g���擾
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerCom.HP <= 0)
        {
            SceneManager.LoadScene("GameoverScene");    // GameOverScene�֑J��
        }
    }
}
