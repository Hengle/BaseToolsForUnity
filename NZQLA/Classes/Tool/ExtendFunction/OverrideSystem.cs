using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// 用于扩展系统的类

namespace NZQLA
{
    /// <summary>安全获取的 Dictionary</summary>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    public class SafeDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {

        /// <summary>安全的索引器 get失败,返回默认值；set失败,自动添加</summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public new TValue this[TKey key]
        {
            get
            {
                try
                {
                    return base[key];
                }
                catch (Exception)
                {
                    return default(TValue);
                }
            }

            set
            {
                try
                {
                    base[key] = value;
                }
                catch (Exception)
                {
                    base.Add(key, value);
                }

            }
        }
    }



    /// <summary>自动添加元素的Dictionary</summary>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    public class AutoNewDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TValue:new()
    {

        /// <summary>安全的索引器 get失败,返回自动New()并添加；set失败,自动添加</summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public new TValue this[TKey key]
        {
            get
            {
                try
                {
                    return base[key];
                }
                catch (Exception)
                {
                    this.Add(key,new TValue());
                    return base[key];
                }
            }

            set
            {
                try
                {
                    base[key] = value;
                }
                catch (Exception)
                {
                    base.Add(key, value);
                }

            }
        }
    }






}
