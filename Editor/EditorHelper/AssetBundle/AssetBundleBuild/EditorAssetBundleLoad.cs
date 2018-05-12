using UnityEngine;
using UnityEditor;

/// <summary></summary>
public class EditorAssetBundleLoad : ScriptableObject
{
    /// <summary></summary>
    [MenuItem("NZQLA/AssetBundle/Load/SelectTargetFile")]
    public static void SelectBundleFile()
    {
        LoadAssetBundleWindow.ShowSelf();
    }

}