using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 50;
    //˭�������
    public TankBase father;
    //�ӵ������������٣���Ч
    public GameObject deadEffect;
    //����ӵ����
    public void SetFather(TankBase father)
    {
        this.father = father;
    }
    //�Ƿ������������
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
            //�ӵ��ķ�������Player Ӧ�ÿ��Դ�cube ���� �͵ذ�
            if (other.tag == "cube" || other.tag == "Enemy" || other.tag == "Plane")
            {
                if (deadEffect != null)
                {
                    GameObject eff = GameObject.Instantiate(deadEffect, transform.position, transform.rotation);
                    //��ը��Ч����Ч��С�Լ��Ƿ���Ҫ������Ϸ�����е���Ч������
                    AudioSource audioSource = eff.GetComponent<AudioSource>();
                    audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
                    audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
                }

                Destroy(this.gameObject);
            }
        }
        else if (father.tag == "Enemy") 
        {
            //�ӵ��ķ������ǵ��� Ӧ�ÿ��Դ�cube ��� �͵ذ�
            if (other.tag == "cube" || other.tag == "Player" || other.tag == "Plane")
            {
                if (deadEffect != null)
                {
                    GameObject eff = GameObject.Instantiate(deadEffect, transform.position, transform.rotation);
                    //��ը��Ч����Ч��С�Լ��Ƿ���Ҫ������Ϸ�����е���Ч������
                    AudioSource audioSource = eff.GetComponent<AudioSource>();
                    audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
                    audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
                }

                Destroy(this.gameObject);
            }
        }

    }

}
