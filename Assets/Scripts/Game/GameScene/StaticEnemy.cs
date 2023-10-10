using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : TankBase
{
    //�����λ��
    public Transform[] firePos;
    //�������ӵ�
    public Bullet bullet;

    //���ڼ�¼�ۼ�ʱ��
    private float time;
    //�Ƿ��������ʱ����
    public bool isRandomDeltaTime = false;
    //�Ƿ����ת��
    public bool isRandomRotation = false;
    //������
    public GameObject deadReward;
    //����
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
