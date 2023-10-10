using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

/// <summary>
/// PlayerPrefs数据管理类 统一管理数据的存储和读取
/// </summary>
public class PlayerPrefsDataMgr
{
    #region 单例模式基本元素
    private static PlayerPrefsDataMgr instance = new PlayerPrefsDataMgr();

    public static PlayerPrefsDataMgr Instance
    {
        get => instance;
    }

    private PlayerPrefsDataMgr() { }
    #endregion

    /// <summary>
    /// 存储数据
    /// </summary>
    /// <param name="data">数据对象</param>
    /// <param name="keyName">数据对象的唯一key 自己控制</param>
    public void SaveData(object data, string keyName)
    {
        //通过Type得到传入数据对象的所有字段
        //然后结合PlayerPrefs进行存储
        Type type = data.GetType();
        //得到所有字段
        FieldInfo[] infos = type.GetFields();

        //自己定义一个key的规则 进行数据存储
        //keyName_数据类型_字段类型_字段名
        string saveKeyName = "";
        FieldInfo info;
        //遍历这些字段 进行数据存储
        for (int i = 0; i < infos.Length; i++) 
        {
            //对每一个字段进行数据存储
            //得到具体的字段信息
            info = infos[i];
            //字段类型 info.FieldType.Name
            //字段名 info.Name
            //按照我们定的规则对key进行拼接
            saveKeyName = keyName + "_" + type.Name + "_"
                + info.FieldType.Name + "_" + info.Name;

            //现在key有了 值就是info.GetValue(data)
            //接下来就是通过PlayerPrefs存每个值
            SaveValue(info.GetValue(data), saveKeyName);
        }

        PlayerPrefs.Save();
    }

    //存储单个数据
    private void SaveValue(object value, string keyName)
    {
        //那么现在问题来了：我不知道值value的类型 我TM拿哪个API存？？
        Type fieldType = value.GetType();
        //类型判断
        #region 基础数据类型
        if (fieldType == typeof(int))
        {
            PlayerPrefs.SetInt(keyName, (int)value);
        }
        else if (fieldType == typeof(float))
        {
            PlayerPrefs.SetFloat(keyName, (float)value);
        }
        else if (fieldType == typeof(string))
        {
            PlayerPrefs.SetString(keyName, value.ToString());
        }
        else if (fieldType == typeof(bool))
        {
            PlayerPrefs.SetInt(keyName, (bool)value ? 1 : 0);
        }
        #endregion

        #region List类型
        //就目前所知 继承了IList的类都是List泛型类
        //所以我们就判断传进来的类型是不是IList的子类
        else if(typeof(IList).IsAssignableFrom(fieldType))
        {
            //父类装子类
            IList list = value as IList;
            //先存储数量
            PlayerPrefs.SetInt(keyName + "_Num", list.Count);

            for (int i = 0; i < list.Count; i++) 
            {
                //再存储具体的值
                //因为不知道具体的值是什么类型的
                //所以刚好可以利用递归来对值的类型进行判断并存储
                SaveValue(list[i], keyName + "_" + i);
            }
        }
        #endregion

        #region Dictionary类型
        //和List类型的思路一样
        else if(typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            //父类装子类
            IDictionary dict = value as IDictionary;
            //先存数量
            PlayerPrefs.SetInt(keyName + "_Num", dict.Count);

            //用于区分的标识
            int index = 0;
            foreach (object item in dict.Keys)
            {
                //因为字典里面有两个值 所以先存key再存value
                SaveValue(item, keyName + "_key_" + index);
                SaveValue(dict[item], keyName + "_value_" + index);
                index++;
            }
        }
        #endregion

        #region 自定义类型
        //以上数据类型都不是 那是自定义类型
        //一些不常用的基本数据类型没写qwq
        else
        {
            //既然此时的value是一个自定义类型的
            //那我TM直接再调用一次SaveData不就行了？
            SaveData(value, keyName);
        }
        #endregion
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="type">想要读取数据的数据类型</param>
    /// <param name="keyName">数据对象的唯一key 自己控制</param>
    /// <returns></returns>
    //不用object对象传入 而使用Type传入 主要目的是节约一行代码（在外部）
    public object LoadData(Type type, string keyName)
    {
        //根据传入的类型和keyName
        //依据存储数据时key的拼接规则 来进行数据的获取赋值 返回出去
        object obj = Activator.CreateInstance(type);
        //得到所有字段
        FieldInfo[] infos = type.GetFields();
        string loadKeyName = "";
        FieldInfo info;
        for (int i = 0; i < infos.Length; i++) 
        {
            info = infos[i];
            loadKeyName = keyName + "_" + type.Name + "_"
                + info.FieldType.Name + "_" + info.Name;
            //填充数据到obj中
            info.SetValue(obj, LoadValue(info.FieldType, loadKeyName));
        }
        return obj;
    }

    //读取单个数据
    private object LoadValue(Type fieldType, string keyName)
    {
        #region 基础数据类型
        if (fieldType == typeof(int))
        {
            return PlayerPrefs.GetInt(keyName);
        }
        else if (fieldType == typeof(float))
        {
            return PlayerPrefs.GetFloat(keyName);
        }
        else if (fieldType == typeof(string))
        {
            return PlayerPrefs.GetString(keyName);
        }
        else if (fieldType == typeof(bool))
        {
            return PlayerPrefs.GetInt(keyName) == 1 ? true : false;
        }
        #endregion

        #region List类型
        else if(typeof(IList).IsAssignableFrom(fieldType))
        {
            //先得长度
            int count = PlayerPrefs.GetInt(keyName + "_Num");
            //实例化一个List对象来进行赋值
            IList list = Activator.CreateInstance(fieldType) as IList;
            for (int i = 0; i < count; i++) 
            {
                //通过fieldType.GetGenericArguments()[0]得到泛型参数
                list.Add(LoadValue(fieldType.GetGenericArguments()[0], keyName + "_" + i));
            }
            return list;
        }
        #endregion

        #region Dictionary类型
        else if(typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            //先得长度
            int count = PlayerPrefs.GetInt(keyName + "_Num");
            //实例化一个Diction对象来进行赋值
            IDictionary dict = Activator.CreateInstance(fieldType) as IDictionary;
            for (int i = 0; i < count; i++)
            {
                dict.Add(LoadValue(fieldType.GetGenericArguments()[0], keyName + "_key_" + i),
                    LoadValue(fieldType.GetGenericArguments()[1], keyName + "_value_" + i));
            }
            return dict;
        }
        #endregion

        #region 自定义类型
        else
        {
            return LoadData(fieldType, keyName);
        }
        #endregion
    }
}