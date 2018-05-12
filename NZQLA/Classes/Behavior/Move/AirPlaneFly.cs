using UnityEngine;
using System.Collections;
using NZQLA;


/// <summary>
/// 模拟战机飞行
/// W/'↑' 向前飞行 ;  S/'↓' 减速
/// A/'←' 向左翻转 向左转弯
/// D/'→' 向右翻转 向右转弯
/// Q      爬升
/// E       俯冲
/// F/"Fire1" 攻击
/// </summary>
public class AirPlaneFly : MonoBehaviour
{
    public float fSpeedMove = 10;
    public float fSpeedRota = 1;
    public bool OnPressFire2;
    public float QuickMutiply = 2f;
    public float fSizeLength = 10;
    public float fSizeWidth = 10;
    [Header("Input Settings")]
    public string strAxisVertical = "Vertical";
    public string strAxisHorizontal = "Horizontal";
    public string strButtonFire2 = "Fire2";
    public string strAxisMouseX = "Mouse X";
    public string strAxisMouseY = "Mouse Y";
    public string strAxisUpDown = "UpDown";



    private void Awake()
    {
        //InputHelper.GetIns().AddKeyDownListener(KeyCode.W, () => { Log.LogAtUnityEditorNormal("加速中..."); });
        //InputHelper.GetIns().AddKeyDownListener(KeyCode.S, () => { Log.LogAtUnityEditorNormal("减速中..."); });
        //InputHelper.GetIns().AddKeyDownListener(KeyCode.A, () => { Log.LogAtUnityEditorNormal("向左翻转中..."); });
        //InputHelper.GetIns().AddKeyDownListener(KeyCode.D, () => { Log.LogAtUnityEditorNormal("向右翻转中..."); });

        InputHelper.GetIns().AddListenerAxis(strAxisVertical, ActionMoveOnVerticalInput);
        InputHelper.GetIns().AddListenerAxis(strAxisHorizontal, ActionTurnOnHorizontalInput);
        InputHelper.GetIns().AddListenerAxis(strAxisUpDown, ActionTurnOnUpDown);

        //InputHelper.GetIns().AddListenerButtonPress(strButtonFire2, ActionOnInputFire2);
        //InputHelper.GetIns().AddListenerButtonRelease(strButtonFire2, ActionOnReleaseFire2);
        //InputHelper.GetIns().AddListenerAxis(strAxisMouseX, ActionTurnOnFire2AndDragFire1X);
        //InputHelper.GetIns().AddListenerAxis(strAxisMouseY, ActionTurnOnFire2AndDragFire1Y);


    }

    //加速飞行/减速
    void ActionMoveOnVerticalInput(float fInput)
    {
        transform.position += transform.forward * fInput * fSpeedMove * Time.deltaTime;
    }

    //转向//翻转
    void ActionTurnOnHorizontalInput(float fInput)
    {
        //transform.Rotate(transform.forward, -2*fSpeedRota * fInput * Time.deltaTime);
        //transform.Rotate(Vector3.up, fSpeedRota * fInput * Time.deltaTime);
        transform.RotateAround(transform.position - 0.5f * transform.forward * fSizeLength, Vector3.up, fSpeedRota * fInput * Time.deltaTime);
    }

    //爬升/俯冲
    void ActionTurnOnUpDown(float fInput)
    {
        //transform.Rotate(transform.right, -fSpeedRota * fInput * Time.deltaTime,Space.Self);

        transform.RotateAround(transform.position - 0.5f * transform.forward * fSizeLength, Vector3.right, fSpeedRota * fInput * Time.deltaTime);
    }





    void ActionTurnOnFire2AndDragFire1X(float fFire1X)
    {
        if (OnPressFire2)
            transform.Rotate(transform.up, fSpeedRota * fFire1X * Time.deltaTime);
    }

    void ActionTurnOnFire2AndDragFire1Y(float fFire1Y)
    {
        if (OnPressFire2)
            transform.Rotate(transform.right, -fSpeedRota * fFire1Y * Time.deltaTime);
    }


    void ActionOnInputFire2()
    {
        OnPressFire2 = true;
        Log.LogAtUnityEditorNormal("Input: Press Fire2");
    }

    void ActionOnReleaseFire2()
    {
        OnPressFire2 = false;
        Log.LogAtUnityEditorNormal("Input: Release Fire2");
    }


}
