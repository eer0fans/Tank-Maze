using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    public CustomGUILabel labScore;
    public CustomGUILabel labTime;
    public CustomGUILabel labSpeed;
    public CustomGUILabel labHp;
    public CustomGUILabel labAtk;
    public CustomGUIButton btnBack;
    public CustomGUIButton btnSetting;
    public CustomGUIButton btnHint;
    public CustomGUITexture texMap;
    public CustomGUITexture texMaxHp;
    public CustomGUITexture texHp;

    //记录当前分数
    public int nowScore;
    public float nowTime;
    public int maxWidth = 500;
    Player player;
    private void Start()
    {
        //退出按钮的监听事件
        btnBack.clickEvent += () =>
        {
            EnsureQuitPanel.Instance.ShowMe();
        };
        //设置按钮的监听事件
        btnSetting.clickEvent += () =>
        {
            SettingPanel.Instance.ShowMe();
        };
        //提示按钮的监听事件
        btnHint.clickEvent += () =>
        {
            HintPanel.Instance.ShowMe();
        };
        //测试代码
        //AddScore(25);
        //SetHp(500, 250);
        GameObject obj = GameObject.FindWithTag("Player");
        player = obj.GetComponent<Player>();

    }

    int time, hour, min, second;
    string timeStr;
    private void Update()
    {
        nowTime += Time.deltaTime;

        time = (int)nowTime;
        timeStr = "";
        hour = time / 3600;
        min = time % 3600 / 60;
        second = time % 60;
        if (hour > 0)
            timeStr += hour + "h";
        if (min > 0)
            timeStr += min + "\'";
        timeStr += second + "\"";
        labTime.content.text = timeStr;

        labSpeed.content.text = "速度：" + player.moveSpeed;

        labHp.content.text = "血量：" + player.nowHp + "/" + player.maxHp;
        labAtk.content.text = "攻击力：" + player.atk;
        if (player.weapon.type == E_Type.Weapon2)
            labAtk.content.text += "*3";
    }
    public void AddScore(int score)
    {
        nowScore += score;
        labScore.content.text = nowScore.ToString();
    }

    public void SetMaxHp(int maxHp,int nowHp,int firstMaxHp)
    {
        //设置总血量条的长度
        maxWidth = (int)((float)maxHp / firstMaxHp * 500);
        texMaxHp.guiPos.width = maxWidth;
        //设置当前血量条的长度
        texHp.guiPos.width = (float)nowHp / maxHp * maxWidth;
    }
    public void SetNowHp(int maxHp, int nowHp)
    {
        //设置当前血量条的长度
        texHp.guiPos.width = (float)nowHp / maxHp * maxWidth;
    }

}
