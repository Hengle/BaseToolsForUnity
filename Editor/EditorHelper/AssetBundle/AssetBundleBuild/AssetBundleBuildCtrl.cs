using UnityEngine;
using UnityEditor;
using System.IO;
using NZQLA;

/// <summary>
/// AssetBundle创建管理器
/// 默认文件以当前文件路径的包含关系打包
/// </summary>
public class AssetBundleBuildCtrl : MonoBehaviour
{
    [MenuItem("NZQLA/AssetBundle/将鼠标选定的资源打包AssetBundle", priority = 2)]
    static void BuildSelect()
    {
        ClearAllBundleName();
        SetTargetAssetBundleNames(EditorSelectionOperate.GetSelectAssets(), EditorAssetBundleBuildSetting.buildSetting.ContainDependences);
        BuildAssetBundle();
    }

    [MenuItem("NZQLA/AssetBundle/将已经设置好Bundle属性的资源打包")]
    static void BuildAssetBundle()
    {
        CreateAssetBundle(EditorAssetBundleBuildSetting.buildSetting.strPathBuildUnderAsset,
           EditorAssetBundleBuildSetting.buildSetting.BuildAssetBundleOptions,
           EditorAssetBundleBuildSetting.buildSetting.BuildTarget);
    }


    [MenuItem("NZQLA/AssetBundle/清空所有的Bundle名称")]
    static void ClearAllBundleName()
    {
        string[] str = AssetDatabase.GetAllAssetBundleNames();
        for (int i = 0; i < str.Length; i++)
        {
            AssetDatabase.RemoveAssetBundleName(str[i], true);
        }
    }


    /// <summary>将当前项目中所有的Bundle打包到指定文件夹</summary>
    static void CreateAssetBundle(string outputPath, BuildAssetBundleOptions buildAssetBundleOptions, BuildTarget buildTarget)
    {
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        //if(AssetDatabase.GetAllAssetBundleNames().isNull())
        //    Debug.

        BuildPipeline.BuildAssetBundles(outputPath, buildAssetBundleOptions, buildTarget);
        AssetDatabase.Refresh();
    }



    /// <summary>设置指定文件夹下资源的打包名称</summary>
    /// <param name="strRecsDirectoryPath">资源主路径(绝对路径)</param>
    /// <param name="ContainDependences">资源包中是否包含依赖资源的标志 True分离打包 False整体打包</param>
    public static void SetTargetDirectoryBundleName(string strRecsDirectoryPath, bool ContainDependences = true)
    {
        if (string.IsNullOrEmpty(strRecsDirectoryPath) || !Directory.Exists(strRecsDirectoryPath))
        {
            Directory.CreateDirectory(strRecsDirectoryPath);
        }


        //获取资源包文件夹信息
        DirectoryInfo floderInfo = new DirectoryInfo(strRecsDirectoryPath);
        //获取该文件夹下的所有文件
        FileInfo[] files = floderInfo.GetFiles("*", SearchOption.AllDirectories);
        //为每个文件进行设置AssetBundle名称
        for (int i = 0; i < files.Length; i++)
        {
            //测试
            SetTargetFileBundleName(files[i].FullName, true);
        }
    }


    /// <summary>设置指定文件的Bundle名称</summary>
    /// <param name="strRecsFullFilePath">指定文件路径(完整路径)</param>
    /// <param name="ContainDependences"></param>
    public static void SetTargetFileBundleName(string strRecsFullFilePath, bool ContainDependences = true)
    {
        if (string.IsNullOrEmpty(strRecsFullFilePath) || !File.Exists(strRecsFullFilePath) || strRecsFullFilePath.EndsWith(".meta"))
            return;

        strRecsFullFilePath = strRecsFullFilePath.Trim();

        string strPathRelative = FileTool.PhysccalFullPathToAssetPath(strRecsFullFilePath);
        //string strPathRelative = "Assets" + strRecsFullFilePath.Substring(Application.dataPath.Length);
        AssetImporter aiMain = AssetImporter.GetAtPath(strPathRelative);
        if (aiMain == null)
            return;


        string strNameBundle = FileTool.RemovePathExtension(strRecsFullFilePath).Substring(Application.dataPath.Length + 1).Replace("\\", "/").ToLower();
        //清除之前可能已经存在的BundleName
        AssetDatabase.RemoveAssetBundleName(strNameBundle, true);
        aiMain.assetBundleName = strNameBundle;

        //处理依赖项
        if (ContainDependences)
        {
            string[] arrDenpen = AssetDatabase.GetDependencies(strPathRelative);
            for (int i = 0; i < arrDenpen.Length; i++)
            {
                if (arrDenpen[i].EndsWith(".cs") || arrDenpen[i].Contains(strPathRelative)) continue;

                SetTargetFileBundleName(string.Format("{0}/{1}", Application.dataPath, FileTool.AssetPathToAssetCutPath(arrDenpen[i])), false);


            }
        }

    }



    /// <summary>设置指定文件的Bundle名称</summary>
    /// <param name="strRecsFullFilePath">指定文件路径(完整路径)</param>
    /// <param name="ContainDependences"></param>
    public static void SetTargetFilesBundleNames(string[] strRecsFullFilePath, bool ContainDependences = true)
    {
        if (strRecsFullFilePath == null || strRecsFullFilePath.Length == 0)
            return;

        strRecsFullFilePath.ActionAtItem<string>((string path) =>
        {
            SetTargetFileBundleName(path, ContainDependences);
        });
    }


    /// <summary>设置指定资源的Bundle名称</summary>
    /// <param name="arrAssets">指定文件路径(完整路径)</param>
    /// <param name="ContainDependences"></param>
    public static void SetTargetAssetBundleNames(UnityEngine.Object[] arrAssets, bool ContainDependences = true)
    {
        if (arrAssets == null || arrAssets.Length == 0)
            return;

        arrAssets.ActionAtItem<UnityEngine.Object>((UnityEngine.Object asset) =>
        {
            SetTargetFileBundleName(FileTool.AssetPathToFullPath(AssetDatabase.GetAssetPath(asset)), ContainDependences);
        });
    }


}
