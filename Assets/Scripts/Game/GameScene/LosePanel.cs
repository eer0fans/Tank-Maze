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

    //��ȷ���˳�����ʱ����ͣ��Ϸ��ʱ
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
