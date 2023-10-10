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
    //当前武器的种类
    public E_Type type;

    //发射的位置
    public Transform[] firePos;
    //关联的子弹
    public Bullet bullet;
    //当前谁拥有的该武器
    //目的是告诉子弹 在打中一个物体之后 是谁打中的
    public TankBase father;
    //设置拥有者
    public void SetFather(TankBase father)
    {
        this.father = father;
    }

    //开火
    public void Fire()
    {
        for(int i = 0; i < firePos.Length; i++)
        {
            Bullet zidan = Instantiate(bullet, firePos[i].position, firePos[i].rotation);
            zidan.SetFather(this.father);
        }
    }

}
