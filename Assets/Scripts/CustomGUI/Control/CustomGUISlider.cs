using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum E_Slider_Type
{
    Horizontal,
    Vertical,
}
public class CustomGUISlider : CustomGUIControl
{
    public float nowValue = 0;
    public float minValue = 0;
    public float maxValue = 1;

    public E_Slider_Type type = E_Slider_Type.Horizontal;
    //Ð¡°´Å¥µÄstyle
    public GUIStyle styleThumb;


    public event UnityAction<float> changeValue;

    private float oldValue = 0;
    protected override void StyleOffDraw()
    {
        switch (type)
        {
            case E_Slider_Type.Horizontal:
                nowValue = GUI.HorizontalSlider(guiPos.RPos, nowValue, minValue, maxValue);

                break;
            case E_Slider_Type.Vertical:
                nowValue = GUI.VerticalSlider(guiPos.RPos, nowValue, minValue, maxValue);
        
                break;
        }
        if(oldValue!=nowValue)
        {
            changeValue?.Invoke(nowValue);
            oldValue = nowValue;
        }
    }

    protected override void StyleOnDraw()
    {
        switch (type)
        {
            case E_Slider_Type.Horizontal:
                nowValue = GUI.HorizontalSlider(guiPos.RPos, nowValue, minValue, maxValue, style, styleThumb);

                break;
            case E_Slider_Type.Vertical:
                nowValue = GUI.VerticalSlider(guiPos.RPos, nowValue, minValue, maxValue, style, styleThumb);

                break;
        }
        if (oldValue != nowValue)
        {
            changeValue?.Invoke(nowValue);
            oldValue = nowValue;
        }

    }
}
