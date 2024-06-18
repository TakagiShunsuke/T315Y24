using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CCodingRule;

public class CTrapSelect : MonoBehaviour
{
    private GameObject player;
    public List<GameObject> TrapList;   //罠の格納List
    public List<Image> ImageList;   //罠の格納List
    public bool m_bSelect=true;
    private int m_nNum;
    [SerializeField] public AudioClip SE_Select;  // 罠選択時のSE
    [SerializeField] public AudioClip SE_Set;  // 罠設置時のSE
    AudioSource m_As; // AudioSourceを追加

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");//検索
        m_nNum = 0;
        RectTransform rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 200);
        m_As = GetComponent<AudioSource>(); // AudioSourceコンポーネントを追加
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bSelect)
        {
            Select();
            //決定
            if (Input.GetKeyDown(KeyCode.E))
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
        Instantiate(TrapList[i], player.transform.position+p, Quaternion.identity);
    }
    private void Select()
    {
        //選択中
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_As.PlayOneShot(SE_Select);   // SE再生
            RectTransform rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100);
            m_nNum += 1;
            if (m_nNum > TrapList.Count - 1) m_nNum = TrapList.Count - 1;
            rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(200, 200);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
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
}
