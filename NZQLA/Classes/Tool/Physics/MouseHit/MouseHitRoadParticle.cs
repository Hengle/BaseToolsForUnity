/*
using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 鼠标点击路面的粒子效果
/// </summary>
public class MouseHitRoadParticle : MonoBehaviour, IPoolObjectPrefab<MouseHitRoadParticle>,IRequareComment<MouseHitRoadParticle,ParticleSystem>,ISetData<Vector3>
{
    private ParticleSystem _self;

    public ParticleSystem self
    {
        get
        {
            return _self;
        }
    }


    public event Action OnDestoryHandler;
    public event Action OnAction;


    public MouseHitRoadParticle CreateInstance(MouseHitRoadParticle prefab)
    {
        return Instantiate<MouseHitRoadParticle>(prefab);             
    }

    public void EnsureHasComponent()
    {
        _self = gameObject.EnsureHasComponent<ParticleSystem>();
    }

    public void ResetInstance(MouseHitRoadParticle prefab)
    {
        transform.Copy(prefab.transform,false);
        gameObject.SetActive(false);
    }

    public void SetData(Vector3 data)
    {
        gameObject.SetActive(true);
        transform.position = data;
    }

    void Awake()
    {
        EnsureHasComponent();
        OnDestoryHandler += OnDestory;
    }

    // Use this for initialization
    void Start()
    {

    }


    void OnDestory()
    {
        gameObject.SetActive(false);
        ObjectPooll<MouseHitRoadParticle>.GetIns().RecoveryItem(this);
    }


    void OnEnable()
    {
        StartCoroutine(DestorySelfAfterSconds());
    }

    IEnumerator DestorySelfAfterSconds()
    {
        yield return new WaitUntil(()=> !self.isPlaying);

        if (OnDestoryHandler != null)
        {
            OnDestoryHandler();
        }
    }



}
*/