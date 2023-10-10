using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���뷽ʽö��
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
/// λ����Ϣ��
/// </summary>
[System.Serializable]
public class CustomGUIPos
{
    //����λ��
    private Rect rPos = new Rect();
    //�ؼ������Ļλ��
    private Vector2 screenPos = new Vector2();
    //�ؼ����ĵ�ƫ��λ��
    private Vector2 centerPos = new Vector2();
    //��Ļ�Ź�����뷽ʽ
    public E_Alignment_Type screen_Alignment_Type = E_Alignment_Type.Center;
    //�ؼ����Ķ��뷽ʽ
    public E_Alignment_Type control_Center_Alignment_Type = E_Alignment_Type.Center;
    //ƫ��λ��
    public Vector2 pos = new Vector2();
    //���
    public float width = 100;
    public float height = 50;

    public Rect RPos 
    {
        get 
        {
            //�ؼ�������㹫ʽ��
            //����λ�� = �����Ļλ�� + ���ĵ�ƫ��λ�� + ƫ��λ��
            CalcScreenPos();
            CalcCenterPos();
            rPos.x = screenPos.x + centerPos.x + pos.x;
            rPos.y = screenPos.y + centerPos.y + pos.y;
            //���
            rPos.width = width;
            rPos.height = height;
            return rPos; 
        }
    }

    //���������Ļλ��
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

    //�������ĵ�ƫ��λ��
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
