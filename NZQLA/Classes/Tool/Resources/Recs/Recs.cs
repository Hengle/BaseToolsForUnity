using UnityEngine;
using System.Collections;
using System;
using Object = UnityEngine.Object;

namespace NZQLA.Recs
{

    /// <summary></summary>
    public interface IRecsLoader
    {
        /// <summary>加载指定类型的资源</summary>
        /// <typeparam name="T">指定资源的类型</typeparam>
        /// <param name="strPath">路径</param>
        /// <param name="OnLoad">加载成功的回调</param>
        /// <param name="OnFail">加载失败的回调</param>
        /// <returns></returns>
        T LoadAsset<T>(string strPath, Action<object> OnLoad = null, Action<object> OnFail = null) where T : Object;

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strPath"></param>
        /// <param name="OnLoad"></param>
        /// <param name="OnFail"></param>
        /// <returns></returns>
        T[] LoadAssets<T>(string strPath, Action<object> OnLoad = null, Action<object> OnFail = null) where T : Object;

        /// <summary>使用指定规则记载资源</summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="LoadInfo">加载信息</param>
        /// <param name="OnLoad">加载成功回调</param>
        /// <param name="OnFail">加载失败回调</param>
        /// <returns></returns>
        T LoadAsset<T>(RecsLoadInfo LoadInfo, Action<object> OnLoad = null, Action<object> OnFail = null) where T : Object;



    }


    /// <summary>资源加载信息</summary>
    [Serializable]
    public class RecsLoadInfo
    {
        /// <summary>路径类型</summary>
        public RecsPathType RecsPathType;

        /// <summary>路径来源</summary>
        public RecsFromType RecsFromType;

        /// <summary>路径</summary>
        public string strPath;

        /// <summary>是否使用异步</summary>
        public bool isAsync;

        /// <summary>空构造</summary>
        public RecsLoadInfo() { }


        /// <summary>完整构造</summary>
        /// <param name="strPath"></param>
        /// <param name="RecsPathType"></param>
        /// <param name="RecsFromType"></param>
        /// <param name="isAsync"></param>
        public RecsLoadInfo(string strPath, RecsPathType RecsPathType, RecsFromType RecsFromType, bool isAsync)
        {
            this.strPath = strPath;
            this.RecsPathType = RecsPathType;
            this.RecsFromType = RecsFromType;
            this.isAsync = isAsync;
        }

        /// <summary></summary>
        public override string ToString()
        {
            return string.Format("RecsLoadInfo  [RecsFromType:{0}],[RecsPathType:{1}],[strPath:{2}]", RecsFromType, RecsPathType, strPath);
        }
    }



    /// <summary>Resources资源加载信息</summary>
    [Serializable]
    public class RecsLoadInfoResources : RecsLoadInfo
    {
        /// <summary></summary>
        public RecsLoadInfoResources()
        {
            RecsFromType = RecsFromType.Recsources;
        }


        /// <summary></summary>
        public RecsLoadInfoResources(string strPath, RecsPathType RecsPathType, bool isAsync) : this()
        {
            this.strPath = strPath;
            this.RecsPathType = RecsPathType;
            this.isAsync = isAsync;
        }

    }



    /// <summary>AssetBundle资源加载信息</summary>
    [Serializable]
    public class RecsLoadInfoAssetBundle : RecsLoadInfo
    {
        /// <summary>资源在Bundle下的路径</summary>
        public string strRecsPath;

        /// <summary></summary>
        public RecsLoadInfoAssetBundle()
        {
            RecsFromType = RecsFromType.AssetBundle;
        }

        /// <summary></summary>
        public RecsLoadInfoAssetBundle(string strPath, string strRecsPath, RecsPathType RecsPathType, bool isAsync) : this()
        {
            this.strPath = strPath;
            this.RecsPathType = RecsPathType;
            this.strRecsPath = strRecsPath;
            this.isAsync = isAsync;
        }

        /// <summary></summary>
        public override string ToString()
        {
            return string.Format("{0},[strRecsPath{1}]", strRecsPath);
        }
    }







}
