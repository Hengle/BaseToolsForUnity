using UnityEngine;
using UnityEditor;
using NZQLA.Recs.AssetBundles;

/// <summary>
/// 加载AssetBundle的窗口
/// </summary>
public class LoadAssetBundleWindow : EditorWindow
{
    /// <summary></summary>
    public string strBundleFile;

    /// <summary></summary>
    public static void ShowSelf()
    {
        LoadAssetBundleWindow window = LoadAssetBundleWindow.CreateInstance<LoadAssetBundleWindow>();

        if (window)
        {
            window.ShowUtility();
        }
    }


    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextField("选择要加载的文件", strBundleFile);
        if (GUILayout.Button("SelectBundleFile"))
        {
            string strPath = EditorUtility.OpenFilePanel("选择要加载的文件", Application.streamingAssetsPath, "*");
            if (strPath != null)
            {
                strBundleFile = strPath;
                AssetBundleLoadTest.GetIns().LoadTargetBundle(strPath);
            }
        }
        EditorGUILayout.EndHorizontal();

    }



}