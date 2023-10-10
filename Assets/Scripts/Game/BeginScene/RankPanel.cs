using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public CustomGUIButton btnClose;
    public List<CustomGUILabel> ranks = new List<CustomGUILabel>();
    public List<CustomGUILabel> names = new List<CustomGUILabel>();
    public List<CustomGUILabel> scores = new List<CustomGUILabel>();
    public List<CustomGUILabel> times = new List<CustomGUILabel>();

    private void Start()
    {
        btnClose.clickEvent += () =>
        {
            HideMe();
            BeginPanel.Instance.ShowMe();
        };

        for (int i = 1; i <= 9; i++)
        {
            //����֪ʶ�㣺���Ҫ�ҵ��������Ӷ�����Ӷ��� ����֪�����е��Ӷ�������
            //��������д
            ranks.Add(transform.Find("Rank/rank (" + i + ")").GetComponent<CustomGUILabel>());
            names.Add(transform.Find("Name/name (" + i + ")").GetComponent<CustomGUILabel>());
            scores.Add(transform.Find("Score/score (" + i + ")").GetComponent<CustomGUILabel>());
            times.Add(transform.Find("Time/time (" + i + ")").GetComponent<CustomGUILabel>());
        }

        #region ��������
        //PlayerPrefs.DeleteAll();
        //GameDataMgr.Instance.SetRankInfo("cxk", 25, 114514);
        //GameDataMgr.Instance.SetRankInfo("xck", 75, 1919);
        //GameDataMgr.Instance.SetRankInfo("ckk", 88, 810);
        //GameDataMgr.Instance.SetRankInfo("xk", 100, 250);
        #endregion

        HideMe();
    }

    //����GameDataMgr�е����ݸ������а����������Ϣ
    public void UpdataRankData()
    {
        List<RankInfo> list = GameDataMgr.Instance.rankData.list;
        for (int i = 0; i < list.Count; i++)
        {
            names[i].content.text = list[i].name;
            scores[i].content.text = list[i].score.ToString();
            //times[i].content.text = list[i].time;
            int time = (int)list[i].time;
            string timestr = "";
            int hour = time / 3600;
            int min = time % 3600 / 60;
            int second = time % 60;
            if (hour > 0)
                timestr += hour + "h";
            if(min > 0)
                timestr += min + "\'";
            timestr += second + "\"";
            times[i].content.text = timestr;
        }
    }

    public override void ShowMe()
    {
        base.ShowMe();
        UpdataRankData();
    }
}
