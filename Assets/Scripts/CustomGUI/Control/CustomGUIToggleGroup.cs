using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUIToggleGroup : MonoBehaviour
{
    public CustomGUIToggle[] toggles;

    //记录上一次为true的toggle
    private CustomGUIToggle frontTurTog;
    private void Start()
    {
        if (toggles.Length == 0)
            return;

        //通过遍历来为每个Toggle添加监听事件函数
        for(int i = 0; i < toggles.Length; i++)
        {
            CustomGUIToggle toggle = toggles[i];
            toggle.changeValue += (value) =>
            {
                //当传入的value是true时 要把另外两个变成false
                if (value)
                {
                    for (int j = 0; j < toggles.Length; j++)
                    {
                        //这里有闭包 toggle是上一个函数中生命的变量
                        //这里改变了它的生命周期
                        if (toggles[j] != toggle)
                        {
                            toggles[j].isSel = false;
                        }
                    }
                    //记录上一次为true的toggle
                    frontTurTog = toggle;
                }
                //当传入的值是false 此时要判断 
                //当前变成false的这个toggle是不是上一次为true的
                //如果是 就不应该让他变成false
                else if (toggle == frontTurTog) 
                {
                    //强制改成true
                    toggle.isSel = true;
                }
            };
        }
    }
}
