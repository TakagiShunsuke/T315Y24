using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultSet : CMonoSingleton<ResultSet>
{
    public List<TMP_Text> ResultText;

    // Start is called before the first frame update
    override protected void Start()
    {
        GameMineData MineResultData = Mine.GetGameMineData();
        GameRemoteBombData RBResultData = RemoteBomb.GetGameRemoteBombData();
        // DisplayResult(resultData);
        ResultText[0].SetText("" + MineResultData.SetMine);
        ResultText[1].SetText("" + MineResultData.UseMine);
        ResultText[2].SetText("" + MineResultData.MineKill);

        ResultText[3].SetText("" + RBResultData.SetRemoteBomb);
        ResultText[4].SetText("" + RBResultData.UseRemoteBomb);
        ResultText[5].SetText("" + RBResultData.RemoteBombKill);

        ResultText[6].SetText("" + (MineResultData.SetMine + RBResultData.SetRemoteBomb));
        ResultText[7].SetText("" + (MineResultData.UseMine + RBResultData.UseRemoteBomb));
        ResultText[8].SetText("" + (MineResultData.MineKill + RBResultData.RemoteBombKill));

        ResultText[9].SetText("" + (MineResultData.MineKill + RBResultData.RemoteBombKill));

        
        Mine.ResetMineData();
        RemoteBomb.ResetRemoteBombData();
    }

    // Update is called once per frame
    override protected void Update()
    {

    }
    public void ToggleActive()
    {
        for (int i = 0; i < ResultText.Count; i++)
        {
            bool currentState = ResultText[i].gameObject.activeSelf;
            ResultText[i].gameObject.SetActive(!currentState);
        }

    }
    private void aaaaaa()
    {
        for (int i = 0; i < ResultText.Count; i++)
        {
            bool currentState = ResultText[i].gameObject.activeSelf;
            ResultText[i].gameObject.SetActive(!currentState);
        }
    }
}
