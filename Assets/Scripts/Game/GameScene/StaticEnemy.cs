using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : TankBase
{
    //发射的位置
    public Transform[] firePos;
    //关联的子弹
    public Bullet bullet;

    //用于记录累加时间
    private float time;
    //是否随机攻击时间间隔
    public bool isRandomDeltaTime = false;
    //是否随机转向
    public bool isRandomRotation = false;
    //死后奖励
    public GameObject deadReward;
    //分数
    public int score = 30;

    public override void Fire()
    {
        for (int i = 0; i < firePos.Length; i++)
        {
            Bullet zidan = Instantiate(bullet, firePos[i].position, firePos[i].rotation);
            zidan.SetFather(this);
        }
        if(isRandomDeltaTime)
        {
            RandomDeltaTime();
        }
        if(bullet.isRandomSpeed)
        {
            bullet.RandomSpeed();
        }
        if(isRandomRotation)
        {
            paoTai.localEulerAngles = new Vector3(0, Random.RandomRange(0, 360), 0);
        }
    }

    public void RandomDeltaTime()
    {
        fireDeltaTime = Random.RandomRange(0.2f, 1);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= fireDeltaTime) 
        {
            Fire();
            time = 0;
        }
        //print(nowHp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            TankBase player = bullet.father;
            if (player != null && player.tag == "Player")
            {
                Wound(player);
            }

        }
    }

    public override void Dead()
    {
        base.Dead();
        GameObject.Instantiate(deadReward, transform.position, transform.rotation);

        GamePanel.Instance.AddScore(score);

    }

    public override void Wound(TankBase otherTank)
    {
        base.Wound(otherTank);
        isShowHp = true;
    }

    private bool isShowHp = false;

    Rect maxHpRect = new Rect();
    Rect nowHpRect = new Rect();
    public Texture maxHpTex;
    public Texture nowHpTex;
    private void OnGUI()
    {
        if (isShowHp)
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
