using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum E_Reward_Type
{
    Hp,
    MaxHp,
    Speed,
}
public class OtherReward : MonoBehaviour
{
    //拾取特效
    public GameObject pickEffect;
    //奖励的类型
    public E_Reward_Type type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //创造一个拾取特效
            GameObject eff = GameObject.Instantiate(pickEffect, transform.position, transform.rotation);
            //特效的音效大小以及是否开启要根据游戏设置中的音效来决定
            AudioSource audioSource = eff.GetComponent<AudioSource>();
            audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            Player player = other.GetComponent<Player>();
            switch (type)
            {
                case E_Reward_Type.Hp:
                    player.nowHp += 15;
                    if (player.nowHp > player.maxHp)
                        player.nowHp = player.maxHp;
                    GamePanel.Instance.SetNowHp(player.maxHp, player.nowHp);
                    break;
                case E_Reward_Type.MaxHp:
                    player.maxHp += 15;
                    player.nowHp += 15;
                    GamePanel.Instance.SetMaxHp(player.maxHp, player.nowHp, player.firstMaxHp);
                    break;
                case E_Reward_Type.Speed:
                    player.moveSpeed += 50;
                    if(player.moveSpeed > player.maxSpeed)
                        player.moveSpeed = player.maxSpeed;
                    break;

            }

            //删除自己
            Destroy(gameObject);
        }

    }

}
