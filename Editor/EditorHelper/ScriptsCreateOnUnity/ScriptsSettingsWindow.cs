using UnityEngine;
using UnityEditor;
using System;

public class ScriptsSettingsWindow : EditorWindow
{
    public static void CollecteScriptSetting()
    {
        ScriptsSettingsWindow.CreateInstance<ScriptsSettingsWindow>().Show();
    }

    public event Action<string> OnSubmitNamespace;


    public void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        //命名空间
        ScriptCreateInitEditor.strNamenamespace = EditorGUILayout.DelayedTextField("命名空间名称", ScriptCreateInitEditor.strNamenamespace);

        //脚本头部说明
        EditorGUILayout.LabelField("脚本头部说明");
        ScriptCreateInitEditor.strHeadDescripe = EditorGUILayout.TextArea(ScriptCreateInitEditor.strHeadDescripe);


        //尾部头部说明
        EditorGUILayout.LabelField("尾部头部说明");
        ScriptCreateInitEditor.strTailDescripe = EditorGUILayout.TextArea(ScriptCreateInitEditor.strTailDescripe);



        if (GUILayout.Button("Close"))
            Close();

        EditorGUILayout.EndVertical();





    }



}