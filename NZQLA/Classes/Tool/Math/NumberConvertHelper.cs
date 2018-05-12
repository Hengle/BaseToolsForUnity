using UnityEngine;
using System.Collections.Generic;


namespace NZQLA
{
    /// <summary>
    /// 数字转换处理
    /// </summary>
    public class NumberConvertHelper : MonoBehaviour
    {

        #region 进制转换
        /// <summary>将十进制数值转换为2进制(递归)</summary>
        /// <param name="_value">指定十进制数值</param>
        /// <param name="listTo2">输出的List</param>
        public static void TranslateTo2(int _value, ref List<int> listTo2)
        {
            //原理：数值/2直到余数<2为止 同时收集余数
            _value = Mathf.Abs(_value);
            if (Mathf.Abs(_value) <= 1)
            {
                listTo2.Add(_value);
            }
            else
            {
                listTo2.Add(_value % 2);
                TranslateTo2(_value / 2, ref listTo2);
            }
        }


        /// <summary>将十进制数值转换为指定进制的可视化List(递归)</summary>
        /// <param name="_value">指定十进制数值</param>
        /// <param name="outList">输出的List</param>
        /// <param name="jinzhi">指定进制数 2 、 8、16 、60......</param>
        public static void Translate10ToN(int _value, ref List<int> outList, int jinzhi)
        {
            if (jinzhi == 0)
                return;

            //原理：数值/进制数 直到余数<进制数为止 同时收集余数
            _value = Mathf.Abs(_value);
            if (Mathf.Abs(_value) < jinzhi)
            {
                outList.Add(_value);
            }
            else
            {
                outList.Add(_value % jinzhi);
                Translate10ToN(_value / jinzhi, ref outList, jinzhi);
            }
        }

        #endregion


    }
}
