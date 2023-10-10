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
            //关闭当前设置面板
            HideMe();
            //新知识点：获取当前场景的名字
            if(SceneManager.GetActiveScene().name=="BeginScene")
            {
                BeginPanel.Instance.ShowMe();
            }
        };
        //isSel表示当前是否开启音乐
        togMusic.changeValue += (isSel) =>
        {
            GameDataMgr.Instance.SetMusicTog(isSel);
            BGM.Instance.SetMusicOnOff(isSel);

        };
        //isSel表示当前是否开启音效
        togSound.changeValue += (isSel) =>
        {
            GameDataMgr.Instance.SetSoundTog(isSel);
        };
        //value是一个从0~1的float值 表示当前音乐大小
        sliderMusic.changeValue += (value) =>
        {
            GameDataMgr.Instance.SetMusicValue(value);
            BGM.Instance.SetMusicValue(value);
        };
        //value是一个从0~1的float值 表示当前音效大小
        sliderSound.changeValue += (value) =>
        {
            GameDataMgr.Instance.SetSoundValue(value);
        };


        //事件都监听完了之后再把自己隐藏
        HideMe();

    }

    //根据GameDataMgr数据管理类中的数据来更新当前面板上的数据
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

    //打开设置面板的时候暂停游戏计时
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
