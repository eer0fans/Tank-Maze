using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MovingEnemy : TankBase
{
    //移动的点位
    public Transform[] MovingPos;
    //当前要去的点位
    private Transform nowMovingTarget;
    //看向的目标
    public GameObject Target;
    //警戒范围
    public int Range;

    //发射的位置
    public Transform firePos;
    //关联的子弹
    public Bullet bullet;

    //用于记录累加时间
    private float time;

    //死后奖励
    public GameObject deadReward;
    //分数
    public int score = 30;
    public override void Fire()
    {
        Bullet zidan = Instantiate(bullet, firePos.position, firePos.rotation);
        zidan.SetFather(this);
    }

    private void Start()
    {
        Target = GameObject.FindWithTag("Player");
        ChoosePoint();
    }
    void Update()
    {
        //处理移动相关
        if (nowMovingTarget != null) 
        {
            transform.LookAt(nowMovingTarget);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, nowMovingTarget.position) <= 0.05f)
            {
                ChoosePoint();
            }
        }


        //处理看向玩家和攻击相关
        if (Target!= null) 
        {
            float distance = Vector3.Distance(transform.position, Target.transform.position);
            if (distance <= Range)
            {
                //当目标和自己的距离小于Range 看向目标并隔固定时间攻击
                paoTai.LookAt(Target.transform.position);
                time += Time.deltaTime;
                if (time >= fireDeltaTime)
                {
                    Fire();
                    time = 0;
                }
            }
        }

    }

    public void ChoosePoint()
    {
        int index = Random.Range(0, MovingPos.Length);
        nowMovingTarget = MovingPos[index];
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
