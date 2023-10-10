using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

/// <summary>
/// PlayerPrefs���ݹ����� ͳһ�������ݵĴ洢�Ͷ�ȡ
/// </summary>
public class PlayerPrefsDataMgr
{
    #region ����ģʽ����Ԫ��
    private static PlayerPrefsDataMgr instance = new PlayerPrefsDataMgr();

    public static PlayerPrefsDataMgr Instance
    {
        get => instance;
    }

    private PlayerPrefsDataMgr() { }
    #endregion

    /// <summary>
    /// �洢����
    /// </summary>
    /// <param name="data">���ݶ���</param>
    /// <param name="keyName">���ݶ����Ψһkey �Լ�����</param>
    public void SaveData(object data, string keyName)
    {
        //ͨ��Type�õ��������ݶ���������ֶ�
        //Ȼ����PlayerPrefs���д洢
        Type type = data.GetType();
        //�õ������ֶ�
        FieldInfo[] infos = type.GetFields();

        //�Լ�����һ��key�Ĺ��� �������ݴ洢
        //keyName_��������_�ֶ�����_�ֶ���
        string saveKeyName = "";
        FieldInfo info;
        //������Щ�ֶ� �������ݴ洢
        for (int i = 0; i < infos.Length; i++) 
        {
            //��ÿһ���ֶν������ݴ洢
            //�õ�������ֶ���Ϣ
            info = infos[i];
            //�ֶ����� info.FieldType.Name
            //�ֶ��� info.Name
            //�������Ƕ��Ĺ����key����ƴ��
            saveKeyName = keyName + "_" + type.Name + "_"
                + info.FieldType.Name + "_" + info.Name;

            //����key���� ֵ����info.GetValue(data)
            //����������ͨ��PlayerPrefs��ÿ��ֵ
            SaveValue(info.GetValue(data), saveKeyName);
        }

        PlayerPrefs.Save();
    }

    //�洢��������
    private void SaveValue(object value, string keyName)
    {
        //��ô�����������ˣ��Ҳ�֪��ֵvalue������ ��TM���ĸ�API�棿��
        Type fieldType = value.GetType();
        //�����ж�
        #region ������������
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

        #region List����
        //��Ŀǰ��֪ �̳���IList���඼��List������
        //�������Ǿ��жϴ������������ǲ���IList������
        else if(typeof(IList).IsAssignableFrom(fieldType))
        {
            //����װ����
            IList list = value as IList;
            //�ȴ洢����
            PlayerPrefs.SetInt(keyName + "_Num", list.Count);

            for (int i = 0; i < list.Count; i++) 
            {
                //�ٴ洢�����ֵ
                //��Ϊ��֪�������ֵ��ʲô���͵�
                //���Ըպÿ������õݹ�����ֵ�����ͽ����жϲ��洢
                SaveValue(list[i], keyName + "_" + i);
            }
        }
        #endregion

        #region Dictionary����
        //��List���͵�˼·һ��
        else if(typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            //����װ����
            IDictionary dict = value as IDictionary;
            //�ȴ�����
            PlayerPrefs.SetInt(keyName + "_Num", dict.Count);

            //�������ֵı�ʶ
            int index = 0;
            foreach (object item in dict.Keys)
            {
                //��Ϊ�ֵ�����������ֵ �����ȴ�key�ٴ�value
                SaveValue(item, keyName + "_key_" + index);
                SaveValue(dict[item], keyName + "_value_" + index);
                index++;
            }
        }
        #endregion

        #region �Զ�������
        //�����������Ͷ����� �����Զ�������
        //һЩ�����õĻ�����������ûдqwq
        else
        {
            //��Ȼ��ʱ��value��һ���Զ������͵�
            //����TMֱ���ٵ���һ��SaveData�������ˣ�
            SaveData(value, keyName);
        }
        #endregion
    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    /// <param name="type">��Ҫ��ȡ���ݵ���������</param>
    /// <param name="keyName">���ݶ����Ψһkey �Լ�����</param>
    /// <returns></returns>
    //����object������ ��ʹ��Type���� ��ҪĿ���ǽ�Լһ�д��루���ⲿ��
    public object LoadData(Type type, string keyName)
    {
        //���ݴ�������ͺ�keyName
        //���ݴ洢����ʱkey��ƴ�ӹ��� ���������ݵĻ�ȡ��ֵ ���س�ȥ
        object obj = Activator.CreateInstance(type);
        //�õ������ֶ�
        FieldInfo[] infos = type.GetFields();
        string loadKeyName = "";
        FieldInfo info;
        for (int i = 0; i < infos.Length; i++) 
        {
            info = infos[i];
            loadKeyName = keyName + "_" + type.Name + "_"
                + info.FieldType.Name + "_" + info.Name;
            //������ݵ�obj��
            info.SetValue(obj, LoadValue(info.FieldType, loadKeyName));
        }
        return obj;
    }

    //��ȡ��������
    private object LoadValue(Type fieldType, string keyName)
    {
        #region ������������
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

        #region List����
        else if(typeof(IList).IsAssignableFrom(fieldType))
        {
            //�ȵó���
            int count = PlayerPrefs.GetInt(keyName + "_Num");
            //ʵ����һ��List���������и�ֵ
            IList list = Activator.CreateInstance(fieldType) as IList;
            for (int i = 0; i < count; i++) 
            {
                //ͨ��fieldType.GetGenericArguments()[0]�õ����Ͳ���
                list.Add(LoadValue(fieldType.GetGenericArguments()[0], keyName + "_" + i));
            }
            return list;
        }
        #endregion

        #region Dictionary����
        else if(typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            //�ȵó���
            int count = PlayerPrefs.GetInt(keyName + "_Num");
            //ʵ����һ��Diction���������и�ֵ
            IDictionary dict = Activator.CreateInstance(fieldType) as IDictionary;
            for (int i = 0; i < count; i++)
            {
                dict.Add(LoadValue(fieldType.GetGenericArguments()[0], keyName + "_key_" + i),
                    LoadValue(fieldType.GetGenericArguments()[1], keyName + "_value_" + i));
            }
            return dict;
        }
        #endregion

        #region �Զ�������
        else
        {
            return LoadData(fieldType, keyName);
        }
        #endregion
    }
}