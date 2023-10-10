using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : BasePanel<SettingPanel>
{
    public CustomGUIButton btnClose;
    public CustomGUIToggle togMusic;
    public CustomGUIToggle togSound;
    public CustomGUISlider sliderMusic;
    public CustomGUISlider sliderSound;

    private void Start()
    {
        btnClose.clickEvent += () =>
        {
            //�رյ�ǰ�������
            HideMe();
            //��֪ʶ�㣺��ȡ��ǰ����������
            if(SceneManager.GetActiveScene().name=="BeginScene")
            {
                BeginPanel.Instance.ShowMe();
            }
        };
        //isSel��ʾ��ǰ�Ƿ�������
        togMusic.changeValue += (isSel) =>
        {
            GameDataMgr.Instance.SetMusicTog(isSel);
            BGM.Instance.SetMusicOnOff(isSel);

        };
        //isSel��ʾ��ǰ�Ƿ�����Ч
        togSound.changeValue += (isSel) =>
        {
            GameDataMgr.Instance.SetSoundTog(isSel);
        };
        //value��һ����0~1��floatֵ ��ʾ��ǰ���ִ�С
        sliderMusic.changeValue += (value) =>
        {
            GameDataMgr.Instance.SetMusicValue(value);
            BGM.Instance.SetMusicValue(value);
        };
        //value��һ����0~1��floatֵ ��ʾ��ǰ��Ч��С
        sliderSound.changeValue += (value) =>
        {
            GameDataMgr.Instance.SetSoundValue(value);
        };


        //�¼�����������֮���ٰ��Լ�����
        HideMe();

    }

    //����GameDataMgr���ݹ������е����������µ�ǰ����ϵ�����
    public void UpdataMusicData()
    {
        MusicData data = GameDataMgr.Instance.musicData;

        togMusic.isSel = data.isOpenMusic;
        togSound.isSel = data.isOpenSound;
        sliderMusic.nowValue = data.musicValue;
        sliderSound.nowValue = data.soundValue;
    }
    protected override void Awake()
    {
        base.Awake();
        UpdataMusicData();
    }

    //����������ʱ����ͣ��Ϸ��ʱ
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
