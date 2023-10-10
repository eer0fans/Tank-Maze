using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUITexture : CustomGUIControl
{
    public Texture texture;
    public ScaleMode scaleMode = ScaleMode.StretchToFill;

    protected override void StyleOffDraw()
    {
        GUI.DrawTexture(guiPos.RPos, texture, scaleMode);
    }

    protected override void StyleOnDraw()
    {
        GUI.DrawTexture(guiPos.RPos, texture, scaleMode);
    }
}
