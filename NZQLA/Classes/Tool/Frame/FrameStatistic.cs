using UnityEngine;
using System.Collections;

namespace NZQLA
{
    /// <summary>
    /// 帧数统计
    /// </summary>
    public class FrameStatistic : MonoBehaviour
    {
        /// <summary></summary>
        [Tooltip("帧数")]
        public int Frame;


        //统计帧数
        private int CountFrame;

        //临时时间
        private float fTimeDelta;


        //统计帧数
        void StatisticFrame()
        {
            if (fTimeDelta > 1)
            {
                Frame = CountFrame;
                CountFrame = 0;
                fTimeDelta = 0;
            }
            else
            {
                fTimeDelta += Time.deltaTime;
                CountFrame++;
            }

        }


        // Update is called once per frame
        void Update()
        {
            StatisticFrame();
        }

        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(Screen.width * 0.375f, 10, Screen.width * 0.25f, Screen.height * 0.25f);

        void OnGUI()
        {
            if (!MyApplication.IsEditorRuntime())
                return;

            style.fontSize = 30;
            GUI.TextArea(rect, "帧数：" + Frame.ToString(), style);
        }


    }
}
