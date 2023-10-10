using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HintPanel2 : BasePanel<HintPanel2>
{
    public CustomGUIButton btnClose;
    public CustomGUIButton btnNext;
    public CustomGUIButton btnPre;

    private void Start()
    {
        btnClose.clickEvent += () =>
        {
            HideMe();
        };
        btnNext.clickEvent += () =>
        {
            HideMe();
            HintPanel3.Instance.ShowMe();
        };
        btnPre.clickEvent += () =>
        {
            HideMe();
            HintPanel.Instance.ShowMe();
        };
        HideMe();
    }

    //打开该面板的时候暂停游戏计时
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
