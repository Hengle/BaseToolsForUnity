using UnityEngine;
using UnityEditor;

/// <summary></summary>
public class MsgWindowData : ScriptableObject
{

}




/// <summary></summary>
public class WindowMsgData
{
    /// <summary></summary>
    public string strWindowTittle;

    /// <summary></summary>
    public string strMsgContent;

    /// <summary></summary>
    public WindowType windowType;

    /// <summary></summary>
    public WindowMsgData() : this("MsgWindow", "Msg......", WindowType.Common)
    {

    }

    /// <summary></summary>
    /// <param name="strWindowTittle"></param>
    /// <param name="strMsgContent"></param>
    /// <param name="windowType"></param>
    public WindowMsgData(string strWindowTittle, string strMsgContent, WindowType windowType)
    {
        this.strWindowTittle = strWindowTittle;
        this.strMsgContent = strMsgContent;
        this.windowType = windowType;
    }
}
