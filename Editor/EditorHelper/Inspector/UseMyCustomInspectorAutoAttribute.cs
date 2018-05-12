using UnityEngine;
using System.Collections;
using System;


/// <summary>被标记的脚本将自动的使用统一的自定义检视视图重写方案</summary>
[AttributeUsage(AttributeTargets.Class)]
public class UseMyCustomInspectorAutoAttribute : Attribute
{
    /// <summary>是否使用自定义的属性显示方案</summary>
    public bool bUseProperty = false;

    /// <summary>是否使用自定义的ContextMenu显示方案</summary>
    public bool bUseContextMenu = true;

    /// <summary>默认不使用属性显示/使用ContextMenu显示</summary>
    public UseMyCustomInspectorAutoAttribute()
    {
        bUseProperty = false;
        bUseContextMenu = true;
    }

    /// <summary></summary>
    /// <param name="bUseProperty"></param>
    /// <param name="bUseContextMenu"></param>
    public UseMyCustomInspectorAutoAttribute(bool bUseProperty, bool bUseContextMenu)
    {
        this.bUseProperty = bUseProperty;
        this.bUseContextMenu = bUseContextMenu;
    }
}
