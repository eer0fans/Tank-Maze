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
        //��ʼ��Ϸ��ť�ļ����¼�
        btnBegin.clickEvent += () =>
        {
            //�л�����Ϸ����
            SceneManager.LoadScene("GameScene");
        };
        //���ð�ť�ļ����¼�
        btnSetting.clickEvent += () =>
        {
            //���������
            SettingPanel.Instance.ShowMe();
            //�����Լ�����ʱû��İ취��
            HideMe();
        };
        //�˳���Ϸ��ť�ļ����¼�
        btnQuit.clickEvent += () =>
        {
            //��ȷ���˳����
            EnsureQuitPanel.Instance.ShowMe();
            HideMe();
        };
        //���а�ť�ļ����¼�
        btnRank.clickEvent += () =>
        {
            //�����а����
            RankPanel.Instance.ShowMe();
            HideMe();
        };
    }
}
