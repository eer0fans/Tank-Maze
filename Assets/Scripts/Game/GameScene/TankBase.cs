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
    //��̨��λ����Ϣ
    public Transform paoTai;

    //������Ч
    public GameObject[] deadEffect;
    //����ʱ����
    public float fireDeltaTime;
    //����
    public abstract void Fire();
    //����
    public virtual void Wound(TankBase otherTank)
    {
        int realAtk = otherTank.atk - this.def;
        this.nowHp -= realAtk > 0 ? realAtk : 0;
        if(this.nowHp <= 0) 
        {
            //Ϊ�˷�ֹ�����Ѫ����ʾ��bug
            this.nowHp = 0;
            this.Dead();
        }
    }
    //����
    public virtual void Dead()
    {
        Destroy(this.gameObject);
        //�����������λ���ϴ���һ����������Ч
        //��ЧҲ���Կ�����һ��Ԥ���� ������Unity�ṩ��������Ч����
        for(int i=0;i<deadEffect.Length;i++)
        {
            GameObject eff = GameObject.Instantiate(deadEffect[i], transform.position, transform.rotation);
            //��ը��Ч����Ч��С�Լ��Ƿ���Ҫ������Ϸ�����е���Ч������
            AudioSource audioSource = eff.GetComponent<AudioSource>();
            if(audioSource != null)
            {
                audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
                audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            }
        }
    }

}
