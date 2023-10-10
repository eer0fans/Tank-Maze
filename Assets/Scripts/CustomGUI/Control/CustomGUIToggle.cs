using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIToggle : CustomGUIControl
{
    public bool isSel = false;
    private bool OldIsSel = false;
    public event UnityAction<bool> changeValue = null;
    protected override void StyleOffDraw()
    {
        isSel = GUI.Toggle(guiPos.RPos, isSel, content);
        //ֻ�б仯ʱ��ִ��changeValue��ĺ���
        if (OldIsSel != isSel) 
        {
            changeValue?.Invoke(isSel);
            OldIsSel = isSel;
        }
    }

    protected override void StyleOnDraw()
    {
        isSel = GUI.Toggle(guiPos.RPos, isSel, content, style);
        if (OldIsSel != isSel)
        {
            changeValue?.Invoke(isSel);
            OldIsSel = isSel;
        }
    }
}
