using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIInput : CustomGUIControl
{
    public string inputStr = "";
    public event UnityAction<string> strChange;

    private string oldStr = "";
    protected override void StyleOffDraw()
    {
        inputStr = GUI.TextField(guiPos.RPos, inputStr);
        if(oldStr != inputStr)
        {
            strChange?.Invoke(inputStr);
            oldStr = inputStr;
        }
    }

    protected override void StyleOnDraw()
    {
        inputStr = GUI.TextField(guiPos.RPos, inputStr, style);
        if (oldStr != inputStr)
        {
            strChange?.Invoke(inputStr);
            oldStr = inputStr;
        }
    }
}
