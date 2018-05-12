using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace NZQLA
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtendFunctionCSharp
    {
        #region 词典

        /// <summary>获取词典中指定索引的元素</summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dic">词典</param>
        /// <param name="key">指定索引</param>
        /// <returns></returns>
        public static V GetItem<K, V>(this Dictionary<K, V> dic, K key)
        {
            if (dic.isNull() || !dic.ContainsKey(key))
                return default(V);

            return dic[key];
        }

        /// <summary>判定词典是否为空    元素数量为0视为空</summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static bool isNull<K, V>(this Dictionary<K, V> dic)
        {
            return dic == null || dic.Count == 0;
        }


        /// <summary>
        /// 为词典添加元素
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dic"></param>
        public static void AddItem<K, V>(this Dictionary<K, V> dic, K key, V value)
        {
            if (dic == null)
            {
                dic = new Dictionary<K, V>();
            }

            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key, value);
            }
        }


        /// <summary>
        /// 遍历词典 并将每一个元素作为参数 传入并执行指定Action
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dic"></param>
        /// <param name="action">指定Action</param>
        public static void ActionAtItem<K, V>(this Dictionary<K, V> dic, params Action<V>[] action)
        {
            if (dic.isNull<K, V>() || action.isNull<Action<V>>())
                return;

            foreach (K key in dic.Keys)
            {
                for (int i = 0; i < action.Length; i++)
                {
                    if (action[i] != null)
                    {
                        action[i](dic[key]);
                    }
                }
            }
        }


        /// <summary>由数组初始化本词典</summary>
        /// <typeparam name="K">类型：键</typeparam>
        /// <typeparam name="V">类型：值</typeparam>
        /// <param name="dic"></param>
        /// <param name="arr">资源数组</param>
        /// <param name="GetValueKeyFunc">用于获取数组元素键的委托</param>
        /// <param name="bCoverWhenHavSameKey">是否覆盖当有重复键的时候</param>
        /// <param name="bClearDicBeforeCreate">初始化时是否清空自身元素</param>
        public static void CreateFromArray<K, V>(this Dictionary<K, V> dic, V[] arr, Func<V, K> GetValueKeyFunc, bool bCoverWhenHavSameKey = false, bool bClearDicBeforeCreate = false)
        {
            //资源数组为空直接返回
            if (arr.isNull())
                return;

            //判空处理
            if (dic == null)
                dic = new Dictionary<K, V>();

            //原有元素处理
            if (dic.Count != 0)
                if (bClearDicBeforeCreate)
                    dic.Clear();

            for (int i = 0; i < arr.Length; i++)
            {
                //获取键
                K k = GetValueKeyFunc(arr[i]);

                //重复键处理
                if (!bCoverWhenHavSameKey)
                {
                    if (dic.ContainsKey(k))
                    {
                        continue;
                    }
                }

                //录入元素
                dic.AddItem<K, V>(k, arr[i]);
            }

        }



        /// <summary>
        /// 将List转为字符串
        /// </summary>
        ///<typeparam name="K"></typeparam>
        ///<typeparam name="V"></typeparam>
        /// <param name="dic">List</param>
        /// <param name="strIndexConnectValue">连接元素索引和元素值的字符串</param>
        /// <param name="strConnectItem">元素间的连接字符串</param>
        /// <returns></returns>
        public static string ToString<K, V>(this Dictionary<K, V> dic, string strIndexConnectValue = " = ", string strConnectItem = "\r\n")
        {
            if (dic.isNull())
                return null;


            string str = dic.GetType().ToString() + "\r\n";
            foreach (K key in dic.Keys)
            {

                str += "[" + key.ToString() + "]" + strIndexConnectValue + dic[key].ToString();
                str += strConnectItem;
            }

            return str;
        }

        #endregion


        #region 数组




        /// <summary>获取数组中指定索引的元素</summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="arr">数组</param>
        /// <param name="Index">指定索引</param>
        /// <returns></returns>
        public static T GetItem<T>(this T[] arr, int Index)
        {
            if (arr.isNull() || Index < 0 || Index >= arr.Length)
                return default(T);

            return arr[Index];
        }


        /// <summary>判定数组书否为空数组     元素数量为0视为空</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool isNull<T>(this T[] arr)
        {
            return arr == null || arr.Length == 0;
        }


        /// <summary>判定一个二维数组是否为空</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool isNull<T>(this T[,] self)
        {
            return self == null || self.GetLength(0) == 0;
        }

        /// <summary>使用指定值初始化一维数组的元素</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="defaultValue"></param>
        public static void Init<T>(this T[] self, T defaultValue)
        {
            if (self.isNull())
                return;

            for (int i = 0; i < self.Length; i++)
            {
                self[i] = defaultValue;
            }
        }


        /// <summary>使用指定值初始化二维数组的元素</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="defaultValue"></param>
        public static void Init<T>(this T[,] self, T defaultValue)
        {
            //if (self.isNull())
            //    return;

            for (int i = 0; i < self.GetLength(0); i++)
            {
                for (int j = 0; j < self.GetLength(1); j++)
                {
                    self[i, j] = defaultValue;
                }
            }
        }


        /// <summary>
        /// 遍历数组 对元素执行指定的Action
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="arr"></param>
        /// <param name="action">Action</param>
        public static void ActionAtItem<T>(this T[] arr, params Action<T>[] action)
        {
            if (action.isNull<Action<T>>() || arr.isNull<T>())
            {
                return;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < action.Length; j++)
                {
                    if (action[j] != null)
                    {
                        action[j](arr[i]);
                    }
                }
            }

        }

        /// <summary>遍历二维数组 对元素执行指定的Action</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="action"></param>
        public static void ActionAtItem<T>(this T[,] arr, params Action<T>[] action)
        {
            if (action.isNull<Action<T>>() || arr.isNull<T>())
            {
                return;
            }

            for (int m = 0; m < arr.GetLength(0); m++)
            {
                for (int n = 0; n < arr.GetLength(1); n++)
                {
                    for (int a = 0; a < action.Length; a++)
                    {
                        if (action[a] != null)
                        {
                            action[a](arr[m, n]);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 遍历数组 对元素执行指定的Action
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="arr"></param>
        /// <param name="action">Action</param>
        public static void ActionAtItemIndex<T>(this T[] arr, params Action<T, int>[] action)
        {
            if (action.isNull<Action<T, int>>() || arr.isNull<T>())
            {
                return;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                int index = i;
                for (int j = 0; j < action.Length; j++)
                {
                    if (action[j] != null)
                    {
                        action[j](arr[i], index);
                    }
                }
            }

        }


        /// <summary>
        /// 随机获取一个元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="arr">数组</param>
        /// <returns></returns>
        public static T GetRandItem<T>(this T[] arr)
        {
            Random r = new Random();
            return arr.isNull() ? default(T) : arr[r.Next(0, arr.Length)];
        }

        /// <summary>
        /// 获取一个随机有效的元素索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int GetRandIndex<T>(this T[] arr)
        {
            Random r = new Random();
            return arr == null ? 0 : (arr.Length < 2 ? 0 : r.Next(0, arr.Length));
        }

        /// <summary>
        /// 将数组转为字符串
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="arr">数组</param>
        /// <param name="strIndexConnectValue">连接元素索引和元素值的字符串</param>
        /// <param name="strConnectItem">元素间的连接字符串</param>
        /// <returns></returns>
        public static string ToOneString<T>(this T[] arr, string strIndexConnectValue = "=", string strConnectItem = " , ")
        {
            if (arr.isNull())
                return null;

            bool isObj = typeof(T).IsClass;
            StringBuilder sb = new StringBuilder(arr.GetType().ToString());
            sb.Append("\r\n");

            for (int i = 0; i < arr.Length; i++)
            {
                sb.Append(string.Format("[{0}]{1}{2}{3}", i.ToString(), strIndexConnectValue, isObj ? (arr[i] == null ? "NULL" : arr[i].ToString()) : arr[i].ToString(),strConnectItem));
            }

            return sb.ToString();
        }

        /// <summary>将二维数组转成一个厂字符串</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="strIndexConnectValue"></param>
        /// <param name="strConnectItem">元素连接符</param>
        /// <param name="strLine">换行符</param>
        /// <returns></returns>
        public static string ToOneString<T>(this T[,] self, string strIndexConnectValue = "=", string strConnectItem = " , ", string strLine = "\r\n")
        {
            if (self.isNull())
                return null;
            bool isObj = typeof(T).IsClass;
            StringBuilder sb = new StringBuilder(self.GetType().ToString() + "\r\n");

            for (int i = 0; i < self.GetLength(0); i++)
            {
                for (int j = 0; j < self.GetLength(1); j++)
                {
                    sb.Append(string.Format("[{0},{1}] {2} {3}", i, j, strIndexConnectValue, isObj ? (self[i, j] == null ? "NULL" : self[i, j].ToString()) : self[i, j].ToString()));
                    sb.Append(strConnectItem);
                }
                sb.Append(strLine);
            }

            return sb.ToString();
        }



        /// <summary>判定索引是否合法</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool isIndexLawful<T>(this T[] self, int index)
        {
            return !(self == null || index >= self.Length || index < 0);
        }

        /// <summary>判定索引是否合法</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="hor">第几列</param>
        /// <param name="ver">第几行</param>
        /// <returns></returns>
        public static bool isIndexLawful<T>(this T[,] self, int hor, int ver)
        {
            return !(self == null || hor < 0 || hor >= self.GetLength(1) || ver < 0 || ver >= self.GetLength(0));
        }



        /// <summary>Copy数组 从起始位置拷贝指定数量的元素</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="indexStart"></param>
        /// <param name="count"></param>
        /// <param name="bCopyMax">true 如果元素不够,Copy剩余的所有元素 ,反之放弃Copy</param>
        /// <returns></returns>
        public static T[] CopyFree<T>(this T[] self, int indexStart, int count, bool bCopyMax = true)
        {
            if (self.isNull() || !self.isIndexLawful(indexStart))
                return null;

            if (!bCopyMax && !self.isIndexLawful(indexStart + count - 1))
            {
                NZQLA.Log.LogAtUnityEditorWarning("没有足够的元素供Copy");
                return null;
            }

            int indexEnd = self.isIndexLawful(indexStart + count) ? indexStart + count : self.Length - 1;

            T[] arr = new T[indexEnd - indexStart + 1];
            for (int i = 0; i + indexStart <= indexEnd; i++)
            {
                arr[i] = self[i + indexStart];
            }

            return arr;
        }

        #endregion






        #region List

        /// <summary>获取List中指定索引的元素</summary>
        /// <typeparam name="T">List元素类型</typeparam>
        /// <param name="list">List</param>
        /// <param name="Index">指定索引</param>
        /// <returns></returns>
        public static T GetItem<T>(this List<T> list, int Index)
        {
            if (list.isNull() || Index < 0 || Index >= list.Count)
                return default(T);

            return list[Index];
        }


        /// <summary>判定List是否为空     元素数量为0视为空</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool isNull<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }


        /// <summary>使用指定值初始化List的元素</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="defaultValue"></param>
        public static void Init<T>(this List<T> self, T defaultValue)
        {
            if (self.isNull())
                return;

            for (int i = 0; i < self.Count; i++)
            {
                self[i] = defaultValue;
            }
        }


        /// <summary>
        /// 遍历List 对元素执行指定的Action
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list"></param>
        /// <param name="action">Action</param>
        public static void ActionAtItem<T>(this List<T> list, params Action<T>[] action)
        {
            if (action.isNull<Action<T>>() || list.isNull<T>())
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < action.Length; j++)
                {
                    if (action[j] != null)
                    {
                        action[j](list[i]);
                    }
                }
            }

        }


        /// <summary>
        /// 遍历List 对元素执行指定的Action
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list"></param>
        /// <param name="action">Action</param>
        public static void ActionAtItemIndex<T>(this List<T> list, params Action<T, int>[] action)
        {
            if (action.isNull<Action<T, int>>() || list.isNull<T>())
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                int Index = i;
                for (int j = 0; j < action.Length; j++)
                {
                    if (action[j] != null)
                    {
                        action[j](list[i], Index);
                    }
                }
            }

        }


        /// <summary>
        /// 随机获取一个元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="list">List</param>
        /// <returns></returns>
        public static T GetRandItem<T>(this List<T> list)
        {
            Random r = new Random();
            return list.isNull() ? default(T) : list[r.Next(0, list.Count)];
        }

        /// <summary>判定索引是否合法</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool isIndexLawful<T>(this List<T> self, int index)
        {
            return !(self == null || index >= self.Count || index < 0);
        }


        /// <summary>
        /// 将List转为字符串
        /// </summary>
        /// <typeparam name="T">List元素类型</typeparam>
        /// <param name="list">List</param>
        /// <param name="strIndexConnectValue">连接元素索引和元素值的字符串</param>
        /// <param name="strConnectItem">元素间的连接字符串</param>
        /// <returns></returns>
        public static string ToOneString<T>(this List<T> list, string strIndexConnectValue = "=", string strConnectItem = " , ")
        {
            if (list.isNull())
                return null;

            bool isObj = typeof(T).IsClass;
            StringBuilder sb = new StringBuilder(list.GetType().ToString());
            sb.Append("\r\n");

            for (int i = 0; i < list.Count; i++)
            {
                sb.Append(string.Format("[{0}]{1}{2}{3}", i.ToString(), strIndexConnectValue, isObj ? (list[i] == null ? "NULL" : list[i].ToString()) : list[i].ToString(), strConnectItem));
            }

            return sb.ToString();
        }


        #endregion




        #region 字符串 String


        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="str">文本内容</param>
        /// <param name="strPath">路径</param>
        /// <param name="bAppend">追加模式</param>
        public static void WriteToFile(this string str, string strPath, bool bAppend = false)
        {
            if (string.IsNullOrEmpty(strPath))
                return;

            if (Directory.Exists(Path.GetDirectoryName(strPath)))
            {
                FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);


                //byte[] buf = new ASCIIEncoding().GetBytes(str);
                byte[] buf = Encoding.UTF8.GetBytes(str);

                if (bAppend)
                {
                    if (fs.CanSeek)
                        fs.Seek(fs.Length, SeekOrigin.Begin);
                }

                fs.Write(buf, 0, buf.Length);

                //StreamWriter sw = new StreamWriter(fs);
                //sw.WriteLine(str);
                //sw.Close();
                fs.Close();
            }

        }


        #endregion


        /// <summary>
        /// 打乱数组内的元素顺
        /// </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="arr"></param>
        public static void RandItemRank<T>(this T[] arr)
        {
            if (arr == null || arr.Length < 2)
                return;

            Random r = new Random();


            int Count = r.Next(arr.Length / 2, arr.Length);

            int m, n;
            for (int i = 0; i < Count; i++)
            {
                m = r.Next(0, arr.Length);
                while (m == (n = r.Next(0, arr.Length)))
                {
                }

                T temp = arr[m];
                arr[m] = arr[n];
                arr[n] = temp;
            }
        }


        /// <summary>
        /// 调用Action 调用之预判其是否为空
        /// </summary>
        /// <param name="a"></param>
        public static void ActionInvokeSafely(this Action a)
        {
            if (a != null)
            {
                a.Invoke();
            }
        }




        #region 位运算
        /// <summary>
        /// 位运算 取出所有为1的位
        /// </summary>
        /// <param name="value">十进制数值</param>
        /// <param name="bIndex">将返回值替换为索引位数而不是值</param>
        /// <returns></returns>
        public static List<int> GetValueFromInt10UnEqual0(this int value, bool bIndex = true)
        {
            List<int> L = new List<int>();

            int temp = 2 << 0;
            for (int i = 0; i < 31; i++)
            {
                temp = 1 << i;
                if (value >= temp)
                {
                    if ((value & temp) > 0)
                    {
                        if (bIndex)
                            L.Add(i);
                        else
                            L.Add(temp);
                    }
                }
            }

            return L;
        }

        #endregion



        #region DeepCopy深层拷贝


        /// <summary>
        /// 深层拷贝(XML) 前提是目标类型(含内部数据类型)支持需要序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T DeepCopyByXML<T>(this T data)
        {
            object copy;
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(ms, data);
                ms.Seek(0, SeekOrigin.Begin);
                copy = xs.Deserialize(ms);
            }

            return (T)copy;
        }


        /// <summary>
        /// 深层拷贝(二进制)  前提是目标类型(含内部数据类型)支持需要序列化 引用类型需要加上[Serializable]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T DeepCopyByBinary<T>(this T data)
        {
            object copy;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter xs = new BinaryFormatter();
                xs.Serialize(ms, data);
                ms.Seek(0, SeekOrigin.Begin);
                copy = xs.Deserialize(ms);
            }

            return (T)copy;
        }




        #endregion


    }
}
