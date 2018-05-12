using UnityEngine;
using System.Collections;
using System;
using System.IO;

namespace NZQLA
{

    /// <summary>
    /// 序列化工具
    /// </summary>
    public class SerializeHelper
    {
        /// <summary>将数据序列化到指定文件</summary>
        /// <typeparam name="T">指定序列化数据的类型</typeparam>
        /// <param name="data">待序列化的数据</param>
        /// <param name="strPath">指定序列化到哪个文件</param>
        /// <param name="serialModel">指定序列化的方式</param>
        public static void SerialDataToFile<T>(T data, string strPath, SerialModel serialModel = SerialModel.XML)
        {
            //路径非法拦截
            if (FileTool.IsErrorPath(strPath))
                return;

            //确保路径可用
            FileTool.EnsureDirectoryExist(strPath);

            Stream destStream = FileTool.ReadyStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);
            SerialDataToStream(data, destStream, serialModel);
        }


        /// <summary>将数据序列化到指定流stream</summary>
        /// <typeparam name="T">指定序列化数据的类型</typeparam>
        /// <param name="data">待序列化的数据</param>
        /// <param name="destStream">指定序列化到哪个stream</param>
        /// <param name="serialModel">指定序列化的方式</param>
        public static void SerialDataToStream<T>(T data, Stream destStream, SerialModel serialModel = SerialModel.XML)
        {
            if (null == destStream)
                return;

            switch (serialModel)
            {
                case SerialModel.XML:
                    new SerialXMLHelper().SerialToStream<T>(data, destStream);
                    break;
                case SerialModel.Binary:
                    new SerialBinaryHelper().SerialToStream<T>(data, destStream);
                    break;
                case SerialModel.Json:
                    new SerialJsonHelper().SerialToStream<T>(data, destStream);
                    break;
                case SerialModel.ProtoBuf:
                    break;
                default:
                    break;
            }

        }


        /// <summary>从指定文件将反序列化成指定类型的数据</summary>
        /// <typeparam name="T">指定以何种类型反序列化</typeparam>
        ///<param name="strPath">待反序列化的文件路径</param>
        /// <param name="serialModel">指定使用何种反序列化的方式</param>
        public static T DeSerialDataFromFile<T>(string strPath, SerialModel serialModel = SerialModel.XML)
        {
            //路径非法拦截
            if (!FileTool.FileExist(strPath))
                return default(T);
            Stream destStream = FileTool.ReadyStream(strPath);
            return DeSerialDataFromStream<T>(destStream, serialModel);
        }



        /// <summary>从指定流stream将反序列化成指定类型的数据</summary>
        /// <typeparam name="T">指定以何种类型反序列化</typeparam>
        /// <param name="destStream">指定待序列化的stream</param>
        /// <param name="serialModel">指定使用何种反序列化的方式</param>
        public static T DeSerialDataFromStream<T>(Stream destStream, SerialModel serialModel = SerialModel.XML)
        {
            if (null == destStream)
                return default(T);

            switch (serialModel)
            {
                case SerialModel.XML:
                    return new SerialXMLHelper().DeSerialFromStream<T>(destStream);
                case SerialModel.Binary:
                    return new SerialBinaryHelper().DeSerialFromStream<T>(destStream);
                case SerialModel.Json:
                    return new SerialJsonHelper().DeSerialFromStream<T>(destStream);
                case SerialModel.ProtoBuf:
                    break;
                default:
                    break;
            }

            return default(T);
        }


        /// <summary>将指定文件反序列化为指定类型的 序列化方式根据文件路径决定 ".xml"(XML) ".json"(Json) ".pb"(ProtoBuf) default(Binary)</summary>
        /// <typeparam name="T">指定数据的类型</typeparam>
        /// <param name="strPath">指定文件路径</param>
        /// <returns></returns>
        public static T DeSerialDataFromFileAuto<T>(string strPath)
        {
            if (string.IsNullOrEmpty(strPath))
                return default(T);

            switch (Path.GetExtension(strPath).ToLower())
            {
                case ".xml":
                    return DeSerialDataFromFile<T>(strPath, SerialModel.XML);
                case ".json":
                    return DeSerialDataFromFile<T>(strPath, SerialModel.Json);
                case ".pb":
                    return DeSerialDataFromFile<T>(strPath, SerialModel.ProtoBuf);
                default:
                    return DeSerialDataFromFile<T>(strPath, SerialModel.Binary);
            }
        }


        /// <summary>将指定类型的数据反序列化到指定文件 序列化方式根据文件路径决定 ".xml"(XML) ".json"(Json) ".pb"(ProtoBuf) default(Binary)</summary>
        /// <typeparam name="T">指定数据的类型</typeparam>
        /// <param name="data">待写入的数据</param>
        /// <param name="strPath">指定文件路径</param>
        /// <returns></returns>
        public static void SerialDataToFileAuto<T>(T data, string strPath)
        {
            if (string.IsNullOrEmpty(strPath))
                return;

            switch (Path.GetExtension(strPath).ToLower())
            {
                case ".xml":
                    SerialDataToFile<T>(data, strPath, SerialModel.XML);
                    break;
                case ".json":
                    SerialDataToFile<T>(data, strPath, SerialModel.Json);
                    break;
                case ".pb":
                    SerialDataToFile<T>(data, strPath, SerialModel.ProtoBuf);
                    break;
                default:
                    SerialDataToFile<T>(data, strPath, SerialModel.Binary);
                    break;
            }
        }


    }


    /// <summary>序列化接口</summary>
    public interface ISerial
    {
        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="stream"></param>
        void SerialToStream<T>(T data, Stream stream);


        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        T DeSerialFromStream<T>(Stream stream);
    }



    /// <summary>序列化方式</summary>
    public enum SerialModel
    {
        /// <summary>使用 XML 进行序列化 
        /// 不支持私有字段/属性
        /// 待序列化的数据类型需要支持序列化[Serializable]  [NonSerialized]可以忽略不需要序列化的数据
        /// 需要有无参构造 否则无法反序列化
        /// </summary>
        XML,


        /// <summary>使用 二进制 进行序列化
        /// 支持私有字段/属性
        /// 待序列化的数据类型需要支持序列化[Serializable]  [NonSerialized]可以忽略不需要序列化的数据
        /// 不需要有无参构造
        /// </summary>
        Binary,

        /// <summary>使用 Json 进行序列化</summary>
        Json,

        /// <summary>使用 ProtoBuf 进行序列化</summary>
        ProtoBuf,
    }



    /// <summary>序列化失败编号</summary>
    public enum SerialError
    {
        /// <summary>未知错误</summary>
        UnKnown,

        /// <summary>空数据</summary>
        EmptyData,

        /// <summary>目标位置不可用</summary>
        DestUnVailed,

        /// <summary>不支持的序列化方式</summary>
        DontSupport,
    }


}
