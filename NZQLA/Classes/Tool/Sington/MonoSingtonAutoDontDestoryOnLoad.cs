using UnityEngine;
using System;
using System.Collections;
namespace NZQLA
{
    /// <summary>
    /// 自动生成的跨场景Unity Mono单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingtonAutoDontDestoryOnLoad<T> : MonoBehaviour, IDisposable where T : MonoSingtonAutoDontDestoryOnLoad<T>
    {
        /// <summary>释放</summary>
        public virtual void Dispose()
        {

        }


        private static readonly object objLock = new object();

        private static T Ins;


        /// <summary>获取实例</summary>
        /// <returns></returns>
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


        /// <summary>MonoMethod: Awake()</summary>
        public virtual void Awake()
        {
            DontDestroyOnLoad(this);
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
