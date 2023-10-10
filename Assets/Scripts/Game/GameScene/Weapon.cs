using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum E_Type
{
    Weapon1,
    Weapon2,
    Weapon3, 
    Weapon4,
}
public class Weapon : MonoBehaviour
{
    //��ǰ����������
    public E_Type type;

    //�����λ��
    public Transform[] firePos;
    //�������ӵ�
    public Bullet bullet;
    //��ǰ˭ӵ�еĸ�����
    //Ŀ���Ǹ����ӵ� �ڴ���һ������֮�� ��˭���е�
    public TankBase father;
    //����ӵ����
    public void SetFather(TankBase father)
    {
        this.father = father;
    }

    //����
    public void Fire()
    {
        for(int i = 0; i < firePos.Length; i++)
        {
            Bullet zidan = Instantiate(bullet, firePos[i].position, firePos[i].rotation);
            zidan.SetFather(this.father);
        }
    }

}
