using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 50;
    //谁发射的我
    public TankBase father;
    //子弹的死亡（销毁）特效
    public GameObject deadEffect;
    //设置拥有者
    public void SetFather(TankBase father)
    {
        this.father = father;
    }
    //是否随机攻击距离
    public bool isRandomSpeed = false;
    public void RandomSpeed()
    {
        moveSpeed = Random.RandomRange(3, 26);
    }

    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        //print(father.tag);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (father == null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        if (father.tag == "Player")
        {
            //子弹的发射者是Player 应该可以打到cube 敌人 和地板
            if (other.tag == "cube" || other.tag == "Enemy" || other.tag == "Plane")
            {
                if (deadEffect != null)
                {
                    GameObject eff = GameObject.Instantiate(deadEffect, transform.position, transform.rotation);
                    //爆炸特效的音效大小以及是否开启要根据游戏设置中的音效来决定
                    AudioSource audioSource = eff.GetComponent<AudioSource>();
                    audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
                    audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
                }

                Destroy(this.gameObject);
            }
        }
        else if (father.tag == "Enemy") 
        {
            //子弹的发射者是敌人 应该可以打到cube 玩家 和地板
            if (other.tag == "cube" || other.tag == "Player" || other.tag == "Plane")
            {
                if (deadEffect != null)
                {
                    GameObject eff = GameObject.Instantiate(deadEffect, transform.position, transform.rotation);
                    //爆炸特效的音效大小以及是否开启要根据游戏设置中的音效来决定
                    AudioSource audioSource = eff.GetComponent<AudioSource>();
                    audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
                    audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
                }

                Destroy(this.gameObject);
            }
        }

    }

}
