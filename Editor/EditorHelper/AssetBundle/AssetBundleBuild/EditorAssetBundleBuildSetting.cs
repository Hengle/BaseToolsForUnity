using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Text;

/// <summary>
/// AssetBundle打包设置
/// 资源选择方式： 使用鼠标点选需要打包的资源(这样更灵活、文件(夹)数量不限)
/// 打包设置：在对应的Window进行各种配置(压缩模式、目标平台、依赖包含、输出路径等)
/// </summary>
public class EditorAssetBundleBuildSetting : MonoBehaviour
{
    /// <summary></summary>
    public static AssetBundleBuildSetting buildSetting = new AssetBundleBuildSetting();


    [MenuItem("NZQLA/MsgWindow")]
    static void TestMsgWindow()
    {
        MsgWindow.ShowSelf(new WindowMsgData("TestMsgWindow", "这是一个消息窗口", WindowType.Utility));
    }



    [MenuItem("NZQLA/AssetBundle/BuildSetting",priority =1)]
    static void BuildSetting()
    {
        AssetBundleBuildSettingWindow.ShowSelf();
    }

    #region 设置压缩模式
    [MenuItem("NZQLA/AssetBundle/Setting/SetBundleOptions/默认模式(LZMA模式、压缩率大、解压慢)")]
    static void SetBundleOptionsOragin()
    {
        buildSetting.BuildAssetBundleOptions = BuildAssetBundleOptions.None;
    }


    [MenuItem("NZQLA/AssetBundle/Setting/SetBundleOptions/LZ4模式(压缩率小、解压快)")]
    static void SetBundleOptionsLZ4()
    {
        buildSetting.BuildAssetBundleOptions = BuildAssetBundleOptions.ChunkBasedCompression;
    }


    [MenuItem("NZQLA/AssetBundle/Setting/SetBundleOptions/不压缩")]
    static void SetBundleOptionsUnCompress()
    {
        buildSetting.BuildAssetBundleOptions = BuildAssetBundleOptions.UncompressedAssetBundle;
    }
    #endregion


    #region 设置打包平台
    [MenuItem("NZQLA/AssetBundle/Setting/SetBuildTarget/StandaloneWindows")]
    static void SetBuildTargetStandaloneWindows()
    {
        buildSetting.BuildTarget = BuildTarget.StandaloneWindows;
    }


    [MenuItem("NZQLA/AssetBundle/Setting/SetBuildTarget/Android")]
    static void SetBuildTargetAndroid()
    {
        buildSetting.BuildTarget = BuildTarget.Android;
    }


    [MenuItem("NZQLA/AssetBundle/Setting/SetBuildTarget/iPhone")]
    static void SetBuildTargetiOS()
    {
        buildSetting.BuildTarget = BuildTarget.iOS;
    }
    #endregion


    #region 设置路径
    /// <summary></summary>
    [MenuItem("NZQLA/AssetBundle/Setting/SetBuildPathUnderAsset")]
    public static void ShowSetPathWindow()
    {
        AssetBundleBuildSettingWindow.ShowSelf();
    }


    /// <summary>设置输出路径的根目录(Asset下)</summary>
    /// <param name="strPath"></param>
    public static void SetPath(string strPath)
    {
        buildSetting.strPathBuildUnderAsset = strPath;
    }

    #endregion


    #region 配置文件
    [MenuItem("AssetBundle/Setting/SettingConfigFile(可忽略)/BackUpSettings")]
    static void BackUpSettings()
    {
        if (null == buildSetting)
            buildSetting = new AssetBundleBuildSetting();

        SerializeObjToXML<AssetBundleBuildSetting>(buildSetting, Application.dataPath + "/Editor/Config/Settings/AssetBundleBuildSetting.xml");
        AssetDatabase.Refresh();
    }

    [MenuItem("AssetBundle/Setting/SettingConfigFile(可忽略)/ReadSettings")]
    static void LoadSettings()
    {
        if (null == buildSetting)
            buildSetting = new AssetBundleBuildSetting();
        AssetBundleBuildSetting AS = DeserializeObjFromXML<AssetBundleBuildSetting>(Application.dataPath + "/Editor/Config/Settings/AssetBundleBuildSetting.xml");
        if (null != AS)
        {
            buildSetting = AS;
        }
    }
    #endregion






    /// <summary>AssetBundle打包设置数据对象</summary>
    [XmlType]
    public class AssetBundleBuildSetting
    {
        /// <summary>输出路径的根目录(Asset下)</summary>
        [XmlElement]
        public string strPathBuildUnderAsset;

        /// <summary>压缩类型</summary>
        [XmlElement]
        public BuildAssetBundleOptions BuildAssetBundleOptions = BuildAssetBundleOptions.None;

        /// <summary>打包平台</summary>
        [XmlElement]
        public BuildTarget BuildTarget = BuildTarget.StandaloneWindows;

        /// <summary>是否包含依赖关系</summary>
        [XmlElement]
        public bool ContainDependences;

        /// <summary></summary>
        public AssetBundleBuildSetting()
        {
            strPathBuildUnderAsset = "";
            BuildAssetBundleOptions = BuildAssetBundleOptions.None;
            BuildTarget = BuildTarget.StandaloneWindows;
            ContainDependences = true;
        }

        /// <summary></summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("BuildSetting：压缩类型-{0} 目标平台-{1} 包含依赖关系-{2} 输出路径根目录-{3}", BuildAssetBundleOptions, BuildTarget, ContainDependences, strPathBuildUnderAsset);
            return sb.ToString();
        }
    }




    #region XML序列化
    /// <summary>将对象序列化至指定文件</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="strDestPath"></param>
    /// <param name="type">补充类型 比如子类类型(解决子类序列化失败的问题)</param>
    public static void SerializeObjToXML<T>(T obj, string strDestPath, params Type[] type) where T : class
    {
        if (string.IsNullOrEmpty(strDestPath))
            return;

        //判定文件夹是否存在
        if (!Directory.Exists(Path.GetDirectoryName(strDestPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(strDestPath));
        }

        FileStream FS = new FileStream(strDestPath, FileMode.Create, FileAccess.Write);
        XmlSerializer ser = new XmlSerializer(typeof(T), type);
        ser.Serialize(FS, obj);
        FS.Close();


    }


    /// <summary>将制定路径的XML反序列化为指定类型的对象</summary>
    /// <typeparam name="T">指定类型</typeparam>
    /// <param name="strRecsPath">指定路径</param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static T DeserializeObjFromXML<T>(string strRecsPath, params Type[] type)
    {
        if (string.IsNullOrEmpty(strRecsPath))
            return default(T);

        FileStream FS = new FileStream(strRecsPath, FileMode.Open, FileAccess.Read);
        XmlSerializer ser = new XmlSerializer(typeof(T), type);
        object obj = ser.Deserialize(FS);

        T tttt;
        if (obj == null)
            tttt = default(T);
        else
            tttt = (T)obj;

        FS.Close();

        return tttt;
    }
    #endregion
}
