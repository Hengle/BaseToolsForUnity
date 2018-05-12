/*
using UnityEngine;
using System.Collections;

/// <summary>
/// 监听鼠标点击命令 并在鼠标点击位置展示粒子效果
/// </summary>
public class ShowMouseHitPos : MonoSingtonAuto<ShowMouseHitPos>
{
    // Use this for initialization
    void Start()
    {
        MouseHit.GetIns().OnMouseHitPosHandler += OnMouseHitPos;
    }

    void OnMouseHitPos(Vector3 vPosHit)
    {
        ObjectPooll<MouseHitRoadParticle>.GetIns().GetOneItem().SetData(vPosHit);
    }
}
*/
