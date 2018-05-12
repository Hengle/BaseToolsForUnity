using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NZQLA
{
    /// <summary>
    /// 自己实现的协程
    /// </summary>
    public class IEnumtorHelper : MonoBehaviour
    {
        /// <summary></summary>
        public static bool MoveNext(IEnumerator subTask)
        {
            if (null == subTask)
                return false;

            //获取枚举器的当前值
            var child = subTask.Current;

            //判定有效性并等待
            if (child != null && child is IEnumerator && MoveNext(child as IEnumerator))
                return true;

            //处理Unity.WWW
            if (child is UnityEngine.WWW && !(child as UnityEngine.WWW).isDone)
                return false;

            if (subTask.MoveNext())
                return true;

            return false;

#if UNITY

#endif

        }






    }
}
