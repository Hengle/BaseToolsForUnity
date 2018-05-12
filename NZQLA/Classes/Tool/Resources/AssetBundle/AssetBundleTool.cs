using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Object = UnityEngine.Object;
using NZQLA;

namespace NZQLA.Recs.AssetBundles
{

    /// <summary>
    /// AssetBundle工具类
    /// </summary>
    public class AssetBundleTool
    {

        /// <summary>将指定名称的AssetBundle下载到指定位置</summary>
        /// <param name="strRecsPathDirectory">目标资源路径的文件夹</param>
        /// <param name="strBundleName">指定需要下载的AssetBundle的名称</param>
        /// <param name="strOutPathDirectory">目标输出路径的文件夹</param>
        /// <param name="OnError">下载失败的回调</param>
        /// <param name="onFinish">下载完毕的回调</param>
        /// <returns></returns>
        public static IEnumerator DownloadAssetBundle(string strRecsPathDirectory, string strBundleName, string strOutPathDirectory, Action<string> OnError, Action onFinish)
        {
            WWW down = new WWW(strRecsPathDirectory + strBundleName);
            if (down == null)
            {
                if (OnError != null) OnError.Invoke(string.Format("获取资源[{0}]失败", strBundleName));
                yield break;
            }

            yield return down;
            if (!string.IsNullOrEmpty(down.error))
            {
                if (OnError != null) OnError.Invoke(string.Format("获取资源[{0}]失败 Error:{1}", strBundleName, down.error));
                yield break;
            }

            FileTool.WriteBytesToFileByBinary(strOutPathDirectory + strBundleName, down.bytes);
        }


        /// <summary></summary>
        /// <param name="man"></param>
        /// <param name="strOutPath"></param>
        /// <param name="bContainsDepen"></param>
        public static void LoadBundlesFromAssetBundleManifest(AssetBundleManifest man, string strOutPath, bool bContainsDepen = true)
        {


        }


        /// <summary>加载指定路径的AssetBundle</summary>
        /// <param name="strPath">指定路径</param>
        /// <param name="bAsyc">是否采用异步加载</param>
        /// <param name="OnError">加载失败回调</param>
        /// <param name="OnSuccess">加载成功回调</param>
        /// <returns></returns>
        public static IEnumerator ILoadAssetBundle(string strPath, bool bAsyc, Action<string> OnError, Action<AssetBundle> OnSuccess = null)
        {
            if (string.IsNullOrEmpty(strPath) || !File.Exists(strPath))
            {
                if (OnError != null)
                {
                    OnError("AssetBundle路径非法!");
                }
                yield break;
            }


            AssetBundle ab = null;
            if (bAsyc)
            {
                AssetBundleCreateRequest acr = AssetBundle.LoadFromFileAsync(strPath);
                yield return acr.isDone;
                ab = acr.assetBundle;
            }
            else
            {
                ab = AssetBundle.LoadFromFile(strPath);
            }

            if (ab == null)
            {
                if (OnError != null)
                {
                    OnError.Invoke("AssetBunle为Null");
                }
            }
            else if (OnSuccess != null)
            {
                OnSuccess.Invoke(ab);
            }
            yield return null;
        }


        /// <summary></summary>
        /// <param name="ab"></param>
        /// <param name="OnFaild"></param>
        /// <returns></returns>
        public static Object[] GetAssetsFromAssetBundle(AssetBundle ab, Action<string> OnFaild = null)
        {
            string error = null;
            if (ab == null)
            {
                error = "Null Ref";
            }
            else if (ab.mainAsset == null)
                error = "Empty AssteBundle";

            Object[] assets = ab.LoadAllAssets();
            if (!string.IsNullOrEmpty(error))
            {
                if (OnFaild != null)
                {
                    OnFaild(error);
                }
            }

            return assets;
        }


        /// <summary></summary>
        /// <param name="ab"></param>
        /// <returns></returns>
        public static IEnumerator ILoadAssetFromAssetBundle(AssetBundle ab)
        {

            yield return null;
        }

    }
}
