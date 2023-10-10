using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBase : MonoBehaviour
{
    public int atk;
    public int def;
    public int maxHp;
    public int nowHp;
    public float moveSpeed = 10;
    public float rotSpeed = 45;
    public float headRotSpeed = 90;
    //炮台的位置信息
    public Transform paoTai;

    //死亡特效
    public GameObject[] deadEffect;
    //开火时间间隔
    public float fireDeltaTime;
    //开火
    public abstract void Fire();
    //受伤
    public virtual void Wound(TankBase otherTank)
    {
        int realAtk = otherTank.atk - this.def;
        this.nowHp -= realAtk > 0 ? realAtk : 0;
        if(this.nowHp <= 0) 
        {
            //为了防止面板上血条显示出bug
            this.nowHp = 0;
            this.Dead();
        }
    }
    //死亡
    public virtual void Dead()
    {
        Destroy(this.gameObject);
        //在死亡物体的位置上创造一个死亡的特效
        //特效也可以看做是一种预设体 是利用Unity提供的粒子特效做的
        for(int i=0;i<deadEffect.Length;i++)
        {
            GameObject eff = GameObject.Instantiate(deadEffect[i], transform.position, transform.rotation);
            //爆炸特效的音效大小以及是否开启要根据游戏设置中的音效来决定
            AudioSource audioSource = eff.GetComponent<AudioSource>();
            if(audioSource != null)
            {
                audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
                audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            }
        }
    }

}
