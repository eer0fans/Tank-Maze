using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对齐方式枚举
/// </summary>
public enum E_Alignment_Type
{
    Up,
    Down,
    Left,
    Right,
    Center,
    Left_Up,
    Left_Down,
    Right_Up,
    Right_Down,
}


/// <summary>
/// 位置信息类
/// </summary>
[System.Serializable]
public class CustomGUIPos
{
    //最终位置
    private Rect rPos = new Rect();
    //控件相对屏幕位置
    private Vector2 screenPos = new Vector2();
    //控件中心点偏移位置
    private Vector2 centerPos = new Vector2();
    //屏幕九宫格对齐方式
    public E_Alignment_Type screen_Alignment_Type = E_Alignment_Type.Center;
    //控件中心对齐方式
    public E_Alignment_Type control_Center_Alignment_Type = E_Alignment_Type.Center;
    //偏移位置
    public Vector2 pos = new Vector2();
    //宽高
    public float width = 100;
    public float height = 50;

    public Rect RPos 
    {
        get 
        {
            //控件坐标计算公式：
            //最终位置 = 相对屏幕位置 + 中心点偏移位置 + 偏移位置
            CalcScreenPos();
            CalcCenterPos();
            rPos.x = screenPos.x + centerPos.x + pos.x;
            rPos.y = screenPos.y + centerPos.y + pos.y;
            //宽高
            rPos.width = width;
            rPos.height = height;
            return rPos; 
        }
    }

    //计算相对屏幕位置
    private void CalcScreenPos()
    {
        switch (screen_Alignment_Type)
        {
            case E_Alignment_Type.Up:
                screenPos.x = Screen.width / 2;
                screenPos.y = 0;
                break;
            case E_Alignment_Type.Down:
                screenPos.x = Screen.width / 2;
                screenPos.y = Screen.height;
                break;
            case E_Alignment_Type.Left:
                screenPos.x = 0;
                screenPos.y = Screen.height / 2;
                break;
            case E_Alignment_Type.Right:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height / 2;
                break;
            case E_Alignment_Type.Center:
                screenPos.x = Screen.width / 2;
                screenPos.y = Screen.height / 2;
                break;
            case E_Alignment_Type.Left_Up:
                screenPos.x = 0;
                screenPos.y = 0;
                break;
            case E_Alignment_Type.Left_Down:
                screenPos.x = 0;
                screenPos.y = Screen.height;
                break;
            case E_Alignment_Type.Right_Up:
                screenPos.x = Screen.width;
                screenPos.y = 0;
                break;
            case E_Alignment_Type.Right_Down:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height;
                break;
        }
    }

    //计算中心点偏移位置
    private void CalcCenterPos()
    {
        switch (control_Center_Alignment_Type)
        {
            case E_Alignment_Type.Up:
                centerPos.x = -width / 2;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Down:
                centerPos.x = -width / 2;
                centerPos.y = -height;
                break;
            case E_Alignment_Type.Left:
                centerPos.x = 0;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Right:
                centerPos.x = -width;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Center:
                centerPos.x = -width / 2;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Left_Up:
                centerPos.x = 0;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Left_Down:
                centerPos.x = 0;
                centerPos.y = -height;
                break;
            case E_Alignment_Type.Right_Up:
                centerPos.x = -width;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Right_Down:
                centerPos.x = -width;
                centerPos.y = -height;
                break;
        }
    }
}
