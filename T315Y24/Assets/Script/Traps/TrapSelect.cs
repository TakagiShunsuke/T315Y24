using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CCodingRule;

public class CTrapSelect : MonoBehaviour
{
    private GameObject player;
    public List<GameObject> TrapList;   //㩂̊i�[List
    public List<Image> ImageList;   //㩂̊i�[List
    public bool m_bSelect=true;
    private int m_nNum;
    [SerializeField] public AudioClip SE_Select;  // 㩑I������SE
    [SerializeField] public AudioClip SE_Set;  // 㩐ݒu����SE
    AudioSource m_As; // AudioSource��ǉ�

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");//����
        m_nNum = 0;
        RectTransform rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 200);
        m_As = GetComponent<AudioSource>(); // AudioSource�R���|�[�l���g��ǉ�
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bSelect)
        {
            Select();
            //����
            if (Input.GetKeyDown(KeyCode.E))
            {
                m_As.PlayOneShot(SE_Set);   // SE�Đ�
                Generation(m_nNum);
                m_bSelect = false;
            }
        }
    }
    private void Generation(int i)
    {
        //�����p
        Vector3 p = player.transform.forward * 2;
        Instantiate(TrapList[i], player.transform.position+p, Quaternion.identity);
    }
    private void Select()
    {
        //�I��
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_As.PlayOneShot(SE_Select);   // SE�Đ�
            RectTransform rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100);
            m_nNum += 1;
            if (m_nNum > TrapList.Count - 1) m_nNum = TrapList.Count - 1;
            rectTransform = ImageList[m_nNum].GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(200, 200);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
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
}
