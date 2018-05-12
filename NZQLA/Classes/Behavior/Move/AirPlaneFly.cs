using UnityEngine;
using System.Collections;
using NZQLA;


/// <summary>
/// ģ��ս������
/// W/'��' ��ǰ���� ;  S/'��' ����
/// A/'��' ����ת ����ת��
/// D/'��' ���ҷ�ת ����ת��
/// Q      ����
/// E       ����
/// F/"Fire1" ����
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
        //InputHelper.GetIns().AddKeyDownListener(KeyCode.W, () => { Log.LogAtUnityEditorNormal("������..."); });
        //InputHelper.GetIns().AddKeyDownListener(KeyCode.S, () => { Log.LogAtUnityEditorNormal("������..."); });
        //InputHelper.GetIns().AddKeyDownListener(KeyCode.A, () => { Log.LogAtUnityEditorNormal("����ת��..."); });
        //InputHelper.GetIns().AddKeyDownListener(KeyCode.D, () => { Log.LogAtUnityEditorNormal("���ҷ�ת��..."); });

        InputHelper.GetIns().AddListenerAxis(strAxisVertical, ActionMoveOnVerticalInput);
        InputHelper.GetIns().AddListenerAxis(strAxisHorizontal, ActionTurnOnHorizontalInput);
        InputHelper.GetIns().AddListenerAxis(strAxisUpDown, ActionTurnOnUpDown);

        //InputHelper.GetIns().AddListenerButtonPress(strButtonFire2, ActionOnInputFire2);
        //InputHelper.GetIns().AddListenerButtonRelease(strButtonFire2, ActionOnReleaseFire2);
        //InputHelper.GetIns().AddListenerAxis(strAxisMouseX, ActionTurnOnFire2AndDragFire1X);
        //InputHelper.GetIns().AddListenerAxis(strAxisMouseY, ActionTurnOnFire2AndDragFire1Y);


    }

    //���ٷ���/����
    void ActionMoveOnVerticalInput(float fInput)
    {
        transform.position += transform.forward * fInput * fSpeedMove * Time.deltaTime;
    }

    //ת��//��ת
    void ActionTurnOnHorizontalInput(float fInput)
    {
        //transform.Rotate(transform.forward, -2*fSpeedRota * fInput * Time.deltaTime);
        //transform.Rotate(Vector3.up, fSpeedRota * fInput * Time.deltaTime);
        transform.RotateAround(transform.position - 0.5f * transform.forward * fSizeLength, Vector3.up, fSpeedRota * fInput * Time.deltaTime);
    }

    //����/����
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
