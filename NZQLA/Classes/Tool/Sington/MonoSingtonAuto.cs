﻿using UnityEngine;
using System;

namespace NZQLA
{
    /// <summary>
    /// MonoBehaviour单例 如果不存在将自动创建
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingtonAuto<T> : MonoBehaviour, IDisposable where T : MonoSingtonAuto<T>
    {

        private static readonly object objLock = new object();

        private static T Ins;


        /// <summary>获取实例</summary>
        public static T GetIns()
        {
            if (Ins == null)
            {
                if ((Ins = FindObjectOfType<T>()) == null)
                {
                    lock (objLock)
                    {
                        Ins = new GameObject(typeof(T).ToString()).AddComponent<T>();
                        Ins.InitOnCreateIns();
                    }
                }
            }
            return Ins;
        }

        /// <summary>释放</summary>
        public virtual void Dispose()
        {

        }


        /// <summary>在实例化的时候会执行的初始化方法</summary>
        public virtual void InitOnCreateIns(object arg = null)
        {

        }




        /// <summary>释放</summary>
        public virtual void DestorySelf()
        {
            DestroyObject(Ins);
        }

    }
}
