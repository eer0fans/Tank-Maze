using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MovingEnemy : TankBase
{
    //�ƶ��ĵ�λ
    public Transform[] MovingPos;
    //��ǰҪȥ�ĵ�λ
    private Transform nowMovingTarget;
    //�����Ŀ��
    public GameObject Target;
    //���䷶Χ
    public int Range;

    //�����λ��
    public Transform firePos;
    //�������ӵ�
    public Bullet bullet;

    //���ڼ�¼�ۼ�ʱ��
    private float time;

    //������
    public GameObject deadReward;
    //����
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
        //�����ƶ����
        if (nowMovingTarget != null) 
        {
            transform.LookAt(nowMovingTarget);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, nowMovingTarget.position) <= 0.05f)
            {
                ChoosePoint();
            }
        }


        //��������Һ͹������
        if (Target!= null) 
        {
            float distance = Vector3.Distance(transform.position, Target.transform.position);
            if (distance <= Range)
            {
                //��Ŀ����Լ��ľ���С��Range ����Ŀ�겢���̶�ʱ�乥��
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
