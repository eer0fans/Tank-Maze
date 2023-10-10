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
            //�ѵ�ǰ���ݴ������а�
            GameDataMgr.Instance.SetRankInfo(input.inputStr, GamePanel.Instance.nowScore, GamePanel.Instance.nowTime);
            //�лؿ�ʼ����
            SceneManager.LoadScene("BeginScene");
        };
        HideMe();
    }

    //�򿪴������ͣ��Ϸ��ʱ
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
