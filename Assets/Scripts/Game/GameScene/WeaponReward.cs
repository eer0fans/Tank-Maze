using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReward : MonoBehaviour
{
    //�������������������
    public Weapon[] weapons;

    public GameObject pickEffect;
    private void OnTriggerEnter(Collider other)
    {
        

        if(other.tag =="Player")
        {
            //����һ��ʰȡ��Ч
            GameObject eff = GameObject.Instantiate(pickEffect, transform.position, transform.rotation);
            //��Ч����Ч��С�Լ��Ƿ���Ҫ������Ϸ�����е���Ч������
            AudioSource audioSource = eff.GetComponent<AudioSource>();
            audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            //���һ����������
            int index = Random.Range(0, weapons.Length);
            Player player = other.GetComponent<Player>();
            player.GetWeapon(weapons[index]);

            //ɾ���Լ�
            Destroy(gameObject);
        }
    }

}
