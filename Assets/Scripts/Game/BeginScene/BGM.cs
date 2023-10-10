using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private static BGM instance;
    public static BGM Instance=>instance;

    public AudioSource audioSource;
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();

        SetMusicOnOff(GameDataMgr.Instance.musicData.isOpenMusic);
        SetMusicValue(GameDataMgr.Instance.musicData.musicValue);
    }

    public void SetMusicOnOff(bool isOpen)
    {
        if(!isOpen)
        {
            audioSource.Pause();
        }
        else if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void SetMusicValue(float value)
    {
        audioSource.volume = value;
    }
}
