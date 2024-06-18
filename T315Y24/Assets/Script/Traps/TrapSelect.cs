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
    //
    public bool m_bSelect=true;
    private int m_nNum;

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
        m_bSelect = true;
        m_Cost = 10;
        // コルーチンを開始
        StartCoroutine(IncreaseCostOverTime());
        Cost_txt.SetText("使用可能コスト:" + m_Cost);     //初期化
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
                Generation(m_nNum);
                m_bSelect = false;
                Cost_txt.SetText("使用可能コスト:" + m_Cost); 
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
            RectTransform rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100);
            m_nNum += 1;
            if (m_nNum > TrapList.Count - 1) m_nNum = TrapList.Count - 1;
            rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(200, 200);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetButtonDown("Left"))
        {
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
