using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HintPanel3 : BasePanel<HintPanel3>
{
    public CustomGUIButton btnClose;
    public CustomGUIButton btnPre;

    private void Start()
    {
        btnClose.clickEvent += () =>
        {
            HideMe();
        };
        btnPre.clickEvent += () =>
        {
            HideMe();
            HintPanel2.Instance.ShowMe();
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
