using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : BasePanel<LosePanel>
{
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnAgain;


    private void Start()
    {
        btnQuit.clickEvent += () =>
        {
            SceneManager.LoadScene("BeginScene");
        };
        btnAgain.clickEvent += () =>
        {
            SceneManager.LoadScene("GameScene");
        };
        HideMe();
    }

    //打开确认退出面板的时候暂停游戏计时
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
