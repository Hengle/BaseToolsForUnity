using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;


namespace NZQLA
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class MyTool : MonoBehaviour
    {



        #region 父子物体









        #endregion




        #region 路点移动

        /// <summary>
        /// 判定一个位置是否在两个路点的方向上
        /// </summary>
        /// <param name="vPosCur"></param>
        /// <param name="vPosA"></param>
        /// <param name="vPosB"></param>
        /// <returns></returns>
        public static bool IsPosOn2WayPointsDir(Vector3 vPosCur, Vector3 vPosA, Vector3 vPosB)
        {
            Vector3 vDirAB, vDirCB;
            vDirAB = (vPosB - vPosA).normalized;
            vDirCB = (vPosB - vPosCur).normalized;

            return Mathf.RoundToInt(Vector3.Dot(vDirAB, vDirCB)) == 1;
        }


        /// <summary>
        /// 尝试在2个路点上以一定速度移动
        /// </summary>
        /// <param name="vPosCur"></param>
        /// <param name="vPosA"></param>
        /// <param name="vPosB"></param>
        /// <param name="fSpeed"></param>
        /// <param name="bAttach"></param>
        /// <returns></returns>
        public static Vector3 TryMoveClampIn2Points(Vector3 vPosCur, Vector3 vPosA, Vector3 vPosB, float fSpeed, out bool bAttach)
        {
            bAttach = false;
            ////判定一个位置是否在两个路点的方向上
            //if (!IsPosOn2WayPointsDir(vPosCur, vPosA, vPosB))
            //    return vPosCur;

            Vector3 vDirAB, vDirCB;
            vDirAB = (vPosB - vPosA).normalized;
            vDirCB = vPosB - vPosCur - vDirAB * fSpeed * Time.deltaTime;
            //检测以当前速度是否会越过目标点B
            if (Vector3.Dot(vDirAB, vDirCB) < 0)
            {
                bAttach = true;
                vPosCur = vPosB;
            }
            else
            {
                vPosCur += vDirAB * fSpeed * Time.deltaTime;
            }
            return vPosCur;
        }


        #endregion





        #region 二进制序列化
        /// <summary>
        /// 使用二进制序列化指定的对象到指定的文件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="strDestPath">指定存储序列化数据的文件</param>
        public static void SerializeObjToBinaryFile<T>(T obj, string strDestPath) where T : class
        {
            if (string.IsNullOrEmpty(strDestPath))
                return;

            //判定文件夹是否存在
            FileTool.EnsureDirectoryExist(strDestPath);

            FileStream FS = new FileStream(strDestPath, FileMode.Create, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(FS, obj);
            FS.Close();
        }




        /// <summary>
        /// 使用二进制反序列化指定的文件到指定类型的对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="strRecsPath">指定存储序列化数据的文件</param>
        /// <returns></returns>
        public static T DeserializeObjFromBinaryFile<T>(string strRecsPath)
        {
            if (string.IsNullOrEmpty(strRecsPath))
                return default(T);

            FileStream FS = new FileStream(strRecsPath, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            object obj = bf.Deserialize(FS);

            T tttt;
            if (obj == null)
                tttt = default(T);
            else
                tttt = (T)obj;

            FS.Close();

            return tttt;
        }
        #endregion




        #region XML序列化
        /// <summary>将对象序列化至指定文件</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="strDestPath"></param>
        /// <param name="type">补充类型 比如子类类型(解决子类序列化失败的问题)</param>
        public static void SerializeObjToXML<T>(T obj, string strDestPath, params Type[] type) where T : class
        {
            if (string.IsNullOrEmpty(strDestPath))
                return;

            //判定文件夹是否存在
            FileTool.EnsureDirectoryExist(strDestPath);

            FileStream FS = new FileStream(strDestPath, FileMode.Create, FileAccess.Write);
            XmlSerializer ser = new XmlSerializer(typeof(T), type);
            ser.Serialize(FS, obj);
            FS.Close();
        }


        /// <summary>将制定路径的XML反序列化为指定类型的对象</summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="strRecsPath">指定路径</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T DeserializeObjFromXML<T>(string strRecsPath, params Type[] type)
        {
            if (string.IsNullOrEmpty(strRecsPath))
                return default(T);

            FileStream FS = new FileStream(strRecsPath, FileMode.Open, FileAccess.Read);
            XmlSerializer ser = new XmlSerializer(typeof(T), type);
            object obj = ser.Deserialize(FS);

            T tttt;
            if (obj == null)
                tttt = default(T);
            else
                tttt = (T)obj;

            FS.Close();

            return tttt;
        }
        #endregion



        #region 对数组、List、词典的元素执行指定的操作

        /// <summary>
        /// 批量对指定数组元素执行指定的行动
        /// </summary>
        /// <typeparam name="T">数组内的元素类型</typeparam>
        /// <param name="arrItem">指定带操作的数组</param>
        /// <param name="action">指定要执行行动的委托</param>
        public static void BacthOperateOnArray<T>(T[] arrItem, params Action<T>[] action)
        {
            //数据有效性判空检查
            if (ArrayIsEmpty<T>(arrItem) || ArrayIsEmpty<Action<T>>(action))
                return;

            for (int i = 0; i < arrItem.Length; i++)
            {
                if (arrItem[i] == null)
                    continue;

                for (int a = 0; a < action.Length; a++)
                {
                    if (action[a] != null)
                        action[a].Invoke(arrItem[i]);
                }
            }
        }


        /// <summary>
        /// 批量对指定List元素执行指定的行动
        /// </summary>
        /// <typeparam name="T">List内的元素类型</typeparam>
        /// <param name="arrItem">指定带操作的数组</param>
        /// <param name="action">指定要执行行动的委托</param>
        public static void BacthOperateOnList<T>(List<T> arrItem, params Action<T>[] action)
        {
            //数据有效性判空检查
            if (arrItem == null || arrItem.Count == 0 || ArrayIsEmpty<Action<T>>(action))
                return;

            for (int i = 0; i < arrItem.Count; i++)
            {
                if (arrItem[i] == null)
                    continue;

                for (int a = 0; a < action.Length; a++)
                {
                    if (action[a] != null)
                        action[a].Invoke(arrItem[i]);
                }
            }
        }


        /// <summary>
        /// 批量对指定词典元素执行指定的行动
        /// </summary>
        /// <typeparam name="K">键的类型</typeparam>
        /// <typeparam name="V">值的类型</typeparam>
        /// <param name="dic">词典</param>
        /// <param name="action">指定要执行行动的委托</param>
        public static void BacthOPerateOnDic<K, V>(Dictionary<K, V> dic, params Action<V>[] action)
        {
            //数据有效性判空检查
            if (dic == null || dic.Count == 0 || ArrayIsEmpty<Action<V>>(action))
                return;

            foreach (KeyValuePair<K, V> item in dic)
            {
                for (int i = 0; i < action.Length; i++)
                {
                    if (action[i] != null)
                        action[i].Invoke(item.Value);
                }
            }

        }


        /// <summary>判定一数组是否为空 </summary>
        /// <typeparam name="T">数组元素类型</typeparam>
        /// <param name="arr">数组</param>
        /// <returns></returns>
        public static bool ArrayIsEmpty<T>(T[] arr)
        {
            return arr == null || arr.Length == 0;
        }


        #endregion





        #region 时间格式转换

        /// <summary>时间格式转换 秒→时分秒</summary>
        /// <param name="fTime">总时间(S)</param>
        /// <param name="strConnectChar">连接符</param>
        /// <returns></returns>
        public static string TimeFormat(float fTime, string strConnectChar)
        {

            return string.Format("{0:00}{1}{2:00}{3}{4:00}", Mathf.FloorToInt(fTime / 3600), strConnectChar, Mathf.FloorToInt(fTime % 3600 / 60), strConnectChar, Mathf.FloorToInt(fTime % 60));
        }

        #endregion




        #region Log

        /// <summary>只在Editor模式进行Log</summary>
        /// <param name="strLogContent"></param>
        /// <param name="strColor"></param>
        public static void LogOnlyAtEditor(string strLogContent, string strColor = "white")
        {
#if UNITY_EDITOR
            Debug.Log(string.Format("<color={0}>{1}</color>", strColor, strLogContent));
#endif
        }

        #endregion

        #region 粒子组件ParticleSystem周边
        /// <summary>
        /// 初始化粒子的陪孩子开关 
        /// 禁止playOnAwake
        /// 如果正在播放/暂停 将其停止并清理
        /// </summary>
        /// <param name="ps"></param>
        public static void InitPartical(ParticleSystem ps)
        {
            if (ps)
            {
                ps.playOnAwake = false;
                ParticalStop(ps
                    );
            }
        }

        /// <summary>
        /// 播放指定粒子
        /// </summary>
        /// <param name="ps"></param>
        public static void ParticalPlay(ParticleSystem ps)
        {
            if (ps)
            {
                if (!ps.isPlaying || ps.isPaused)
                {
                    ps.Play();
                }
            }
        }


        /// <summary>
        /// 播放指定粒子
        /// </summary>
        /// <param name="ps"></param>
        public static void ParticalStop(ParticleSystem ps)
        {
            if (ps)
            {
                if (ps.isPlaying || ps.isPaused)
                {
                    ps.Stop();
                    ps.Clear();
                }
            }
        }


        /// <summary></summary>
        /// <param name="ps"></param>
        /// <param name="rate"></param>
        public static void SetEmmisonRateModule(ParticleSystem ps, float rate)
        {
            if (ps != null)
            {
                ParticleSystem.Burst[] arr = new ParticleSystem.Burst[] { };
                ps.emission.SetBursts(arr);
                ps.emissionRate = rate;
                //ps.emission.rate = new ParticleSystem.MinMaxCurve(rate);
            }
        }
        #endregion



        #region Vector3
        /// <summary>将Vector3.Tostring()产生的字符串解析为Vector3</summary>
        /// <param name="strV3ToString">将Vector3.Tostring()产生的字符串</param>
        /// <returns></returns>
        public static Vector3 ParseStrToV3(string strV3ToString)
        {
            //参数判空
            if (string.IsNullOrEmpty(strV3ToString))
            {
#if UNITY_EDITOR
                Debug.Log("<color=#ff0000ff>非法字符 无法解析为Vector3" + strV3ToString + "</color>");
#endif
                return Vector3.zero;
            }

            //切割参数并检查合法性
            string[] arr = strV3ToString.Trim('(', ')').Split(',');
            if (arr == null || arr.Length != 3)
            {
#if UNITY_EDITOR
                Debug.Log("<color=#ff0000ff>非法字符 无法解析为Vector3" + strV3ToString + "</color>");
#endif
                return Vector3.zero;
            }

            float tempParseValue = 0;
            Vector3 V = Vector3.zero;
            for (int i = 0; i < 3; i++)
            {
                if (!float.TryParse(arr[i], out tempParseValue))
                {
#if UNITY_EDITOR
                    Debug.Log("<color=#ff0000ff>非法字符 无法解析为Vector3" + strV3ToString + "</color>");
#endif
                    return Vector3.zero;
                }
                V[i] = tempParseValue;
            }

            return V;
        }

        #endregion




    }


}
