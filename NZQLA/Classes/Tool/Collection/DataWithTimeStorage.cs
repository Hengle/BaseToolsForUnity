using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NZQLA;
using System.Xml.Serialization;

/// <summary>
/// 用于管理带有时间戳的数据
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
[XmlType]
public class DataWithTimeStorage<T>
{
    //数据
    [XmlElement]
    private List<DataWithTime<T>> data = new List<DataWithTime<T>>();

    //时间戳的范围边界
    [XmlIgnore]
    [NonSerialized]
    private Range<DateTime> TimeBorder;

    //时间戳的范围边界
    [XmlIgnore]
    [NonSerialized]
    private Range<int> IndexBorder;

    private int IndexCur;

    /// <summary>完成时间回调</summary>
    private event Action FinishHandler;


    /// <summary>寻找指定时间的数据索引</summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public int FindData(DateTime time)
    {
        if (data.isNull())
            return -1;

        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].time == time)
                return i;
        }

        return -1;
    }


    public void SetRange(DateTime from, DateTime to)
    {
        if (from > to)
        {
            var temp = from;
            from = to;
            to = temp;
        }

        var indexFrom = FindData(from);
        if (indexFrom == -1)
            return;

        var indexTo = FindData(to);
        if (indexTo == -1)
            indexTo = data.Count - 1;

        IndexBorder = new Range<int>(indexFrom, indexTo, CompareBase.ComparsionInt);
    }

    public void SetRange()
    {
        IndexBorder = new Range<int>(0, data.Count - 1, CompareBase.ComparsionInt);
    }

    public void Ready()
    {
        IndexCur = 0;
    }


    public void Reset()
    {
        IndexCur = 0;
    }

    /// <summary>添加一个数据</summary>
    /// <param name="item"></param>
    /// <param name="time"></param>
    public void AddData(T item, DateTime time)
    {
        if (data == null)
            data = new List<DataWithTime<T>>();
        data.Add(new DataWithTime<T>(item, time));
    }

    /// <summary>添加一个数据</summary>
    /// <param name="item"></param>
    public void AddData(T item)
    {
        if (data == null)
            data = new List<DataWithTime<T>>();
        data.Add(new DataWithTime<T>(item, DateTime.Now));
    }

    /// <summary>获取指定索引的数据</summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public DataWithTime<T> GetDataAtIndex(int index)
    {
        if (data.isIndexLawful(index))
        {
            IndexCur = index;
            if (IndexCur == data.Count - 1)
            {
                if (FinishHandler != null)
                    FinishHandler();
            }
            return data[IndexCur];
        }
        return null;
    }

    /// <summary>获取指定时间戳的数据</summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public DataWithTime<T> GetDataAtTime(DateTime time)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].time == time)
                return data[i];
        }

        return null;
    }

    public DataWithTime<T> GetDataNext()
    {
        return GetDataAtIndex(IndexCur + 1);
    }




    /// <summary>将数据按照时间戳的方式进行排序</summary>
    public void Sort()
    {
        if (data != null)
            data.Sort(new DataWithTime<T>());
    }

    /// <summary>刷新边界</summary>
    public void RefreshBorder()
    {
        if (data.isNull() || data.Count < 2)
            return;

        if (TimeBorder == null)
            TimeBorder = new Range<DateTime>(data[0].time, data[data.Count].time, DataWithTime<T>.DateTimeComparsion);
        else
        {
            TimeBorder.min = data[0].time;
            TimeBorder.max = data[data.Count].time;
        }
    }




    public void Serial(string strPath)
    {
        SerializeHelper.SerialDataToFile<List<DataWithTime<T>>>(data, strPath);
    }

    public void DeSerial(string strPath)
    {
        data = SerializeHelper.DeSerialDataFromFile<List<DataWithTime<T>>>(strPath);
    }


    public void AddFinishListener(Action a)
    {
        if (a != null)
            FinishHandler += a;
    }

    public void RemoveFinishListener(Action a)
    {
        if (a != null)
            FinishHandler -= a;
    }


}



/// <summary>
/// 拥有时间戳的数据
/// </summary>
/// <typeparam name="T"></typeparam>
[XmlType]
[Serializable]
public class DataWithTime<T> : IComparer<DataWithTime<T>>
{
    [XmlElement]
    public DateTime time;

    [XmlElement]
    public T data;

    public DataWithTime() { }

    public DataWithTime(T data)
    {
        this.time = DateTime.Now;
        this.data = data;
    }


    public DataWithTime(T data, DateTime time)
    {
        this.time = time;
        this.data = data;
    }

    public int Compare(DataWithTime<T> x, DataWithTime<T> y)
    {
        if (x.time > y.time)
            return 1;
        else if (x.time < y.time)
            return -1;
        return 0;
    }

    public override string ToString()
    {
        return string.Format("{0}{1}", time.ToString(), data.ToString());
    }

    public static int DateTimeComparsion(DateTime a, DateTime b)
    {
        return a > b ? 1 : (a < b ? -1 : 0);
    }
}