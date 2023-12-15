using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipFPS : MonoBehaviour
{

    //Э��id
    private int id;
    
    //Э���ֵ�
    Dictionary<int,Coroutine> dic=new Dictionary<int, Coroutine> ();


    /// <summary>
    /// ��ӷ�֡
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="num">һ֡���ص�����</param>
    /// <param name="action">������ʲô</param>
    public void AddFPS<T>(List<T> list, int num, Action<T> action)
    {
        int name=id++;
        Coroutine coroutine = StartCoroutine(GetFromInfoList(name,list,num,action));
        dic.Add(name,coroutine);
    }

    /// <summary>
    /// ��ʼ��֡����
    /// </summary>
    /// <param name="name">Э������</param>
    /// <param name="list"></param>
    /// <param name="num">һ֡���ص�����</param>
    /// <param name="action">������ʲô</param>
    IEnumerator GetFromInfoList<T>(int name, List<T> list, int num, Action<T> action)
    {
        int InfoListIndex = 0;
        foreach (T item in list)
        {
            InfoListIndex++;
            action(item);
            if (InfoListIndex % num==0)
            {
                yield return null;//�ȴ�һ֡
            }
        }
        StopFPS(name);
    }


    /// <summary>
    /// ��ӷ�֡
    /// </summary>
    /// <param name="max">��֡���ص�������</param>
    /// <param name="num">һ֡���ص�����</param>
    /// <param name="action">������ʲô</param>
    public void AddFPS(int max,int num,Action<int> action)
    {
        int name=id++ ;
        Coroutine coroutine = StartCoroutine(GetFromInfoList(name, max, num, action));
        dic.Add(name, coroutine);
    }


    /// <summary>
    /// ��ʼ��֡����
    /// </summary>
    /// <param name="name">Э������</param>
    /// <param name="max">��֡���ص�������</param>
    /// <param name="num">һ֡���ص�����</param>
    /// <param name="action">������ʲô</param>
    IEnumerator GetFromInfoList(int name, int max, int num, Action<int> action)
    {
        for (int i = 0; i < max; i++)
        {
            action(i);
            if (i % num==0)
            {
                yield return null;
            }
        }
        StopFPS(name);
    }


    /// <summary>
    /// �Ƴ�Э��
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void StopFPS(int name)
    {
        if (dic.ContainsKey(name))
        {
            StopCoroutine(dic[name]);
            dic.Remove(name);   
        }
    }

}
