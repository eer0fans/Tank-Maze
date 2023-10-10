using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginPanel : BasePanel<BeginPanel>
{
    public CustomGUIButton btnBegin;
    public CustomGUIButton btnSetting;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnRank;


    private void Start()
    {
        //开始游戏按钮的监听事件
        btnBegin.clickEvent += () =>
        {
            //切换到游戏场景
            SceneManager.LoadScene("GameScene");
        };
        //设置按钮的监听事件
        btnSetting.clickEvent += () =>
        {
            //打开设置面板
            SettingPanel.Instance.ShowMe();
            //隐藏自己（暂时没别的办法）
            HideMe();
        };
        //退出游戏按钮的监听事件
        btnQuit.clickEvent += () =>
        {
            //打开确认退出面板
            EnsureQuitPanel.Instance.ShowMe();
            HideMe();
        };
        //排行榜按钮的监听事件
        btnRank.clickEvent += () =>
        {
            //打开排行榜面板
            RankPanel.Instance.ShowMe();
            HideMe();
        };
    }
}
