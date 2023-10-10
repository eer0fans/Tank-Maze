using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCube : MonoBehaviour
{
    public int maxHp = 30;
    public int nowHp = 30;

    public GameObject deadEffect;

    public GameObject deadReward1;
    public GameObject deadReward2;
    public GameObject deadReward3;
    //分数
    public int score = 10;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            TankBase player = bullet.father;
            if (player != null && player.tag == "Player") 
            {
                nowHp -= player.atk;
                isShowHp = true;
                if(nowHp <= 0)
                {
                    nowHp = 0;
                    Dead();
                }
            }
        }
    }

    public void Dead()
    {
        Destroy(this.gameObject);
        //在死亡物体的位置上创造一个死亡的特效
        //特效也可以看做是一种预设体 是利用Unity提供的粒子特效做的
        GameObject eff = GameObject.Instantiate(deadEffect, transform.position, transform.rotation);
        //爆炸特效的音效大小以及是否开启要根据游戏设置中的音效来决定
        AudioSource audioSource = eff.GetComponent<AudioSource>();
        audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
        audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;

        //立方体死亡后除了要创建死亡特效 还要随机创建奖励
        int randomNum = Random.RandomRange(0, 100);
        if (randomNum >= 0 && randomNum < 20) 
        {
            GameObject obj = GameObject.Instantiate(deadReward1, transform.position, transform.rotation);
        }
        else if (randomNum >= 20 && randomNum < 35)
        {
            GameObject obj = GameObject.Instantiate(deadReward2, transform.position, transform.rotation);
        }
        else if (randomNum >= 35 && randomNum < 50)
        {
            GameObject obj = GameObject.Instantiate(deadReward3, transform.position, transform.rotation);
        }
        GamePanel.Instance.AddScore(score);

    }


    private bool isShowHp = false;

    Rect maxHpRect = new Rect();
    Rect nowHpRect = new Rect();
    public Texture maxHpTex;
    public Texture nowHpTex;
    private void OnGUI()
    {
        if(isShowHp)
        {
            //先将世界坐标转为屏幕坐标
            Vector2 point = Camera.main.WorldToScreenPoint(transform.position);
            //然后将屏幕坐标转为GUI坐标
            point.y = Screen.height - point.y;

            //在point点处画两张图
            maxHpRect.x = point.x - 75;
            maxHpRect.y = point.y - 100;
            maxHpRect.width = 150;
            maxHpRect.height = 20;
            GUI.DrawTexture(maxHpRect, maxHpTex);
            nowHpRect.x = point.x - 75;
            nowHpRect.y = point.y - 100;
            nowHpRect.width = (float)nowHp / maxHp * 150;
            nowHpRect.height = 20;
            GUI.DrawTexture(nowHpRect, nowHpTex);
        }
    }
}
