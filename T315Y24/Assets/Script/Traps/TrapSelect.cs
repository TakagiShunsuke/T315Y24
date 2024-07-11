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
    //整理予定
    public List<GameObject> TrapList;   //罠の格納List
    public List<Image> ImageList;   //罠の格納List
    public List<int> CostList;   //罠のコスト格納List
    public List<TMP_Text> CostText;   //罠のコストテキスト格納List
    //
    public bool m_bSelect=true;
    private int m_nNum;
    [SerializeField] public AudioClip SE_Select;  // 罠選択時のSE
    [SerializeField] public AudioClip SE_Set;  // 罠設置時のSE
    AudioSource m_As; // AudioSourceを追加

    public static int m_Cost;
    public float increaseInterval = 5.0f;  // コストを増やす間隔（秒）

    [SerializeField] private TMP_Text Cost_txt; //表示させるテキスト(TMP)
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");//検索
        m_nNum = 0;
        RectTransform rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 200);
        m_As = GetComponent<AudioSource>(); // AudioSourceコンポーネントを追加
        m_bSelect = true;
        m_Cost = 10;
        // コルーチンを開始
        StartCoroutine(IncreaseCostOverTime());
        Cost_txt.SetText("使用可能コスト:" + m_Cost);     //初期化
        CostText[0].SetText(""+CostList[0]);     //初期化
        CostText[1].SetText("" + CostList[1]);     //初期化
        InputDeviceManager.Instance.OnChangeDeviceType.AddListener(OnChangeDeviceTypeHandler);

    }
    private void OnChangeDeviceTypeHandler()
    {
        // 入力デバイスの種別が変更されたときの処理
        Debug.Log("入力デバイスの種別が変更されました。\n現在の入力デバイスの種別：" + InputDeviceManager.Instance.CurrentDeviceType);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bSelect)
        {
            Select();
            //決定
            //if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Decision")) && CostCheck(m_nNum))
            //{
            //    m_As.PlayOneShot(SE_Set);   // SE再生
            //    Generation(m_nNum);
            //    m_bSelect = false;
            //    Cost_txt.SetText("使用可能コスト:" + m_Cost);
            //}

            
            if (InputDeviceManager.Instance != null)
            {
                // 現在の入力デバイスタイプを取得
                InputDeviceManager.InputDeviceType currentDeviceType = InputDeviceManager.Instance.CurrentDeviceType;
                //決定
                // 現在のデバイスタイプに応じた処理を行う
                Debug.Log(currentDeviceType);
                switch (currentDeviceType)
                {
                    case InputDeviceManager.InputDeviceType.Keyboard:
                        Debug.Log("Keyboardが使用されています");
                        if (Input.GetKeyDown(KeyCode.E) && CostCheck(m_nNum)) //ダッシュ入力
                        {
                            m_As.PlayOneShot(SE_Set);   // SE再生
                            Generation(m_nNum);
                            m_bSelect = false;
                            Cost_txt.SetText("使用可能コスト:" + m_Cost);
                        }
                        break;
                    case InputDeviceManager.InputDeviceType.Xbox:
                        Debug.Log("XBOXが使用されています");
                        if (Input.GetButtonDown("Decision") && CostCheck(m_nNum))
                        {
                            m_As.PlayOneShot(SE_Set);   // SE再生
                            Generation(m_nNum);
                            m_bSelect = false;
                            Cost_txt.SetText("使用可能コスト:" + m_Cost);
                        }
                        break;
                    case InputDeviceManager.InputDeviceType.DualShock4:
                        Debug.Log("DualShock4(PS4)が使用されています");
                        if (Input.GetButtonDown("Decision") && CostCheck(m_nNum))
                        {
                            m_As.PlayOneShot(SE_Set);   // SE再生
                            Generation(m_nNum);
                            m_bSelect = false;
                            Cost_txt.SetText("使用可能コスト:" + m_Cost);
                        }
                        break;
                    case InputDeviceManager.InputDeviceType.DualSense:
                        Debug.Log("DualSense(PS5)が使用されています");
                        if (Input.GetButtonDown("Decision") && CostCheck(m_nNum))
                        {
                            m_As.PlayOneShot(SE_Set);   // SE再生
                            Generation(m_nNum);
                            m_bSelect = false;
                            Cost_txt.SetText("使用可能コスト:" + m_Cost);
                        }
                        break;
                    case InputDeviceManager.InputDeviceType.Switch:
                        Debug.Log("SwitchのProコントローラーが使用されています");
                        if (Input.GetButtonDown("Decision") && CostCheck(m_nNum))
                        {
                            m_As.PlayOneShot(SE_Set);   // SE再生
                            Generation(m_nNum);
                            m_bSelect = false;
                            Cost_txt.SetText("使用可能コスト:" + m_Cost);
                        }
                        break;
                    default:
                        Debug.Log("未知の入力デバイスが使用されています");
                        break;
                }
            }
        }
    }
    private void Generation(int i)
    {
        //生成用
        Vector3 p = player.transform.forward * 2;
        GameObject a;
        a = Instantiate(TrapList[i], player.transform.position + p, Quaternion.identity);
        a.GetComponent<CTrap>().m_bSetting = false;
    }
    private void Select()
    {
        //選択中
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetButtonDown("Right"))
        {
            m_As.PlayOneShot(SE_Select);   // SE再生
            RectTransform rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100);
            m_nNum += 1;
            if (m_nNum > TrapList.Count - 1) m_nNum = TrapList.Count - 1;
            rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(200, 200);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetButtonDown("Left"))
        {
            m_As.PlayOneShot(SE_Select);   // SE再生
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
            // 指定した時間だけ待機
            yield return new WaitForSeconds(increaseInterval);

            // コストを増やす
            m_Cost++;
            Debug.Log("Cost increased to: " + m_Cost);
            Cost_txt.SetText("使用可能コスト:" + m_Cost);
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
