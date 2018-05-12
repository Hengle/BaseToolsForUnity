using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

namespace NZQLA.EditorHelper
{
    /// <summary>创建项目开发基本需要的文件夹</summary>
    public class EditorTool_CreateNecessaryDirecory : MonoBehaviour
    {

        [MenuItem("EditorTool/CreateNecessaryDirecory")]
        static void CreateNecessaryDirecory()
        {
            CreateDirectory(Application.dataPath + "/Editor");
            CreateDirectory(Application.dataPath + "/Plugins");
            CreateDirectory(Application.dataPath + "/Plugins/Editor");
            CreateDirectory(Application.dataPath + "/Scenes");
            CreateDirectory(Application.dataPath + "/Scripts");
            CreateDirectory(Application.dataPath + "/Model");
            CreateDirectory(Application.dataPath + "/Resources");
            CreateDirectory(Application.dataPath + "/Gizmos");
            CreateDirectory(Application.streamingAssetsPath);

            AssetDatabase.Refresh();
        }


        //创建指定文件夹
        static void CreateDirectory(string strPath)
        {
            if (string.IsNullOrEmpty(strPath))
                return;


            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);
        }


    }
}
