using UnityEngine;
using System.Collections;
using UnityEditor;
using NZQLA;

/// <summary></summary>
public class MsgWindow : EditorWindow
{
    private static WindowMsgData data;


    /// <summary></summary>
    public static void ShowSelf<T>(T data1) where T : WindowMsgData
    {
        if (data1 == null)
            return;
        data = data1;

        var window = MsgWindow.CreateInstance<MsgWindow>();

        Setting(window);


        switch (data.windowType)
        {
            case WindowType.Common:
                window.Show();
                break;
            case WindowType.Utility:
                window.ShowUtility();
                break;
            case WindowType.Popup:
                window.ShowPopup();
                break;
            case WindowType.Aux:
                window.ShowAuxWindow();
                break;
            default:
                break;
        }
    }


    void OnGUI()
    {
        Debug.Log("OnGUI");

        EditorGUILayout.BeginVertical();



        EditorGUILayout.TextField("Msg:", data.strMsgContent, GUILayout.ExpandHeight(true));

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
            window.titleContent = new GUIContent(data.strWindowTittle);
            window.minSize = new Vector2(500, 100);
            window.maxSize = new Vector2(800, 120);
        }
    }


}

/// <summary></summary>
public enum WindowType
{
    /// <summary></summary>
    Common,

    /// <summary></summary>
    Utility,

    /// <summary></summary>
    Popup,

    /// <summary></summary>
    Aux,
}
