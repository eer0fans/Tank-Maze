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
    //����
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
        //�����������λ���ϴ���һ����������Ч
        //��ЧҲ���Կ�����һ��Ԥ���� ������Unity�ṩ��������Ч����
        GameObject eff = GameObject.Instantiate(deadEffect, transform.position, transform.rotation);
        //��ը��Ч����Ч��С�Լ��Ƿ���Ҫ������Ϸ�����е���Ч������
        AudioSource audioSource = eff.GetComponent<AudioSource>();
        audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
        audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;

        //���������������Ҫ����������Ч ��Ҫ�����������
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
            //�Ƚ���������תΪ��Ļ����
            Vector2 point = Camera.main.WorldToScreenPoint(transform.position);
            //Ȼ����Ļ����תΪGUI����
            point.y = Screen.height - point.y;

            //��point�㴦������ͼ
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
