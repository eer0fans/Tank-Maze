using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    //��������Ҫ������ ÿ��������Ч����ʱ���Ա��������
    //Ȼ���´ο�����Ϸʱ��Ĭ�ϱ��ָ�����
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;

    //��Ч����
    public MusicData musicData;
    //���а�����
    public RankList rankData;
    
    private GameDataMgr() 
    {
        //�ڳ�ʼ����ʱ��ʵ��ȡ����
        musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData), "music") as MusicData;
        //��ֹ��һ�γ�ʼ����ʱ��ȫ��0/false
        if(musicData.isFirstLoad)
        {
            musicData.isFirstLoad = false;
            musicData.isOpenMusic = true;
            musicData.isOpenSound = true;
            musicData.musicValue = 1;
            musicData.soundValue = 1;
            //���������ٴ�һ�� ��Ȼÿ���ؿ���Ϸ ����ִ����� ��û��ʵ��ÿ�ο���������һ�ε�������
            PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
        }

        //ȡ���а�����
        rankData = PlayerPrefsDataMgr.Instance.LoadData(typeof(RankList), "rank") as RankList;
    }

    //�ṩһЩAPI�����������
    #region ��������ص�����
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

    //�����а��һ������
    public void SetRankInfo(string name, int score, float time)
    {
        //����rankData��ʱװ�� ������ Ȼ��ֻҪǰ9�� ���洢
        rankData.list.Add(new RankInfo(name, score, time));
        //����
        rankData.list.Sort((a, b) => 
        { 
            return a.time < b.time ? -1 : 1; 
        });
        //Ȼ���Ƴ�9��֮�������
        for (int i = rankData.list.Count - 1; i >= 9; i--) 
        {
            rankData.list.RemoveAt(i);
        }
        //���洢
        PlayerPrefsDataMgr.Instance.SaveData(rankData, "rank");

    }

}
