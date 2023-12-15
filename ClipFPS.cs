using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipFPS : MonoBehaviour
{

    //协程id
    private int id;
    
    //协程字典
    Dictionary<int,Coroutine> dic=new Dictionary<int, Coroutine> ();


    /// <summary>
    /// 添加分帧
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="num">一帧加载的数量</param>
    /// <param name="action">具体做什么</param>
    public void AddFPS<T>(List<T> list, int num, Action<T> action)
    {
        int name=id++;
        Coroutine coroutine = StartCoroutine(GetFromInfoList(name,list,num,action));
        dic.Add(name,coroutine);
    }

    /// <summary>
    /// 开始分帧加载
    /// </summary>
    /// <param name="name">协程名称</param>
    /// <param name="list"></param>
    /// <param name="num">一帧加载的数量</param>
    /// <param name="action">具体做什么</param>
    IEnumerator GetFromInfoList<T>(int name, List<T> list, int num, Action<T> action)
    {
        int InfoListIndex = 0;
        foreach (T item in list)
        {
            InfoListIndex++;
            action(item);
            if (InfoListIndex % num==0)
            {
                yield return null;//等待一帧
            }
        }
        StopFPS(name);
    }


    /// <summary>
    /// 添加分帧
    /// </summary>
    /// <param name="max">分帧加载的总数量</param>
    /// <param name="num">一帧加载的数量</param>
    /// <param name="action">具体做什么</param>
    public void AddFPS(int max,int num,Action<int> action)
    {
        int name=id++ ;
        Coroutine coroutine = StartCoroutine(GetFromInfoList(name, max, num, action));
        dic.Add(name, coroutine);
    }


    /// <summary>
    /// 开始分帧加载
    /// </summary>
    /// <param name="name">协程名称</param>
    /// <param name="max">分帧加载的总数量</param>
    /// <param name="num">一帧加载的数量</param>
    /// <param name="action">具体做什么</param>
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
    /// 移除协程
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
