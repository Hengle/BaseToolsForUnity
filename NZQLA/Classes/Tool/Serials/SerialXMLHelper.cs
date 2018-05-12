using System.IO;
using System;
using System.Xml.Serialization;

namespace NZQLA
{
    /// <summary>
    /// 使用XML进行序列化相关操作
    /// </summary>
    public class SerialXMLHelper : ISerial
    {
        /// <summary>反序列化</summary>
        /// <typeparam name="T">指定序列化数据的类型</typeparam>
        /// <param name="stream">待反序列化的stream</param>
        /// <returns></returns>
        public T DeSerialFromStream<T>(Stream stream)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            object obj = ser.Deserialize(stream);

            T data;
            if (obj == null)
                data = default(T);
            else
                data = (T)obj;

            stream.Close();

            return data;
        }



     

        /// <summary>序列化</summary>
        /// <typeparam name="T">指定序列化数据的类型</typeparam>
        /// <param name="data">待序列化的数据</param>
        /// <param name="stream">待序列化的stream</param>
        public void SerialToStream<T>(T data, Stream stream)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            ser.Serialize(stream, data);
            stream.Flush();
            stream.Close();
        }
    }
}
