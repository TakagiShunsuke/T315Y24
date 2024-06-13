using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CCodingRule;

public class CTrapSelect : MonoBehaviour
{
    private GameObject player;
    public List<GameObject> TrapList;   //„©ÇÃäiî[List
    public List<Image> ImageList;   //„©ÇÃäiî[List
    public bool m_bSelect=true;
    private int m_nNum;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");//åüçı
        m_nNum = 0;
        RectTransform rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 200);

    }

    // Update is called once per frame
    void Update()
    {
        if (m_bSelect)
        {
            Select();
            //åàíË
            if (Input.GetKeyDown(KeyCode.E))
            {
                Generation(m_nNum);
                m_bSelect = false;
            }
        }
    }
    private void Generation(int i)
    {
        //ê∂ê¨óp
        Vector3 p = player.transform.forward * 2;
        Instantiate(TrapList[i], player.transform.position+p, Quaternion.identity);
    }
    private void Select()
    {
        //ëIëíÜ
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            RectTransform rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100);
            m_nNum += 1;
            if (m_nNum > TrapList.Count - 1) m_nNum = TrapList.Count - 1;
            rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(200, 200);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
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
}
