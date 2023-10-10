using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : TankBase
{
    //摄像机位置信息
    public Transform camera;
    //滚轮控制摄像机角度变化的速度
    public float cameraRotSpeedX = 150;
    //鼠标右键控制摄像机距离的速度
    public float cameraSpeed = 75;
    //关联的武器 默认玩家同一时间只能拥有一个武器
    public Weapon weapon;
    //最初的总血量
    public int firstMaxHp = 100;
    //最大移速
    public int maxSpeed = 500;
    //放武器的位置
    public Transform WeaponPos;

    //用于记录累加时间
    private float time;

    //获取坦克的刚体组件
    public Rigidbody rigidbody;
    //作弊按钮（测试用）
    public bool isCheatOn;

    void Start()
    {
        maxHp = firstMaxHp;
        GamePanel.Instance.SetNowHp(maxHp, nowHp);
        rigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {

        //1.ws键 控制前进后退
        float Vertical = Input.GetAxis("Vertical");
        if (Vertical != 0)
        {
            //transform.Translate(Vector3.forward * Vertical * moveSpeed * Time.deltaTime);
            rigidbody.AddRelativeForce(Vector3.forward * moveSpeed * Vertical);
        }
        //2.ad键 控制旋转
        float Horizontal = Input.GetAxis("Horizontal");
        if (Horizontal != 0 && Vertical > 0) 
        {
            transform.Rotate(Vector3.up, Horizontal * rotSpeed * Time.deltaTime);
        }
        if (Horizontal != 0 && Vertical < 0)
        {
            transform.Rotate(Vector3.down, Horizontal * rotSpeed * Time.deltaTime);
        }
        //3.鼠标左右移动 控制 炮台旋转
        float MouseX = Input.GetAxis("Mouse X");
        if (MouseX != 0)
        {
            paoTai.Rotate(Vector3.up, MouseX * headRotSpeed * Time.deltaTime);
        }
        //4.鼠标左键开火
        time += Time.deltaTime;
        if (Input.GetMouseButton(0) && time >= fireDeltaTime) 
        {
            Fire();
            time = 0;
        }
        //5.按住鼠标右键控制摄像头距离
        if (Input.GetMouseButton(1) && MouseX != 0)
        {
            camera.Translate(Vector3.forward * cameraSpeed * -MouseX * Time.deltaTime);
        }
        //6.鼠标上下滚轮控制视野角度
        float Scroll = Input.mouseScrollDelta.y;
        if (Scroll!=0)
        {
            camera.RotateAround(transform.position, camera.right, cameraRotSpeedX * -Scroll * Time.deltaTime);
        }
        if(isCheatOn)
        {
            Cheat();
        }
    }

    //开火
    public override void Fire()
    {
        if(weapon != null)
        {
            weapon.Fire();
        }
    }
    //受伤
    public override void Wound(TankBase otherTank)
    {
        base.Wound(otherTank);
        //自己受伤之后 还要更新主面板上的血条
        GamePanel.Instance.SetNowHp(maxHp, nowHp);
    }
    //死亡
    public override void Dead()
    {
        //删除自己之前 先断绝和摄像机的关系
        camera.parent = null;
        //打开死亡后的确认面板
        LosePanel.Instance.ShowMe();
        base.Dead();
    }
    //获得一个武器
    public void GetWeapon(Weapon weapon)
    {
        if(this.weapon != null)
        {
            Destroy(this.weapon.gameObject);
            this.weapon = null;
        }

        //第三个参数：是否保留在世界坐标系中的
        Weapon nowWeapon = GameObject.Instantiate(weapon, WeaponPos, false);
        this.weapon = nowWeapon;
        this.weapon.SetFather(this);

        switch (this.weapon.type)
        {
            case E_Type.Weapon1:
                this.atk = 5;
                fireDeltaTime = 0.3f;
                break;
            case E_Type.Weapon2:
                fireDeltaTime = 0.3f;
                this.atk = 5;
                break;
            case E_Type.Weapon3:
                fireDeltaTime = 0.2f;
                this.atk = 25;
                break;
            case E_Type.Weapon4:
                fireDeltaTime = 0.1f;
                this.atk = 5;
                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            TankBase enemy = bullet.father;
            if (enemy != null && enemy.tag == "Enemy")
            {
                Wound(enemy);
            }

        }

    }

    public void Cheat()
    {
        this.def = 999;
    }
}
