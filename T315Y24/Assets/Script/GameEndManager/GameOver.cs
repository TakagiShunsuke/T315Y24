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
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CGameOver : MonoBehaviour
{
    //���ϐ��錾
    [SerializeField]GameObject Player;          // �v���C���[�I�u�W�F�N�g
    CPlayerScript PlayerCom;                     //�v���C���[�̃X�N���v�g�擾�p
    //public InkTransition inkTransition;
    [SerializeField] private Material SceneFadeMaterial;  // �}�e���A��
    [SerializeField] private float fadeTime = 2.0f;       // �t�F�[�h����
    [SerializeField] private string _propertyName = "_Progress";

    public UnityEvent OnTransitionDone;
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
            //float currentTime = 0.0f;   // ������

            //while (currentTime < fadeTime)
            //{
            //    currentTime += Time.deltaTime;
            //    SceneFadeMaterial.SetFloat(_propertyName, Mathf.Clamp01(1 - currentTime / fadeTime));
            //    //inkTransition.StartTransition();

            //}
            TriggerGameOver();
            StartCoroutine(TransitionCoroutine());
            //SceneManager.LoadScene("ResultScene");    // ResultScene�֑J��
        }
    }
    public void TriggerGameOver()
    {
        IFeatureGameOver[] gameOverObjects = FindObjectsOfType<MonoBehaviour>().OfType<IFeatureGameOver>().ToArray();
        foreach (IFeatureGameOver obj in gameOverObjects)
        {
            obj.OnGameOver();
        }
    }
        private IEnumerator TransitionCoroutine()
    {
        float currentTime = 0.0f;   // ������
        while (currentTime < fadeTime) // �t�F�[�h���Ԃ�菬����������s��
        {
            currentTime += Time.deltaTime;
            SceneFadeMaterial.SetFloat(_propertyName, Mathf.Clamp01(1 - currentTime / fadeTime));    // propertyName�Œ�`�������l�����Ԃ̊����ɍ��킹�ăX���C�h����
            yield return null;
        }
        OnTransitionDone.Invoke();  // �C�x���g�̌Ăяo��
        SceneManager.LoadScene("ResultScene");    // ResultScene�֑J��
    }
}
