using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 用于保存一些常用的数据类型的大小比较
/// </summary>
public class CompareBase
{


    public static int ComparsionInt(float a, float b)
    {
        return a > b ? 1 : (a < b ? -1 : 0);
    }


    public static int ComparsionInt(int a, int b)
    {
        return a > b ? 1 : (a < b ? -1 : 0);
    }


    public static int ComparsionDateTime(DateTime a, DateTime b)
    {
        return a > b ? 1 : (a < b ? -1 : 0);
    }

}
