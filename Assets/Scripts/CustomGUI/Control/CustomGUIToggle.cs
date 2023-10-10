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
        //只有变化时才执行changeValue里的函数
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
