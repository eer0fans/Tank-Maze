using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCursor : MonoBehaviour
{


    void Start()
    {
        //设置鼠标无法移除游戏界面
        Cursor.lockState = CursorLockMode.Confined;

    }
}
