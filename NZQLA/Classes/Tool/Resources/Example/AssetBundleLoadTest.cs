using UnityEngine;
using System.Collections;
using System.IO;
using NZQLA;

namespace NZQLA.Recs.AssetBundles
{
    /// <summary>
    /// AssetBundle加载
    /// </summary>
    public class AssetBundleLoadTest : MonoSingtonAuto<AssetBundleLoadTest>
    {
        /// <summary></summary>
        public AssetBundle bundleLoad;

        /// <summary></summary>
        public UnityEngine.Object[] arrRecsLoad;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("哈哈哈哈");
            }
        }

        /// <summary>加载指定的AssetBundle</summary>
        /// <param name="strPath">指定AssetBundle的路径</param>
        public void LoadTargetBundle(string strPath)
        {
            StartCoroutine(LoadBundle(strPath));
        }

        IEnumerator LoadBundle(string strPath)
        {
            if (string.IsNullOrEmpty(strPath) || !File.Exists(strPath))
                yield break;

            AssetBundleCreateRequest ar = AssetBundle.LoadFromFileAsync(strPath);
            if (ar == null)
                yield break;

            yield return new WaitUntil(() => ar.isDone);
            bundleLoad = ar.assetBundle;
            Debug.Log(string.Format("加载Bundle完毕 Name:{0} 路径:{1}", bundleLoad.name, strPath));


            yield return null;
        }

        //从Bundle加载资源
        IEnumerator LoadRecsFromBundle(AssetBundle bundle)
        {
            if (bundle == null)
                yield break;

            //string[] arrRecsNames = bundle.GetAllAssetNames();
            AssetBundleRequest ar = bundle.LoadAllAssetsAsync();

            if (ar == null)
                yield break;

            yield return new WaitUntil(() => ar.isDone);

            arrRecsLoad = ar.allAssets;


            Debug.Log(string.Format("从Bundle加载资源完毕"));
        }



        //void Test()
        //{
        //    for (int i = 0; i < arrRecsLoad.Length; i++)
        //    {
        //        if (arrRecsLoad[i] is RuntimeAnimatorController)
        //        {
        //            GameObject obj = new GameObject(arrRecsLoad[i].name);
        //            obj.AddComponent<SpriteRenderer>();
        //            obj.AddComponent<Animator>().runtimeAnimatorController = arrRecsLoad[i] as RuntimeAnimatorController;
        //            Debug.Log("添加Ani");
        //        }
        //    }
        //}


    }
}
