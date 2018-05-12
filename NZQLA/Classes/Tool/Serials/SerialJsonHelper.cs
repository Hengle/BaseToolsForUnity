using UnityEngine;
using System;
using System.IO;

namespace NZQLA
{
    /// <summary>
    /// Json序列化
    /// </summary>
    public class SerialJsonHelper
    {
        /// <summary>反序列化</summary>
        /// <typeparam name="T">指定序列化数据的类型</typeparam>
        /// <param name="stream">待反序列化的stream</param>
        /// <returns></returns>
        public T DeSerialFromStream<T>(Stream stream)
        {
            if (stream == null)
                return default(T);
            StreamReader sr = new StreamReader(stream);
            string strData = sr.ReadToEnd();

            T data = JsonUtility.FromJson<T>(strData);
            stream.Close();

            return data;
        }


        /// <summary>序列化</summary>
        /// <typeparam name="T">指定序列化数据的类型</typeparam>
        /// <param name="data">待序列化的数据</param>
        /// <param name="stream">待序列化的stream</param>
        public void SerialToStream<T>(T data, Stream stream)
        {
            if (stream == null)
                return;
            StreamWriter sr = new StreamWriter(stream);
            sr.Write(JsonUtility.ToJson(data));
            sr.Flush();
            sr.Close();
        }


    }
}
