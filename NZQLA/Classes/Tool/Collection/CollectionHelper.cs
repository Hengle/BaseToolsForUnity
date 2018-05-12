using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


namespace NZQLA
{
    /// <summary>
    /// 提供一些关于Collection(T[]、List)方便使用的接口/外调方法
    /// </summary>
    public class CollectionHelper
    {
        #region Create
        /// <summary>创建一个指定长度的指定类型的一维数组，并使用指定值初始化所有元素</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="len"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T[] CreateArray1<T>(int len, T defaultValue)
        {
            T[] arr = new T[len];
            arr.Init(defaultValue);
            return arr;
        }


        /// <summary>创建一个指定尺寸的指定类型的二维数组，并使用指定值初始化所有元素</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="height">行的数量</param>
        /// <param name="width">行内元素数量</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T[,] CreateArray2<T>(int height, int width, T defaultValue)
        {
            T[,] arr = new T[height, width];
            arr.Init(defaultValue);
            return arr;
        }



        /// <summary>创建一个指定长度的指定类型的List，并使用指定值初始化所有元素</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">元素数量</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static List<T> CreateList<T>(int count, T defaultValue)
        {
            List<T> arr = new List<T>(count);
            for (int i = 0; i < arr.Capacity; i++)
            {
                arr.Add(defaultValue);
            }
            return arr;
        }


        #endregion

    }
}
