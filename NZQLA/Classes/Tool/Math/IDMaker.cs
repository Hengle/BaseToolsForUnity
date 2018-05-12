using System;

namespace NZQLA
{
    /// <summary>
    /// ID 生成器
    /// </summary>
    public class IDMaker
    {

        /// <summary>使用当前时间创建一个ID</summary>
        /// <returns></returns>
        public static string CreateIDByTime()
        {
            return string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);
        }


        /// <summary>使用当前时间+随机数字创建一个ID</summary>
        /// <param name="IDLength">数字的长度</param>
        /// <returns></returns>
        public static string CreateIDByTimeAndNum(int IDLength)
        {
            return string.Format("{0:yyyyMMddHHmmssfff}{1}", DateTime.Now, new Random().Next(0, (int)Math.Pow(10, IDLength - 17)));
        }

    }
}
