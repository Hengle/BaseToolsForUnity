using UnityEngine;
using System.Collections;
using System;
using Object = UnityEngine.Object;
using NZQLA.Recs.AssetBundles;
using NZQLA;

namespace NZQLA.Recs
{
    /// <summary>
    /// 统一的资源加载
    /// </summary>
    public class RecsLoader : MonoSingtonAuto<RecsLoader>
    {

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strPath"></param>
        /// <param name="pathType"></param>
        /// <param name="OnError"></param>
        /// <param name="OnSuccess"></param>
        /// <returns></returns>
        public static T LoadRecs<T>(string strPath, RecsPathType pathType, Action<string> OnError, Action<Object> OnSuccess = null) where T : UnityEngine.Object
        {
            if (string.IsNullOrEmpty(strPath))
                return null;

            switch (RecsAssetBundleMgr.GetRecsSources())
            {
                case RecsSource.FromRecsources:
                    //准备路径Resources下
                    if (pathType != RecsPathType.ResourcesPath)
                    {
                        strPath = FileTool.RecsPathSwitch(strPath, pathType, RecsPathType.ResourcesPath);
                    }

                    //尝试加载
                    T asset = null;
                    try
                    {
                        asset = Resources.Load<T>(strPath);
                    }
                    catch (Exception ex)
                    {
                        if (OnError != null)
                        {
                            OnError(ex.Message);
                        }
                    }

                    if (asset != null)
                    {
                        OnSuccess(asset);
                    }
                    return asset;

                case RecsSource.FormAssetBundleServer:
                    break;

                case RecsSource.FormAssetBundleLocal:
                    GetIns().StartCoroutine(AssetBundleTool.ILoadAssetBundle(FileTool.RecsPathSwitch(strPath, pathType, RecsPathType.PhysicalFullPath), true, OnError,
                    (AssetBundle a) =>
                    {
                        if (OnSuccess != null)
                        {
                            OnSuccess.Invoke(a);
                        }
                    }));
                    break;
                default:
                    break;
            }

            return null;


        }


        /// <summary>加载指定的资源</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="LoadInfo">加载信息</param>
        /// <param name="OnSuccess">加载成功的回调</param>
        /// <param name="OnFail">加载失败的回调</param>
        /// <returns></returns>
        public static T LoadRecs<T>(RecsLoadInfo LoadInfo, Action<object> OnSuccess = null, Action<object> OnFail = null) where T : UnityEngine.Object
        {
            switch (LoadInfo.RecsFromType)
            {
                case RecsFromType.UnKnown:
                    break;

                case RecsFromType.Recsources:
                    return RecsourcesHelper.GetIns().LoadAsset<T>(LoadInfo, OnSuccess, OnFail);

                case RecsFromType.AssetBundle:
                    return AssetBundleHelper.GetIns().LoadAsset<T>(LoadInfo, OnSuccess, OnFail);

                case RecsFromType.File:
                    break;
                default:
                    break;
            }

            return null;
        }



    }







    /// <summary>资源来源</summary>
    public enum RecsSource
    {
        /// <summary></summary>
        FromRecsources,

        /// <summary></summary>
        FormAssetBundleServer,

        /// <summary></summary>
        FormAssetBundleLocal,
    }

    /// <summary>资源类型</summary>
    public enum RecsFromType
    {
        /// <summary></summary>
        UnKnown,

        /// <summary></summary>
        Recsources,

        /// <summary></summary>
        AssetBundle,
        
        /// <summary></summary>
        File,
    }

    /// <summary>资源路径类型</summary>
    public enum RecsPathType
    {
        /// <summary></summary>
        None = 0,
       
        /// <summary>物理完整路径"D:/Recs/..."</summary>
        PhysicalFullPath = 1 << 1,

        /// <summary>"Asset/Recs/abc/ad/..."</summary>
        AssetPath = 1 << 2,

        /// <summary>"Recs/abc/ad/..."</summary>
        AssetCutPath = 1 << 3,

        /// <summary>"[Resources]下没有后缀的路径"</summary>
        ResourcesPath = 1 << 4,

        /// <summary>"StreamingAssets/..."</summary>
        StreamingPath = 1 << 5,

        /// <summary>"www.13.123.4354.2354.Recs/dasd..."</summary>
        NetUrl = 1 << 6,

        /// <summary>物理完整路径"file://D:/Recs/..."</summary>
        PhysicalUrl = 1 << 7,

        /// <summary>"file://D:/Recs/..."或"www.13.123.4354.2354.Recs/dasd..."</summary>
        Url = 1 << 8,
    }

    /// <summary>Url类型</summary>
    public enum UrlType
    {
        /// <summary>"www.13.123.4354.2354.Recs/dasd..."</summary>
        NetUrl,

        /// <summary>物理完整路径"file://D:/Recs/..."</summary>
        PhysicalUrl,
    }


}
