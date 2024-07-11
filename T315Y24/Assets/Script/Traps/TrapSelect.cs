using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CCodingRule;
using static InputDeviceManager;

public class CTrapSelect : MonoBehaviour
{

    private GameObject player;
    //�����\��
    public List<GameObject> TrapList;   //㩂̊i�[List
    public List<Image> ImageList;   //㩂̊i�[List
    public List<int> CostList;   //㩂̃R�X�g�i�[List
    public List<TMP_Text> CostText;   //㩂̃R�X�g�e�L�X�g�i�[List
    //
    public bool m_bSelect=true;
    private int m_nNum;
    [SerializeField] public AudioClip SE_Select;  // 㩑I������SE
    [SerializeField] public AudioClip SE_Set;  // 㩐ݒu����SE
    AudioSource m_As; // AudioSource��ǉ�

    public static int m_Cost;
    public float increaseInterval = 5.0f;  // �R�X�g�𑝂₷�Ԋu�i�b�j

    [SerializeField] private TMP_Text Cost_txt; //�\��������e�L�X�g(TMP)
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");//����
        m_nNum = 0;
        RectTransform rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 200);
        m_As = GetComponent<AudioSource>(); // AudioSource�R���|�[�l���g��ǉ�
        m_bSelect = true;
        m_Cost = 10;
        // �R���[�`�����J�n
        StartCoroutine(IncreaseCostOverTime());
        Cost_txt.SetText("�g�p�\�R�X�g:" + m_Cost);     //������
        CostText[0].SetText(""+CostList[0]);     //������
        CostText[1].SetText("" + CostList[1]);     //������
        InputDeviceManager.Instance.OnChangeDeviceType.AddListener(OnChangeDeviceTypeHandler);

    }
    private void OnChangeDeviceTypeHandler()
    {
        // ���̓f�o�C�X�̎�ʂ��ύX���ꂽ�Ƃ��̏���
        Debug.Log("���̓f�o�C�X�̎�ʂ��ύX����܂����B\n���݂̓��̓f�o�C�X�̎�ʁF" + InputDeviceManager.Instance.CurrentDeviceType);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bSelect)
        {
            Select();
            //����
            //if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Decision")) && CostCheck(m_nNum))
            //{
            //    m_As.PlayOneShot(SE_Set);   // SE�Đ�
            //    Generation(m_nNum);
            //    m_bSelect = false;
            //    Cost_txt.SetText("�g�p�\�R�X�g:" + m_Cost);
            //}

            
            if (InputDeviceManager.Instance != null)
            {
                // ���݂̓��̓f�o�C�X�^�C�v���擾
                InputDeviceManager.InputDeviceType currentDeviceType = InputDeviceManager.Instance.CurrentDeviceType;
                //����
                // ���݂̃f�o�C�X�^�C�v�ɉ������������s��
                Debug.Log(currentDeviceType);
                switch (currentDeviceType)
                {
                    case InputDeviceManager.InputDeviceType.Keyboard:
                        Debug.Log("Keyboard���g�p����Ă��܂�");
                        if (Input.GetKeyDown(KeyCode.E) && CostCheck(m_nNum)) //�_�b�V������
                        {
                            m_As.PlayOneShot(SE_Set);   // SE�Đ�
                            Generation(m_nNum);
                            m_bSelect = false;
                            Cost_txt.SetText("�g�p�\�R�X�g:" + m_Cost);
                        }
                        break;
                    case InputDeviceManager.InputDeviceType.Xbox:
                        Debug.Log("XBOX���g�p����Ă��܂�");
                        if (Input.GetButtonDown("Decision") && CostCheck(m_nNum))
                        {
                            m_As.PlayOneShot(SE_Set);   // SE�Đ�
                            Generation(m_nNum);
                            m_bSelect = false;
                            Cost_txt.SetText("�g�p�\�R�X�g:" + m_Cost);
                        }
                        break;
                    case InputDeviceManager.InputDeviceType.DualShock4:
                        Debug.Log("DualShock4(PS4)���g�p����Ă��܂�");
                        if (Input.GetButtonDown("Decision") && CostCheck(m_nNum))
                        {
                            m_As.PlayOneShot(SE_Set);   // SE�Đ�
                            Generation(m_nNum);
                            m_bSelect = false;
                            Cost_txt.SetText("�g�p�\�R�X�g:" + m_Cost);
                        }
                        break;
                    case InputDeviceManager.InputDeviceType.DualSense:
                        Debug.Log("DualSense(PS5)���g�p����Ă��܂�");
                        if (Input.GetButtonDown("Decision") && CostCheck(m_nNum))
                        {
                            m_As.PlayOneShot(SE_Set);   // SE�Đ�
                            Generation(m_nNum);
                            m_bSelect = false;
                            Cost_txt.SetText("�g�p�\�R�X�g:" + m_Cost);
                        }
                        break;
                    case InputDeviceManager.InputDeviceType.Switch:
                        Debug.Log("Switch��Pro�R���g���[���[���g�p����Ă��܂�");
                        if (Input.GetButtonDown("Decision") && CostCheck(m_nNum))
                        {
                            m_As.PlayOneShot(SE_Set);   // SE�Đ�
                            Generation(m_nNum);
                            m_bSelect = false;
                            Cost_txt.SetText("�g�p�\�R�X�g:" + m_Cost);
                        }
                        break;
                    default:
                        Debug.Log("���m�̓��̓f�o�C�X���g�p����Ă��܂�");
                        break;
                }
            }
        }
    }
    private void Generation(int i)
    {
        //�����p
        Vector3 p = player.transform.forward * 2;
        GameObject a;
        a = Instantiate(TrapList[i], player.transform.position + p, Quaternion.identity);
        a.GetComponent<CTrap>().m_bSetting = false;
    }
    private void Select()
    {
        //�I��
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetButtonDown("Right"))
        {
            m_As.PlayOneShot(SE_Select);   // SE�Đ�
            RectTransform rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100);
            m_nNum += 1;
            if (m_nNum > TrapList.Count - 1) m_nNum = TrapList.Count - 1;
            rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(200, 200);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetButtonDown("Left"))
        {
            m_As.PlayOneShot(SE_Select);   // SE�Đ�
            RectTransform rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100);
            m_nNum -= 1;
            if (m_nNum < 0) m_nNum = 0;
            rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(200, 200);
        }
    }
    public void SetSelect()
    {
        m_bSelect = true;
    }
    private IEnumerator IncreaseCostOverTime()
    {
        while (true)
        {
            // �w�肵�����Ԃ����ҋ@
            yield return new WaitForSeconds(increaseInterval);

            // �R�X�g�𑝂₷
            m_Cost++;
            Debug.Log("Cost increased to: " + m_Cost);
            Cost_txt.SetText("�g�p�\�R�X�g:" + m_Cost);
        }
    }
    private bool CostCheck(int i)
    {
        m_Cost-= CostList[i];
        if(m_Cost>=0) { return true; }
        else
        {
            m_Cost += CostList[i];
            return false;
        }
        
    }
}
