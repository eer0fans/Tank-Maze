using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : BasePanel<WinPanel>
{
    public CustomGUIButton btnSure;
    public CustomGUIInput input;


    private void Start()
    {
        btnSure.clickEvent += () =>
        {
            //把当前数据存入排行榜
            GameDataMgr.Instance.SetRankInfo(input.inputStr, GamePanel.Instance.nowScore, GamePanel.Instance.nowTime);
            //切回开始场景
            SceneManager.LoadScene("BeginScene");
        };
        HideMe();
    }

    //打开此面板暂停游戏计时
    public override void ShowMe()
    {
        base.ShowMe();
        Time.timeScale = 0;
    }

    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }

}
