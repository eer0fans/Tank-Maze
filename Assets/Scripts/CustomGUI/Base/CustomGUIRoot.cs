using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{
    //用于存储子对象所有GUI控件的容器
    private CustomGUIControl[] allControls;


    private void Start()
    {
        allControls = this.GetComponentsInChildren<CustomGUIControl>();
    }


    //统一绘制子对象控件的内容
    private void OnGUI()
    {
        //每一次绘制前 得到所有子对象控件的父类脚本
        //这句代码浪费性能 可以改为只有在编辑状态下才会一直执行
        //if (!Application.isPlaying)
        //{
            allControls = this.GetComponentsInChildren<CustomGUIControl>();
        //}
        //遍历每一个控件 让其执行绘制
        for (int i = 0; i < allControls.Length; i++)
        {
            allControls[i].DrawGUI();
        }
    }
}
