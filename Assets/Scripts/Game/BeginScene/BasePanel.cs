using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel<T> : MonoBehaviour where T:class
{
    #region ����ģʽ����Ԫ��
    private static T instance;

    public static T Instance => instance;

    protected virtual void Awake()
    {
        //��Ϊ�Ǽ̳�Mono���� ����instance����new ֻ����awake����г�ʼ��
        //����������ֱ��=this ��Ϊthis��BasePanel<T>������� ���Ի�Ҫ��as��T
        //���ܰ���as��T��ǰ�� ��T������һ���� �����ǽṹ�� ����Ҫ�ټ���T�ķ���Լ��
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
