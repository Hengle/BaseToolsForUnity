using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace NZQLA
{
    /// <summary>
    /// 加载界面UI面板
    /// </summary>
    public class LoadRecsUI : MonoSingtonUnAuto<LoadRecsUI>
    {
        /// <summary></summary>
        [Tooltip("标题")]
        public Text TittleUI;

        /// <summary></summary>
        [Tooltip("进度条")]
        public Slider ProcessUI;

        /// <summary></summary>
        public GameObject objPanel;

        /// <summary></summary>
        public override void Awake()
        {
            base.Awake();

            if (TittleUI == null)
            {
                TittleUI = transform.FindChild("LoadingInfoPanel/Tittle").gameObject.GetComponent<Text>();
                ProcessUI = transform.FindChild("LoadingInfoPanel/Process").gameObject.GetComponent<Slider>();
            }

            ShowOrHide(false);
            ShowOrHideProcess(false);
        }



        /// <summary>设置标题</summary>
        /// <param name="strTittle"></param>
        public void SetTittle(string strTittle)
        {
            if (TittleUI)
            {
                TittleUI.text = strTittle;
            }


        }

        /// <summary>设置进度</summary>
        /// <param name="fProcess"></param>
        public void SetProcess(float fProcess)
        {
            if (ProcessUI)
            {
                ProcessUI.value = fProcess;
            }
        }

        /// <summary>显隐控制</summary>
        /// <param name="bShow">显示(true)</param>
        public void ShowOrHide(bool bShow)
        {
            objPanel.SetActive(bShow);
        }

        /// <summary>配置并显示</summary>
        /// <param name="strTittle">指定标题</param>
        /// <param name="bShowProcess">是否需要进度条</param>
        /// <param name="fProcess"></param>
        public void InitAndShow(string strTittle, bool bShowProcess = true, float fProcess = 0f)
        {
            SetTittle(strTittle);

            if (bShowProcess)
            {
                SetProcess(fProcess);
            }
            ShowOrHideProcess(bShowProcess);
            ShowOrHide(true);
        }

        /// <summary>初始化标题、进度 并显示进度条</summary>
        /// <param name="strTittle">指定标题</param>
        public void ShowProcessInit(string strTittle)
        {
            SetTittle(strTittle);
            SetProcess(0);
        }



        /// <summary>显隐控制(进度)</summary>
        /// <param name="bShow">显示(true)</param>
        public void ShowOrHideProcess(bool bShow)
        {
            ProcessUI.gameObject.SetActive(bShow);
        }



        #region 测试
        //float fProcess = 0;
        //public void Update()
        //{

        //    fProcess += Time.deltaTime;
        //    fProcess = Mathf.Clamp01(fProcess);

        //    SetTittle("加载中......");
        //    SetProcess(fProcess);
        //}
        #endregion
    }
}
