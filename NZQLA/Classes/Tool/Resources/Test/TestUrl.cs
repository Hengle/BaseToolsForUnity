using UnityEngine;
using System.Collections;
using NZQLA;
using NZQLA.Recs.AssetBundles;

/// <summary></summary>
public class TestUrl : MonoBehaviour
{
    /// <summary></summary>
    [ContextMenuItem("测试url的合法性", "testUrl", order = 1)]
    public string url = "File://C:/hhh.txt";


    /// <summary></summary>
    public string strFullPath, strFullPhysicalPath, strAssetPath, strAssetCutPath, strRecsoursePath;

    /// <summary></summary>
    public string strRecsPath = "Recs/";



    /// <summary></summary>
    public UnityEngine.Object recs;

    void Awake()
    {

    }

    [ContextMenu("位运算")]
    void TestWeiOperate()
    {
        Debug.Log(string.Format("1<<0 = {0}", 1 << 0));
        Debug.Log(string.Format("1<<1 = {0}", 1 << 1));
        Debug.Log(string.Format("1<<2 = {0}", 1 << 2));
        Debug.Log(string.Format("1<<3 = {0}", 1 << 3));
        Debug.Log(string.Format("1<<4 = {0}", 1 << 4));
        Debug.Log(string.Format("1<<5 = {0}", 1 << 5));
        Debug.Log(string.Format("1<<6 = {0}", 1 << 6));
        Debug.Log(string.Format("1<<7 = {0}", 1 << 7));
        Debug.Log(string.Format("1<<8 = {0}", 1 << 8));
        Debug.Log(string.Format("1<<9 = {0}", 1 << 9));
        Debug.Log(string.Format("1<<10 = {0}", 1 << 10));

    }


    void testUrl()
    {
        Debug.Log(string.Format("url:[{0}]{1}", url, FileTool.isUrlPath(url) ? "合法" : "非法"));
    }


    //[ContextMenu("GetAssetPathFromRecs")]
    //void GetAssetPathFromRecs()
    //{
    //    if (recs != null)
    //    {
    //        strAssetPath = UnityEditor.AssetDatabase.GetAssetPath(recs);
    //    }
    //}

    [ContextMenu("AssetPathToFullPath")]
    void AssetPathToFullPath()
    {

        strFullPath = FileTool.AssetPathToFullPath(strAssetPath);
    }

    [ContextMenu("FullPathToAssetPath")]
    void FullPathToAssetPath()
    {
        strAssetPath = FileTool.PhysccalFullPathToAssetPath(strFullPath);
    }


    [ContextMenu("AssetPathToAssetCutPath")]
    void AssetPathToAssetCutPath()
    {
        strAssetCutPath = FileTool.AssetPathToAssetCutPath(strAssetPath);
    }


    [ContextMenu("FullPhysicalPathToPhysicalUrl")]
    void FullPhysicalPathToPhysicalUrl()
    {
        strFullPhysicalPath = FileTool.FullPhysicalPathToPhysicalUrl(strFullPath);
    }


    [ContextMenu("PhysicalUrlToFullPhysicalPath")]
    void PhysicalUrlToFullPhysicalPath()
    {
        strFullPath = FileTool.PhysicalUrlToFullPhysicalPath(strFullPhysicalPath);
    }


    [ContextMenu("AssetCutPathToFullPath")]
    void AssetCutPathToFullPath()
    {
        strFullPath = FileTool.AssetCutPathToFullPath(strAssetCutPath);
    }

    [ContextMenu("ToRecsourcsPath")]
    void ToRecsourcsPath()
    {
        strRecsoursePath = RecsPathCtrl.GetIns().ToRecsourcsPath(strFullPhysicalPath);
    }


    /// <summary></summary>
    public string strSubstring = "abcSubstring123456789";

    [ContextMenu("TestSubstring")]
    void TestSubstring()
    {
        strSubstring = strSubstring.Substring("Substring".Length);
    }


    /// <summary></summary>
    public string strIndexOf = "strIndexOfabc12345678";

    [ContextMenu("TestIndexOf")]
    void TestIndexOf()
    {
        strIndexOf = strIndexOf.Substring(strIndexOf.IndexOf("abc"));
    }


    /// <summary></summary>
    public string strGetFilePathWithoutExtension = "strIndexOfabc12345678";

    [ContextMenu("GetFilePathWithoutExtension")]
    void GetFilePathWithoutExtension()
    {
        strGetFilePathWithoutExtension = FileTool.GetFilePathWithoutExtension(strGetFilePathWithoutExtension);
    }


    /// <summary></summary>
    public string strToRecsourcesPath = "Assets/Resources/Audio/123.mp3";

    [ContextMenu("ToRecsourcesPath")]
    void ToRecsourcesPath()
    {
        //strToRecsourcesPath = FileTool.ToRecsourcesPath(strToRecsourcesPath);
        strToRecsourcesPath = FileTool.PathCutString(strToRecsourcesPath, "Resources/", false, false);
    }




}
