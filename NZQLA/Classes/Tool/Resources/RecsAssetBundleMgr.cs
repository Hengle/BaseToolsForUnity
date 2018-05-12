using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using NZQLA;
using NZQLA.Recs;

namespace NZQLA.Recs.AssetBundles
{
    /// <summary>
    /// 资源加载管理
    /// 资源检查、下载、更新、载入
    /// </summary>
    public class RecsAssetBundleMgr : MonoSingtonAuto<RecsAssetBundleMgr>
    {

        /// <summary></summary>
        public AssetBundleManifest ManifestServer;//服务端数据

        /// <summary></summary>
        public AssetBundleManifest ManifestLocal;//客户端数据

        /// <summary></summary>
        public List<UnityEngine.Object> recsList = new List<UnityEngine.Object>();

        /// <summary></summary>
        public Dictionary<string, UnityEngine.Object> dicRecs = new Dictionary<string, UnityEngine.Object>();


        void Awake()
        {
            //单例准备
            LoadRecsUI.GetIns();
            MsgUICtrl.GetIns();
        }

        void Start()
        {
            //开始准备资源
            ReadyRecs();


        }


        //准备资源
        void ReadyRecs()
        {
            StartCoroutine(IReadyRecs());
        }


        IEnumerator IReadyRecs()
        {

            ////加载资源
            //yield return StartCoroutine(ILoadAllBundles());
            //yield break;


            Log.LogAndSave("检查资源包");
            LoadRecsUI.GetIns().InitAndShow("检查资源包", false);
            if (isNeedDownAllFilesFromServer())
            {
                yield return new WaitForSeconds(0.5f);

                Log.LogAndSave("开始下载资源包");
                LoadRecsUI.GetIns().InitAndShow("开始下载资源包");

                yield return StartCoroutine(IDownAllBundlesFromServerToLocal());

                //yield return new WaitUntil(() => 0 > 10);
            }

            yield return new WaitForSeconds(0.5f);

            LoadRecsUI.GetIns().InitAndShow("资源下载完毕 开始检查资源>>>>>>", true, 1);

            //比对本地和服务器数据记录下需要更新的资源
            yield return StartCoroutine(ICheckAllBundles());

            //依据记录的资源更新列表覆盖更新本地资源
            yield return StartCoroutine(IUpdateAllBundles());


            ////加载资源
            //yield return StartCoroutine(ILoadAllBundles());


            Log.SaveLogToFile(true);

            yield return null;
        }



        /// <summary>判定本地是否缓存有AssetBundleManifest</summary>
        /// <returns></returns>
        bool isNeedDownAllFilesFromServer()
        {
            return !File.Exists(RecsPathCtrl.GetIns().GetPathManifest(true));
        }


        //一次性从服务器将所有AssetBundle文件下载到本地缓存
        IEnumerator IDownAllBundlesFromServerToLocal()
        {
            //更新Manifest至本地
            yield return StartCoroutine(IUpdateManifestToLocal());

            //显示读取信息
            Log.LogAndSave("下载根数据成功");
            MsgUICtrl.GetIns().ShowMsg("下载根数据成功", "将根数据保存至本地 " + RecsPathCtrl.GetIns().GetPathManifest(true), null, true);


            //获取Manifest包含的所有的AssetBundle信息
            string[] arrAllBundles = ManifestServer.GetAllAssetBundles();

            if (arrAllBundles == null || arrAllBundles.Length == 0)
            {
                MsgUICtrl.GetIns().ShowMsg("空资源", "根数据下没有其它资源 ", null, true, 1);
                Log.LogAndSave("根数据下没有其它资源");
                yield break;
            }

            //下载所有的AssetBundle文件到本地
            for (int i = 0; i < arrAllBundles.Length; i++)
            {
                LoadRecsUI.GetIns().InitAndShow(string.Format("下载资源[{0}]", arrAllBundles[i]), true, i * 1f / arrAllBundles.Length);
                yield return StartCoroutine(AssetBundleTool.DownloadAssetBundle(
                    RecsPathCtrl.GetIns().RecsRootPathServer
                    , arrAllBundles[i],
                    RecsPathCtrl.GetIns().RecsRootPathLocal,
                                    (string str) =>
                                    {
                                        Log.LogAndSave("下载资源失败 " + str);
                                        MsgUICtrl.GetIns().ShowMsg("下载资源失败", str, null, true, 1);
                                    },
                                    () => LoadRecsUI.GetIns().InitAndShow(string.Format("下载资源[{0}]}", arrAllBundles[i]), true, (i + 1) * 1f / arrAllBundles.Length)));


#if UNITY_EDITOR//测试
                yield return new WaitForSeconds(0.05f);
#endif
            }

            LoadRecsUI.GetIns().InitAndShow(string.Format("下载资源成功"), true, 1);
            yield return new WaitForSeconds(1);

            yield return null;
        }



        //比对本地和服务器数据记录下需要更新的资源
        IEnumerator ICheckAllBundles()
        {
            //获取服务器的AssetBundleManifest信息
            ManifestInfo serverInfo = new ManifestInfo();
            yield return StartCoroutine(serverInfo.Init(RecsPathCtrl.GetIns().GetPathManifest(false)));
            yield return StartCoroutine(serverInfo.GetBundlesInfo());

            //获取本地的AssetBundleManifest信息
            ManifestInfo LocalInfo = new ManifestInfo();
            yield return StartCoroutine(LocalInfo.Init("file://" + RecsPathCtrl.GetIns().GetPathManifest(true)));
            yield return StartCoroutine(LocalInfo.GetBundlesInfo());

            StringBuilder sb = new StringBuilder();
            //开始比对信息
            foreach (var key in serverInfo.dicBundlesInfo.Keys)
            {
                #region 存在性检查
                //不存在该资源(根据AssetBundleManifest的记录是否存在  如果误删部分文件 但是AssetBundleManifest信息还是之前的 就没有意义)
                if (!LocalInfo.dicBundlesInfo.ContainsKey(key))
                {
                    //记录进更新资源列表
                    sb.AppendLine(key);
                    continue;
                }

                //检查文件是否存在
                if (!File.Exists(RecsPathCtrl.GetIns().RecsRootPathLocal + key))
                {
                    //记录进更新资源列表
                    sb.AppendLine(key);
                    continue;
                }
                #endregion


                #region 一致性检查
                //资源HashID不一致
                if (!LocalInfo.dicBundlesInfo[key].Equals(serverInfo.dicBundlesInfo[key]))
                {
                    //记录进更新资源列表
                    sb.AppendLine(key);
                }
                #endregion
            }


            //将更新资源列表写入文件
            FileTool.WriteStringToFileByFileStream(RecsPathCtrl.GetIns().GetPathUpdateInfoFile(true), sb.ToString(), () => Log.LogAndSave("将资源更新列表写入文件" + RecsPathCtrl.GetIns().GetPathUpdateInfoFile(true)), Encoding.UTF8, false);

            yield return null;
        }


        //依据记录的资源更新列表覆盖更新本地资源
        IEnumerator IUpdateAllBundles()
        {
            string strUpdateInfo = FileTool.ReadFile(RecsPathCtrl.GetIns().GetPathUpdateInfoFile(true), Encoding.UTF8, () => Log.LogAndSave("读取资源更新列表完毕"));
            MsgUICtrl.GetIns().ShowMsg("读取资源更新列表完毕", strUpdateInfo, null, true, 0.5f);

            string[] updateList = strUpdateInfo.Split('\n', '\r');
            for (int i = 0; i < updateList.Length; i++)
            {
                if (string.IsNullOrEmpty(updateList[i].Trim()))
                    continue;

                LoadRecsUI.GetIns().InitAndShow(string.Format("更新资源[{0}]", updateList[i]), true, i * 1f / updateList.Length * 1f);
                yield return StartCoroutine(AssetBundleTool.DownloadAssetBundle(RecsPathCtrl.GetIns().RecsRootPathServer, updateList[i], RecsPathCtrl.GetIns().RecsRootPathLocal,
                       (error) => { MsgUICtrl.GetIns().ShowMsg("更新资源失败", error, null, true, 1); },
                   null
                   ));

#if UNITY_EDITOR//测试
                yield return new WaitForSeconds(0.05f);
#endif
            }
            LoadRecsUI.GetIns().InitAndShow(string.Format("更新资源完成"), true, 1);


            //刷新本地的Manifest
            //  yield return StartCoroutine(IUpdateManifestToLocal());


            yield return null;
        }



        //更新Manifest至本地
        IEnumerator IUpdateManifestToLocal()
        {
            WWW main = new WWW(RecsPathCtrl.GetIns().GetPathManifest(false));
            if (main == null)
            {
                MsgUICtrl.GetIns().ShowMsg("错误", "获取服务器数据失败", null);
                Log.LogAndSave("获取服务器数据失败");

                yield break;
            }

            yield return main;
            if (!string.IsNullOrEmpty(main.error))
            {
                Log.LogAndSave("下载资源失败 " + main.error);
                MsgUICtrl.GetIns().ShowMsg("下载资源失败", main.error);
                yield break;
            }

            //显示读取信息
            Log.LogAndSave("获取根数据成功");
            MsgUICtrl.GetIns().ShowMsg("获取根数据成功", "AssetBundleManifest读取成功", null, true);

            //加载
            AssetBundle ab = main.assetBundle;
            ManifestServer = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            ab.Unload(false);

            //将AssetBundle根文件数据写入缓存
            FileTool.WriteBytesToFileByBinary(RecsPathCtrl.GetIns().GetPathManifest(true), main.bytes);
        }


        IEnumerator ILoadAllBundles()
        {
            //--开始加载数据
            bool bWait = false;
            MsgUICtrl.GetIns().ShowMsg("开始加载数据", "加载本地数据", () => bWait = true, true, 3);
            //MsgUICtrl.GetIns().ShowMsg("开始加载数据", "加载本地数据", null, true, 3);
            Log.LogAndSave("开始加载数据");
            yield return new WaitUntil(() => bWait);



            WWW localMan = new WWW("file://" + RecsPathCtrl.GetIns().GetPathManifest(true));
            if (localMan == null)
            {
                yield break;
            }

            yield return localMan;
            if (!string.IsNullOrEmpty(localMan.error))
            {
                MsgUICtrl.GetIns().ShowMsg("加载资源失败", "Local Manifst丢失", null, true, 1);
                Log.LogAndSave("加载资源失败:Local Manifst丢失");
                ////下载资源
                //yield return StartCoroutine(IDownAllBundlesFromServerToLocal());

                ////加载本地资源
                //yield return StartCoroutine(ILoadAllBundles());

                yield break;
            }

            //获取本地的AssetBundlemanifest
            AssetBundle ab = localMan.assetBundle;
            if (ab == null)
            {
                MsgUICtrl.GetIns().ShowMsg("加载资源失败", "Local Manifst丢失", null, true, 1);
                Log.LogAndSave("加载资源失败:Local Manifst丢失");
                yield break;
            }

            AssetBundleManifest man = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            ab.Unload(false);
            if (man == null)
            {
                MsgUICtrl.GetIns().ShowMsg("加载资源失败", "资源包为空", null, true, 1);
                Log.LogAndSave("加载资源失败:资源包为空");

                yield break;
            }

            recsList.Clear();
            dicRecs.Clear();
            string[] arrBundles = man.GetAllAssetBundles();
            for (int i = 0; i < arrBundles.Length; i++)
            {
                LoadRecsUI.GetIns().InitAndShow(string.Format("加载{0}成功", arrBundles[i]), true, (float)i / arrBundles.Length);
                AssetBundle tempAB = null;
                yield return StartCoroutine(AssetBundleTool.ILoadAssetBundle(RecsPathCtrl.GetIns().RecsRootPathLocal + arrBundles[i], true,
                    (error) =>
                    {
                        MsgUICtrl.GetIns().ShowMsg("加载AssetBundle失败", error, null, true, 0.1f);
                        Log.LogAndSave(string.Format("加载{0}失败 ERROR:{1}", arrBundles[i], error));
                    },
                    (adLoad) =>
                    {
                        tempAB = adLoad;
                        LoadRecsUI.GetIns().InitAndShow(string.Format("加载{0}成功", tempAB.name), true, (float)(i + 1) / arrBundles.Length);
                        Log.LogAndSave(string.Format("加载{0}成功", tempAB.name));

                        //从AssetBundle中加载所有资源后释放AssetBundle
                        //AssetBundleRequest ar = tempAB.LoadAllAssetsAsync();
                        //yield return new WaitUntil(()=>ar.isDone);

                        recsList.AddRange(tempAB.LoadAllAssets());
                        //adLoad.Unload(false);
                    }
                    ));
            }




            yield return null;
        }




        /// <summary></summary>
        public class ManifestInfo
        {
            /// <summary></summary>
            public AssetBundleManifest Manifest;

            /// <summary>(名称,hash)</summary>
            public Dictionary<string, string> dicBundlesInfo = new Dictionary<string, string>();


            /// <summary></summary>
            public ManifestInfo() { }

            /// <summary></summary>
            public ManifestInfo(AssetBundleManifest manifest) { this.Manifest = manifest; }

            /// <summary></summary>
            public ManifestInfo(string strPathManifestBundleFile)
            {

            }


            /// <summary></summary>
            public IEnumerator Init(string strPathManifestBundleFile)
            {
                WWW main = new WWW(strPathManifestBundleFile);
                if (main == null)
                    yield break;

                yield return main;

                if (!string.IsNullOrEmpty(main.error))
                {
                    yield break;
                }

                AssetBundle ab = main.assetBundle;
                if (ab == null)
                {
                    yield break;
                }

                AssetBundleManifest m = main.assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                if (m == null)
                {
                    yield break;
                }

                ab.Unload(false);
                Manifest = m;

            }



            /// <summary></summary>
            public IEnumerator GetBundlesInfo()
            {
                if (Manifest == null)
                {
                    yield break;
                }

                string[] arrBundles = Manifest.GetAllAssetBundles();
                for (int i = 0; i < arrBundles.Length; i++)
                {
                    dicBundlesInfo.Add(arrBundles[i], Manifest.GetAssetBundleHash(arrBundles[i]).ToString());
                }

                yield return null;
            }


        }



        /// <summary>
        /// 获取资源来源的类型
        /// </summary>
        /// <returns></returns>
        public static RecsSource GetRecsSources()
        {

#if UNITY_EDITOR
            //return RecsSource.FormAssetBundleLocal;
            return RecsSource.FromRecsources;
#endif

#if TEST || EDITOR
        return RecsSource.FormAssetBundleServer;
#endif
            return RecsSource.FromRecsources;

            //#if UNITY_EDITOR
            //        return RecsSource.FromRecsources;
            //#endif

            //#if TEST || EDITOR
            //        return RecsSource.FormAssetBundleServer;
            //#endif
            //        return RecsSource.FormAssetBundleLocal;

        }





        //public static string GetRecsPath(string strPath,RecsSource targetRecsFrom)
        //{
        //    if (string.IsNullOrEmpty(strPath))
        //        return null;

        //    switch (targetRecsFrom)
        //    {
        //        case RecsSource.FromRecsources:
        //            return FileTool.GetFileNameWithoutExtension(strPath);
        //        case RecsSource.FormAssetBundleServer:
        //            break;
        //        case RecsSource.FormAssetBundleLocal:
        //            return asset
        //            break;
        //        default:
        //            break;
        //    }
        //}



    }



}
