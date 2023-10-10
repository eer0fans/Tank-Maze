using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum E_Style_Onoff
{
    On,
    Off,
}

public abstract class CustomGUIControl : MonoBehaviour
{
    //位置信息
    public CustomGUIPos guiPos = new CustomGUIPos();
    //内容信息
    public GUIContent content;
    //自定义样式
    public GUIStyle style;
    //是否开启自定义样式
    public E_Style_Onoff styleOnOrOff = E_Style_Onoff.Off;

    //提供给外部绘制GUI控件的方法
    public void DrawGUI()
    {
        switch (styleOnOrOff)
        {
            case E_Style_Onoff.On:
                StyleOnDraw();
                break;
            case E_Style_Onoff.Off:
                StyleOffDraw();
                break;
        }
    }

    protected abstract void StyleOnDraw();

    protected abstract void StyleOffDraw();
}
