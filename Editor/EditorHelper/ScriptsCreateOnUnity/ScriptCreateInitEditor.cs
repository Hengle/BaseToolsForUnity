//**********************************
//作者: #AuthorName#
//创建时间: 2018-03-16 16:16:36
//备注：修改注释
//**********************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;

public class ScriptCreateInitEditor : UnityEditor.AssetModificationProcessor
{
    public List<OperateDataItem> OperateDataList = new List<OperateDataItem>();

    public static string strNamenamespace = "NZQLA";
    public static string strHeadDescripe = "";
    public static string strTailDescripe = "";


    [MenuItem("EditorTool/SettinsScriptsOnCreate")]
    static void SettinsScriptsOnCreate()
    {
        ScriptsSettingsWindow.CollecteScriptSetting();
    }

    private static void OnWillCreateAsset(string path)
    {

        path = path.Replace(".meta", "");

        if (path.EndsWith(".cs"))
        {

            string content = File.ReadAllText(path);

            content = content.Replace("2018-03-16 16:16:36", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Replace("#namespace#", strNamenamespace);

            content = string.Format("{0}{1}{2}", strHeadDescripe, content, strTailDescripe);

            File.WriteAllText(path, content);

            AssetDatabase.Refresh();

        }
    }


    public class OperateDataItem
    {
        public OperateType type;
    }

    public class OperateReplaceStr : OperateDataItem
    {
        public string strSource;
        public string strValue;
    }

    public class OperateInsert : OperateDataItem
    {
        public InsertType type;
        public string strValue;
    }


    public enum OperateType
    {
        Replace,
        Insert,
    }

    public enum InsertType
    {
        AtHead,
        AtTail,
    }


}
