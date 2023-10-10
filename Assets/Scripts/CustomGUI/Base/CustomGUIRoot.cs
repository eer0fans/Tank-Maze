using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{
    //���ڴ洢�Ӷ�������GUI�ؼ�������
    private CustomGUIControl[] allControls;


    private void Start()
    {
        allControls = this.GetComponentsInChildren<CustomGUIControl>();
    }


    //ͳһ�����Ӷ���ؼ�������
    private void OnGUI()
    {
        //ÿһ�λ���ǰ �õ������Ӷ���ؼ��ĸ���ű�
        //�������˷����� ���Ը�Ϊֻ���ڱ༭״̬�²Ż�һֱִ��
        //if (!Application.isPlaying)
        //{
            allControls = this.GetComponentsInChildren<CustomGUIControl>();
        //}
        //����ÿһ���ؼ� ����ִ�л���
        for (int i = 0; i < allControls.Length; i++)
        {
            allControls[i].DrawGUI();
        }
    }
}
