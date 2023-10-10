using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel<T> : MonoBehaviour where T:class
{
    #region 单例模式基本元素
    private static T instance;

    public static T Instance => instance;

    protected virtual void Awake()
    {
        //因为是继承Mono的类 所以instance不能new 只能在awake里进行初始化
        //而不能让其直接=this 因为this是BasePanel<T>这个基类 所以还要再as成T
        //而能把它as成T的前提 是T必须是一个类 不能是结构体 所以要再加上T的泛型约束
        instance = this as T;
    }
    #endregion

    public virtual void ShowMe()
    {
        gameObject.SetActive(true);
    }

    public virtual void HideMe() 
    {
        gameObject.SetActive(false);
    }
}
