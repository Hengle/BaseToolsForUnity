using UnityEngine;
using System.Collections;
using UnityEditor;
using NZQLA;

/// <summary>
/// AssetBundle打包配置窗口
/// </summary>
public class AssetBundleBuildSettingWindow : EditorWindow
{

    /// <summary></summary>
    public static void ShowSelf()
    {
        var window = AssetBundleBuildSettingWindow.CreateInstance<AssetBundleBuildSettingWindow>();

        Setting(window);

        window.ShowUtility();
    }


    void OnGUI()
    {
        EditorGUILayout.BeginVertical();


        //EditorAssetBundleBuildSetting.buildSetting.strPathBuildUnderAsset = EditorGUILayout.DelayedTextField("输出路径根目录(StreamingAssets下)", EditorAssetBundleBuildSetting.buildSetting.strPathBuildUnderAsset,GUILayout.ExpandWidth(true));

        //指定是否包含依赖关系
        EditorAssetBundleBuildSetting.buildSetting.ContainDependences = EditorGUILayout.Toggle("是否包含依赖关系", EditorAssetBundleBuildSetting.buildSetting.ContainDependences);

        //设置压缩模式
        EditorAssetBundleBuildSetting.buildSetting.BuildAssetBundleOptions = (BuildAssetBundleOptions)EditorGUILayout.EnumPopup("压缩类型", EditorAssetBundleBuildSetting.buildSetting.BuildAssetBundleOptions);

        //设置目标平台
        EditorAssetBundleBuildSetting.buildSetting.BuildTarget = (BuildTarget)EditorGUILayout.EnumPopup("目标平台", EditorAssetBundleBuildSetting.buildSetting.BuildTarget);

        //选择输出路径
        EditorGUILayout.BeginHorizontal();
        EditorAssetBundleBuildSetting.buildSetting.strPathBuildUnderAsset = EditorGUILayout.DelayedTextField("输出路径根目录(StreamingAssets下)", EditorAssetBundleBuildSetting.buildSetting.strPathBuildUnderAsset, GUILayout.ExpandWidth(true));
        if (GUILayout.Button("Select"))
        {
            var path = EditorUtility.OpenFolderPanel("输出路径根目录(StreamingAssets下)", EditorAssetBundleBuildSetting.buildSetting.strPathBuildUnderAsset, Application.streamingAssetsPath);
            if (path != null)
            {
                EditorAssetBundleBuildSetting.buildSetting.strPathBuildUnderAsset = path;
                MyTool.LogOnlyAtEditor("选择OutPath : " + path);
            }
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        //点击关闭按钮
        if (GUILayout.Button("Close"))
        {
            MyTool.LogOnlyAtEditor(EditorAssetBundleBuildSetting.buildSetting.ToString());
            Close();
        }

        EditorGUILayout.EndVertical();
    }




    static void Setting(EditorWindow window)
    {
        if (window)
        {
            //window.maximized = false;
            window.wantsMouseEnterLeaveWindow = true;
            window.titleContent = new GUIContent("AssetBunld参数配置");
            window.minSize = new Vector2(500, 100);
            window.maxSize = new Vector2(800, 120);
        }
    }

}
