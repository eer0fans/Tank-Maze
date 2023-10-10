using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUILabel : CustomGUIControl
{
    protected override void StyleOffDraw()
    {
        GUI.Label(guiPos.RPos, content);
    }

    protected override void StyleOnDraw()
    {
        GUI.Label(guiPos.RPos, content, style);
    }
}
