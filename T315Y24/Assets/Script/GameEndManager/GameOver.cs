/*=====
<GameOver.cs>
���쐬�ҁFsuzumura

�����e


�����ӎ���


���X�V����
__Y24
_M05
D
16: �v���C���[�̃N���X���l�[���ɑΉ�:takagi
31: ���t�@�N�^�����O:takagi

_M06
D
25: �V�[���J�ڑJ�ڐ�ύX: yamamoto
=====*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGameOver : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField]GameObject Player;          // �v���C���[�I�u�W�F�N�g
    CPlayerScript PlayerCom;                     //�v���C���[�̃X�N���v�g�擾�p
    public InkTransition inkTransition;
    
    [SerializeField] private float fadeTime = 2.0f;       // �t�F�[�h����
    // Start is called before the first frame update
    private void Start()
    {
        PlayerCom = Player.GetComponent<CPlayerScript>();   // �v���C���[�X�N���v�g���擾
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (PlayerCom.HP <= 0)
        {
            float currentTime = 0.0f;   // ������

            while (currentTime < fadeTime)
            {
                currentTime += Time.deltaTime;
                //SceneFadeMaterial.SetFloat(_propertyName, Mathf.Clamp01(currentTime / fadeTime));
                inkTransition.StartTransition();
                
            }
            SceneManager.LoadScene("ResultScene");    // ResultScene�֑J��
        }
    }
}
