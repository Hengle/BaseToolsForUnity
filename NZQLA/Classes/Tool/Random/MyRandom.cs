using System;

namespace NZQLA
{
    /// <summary>
    /// 随机生成器
    /// </summary>
    public class MyRandom : SingtonAuto<MyRandom>
    {
        #region 线性同余参数

        //线性同余随机数生成算法
        private const int PrimeA = 214013;
        private const int PrimeB = 2531011;

        #endregion

        //归一化
        private const float Mask15Bit_1 = 1.0f / 0x7fff;
        private const int Mask15Bit = 0x7fff;

        private int m_Value = 0;

        /// <summary>随机数种子 可以确保在同一种子下随机得到的结果一样</summary>
        public int Seed
        {
            set { m_Value = value; }
            get { return m_Value; }
        }

        /// <summary>
        /// 采用线性同余算法产生一个0~1之间的随机小数
        /// </summary>
        /// <returns></returns>
        public float RndIn01()
        {
            float val = ((((m_Value = m_Value * PrimeA + PrimeB) >> 16) & Mask15Bit) - 1) * Mask15Bit_1;
            return (val > 0.99999f ? 0.99999f : val);
        }


        /// <summary>随即一个指定范围的Float</summary>
        /// <param name="fRange"></param>
        /// <returns></returns>
        public float RandAround(float fRange)
        {
            return Range(-Math.Abs(fRange), Math.Abs(fRange));
        }

        /// <summary>随即一个指定范围的Float</summary>
        /// <param name="Range"></param>
        /// <returns></returns>
        public int RandAround(int Range)
        {
            return this.Range(-Math.Abs(Range), Math.Abs(Range));
        }

        /// <summary>随即一个指定范围的Float</summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public float Range(float min, float max)
        {
            return min + RndIn01() * (max - min);
        }


        /// <summary>随即一个指定范围的int</summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Range(int min, int max)
        {
            return (int)(min + RndIn01() * (max - min));
        }


        /// <summary>随意几个小于指定值的float(无符号)</summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public float RangeUnsignFloat(float max)
        {
            return Range(0, max);
        }

        /// <summary>随意几个小于指定值的int(无符号)</summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public int RangeUnsignInt(int max)
        {
            return Range(0, max);
        }



    }

}
