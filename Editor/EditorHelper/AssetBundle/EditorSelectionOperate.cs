using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
using System.IO;
using Object = UnityEngine.Object;
using NZQLA;

/// <summary></summary>
[ExecuteInEditMode]
public class EditorSelectionOperate : MonoBehaviour
{
    /// <summary></summary>
    public event Action<Object[]> OnSelectAsset;



    [MenuItem("NZQLA/Selection/ShowSelections")]
    static void ShowSelction()
    {
        Object[] arr = Selection.objects;
        if (arr.isNull())
            return;

        arr.ActionAtItem<Object>((Object obj) =>
        {
            if (obj != null)
            {
                MyTool.LogOnlyAtEditor(obj.name + "   " + obj.GetType());
            }
        });
    }


    /// <summary></summary>
    [MenuItem("NZQLA/Selection/GetSelectAssets")]
    public static UnityEngine.Object[] GetSelectAssets()
    {
        UnityEngine.Object[] arr = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        if (arr == null || arr.Length == 0)
            return null;

        MyTool.LogOnlyAtEditor("选择了以下资源.......");
        arr.ActionAtItem<UnityEngine.Object>((UnityEngine.Object o) =>
        {
            if (null != o)
            {
                MyTool.LogOnlyAtEditor(AssetDatabase.GetAssetPath(o));
            }
        });

        return arr;
    }


    /// <summary></summary>
    [MenuItem("NZQLA/Selection/GetSelectAssetsDirectory")]
    public static UnityEngine.Object[] GetSelectAssetsDirectory()
    {
        UnityEngine.Object[] arr = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
        if (arr == null || arr.Length == 0)
            return null;



        MyTool.LogOnlyAtEditor("选择了以下资源.......");
        arr.ActionAtItem<UnityEngine.Object>((UnityEngine.Object o) =>
        {
            if (null != o)
            {
                MyTool.LogOnlyAtEditor(AssetDatabase.GetAssetPath(o));
            }
        });

        return arr;
    }

    Object[] arrRecsSelect = null;
    void Update()
    {
        Debug.Log("EditorSelectionOperate");


        arrRecsSelect = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
        if (arrRecsSelect != null)
        {
            if (OnSelectAsset != null)
            {
                OnSelectAsset(arrRecsSelect);
            }
        }


    }
}
