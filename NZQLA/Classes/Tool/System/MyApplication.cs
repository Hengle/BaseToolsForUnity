using UnityEngine;
using System.Collections;

namespace NZQLA
{
    /// <summary>
    /// 用于判定Unity Application的一些状态
    /// </summary>
    public class MyApplication
    {

        #region Runtime

        /// <summary>判定当前的运行环境是不是在Editor模式下</summary>
        /// <returns></returns>
        public static bool IsEditorRuntime()
        {
            return Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.OSXEditor;
        }


        #endregion


    }
}
