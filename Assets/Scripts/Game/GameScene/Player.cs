using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : TankBase
{
    //�����λ����Ϣ
    public Transform camera;
    //���ֿ���������Ƕȱ仯���ٶ�
    public float cameraRotSpeedX = 150;
    //����Ҽ����������������ٶ�
    public float cameraSpeed = 75;
    //���������� Ĭ�����ͬһʱ��ֻ��ӵ��һ������
    public Weapon weapon;
    //�������Ѫ��
    public int firstMaxHp = 100;
    //�������
    public int maxSpeed = 500;
    //��������λ��
    public Transform WeaponPos;

    //���ڼ�¼�ۼ�ʱ��
    private float time;

    //��ȡ̹�˵ĸ������
    public Rigidbody rigidbody;
    //���װ�ť�������ã�
    public bool isCheatOn;

    void Start()
    {
        maxHp = firstMaxHp;
        GamePanel.Instance.SetNowHp(maxHp, nowHp);
        rigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {

        //1.ws�� ����ǰ������
        float Vertical = Input.GetAxis("Vertical");
        if (Vertical != 0)
        {
            //transform.Translate(Vector3.forward * Vertical * moveSpeed * Time.deltaTime);
            rigidbody.AddRelativeForce(Vector3.forward * moveSpeed * Vertical);
        }
        //2.ad�� ������ת
        float Horizontal = Input.GetAxis("Horizontal");
        if (Horizontal != 0 && Vertical > 0) 
        {
            transform.Rotate(Vector3.up, Horizontal * rotSpeed * Time.deltaTime);
        }
        if (Horizontal != 0 && Vertical < 0)
        {
            transform.Rotate(Vector3.down, Horizontal * rotSpeed * Time.deltaTime);
        }
        //3.��������ƶ� ���� ��̨��ת
        float MouseX = Input.GetAxis("Mouse X");
        if (MouseX != 0)
        {
            paoTai.Rotate(Vector3.up, MouseX * headRotSpeed * Time.deltaTime);
        }
        //4.����������
        time += Time.deltaTime;
        if (Input.GetMouseButton(0) && time >= fireDeltaTime) 
        {
            Fire();
            time = 0;
        }
        //5.��ס����Ҽ���������ͷ����
        if (Input.GetMouseButton(1) && MouseX != 0)
        {
            camera.Translate(Vector3.forward * cameraSpeed * -MouseX * Time.deltaTime);
        }
        //6.������¹��ֿ�����Ұ�Ƕ�
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

    //����
    public override void Fire()
    {
        if(weapon != null)
        {
            weapon.Fire();
        }
    }
    //����
    public override void Wound(TankBase otherTank)
    {
        base.Wound(otherTank);
        //�Լ�����֮�� ��Ҫ����������ϵ�Ѫ��
        GamePanel.Instance.SetNowHp(maxHp, nowHp);
    }
    //����
    public override void Dead()
    {
        //ɾ���Լ�֮ǰ �ȶϾ���������Ĺ�ϵ
        camera.parent = null;
        //���������ȷ�����
        LosePanel.Instance.ShowMe();
        base.Dead();
    }
    //���һ������
    public void GetWeapon(Weapon weapon)
    {
        if(this.weapon != null)
        {
            Destroy(this.weapon.gameObject);
            this.weapon = null;
        }

        //�������������Ƿ�������������ϵ�е�
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
