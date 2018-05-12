using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NZQLA;




public delegate void OnKeyDown();
public delegate void OnKeyPress();
public delegate void OnKeyRelease();
public delegate void OnAxis(float fAxisValue);
public delegate void OnButtonDown();
public delegate void OnButtonPress();
public delegate void OnButtonRelease();

/// <summary>
/// 用于监听用户的输入
/// 单例脚本
/// 支持订阅相关输入消息
/// </summary>
public class InputHelper : MonoSingtonAuto<InputHelper>
{
    public bool LogOnListen = false;

    #region Cache input observer
    private Dictionary<KeyCode, OnKeyDown> dicListenerKeyDown = new Dictionary<KeyCode, OnKeyDown>();
    private Dictionary<KeyCode, OnKeyPress> dicListenerKeyPress = new Dictionary<KeyCode, OnKeyPress>();
    private Dictionary<KeyCode, OnKeyRelease> dicListenerKeyUp = new Dictionary<KeyCode, OnKeyRelease>();
    private Dictionary<string, OnAxis> dicListenerAxis = new Dictionary<string, OnAxis>();
    private Dictionary<string, OnButtonDown> dicListenerButtonDown = new Dictionary<string, OnButtonDown>();
    private Dictionary<string, OnButtonPress> dicListenerButtonPress = new Dictionary<string, OnButtonPress>();
    private Dictionary<string, OnButtonRelease> dicListenerButtonRelease = new Dictionary<string, OnButtonRelease>();
    #endregion



    private void Update()
    {

        ListenKeyDown();

        ListenKeyPress();

        ListenKeyUp();

        ListenAxis();

        ListenButtonDown();

        ListenButtonPress();

        ListenButtonRelease();
    }



    #region Rigiser/UnRigister Listener
    public void AddListenerKeyDown(KeyCode key, OnKeyDown action)
    {
        if (dicListenerKeyDown == null)
            dicListenerKeyDown = new Dictionary<KeyCode, OnKeyDown>();

        if (dicListenerKeyDown.ContainsKey(key))
        {
            if (dicListenerKeyDown[key] == null)
                dicListenerKeyDown[key] = action;
            dicListenerKeyDown[key] += action;
        }
        else
        {
            dicListenerKeyDown.Add(key, action);
        }
    }

    public void RemoveListenerKeyDown(KeyCode key)
    {
        if (dicListenerKeyDown == null || !dicListenerKeyDown.ContainsKey(key))
            return;

        dicListenerKeyDown.Remove(key);
    }


    public void AddListenerKeyPress(KeyCode key, OnKeyPress action)
    {
        if (dicListenerKeyPress == null)
            dicListenerKeyPress = new Dictionary<KeyCode, OnKeyPress>();

        if (dicListenerKeyPress.ContainsKey(key))
        {
            if (dicListenerKeyPress[key] == null)
                dicListenerKeyPress[key] = action;
            dicListenerKeyPress[key] += action;
        }
        else
        {
            dicListenerKeyPress.Add(key, action);
        }
    }

    public void RemoveListenerKeyPress(KeyCode key)
    {
        if (dicListenerKeyPress == null || !dicListenerKeyPress.ContainsKey(key))
            return;

        dicListenerKeyPress.Remove(key);
    }


    public void AddListenerKeyRelease(KeyCode key, OnKeyRelease action)
    {
        if (dicListenerKeyUp == null)
            dicListenerKeyUp = new Dictionary<KeyCode, OnKeyRelease>();

        if (dicListenerKeyUp.ContainsKey(key))
        {
            if (dicListenerKeyUp[key] == null)
                dicListenerKeyUp[key] = action;
            dicListenerKeyUp[key] += action;
        }
        else
        {
            dicListenerKeyUp.Add(key, action);
        }
    }

    public void RemoveListenerKeyRelease(KeyCode key)
    {
        if (dicListenerKeyUp == null || !dicListenerKeyUp.ContainsKey(key))
            return;

        dicListenerKeyUp.Remove(key);
    }






    public void AddListenerAxis(string AxisName, OnAxis action)
    {
        if (dicListenerAxis == null)
            dicListenerAxis = new Dictionary<string, OnAxis>();

        if (dicListenerAxis.ContainsKey(AxisName))
        {
            if (dicListenerAxis[AxisName] == null)
                dicListenerAxis[AxisName] = action;
            dicListenerAxis[AxisName] += action;
        }
        else
        {
            dicListenerAxis.Add(AxisName, action);
        }
    }

    public void RemoveListenerAxis(string AixsName)
    {
        if (dicListenerAxis == null || !dicListenerAxis.ContainsKey(AixsName))
            return;

        dicListenerAxis.Remove(AixsName);
    }





    public void AddListenerButtonDown(string butonName, OnButtonDown action)
    {
        if (dicListenerButtonDown == null)
            dicListenerButtonDown = new Dictionary<string, OnButtonDown>();

        if (dicListenerButtonDown.ContainsKey(butonName))
        {
            if (dicListenerButtonDown[butonName] == null)
                dicListenerButtonDown[butonName] = action;
            dicListenerButtonDown[butonName] += action;
        }
        else
        {
            dicListenerButtonDown.Add(butonName, action);
        }
    }

    public void RemoveListenerButtonDown(string butonName)
    {
        if (dicListenerButtonDown == null || !dicListenerButtonDown.ContainsKey(butonName))
            return;

        dicListenerButtonDown.Remove(butonName);
    }


    public void AddListenerButtonRelease(string butonName, OnButtonRelease action)
    {
        if (dicListenerButtonRelease == null)
            dicListenerButtonRelease = new Dictionary<string, OnButtonRelease>();

        if (dicListenerButtonRelease.ContainsKey(butonName))
        {
            if (dicListenerButtonRelease[butonName] == null)
                dicListenerButtonRelease[butonName] = action;
            dicListenerButtonRelease[butonName] += action;
        }
        else
        {
            dicListenerButtonRelease.Add(butonName, action);
        }
    }

    public void RemoveListenerButtonRelease(string butonName)
    {
        if (dicListenerButtonRelease == null || !dicListenerButtonRelease.ContainsKey(butonName))
            return;

        dicListenerButtonRelease.Remove(butonName);
    }


    public void AddListenerButtonPress(string butonName, OnButtonPress action)
    {
        if (dicListenerButtonPress == null)
            dicListenerButtonPress = new Dictionary<string, OnButtonPress>();

        if (dicListenerButtonPress.ContainsKey(butonName))
        {
            if (dicListenerButtonPress[butonName] == null)
                dicListenerButtonPress[butonName] = action;
            dicListenerButtonPress[butonName] += action;
        }
        else
        {
            dicListenerButtonPress.Add(butonName, action);
        }
    }

    public void RemoveListenerButtonPress(string butonName)
    {
        if (dicListenerButtonPress == null || !dicListenerButtonPress.ContainsKey(butonName))
            return;

        dicListenerButtonPress.Remove(butonName);
    }

    #endregion



    #region Listen
    //监听输入:KeyDown
    private void ListenKeyDown()
    {
        if (dicListenerKeyDown.isNull())
            return;

        foreach (KeyCode key in dicListenerKeyDown.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                if (dicListenerKeyDown[key] != null)
                {
                    dicListenerKeyDown[key]();
                    if (LogOnListen)
                        Log.LogAtUnityEditor(string.Format("Key Down :[{0}]", key), "#ffffffff");
                }
            }
        }
    }

    //监听输入:KeyPress
    private void ListenKeyPress()
    {
        if (dicListenerKeyPress.isNull())
            return;

        foreach (KeyCode key in dicListenerKeyPress.Keys)
        {
            if (Input.GetKey(key))
            {
                if (dicListenerKeyPress[key] != null)
                {
                    dicListenerKeyPress[key]();
                    if (LogOnListen)
                        Log.LogAtUnityEditor(string.Format("Key Press :[{0}]", key), "#ffffffff");
                }
            }
        }
    }

    //监听输入:KeyUp
    private void ListenKeyUp()
    {
        if (dicListenerKeyUp.isNull())
            return;

        foreach (KeyCode key in dicListenerKeyUp.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                if (dicListenerKeyUp[key] != null)
                {
                    dicListenerKeyUp[key]();
                    if (LogOnListen)
                        Log.LogAtUnityEditor(string.Format("Key Up :[{0}]", key), "#ffffffff");
                }
            }
        }
    }


    private float fAxisValueTemp = 0;
    //监听输入:Axis变化
    private void ListenAxis()
    {
        if (dicListenerAxis.isNull())
            return;

        foreach (var key in dicListenerAxis.Keys)
        {
            fAxisValueTemp = Input.GetAxis(key);
            if (dicListenerAxis[key] != null)
                dicListenerAxis[key](fAxisValueTemp);

            if (LogOnListen)
                Log.LogAtUnityEditor(string.Format("Axis[{0}]:[{1}]", key, fAxisValueTemp), "#ffffffff");
        }
    }


    //监听输入:ButtonDown
    private void ListenButtonDown()
    {
        if (dicListenerButtonDown.isNull())
            return;

        foreach (var key in dicListenerButtonDown.Keys)
        {
            if (Input.GetButtonDown(key))
            {
                if (dicListenerButtonDown[key] != null)
                {
                    dicListenerButtonDown[key]();
                    if (LogOnListen)
                        Log.LogAtUnityEditor(string.Format("Button Down :[{0}]", key), "#ffffffff");
                }
            }
        }
    }


    //监听输入:ButtonPress
    private void ListenButtonPress()
    {
        if (dicListenerButtonPress.isNull())
            return;

        foreach (var button in dicListenerButtonPress.Keys)
        {
            if (Input.GetButton(button))
            {
                if (dicListenerButtonPress[button] != null)
                {
                    dicListenerButtonPress[button]();
                    if (LogOnListen)
                        Log.LogAtUnityEditor(string.Format("Button Press :[{0}]", button), "#ffffffff");
                }
            }
        }
    }

    //监听输入:ButtonRelease
    private void ListenButtonRelease()
    {
        if (dicListenerButtonRelease.isNull())
            return;

        foreach (var button in dicListenerButtonRelease.Keys)
        {
            if (Input.GetButtonUp(button))
            {
                if (dicListenerButtonRelease[button] != null)
                {
                    dicListenerButtonRelease[button]();
                    if (LogOnListen)
                        Log.LogAtUnityEditor(string.Format("Button Release :[{0}]", button), "#ffffffff");
                }
            }
        }
    }

    #endregion

}
