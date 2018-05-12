using UnityEngine;
using System.Collections;

/// <summary>
/// 测试使用Sin()控制对象的Scale
/// </summary>
public class SinGameObject : MonoBehaviour
{
    /// <summary></summary>
    public float fSpeed = 60;

    /// <summary></summary>
    public float fTime;
    // Use this for initialization
    void Start()
    {

    }

    Vector3 vScale = Vector3.one;
    // Update is called once per frame
    void Update()
    {
        fTime += Time.deltaTime;
        vScale.x = vScale.y = vScale.z = Mathf.Sin(fTime);

        transform.localScale = vScale;
    }
}
