using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 音效数据类 用于存储音乐设置相关的数据
/// </summary>
public class MusicData
{
    public bool isOpenMusic;
    public bool isOpenSound;
    public float musicValue;
    public float soundValue;

    //是否第一次加载
    public bool isFirstLoad = true;
}
