using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReward : MonoBehaviour
{
    //可能随机到的武器种类
    public Weapon[] weapons;

    public GameObject pickEffect;
    private void OnTriggerEnter(Collider other)
    {
        

        if(other.tag =="Player")
        {
            //创造一个拾取特效
            GameObject eff = GameObject.Instantiate(pickEffect, transform.position, transform.rotation);
            //特效的音效大小以及是否开启要根据游戏设置中的音效来决定
            AudioSource audioSource = eff.GetComponent<AudioSource>();
            audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            //随机一个武器类型
            int index = Random.Range(0, weapons.Length);
            Player player = other.GetComponent<Player>();
            player.GetWeapon(weapons[index]);

            //删除自己
            Destroy(gameObject);
        }
    }

}
