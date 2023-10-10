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

    //��¼��ǰ����
    public int nowScore;
    public float nowTime;
    public int maxWidth = 500;
    Player player;
    private void Start()
    {
        //�˳���ť�ļ����¼�
        btnBack.clickEvent += () =>
        {
            EnsureQuitPanel.Instance.ShowMe();
        };
        //���ð�ť�ļ����¼�
        btnSetting.clickEvent += () =>
        {
            SettingPanel.Instance.ShowMe();
        };
        //��ʾ��ť�ļ����¼�
        btnHint.clickEvent += () =>
        {
            HintPanel.Instance.ShowMe();
        };
        //���Դ���
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

        labSpeed.content.text = "�ٶȣ�" + player.moveSpeed;

        labHp.content.text = "Ѫ����" + player.nowHp + "/" + player.maxHp;
        labAtk.content.text = "��������" + player.atk;
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
        //������Ѫ�����ĳ���
        maxWidth = (int)((float)maxHp / firstMaxHp * 500);
        texMaxHp.guiPos.width = maxWidth;
        //���õ�ǰѪ�����ĳ���
        texHp.guiPos.width = (float)nowHp / maxHp * maxWidth;
    }
    public void SetNowHp(int maxHp, int nowHp)
    {
        //���õ�ǰѪ�����ĳ���
        texHp.guiPos.width = (float)nowHp / maxHp * maxWidth;
    }

}
