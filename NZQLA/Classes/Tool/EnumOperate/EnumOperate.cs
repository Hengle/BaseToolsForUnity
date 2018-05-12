using System.Collections;
using System;

namespace NZQLA
{
    /// <summary>
    /// 枚举操作
    /// </summary>
    public class EnumOperate
    {


        /// <summary>获取指定枚举对应的名称</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">枚举的值</param>
        /// <returns></returns>
        public static string GetEnumNameByValue<T>(T value)
        {
            if (!typeof(T).IsEnum)
                return null;

            var values = Enum.GetValues(typeof(T));
            return Enum.GetName(typeof(T), Array.BinarySearch(values, value));
        }




    }
}