using UnityEngine;
using System.Collections;
using System;
using NZQLA;

namespace NZQLA.Recs.AssetBundles
{

    /// <summary>
    /// 提供资源的根路径
    /// </summary>
    public class RecsPathCtrl
    {

        private static RecsPathCtrl ins = null;

        /// <summary>实例</summary>
        /// <returns></returns>
        public static RecsPathCtrl GetIns()
        {
            if (ins == null)
                ins = new RecsPathCtrl();
            return ins;
        }

        /// <summary>资源根路径</summary>
        public string strPathBundleRoot = "AssetsResources";


        /// <summary>资源更新信息存放的文件的路径</summary>
        public string strPathUpdateInfoFile = "UpdateInfo.txt";

        /// <summary>AssetBundle资源列表文件路径(相对于根路径)</summary>
        public string strPathManifest = "AssetBundle";

        private string PCPath = Application.dataPath + "/../Captures/";

        private string AdnroidPath = "/storage/emulated/0/T-ShirtPhotos/";

        private string MacPath = Application.dataPath + "/../Captures/";

        private string IphonePath = Application.dataPath + "/../Captures/";

        /// <summary>
        /// 外部资源存放的路径（AssetBundle、xml、txt）【本地缓存】
        /// </summary>
        public string RecsRootPathLocal
        {
            get
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:   //安卓
                    case RuntimePlatform.IPhonePlayer:  //Iphone
                        return string.Concat(Application.persistentDataPath, string.Format("/{0}/", strPathBundleRoot));
                    case RuntimePlatform.OSXEditor: //MAC
                    case RuntimePlatform.OSXPlayer:
                    case RuntimePlatform.WindowsEditor: //windows
                    case RuntimePlatform.WindowsPlayer:
                        return string.Concat(Application.dataPath, string.Format("/../{0}/", strPathBundleRoot));
                    default:
                        return string.Concat(Application.dataPath, string.Format("/../{0}/", strPathBundleRoot));

                }

            }
        }

        /// <summary>
        /// 初始资源存放的位置（只读）【存放最新资源】
        /// </summary>
        public string RecsRootPathServer
        {
            get
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:   //安卓
                    case RuntimePlatform.IPhonePlayer:  //Iphone
                        return string.Concat(Application.streamingAssetsPath, string.Format("/{0}/", strPathBundleRoot));
                    case RuntimePlatform.OSXEditor: //MAC
                    case RuntimePlatform.OSXPlayer:
                    case RuntimePlatform.WindowsEditor: //windows
                    case RuntimePlatform.WindowsPlayer:
                        return string.Concat("file://", Application.streamingAssetsPath, string.Format("/{0}/", strPathBundleRoot));
                    default:
                        return string.Concat("file://", Application.streamingAssetsPath, string.Format("/{0}/", strPathBundleRoot));
                }
            }
        }

        /// <summary>资源更新信息存放的文件的路径</summary>
        /// <param name="bLocal">本地/服务器</param>
        /// <returns></returns>
        public string GetPathUpdateInfoFile(bool bLocal)
        {
            return (bLocal ? RecsRootPathLocal : RecsRootPathServer) + strPathUpdateInfoFile;
        }

        /// <summary>获取AssetBundle资源列表文件路径(相对于根路径)</summary>
        /// <param name="bLocal">本地/服务器</param>
        /// <returns></returns>
        public string GetPathManifest(bool bLocal)
        {
            return (bLocal ? RecsRootPathLocal : RecsRootPathServer) + strPathBundleRoot;
        }


        /// <summary>
        /// 图片存放的路径
        /// </summary>
        public string TargetTexturePath
        {
            get
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:   //安卓
                        return AdnroidPath;
                    case RuntimePlatform.WindowsEditor: //windows
                    case RuntimePlatform.WindowsPlayer:
                        return PCPath;
                    case RuntimePlatform.OSXEditor: //MAC
                    case RuntimePlatform.OSXPlayer:
                        return MacPath;
                    case RuntimePlatform.IPhonePlayer:  //Iphone
                        return IphonePath;
                    default:
                        return null;
                }
            }
        }



        /// <summary></summary>
        /// <param name="strPath"></param>
        /// <param name="OnFail"></param>
        /// <returns></returns>
        public string ToRecsourcsPath(string strPath, Action<string> OnFail = null)
        {
            if (string.IsNullOrEmpty(strPath))
                return null;

            if (FileTool.isFileUrl(strPath))
                strPath = FileTool.PhysicalUrlToFullPhysicalPath(strPath);

            if (FileTool.IsFullPath(strPath))
            {
                strPath = FileTool.PhysccalFullPathToAssetPath(strPath);
                strPath = FileTool.AssetPathToAssetCutPath(strPath);
            }

            return FileTool.GetFileNameWithoutExtension(strPath);
        }

    }

}
