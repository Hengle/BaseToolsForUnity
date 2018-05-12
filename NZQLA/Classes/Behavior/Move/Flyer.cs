using UnityEngine;
using System.Collections;
using NZQLA;


/// <summary>
/// W,Sǰ/���ƶ�
/// A,D����/���ƶ�
/// �Ҽ�����ʱ�������ݵ���������ƫ���޸�ǰ������
/// </summary>
public class Flyer : MonoBehaviour
{
    public float fSpeedMove = 10;
    public float fSpeedRota = 1;
    public bool OnPressFire2;
    public float QuickMutiply = 2f;
    [Header("Input Settings")]
    public string strAxisVertical = "Vertical";
    public string strAxisHorizontal = "Horizontal";
    public string strButtonFire2 = "Fire2";
    public string strAxisMouseX = "Mouse X";
    public string strAxisMouseY= "Mouse Y";


    private void Awake()
    {
        //InputHelper.GetIns().AddKeyDownListener(KeyCode.W, () => { Log.LogAtUnityEditorNormal("������..."); });
        //InputHelper.GetIns().AddKeyDownListener(KeyCode.S, () => { Log.LogAtUnityEditorNormal("������..."); });
        //InputHelper.GetIns().AddKeyDownListener(KeyCode.A, () => { Log.LogAtUnityEditorNormal("����ת��..."); });
        //InputHelper.GetIns().AddKeyDownListener(KeyCode.D, () => { Log.LogAtUnityEditorNormal("���ҷ�ת��..."); });

        InputHelper.GetIns().AddListenerAxis(strAxisVertical, ActionMoveOnVerticalInput);
        InputHelper.GetIns().AddListenerAxis(strAxisHorizontal, ActionMoveOnHorizontalInput);
        InputHelper.GetIns().AddListenerButtonPress(strButtonFire2, ActionOnInputFire2);
        InputHelper.GetIns().AddListenerButtonRelease(strButtonFire2, ActionOnReleaseFire2);
        InputHelper.GetIns().AddListenerAxis(strAxisMouseX, ActionTurnOnFire2AndDragFire1X);
        InputHelper.GetIns().AddListenerAxis(strAxisMouseY, ActionTurnOnFire2AndDragFire1Y);


    }


    void ActionMoveOnVerticalInput(float fInput)
    {
        transform.position += transform.forward * fInput * fSpeedMove * Time.deltaTime;
    }

    void ActionMoveOnHorizontalInput(float fInput)
    {
        transform.position += transform.right * fInput * fSpeedMove * Time.deltaTime;
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
