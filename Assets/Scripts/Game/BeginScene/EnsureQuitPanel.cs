using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnsureQuitPanel : BasePanel<EnsureQuitPanel>
{
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnCancel;


    private void Start()
    {
        btnQuit.clickEvent += () =>
        {
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                SceneManager.LoadScene("BeginScene");
            }
            else if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                Application.Quit();
            }

        };
        btnCancel.clickEvent += () =>
        {
            HideMe();
            if(SceneManager.GetActiveScene().name=="BeginScene")
            {
                BeginPanel.Instance.ShowMe();
            }
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
