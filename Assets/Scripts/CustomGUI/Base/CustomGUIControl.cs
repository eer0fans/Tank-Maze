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
    //λ����Ϣ
    public CustomGUIPos guiPos = new CustomGUIPos();
    //������Ϣ
    public GUIContent content;
    //�Զ�����ʽ
    public GUIStyle style;
    //�Ƿ����Զ�����ʽ
    public E_Style_Onoff styleOnOrOff = E_Style_Onoff.Off;

    //�ṩ���ⲿ����GUI�ؼ��ķ���
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
