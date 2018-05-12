using UnityEngine;
using System.Collections;

namespace NZQLA
{
    /// <summary>
    /// 非自动的单例模板基类 需要手动在场景内挂载在GameObject上
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingtonUnAuto<T> : MonoBehaviour where T : MonoSingtonUnAuto<T>
    {

        private static T Ins;
        /// <summary>获取实例</summary>
        public static T GetIns()
        {
            if (Ins == null)
            {
                if ((Ins = FindObjectOfType<T>()) == null)
                {
                    if (MyApplication.IsEditorRuntime())
                    {
                        Log.LogAtUnityEditorError(string.Format("获取单例[{0}]失败", typeof(T).ToString()));
                    }
                }
            }
            return Ins;
        }



        /// <summary>MonoMethod: Awake()</summary>
        public virtual void Awake()
        {
            if (Ins == null)
            {
                Ins = this as T;
            }
            else if (Ins != this)
            {
                DestroyObject(this);
            }
        }

        /// <summary>基于</summary>
        /// <param name="obj"></param>
        public static void CreateInsAtObj(GameObject obj = null)
        {
            if (Ins != null)
                return;

            if (obj == null)
                return;

            Ins = obj.GetComponent<T>();
            if (Ins == null)
                Ins = obj.AddComponent<T>();
        }


        /// <summary>Release</summary>
        /// <param name="destoryGameobject">是否删除GameObject</param>
        public virtual void Release(bool destoryGameobject = false)
        {
            if (destoryGameobject)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }

    }
}
