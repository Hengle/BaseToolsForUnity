using UnityEngine;
using System.Collections;
using System;
using NZQLA.Recs.AssetBundles;
using System.IO;
using NZQLA;
using Object = UnityEngine.Object;

namespace NZQLA.Recs
{
    /// <summary></summary>
    public class AssetBundleHelper : MonoSingtonAuto<AssetBundleHelper>, IRecsLoader
    {
        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="LoadInfo"></param>
        /// <param name="OnLoad"></param>
        /// <param name="OnFail"></param>
        /// <returns></returns>
        public T LoadAsset<T>(RecsLoadInfo LoadInfo, Action<object> OnLoad = null, Action<object> OnFail = null) where T : UnityEngine.Object
        {
            T asset = null;
            StartCoroutine(ILoadRecsFromBundlePath(LoadInfo, (o) =>
            {
                asset = o as T;
                if (OnLoad != null)
                {
                    OnLoad(o);
                }
            },
            OnFail));

            return asset;
        }

        //public static async AssetBundle LoadRecsFromAssetBundleFile(string strPathBundle, string recsName)
        //{
        //    AssetBundleCreateRequest ar = null;
        //    Task<bool> loadAssetbundle = Task.Run<bool>(() =>
        //   {
        //       try
        //       {
        //           ar = AssetBundle.LoadFromFileAsync(strPathBundle);
        //       }
        //       catch (Exception ex)
        //       {

        //       }
        //       return ar.isDone;
        //   });
        //    await loadAssetbundle;

        //    if (ar != null)
        //    {
        //        ar.assetBundle.LoadAsset(recsName);
        //    }


        //    return null;
        //}



        //public static async AssetBundle LoadAssetBundle(string strPath)
        //{
        //    AssetBundleCreateRequest ar = AssetBundle.LoadFromFileAsync(strPath);
        //    await ar.isDone;


        //}


        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strPath"></param>
        /// <param name="OnLoad"></param>
        /// <param name="OnFail"></param>
        /// <returns></returns>
        public T LoadAsset<T>(string strPath, Action<object> OnLoad = null, Action<object> OnFail = null) where T : UnityEngine.Object
        {
            throw new NotImplementedException();
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strPath"></param>
        /// <param name="OnLoad"></param>
        /// <param name="OnFail"></param>
        /// <returns></returns>
        public T[] LoadAssets<T>(string strPath, Action<object> OnLoad = null, Action<object> OnFail = null) where T : UnityEngine.Object
        {
            throw new NotImplementedException();
        }


        IEnumerator ILoadRecsFromBundlePath(RecsLoadInfo LoadInfo, Action<object> OnLoad = null, Action<object> OnFail = null)
        {

            if (LoadInfo == null || LoadInfo.RecsFromType != RecsFromType.AssetBundle || string.IsNullOrEmpty(LoadInfo.strPath) /*|| !FileTool.FileExist(LoadInfo.strPath, LoadInfo.RecsPathType)*/)
            {
                if (OnFail != null)
                {
                    OnFail("资源不存在或路径错误");
                }
                yield break;
            }
            RecsLoadInfoAssetBundle rb = LoadInfo as RecsLoadInfoAssetBundle;


            yield return StartCoroutine(ILoadAssetBundle<AssetBundle>(rb.strPath,
                (ab) =>
                {
                    //Test
                    Debug.Log(string.Format("加载AssetBundle[{0}]成功", ab.ToString()));
                    if (ab != null && ab is AssetBundle)
                    {
                        StartCoroutine(ILoadRecsFromBundle<Object>(ab as AssetBundle, rb.strRecsPath, rb.isAsync, OnLoad, OnFail));
                    }
                }, OnFail));

        }



        /// <summary>使用协程加载指定路径的AssetBundle</summary>
        /// <typeparam name="AssetBundle"></typeparam>
        /// <param name="strPath">AssetBundle文件路径</param>
        /// <param name="OnLoad">加载完成的回调</param>
        /// <param name="OnFail">加载失败的回调</param>
        /// <returns></returns>
        IEnumerator ILoadAssetBundle<AssetBundle>(string strPath, Action<object> OnLoad = null, Action<object> OnFail = null)
        {
            if (string.IsNullOrEmpty(strPath) || !File.Exists(strPath))
            {
                if (OnFail != null)
                {
                    OnFail("ERROR：不存在的AssetBundle文件");
                    yield break;
                }
            }

            AssetBundleCreateRequest ar = UnityEngine.AssetBundle.LoadFromFileAsync(strPath);
            if (ar == null)
            {
                if (OnFail != null)
                {
                    OnFail("ERROR：AssetBundle为空");
                    yield break;
                }
                yield break;
            }

            yield return new WaitUntil(() => ar.isDone);

            yield return ar.assetBundle;
            if (OnLoad != null)
            {
                OnLoad(ar.assetBundle);
            }
        }




        //从Bundle加载资源
        IEnumerator ILoadRecsFromBundle<T>(AssetBundle bundle, string strAssetName, bool isAsyc, Action<object> OnLoad = null, Action<object> OnFail = null) where T : UnityEngine.Object
        {
            if (bundle == null)
                yield break;
            T asset = null;

            //异步处理
            if (isAsyc)
            {
                AssetBundleRequest ar = bundle.LoadAssetAsync<T>(strAssetName);
                if (ar != null)
                {
                    yield return new WaitUntil(() => ar.isDone);
                    asset = (T)ar.asset;
                    yield return ar.asset;
                }
            }
            else
            {
                asset = bundle.LoadAsset<T>(strAssetName);
                yield return bundle.LoadAsset<T>(strAssetName);
            }

            //执行相关回调
            if (asset == null)
            {
                //加载失败
                if (OnFail != null)
                {
                    OnFail("加载失败");
                }
            }
            else
            {
                //加载成功
                if (OnLoad != null)
                {
                    OnLoad(asset);
                }
            }

        }





    }





}
