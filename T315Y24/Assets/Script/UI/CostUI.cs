using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class CostUI : MonoBehaviour
{
    public Image UIobj;
    
    public float countTime = 5.0f;
    [SerializeField] private TMP_Text Cost_txt; //表示させるテキスト(TMP)
    void Start()
    {
        UIobj.fillAmount = 0.0f;
        Cost_txt.SetText("" + CTrapSelect.m_Cost);     //初期化
    }
    // Update is called once per frame
    void Update()
    {
        
        UIobj.fillAmount += 1.0f / countTime * Time.deltaTime;
        if(UIobj.fillAmount>=1.0f)
        {
            UIobj.fillAmount = 0.0f;
            CTrapSelect.m_Cost++;
        }
        Cost_txt.SetText("" + CTrapSelect.m_Cost);
    }
}
