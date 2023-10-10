using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIButton : CustomGUIControl
{
    //�ṩ���ⲿ ������Ӧ��ť������¼�
    public event UnityAction clickEvent;

    protected override void StyleOffDraw()
    {
        if(GUI.Button(guiPos.RPos, content))
        {
            clickEvent?.Invoke();
        }
    }

    protected override void StyleOnDraw()
    {
        if (GUI.Button(guiPos.RPos, content, style)) 
        {
            clickEvent?.Invoke();
        }
    }
}
