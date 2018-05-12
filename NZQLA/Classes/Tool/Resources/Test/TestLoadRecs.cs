using UnityEngine;
using System.Collections;
using NZQLA.Recs.AssetBundles;
using NZQLA.Recs;

/// <summary></summary>
public class TestLoadRecs : MonoBehaviour
{
    /// <summary></summary>
    public string strPath;

    /// <summary></summary>
    public Object asset;

    /// <summary></summary>
    public Object[] assets;

    /// <summary></summary>
    public RecsPathType pathType;

    [ContextMenu("测试加载")]
    void LoadAsset()
    {
        asset = RecsLoader.LoadRecs<Object>(strPath, pathType, (str) => Debug.Log(str),
            (obj) =>
            {
                Debug.Log(string.Format("加载资源[{0}]成功", obj.name));
                AssetBundle ab = obj as AssetBundle;
                asset = ab.mainAsset;
                assets = ab.LoadAllAssets();
                ab.Unload(false);
            });
    }


    //E:/LQ/NZQLA/Fight/Unity/AssetBundle/ResourcesFrame/AssetsResources/recs/graphics/effect/arrow

    /// <summary></summary>
    public RecsLoadInfoAssetBundle infoLoad;

    /// <summary></summary>
    public Object assetLoadByInfo;

    /// <summary></summary>
    [ContextMenu("加载指定的资源")]
    public void LoadRecsByIndo()
    {
        assetLoadByInfo = RecsLoader.LoadRecs<Object>(infoLoad,(o)=> {
            assetLoadByInfo = o as Object;
        });
    }



}
