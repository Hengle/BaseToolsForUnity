using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using NZQLA;

/// <summary>
/// 消息提示UI面板控制
/// 通过配置显示一个消息面板
/// 配置选项(标题、内容、关闭选项)
/// </summary>
public class MsgUICtrl : MonoSingtonUnAuto<MsgUICtrl>
{
    /// <summary></summary>
    public GameObject objPanel;

    /// <summary></summary>
    public Text TittleUI;

    /// <summary></summary>
    public Text ContentUI;

    /// <summary></summary>
    public Button BtnOK;

    /// <summary></summary>
    public bool bNeedCloseDelay;

    /// <summary></summary>
    public float fTimeDelayWaitClose = 0.3f;

    /// <summary></summary>
    public event Action BtnOKHandler;



    /// <summary></summary>
    public override void Awake()
    {
        base.Awake();
        ShowOrHide(false);
    }


    // Use this for initialization
    void Start()
    {
        if (BtnOK)
        {
            BtnOK.onClick.AddListener(OnClickBtnOK);
        }
    }

    float fTimeTemp = 0;
     void Update()
    {
        if (bNeedCloseDelay)
        {
            Closedelay();
        }
    }

    private void Closedelay()
    {
        if (fTimeTemp >= fTimeDelayWaitClose)
        {
            SimluateClickbtnOK();
        }
        fTimeTemp += Time.deltaTime;
    }



    #region 外调

    /// <summary>显隐控制</summary>
    /// <param name="bShow"></param>
    public void ShowOrHide(bool bShow)
    {
        objPanel.SetActive(bShow);
    }


    /// <summary>外调 初始化并且显示</summary>
    /// <param name="strTittle">标题</param>
    /// <param name="strContent">内容</param>
    /// <param name="btnOK">按钮回调</param>
    /// <param name="bNeedCloseDelay">是否需要延时关闭</param>
    /// <param name="fTimeDelayWaitClose">延时时间</param>
    public void ShowMsg(string strTittle, string strContent, Action btnOK = null, bool bNeedCloseDelay = false, float fTimeDelayWaitClose = 0.5f)
    {
        if (TittleUI)
        {
            TittleUI.text = strTittle;
        }

        if (ContentUI)
        {
            ContentUI.text = strContent;
        }

        if (btnOK != null)
        {
            BtnOKHandler += btnOK;
        }

        ShowOrHide(true);

        this.bNeedCloseDelay = bNeedCloseDelay;
        this.fTimeDelayWaitClose = fTimeDelayWaitClose;
        if (bNeedCloseDelay)
        {
            fTimeTemp = 0;
        }
    }


    /// <summary>模拟点击按钮[OK]</summary>
    public void SimluateClickbtnOK()
    {
        OnClickBtnOK();
    }


    #endregion






    void OnClickBtnOK()
    {
        gameObject.SetActive(false);
        if (BtnOKHandler != null)
        {
            BtnOKHandler();
            BtnOKHandler = null;
        }
    }
}
