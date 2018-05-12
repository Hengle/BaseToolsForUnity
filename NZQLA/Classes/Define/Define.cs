using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;

//用于定义一些数据

public class Define
{


}








/// <summary>范围</summary>
/// <typeparam name="T"></typeparam>
public class Range<T>
{
    public T min;
    public T max;
    public Comparison<T> comparison;

    public Range() { }

    public Range(T min, T max, Comparison<T> comparison)
    {
        this.min = min;
        this.max = max;
        this.comparison = comparison;
    }


    public bool isInRange(T index)
    {
        return !(comparison(min, index) > 0 || comparison(max, index) < 0);
    }
}


public enum State
{
    UnStart,
    Working,
    Pause,
    Finish,
}