using UnityEngine;
using System.Collections;

namespace NZQLA
{
    /// <summary>用于处理[GameObject]周边</summary>
    public static class GameObjectHelper
    {
        /// <summary>查找指定名称的GameObject</summary>
        /// <param name="strName">指定名称</param>
        /// <returns></returns>
        public static GameObject FindObjByName(string strName)
        {
            if (string.IsNullOrEmpty(strName))
                return null;

            return GameObject.Find(strName);
        }


        /// <summary>确保对象挂载有指定组件</summary>
        /// <typeparam name="T">指定组件类型</typeparam>
        /// <param name="obj">指定对象</param>
        /// <returns></returns>
        public static T EnsureHasComponent<T>(GameObject obj) where T : Component
        {
            if (null == obj)
                return null;

            T com = obj.GetComponent<T>();

            if (null == com)
                com = obj.AddComponent<T>();

            return com;
        }


        /// <summary>查找指定对象的子物体</summary>
        /// <param name="self"></param>
        /// <param name="strChildName">指定子物体的名称</param>
        /// <param name="OnlyFindInTopChildren">只在第一层的子物体内查找</param>
        /// <returns></returns>
        public static Transform FindChildOnSelf(this Transform self, string strChildName, bool OnlyFindInTopChildren = true)
        {
            if (self == null || self.childCount == 0 || string.IsNullOrEmpty(strChildName))
                return null;

            for (int i = 0; i < self.childCount; i++)
            {
                Transform temp = self.GetChild(i);
                if (temp.name.Equals(strChildName))
                {
                    return temp;
                }
            }

            if (!OnlyFindInTopChildren)
            {
                for (int i = 0; i < self.childCount; i++)
                {
                    Transform temp = self.GetChild(i).FindChildOnSelf(strChildName, false);
                    if (temp != null)
                        return temp;
                }
            }
            return null;
        }


    }
}
