using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    //现在我们要做的是 每次设置音效数据时可以保存该数据
    //然后下次开启游戏时就默认保持该数据
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    //音效数据
    public MusicData musicData;
    //排行榜数据
    public RankList rankData;
    
    private GameDataMgr() 
    {
        //在初始化的时候实现取数据
        musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData), "music") as MusicData;
        //防止第一次初始化的时候全是0/false
        if(musicData.isFirstLoad)
        {
            musicData.isFirstLoad = false;
            musicData.isOpenMusic = true;
            musicData.isOpenSound = true;
            musicData.musicValue = 1;
            musicData.soundValue = 1;
            //最后别忘了再存一次 不然每次重开游戏 都会执行这个 就没法实现每次开都保存上一次的数据了
            PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
        }

        //取排行榜数据
        rankData = PlayerPrefsDataMgr.Instance.LoadData(typeof(RankList), "rank") as RankList;
    }

    //提供一些API来方便存数据
    #region 存音乐相关的数据
    public void SetMusicTog(bool isSel)
    {
        musicData.isOpenMusic = isSel;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }
    public void SetSoundTog(bool isSel)
    {
        musicData.isOpenSound = isSel;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }
    public void SetMusicValue(float value)
    {
        musicData.musicValue = value;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }
    public void SetSoundValue(float value)
    {
        musicData.soundValue = value;
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }
    #endregion

    //存排行榜的一条数据
    public void SetRankInfo(string name, int score, float time)
    {
        //先用rankData暂时装下 再排序 然后只要前9条 最后存储
        rankData.list.Add(new RankInfo(name, score, time));
        //排序
        rankData.list.Sort((a, b) => 
        { 
            return a.time < b.time ? -1 : 1; 
        });
        //然后移除9条之后的数据
        for (int i = rankData.list.Count - 1; i >= 9; i--) 
        {
            rankData.list.RemoveAt(i);
        }
        //最后存储
        PlayerPrefsDataMgr.Instance.SaveData(rankData, "rank");

    }

}
