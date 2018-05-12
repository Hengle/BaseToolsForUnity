using UnityEngine;
using System.Collections;
using System;
using NZQLA;

namespace NZQLA.Recs
{
    /// <summary>
    /// 使用Resources加载资源
    /// </summary>
    public class RecsourcesHelper : MonoSingtonAuto<RecsourcesHelper>, IRecsLoader
    {
        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="LoadInfo"></param>
        /// <param name="OnLoad"></param>
        /// <param name="OnFail"></param>
        /// <returns></returns>
        public T LoadAsset<T>(RecsLoadInfo LoadInfo, Action<object> OnLoad = null, Action<object> OnFail = null) where T : UnityEngine.Object
        {
            string strError = null;
            if (LoadInfo == null)
                strError = "Null：加载信息为空";

            if (LoadInfo.RecsFromType != RecsFromType.Recsources)
                strError = "加载信息错误 不合适的加载方式";

            if (string.IsNullOrEmpty(LoadInfo.strPath))
                strError = "Null:path is null";

            //路径类型检查
            if (LoadInfo.RecsPathType != RecsPathType.ResourcesPath)
            {
                LoadInfo.strPath = FileTool.ToRecsourcesPath(LoadInfo.strPath);
            }

            T recs = null;
            try
            {
                recs = Resources.Load<T>(LoadInfo.strPath);
            }
            catch (Exception ex)
            {
                if (OnFail != null)
                {
                    OnFail(ex.Message);
                    return null;
                }
            }

            if (!string.IsNullOrEmpty(strError))
            {
                if (OnFail != null)
                {
                    OnFail(strError);
                }
            }
            else if (OnLoad != null)
            {
                OnLoad(recs);
            }

            return recs;

        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strPath"></param>
        /// <param name="OnLoad"></param>
        /// <param name="OnFail"></param>
        /// <returns></returns>
        public T LoadAsset<T>(string strPath, Action<object> OnLoad = null, Action<object> OnFail = null) where T : UnityEngine.Object
        {
            T recs = null;
            try
            {
                recs = Resources.Load<T>(strPath);
                if (OnLoad != null)
                {
                    OnLoad(recs);
                }
            }
            catch (Exception ex)
            {
                if (OnFail != null)
                {
                    OnFail(ex.Message);
                }
            }

            return recs;
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strPath"></param>
        /// <param name="OnLoad"></param>
        /// <param name="OnFail"></param>
        /// <returns></returns>
        public T[] LoadAssets<T>(string strPath, Action<object> OnLoad = null, Action<object> OnFail = null) where T : UnityEngine.Object
        {
            T[] recs = null;
            try
            {
                recs = Resources.LoadAll<T>(strPath);
                if (OnLoad != null)
                {
                    OnLoad(recs);
                }
            }
            catch (Exception ex)
            {
                if (OnFail != null)
                {
                    OnFail(ex.Message);
                }
            }

            return recs;
        }
    }
}
