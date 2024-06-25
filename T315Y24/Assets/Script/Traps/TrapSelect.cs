using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CCodingRule;

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
        
        CostText[0].SetText(""+CostList[0]);     //初期化
        CostText[1].SetText(""+CostList[1]);     //初期化
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bSelect)
        {
            Select();
            //決定
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Decision"))&& CostCheck(m_nNum))
            {
                m_As.PlayOneShot(SE_Set);   // SE再生
                Generation(m_nNum);
                m_bSelect = false;
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
