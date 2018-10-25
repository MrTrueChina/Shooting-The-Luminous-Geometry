﻿/*
 *  执行顺序要在所有子弹控制器之前，抢在所有控制器执行Start开始控制前把他们都关掉
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownEffect : BulletContorllerBase
{
    /*
     *  第一步是抢先一步关闭所有子弹控制器
     *  
     *  特效是一定时间内的渐显和缩小
     */
    [SerializeField]
    Sprite _effectSprite;
    [SerializeField]
    float _effectStartDiameter;
    [SerializeField]
    float _effectEndDiameter;
    [SerializeField]
    float _effectTime;


    SpriteRenderer _renderer;
    float _effectStartTime;
    float _effectEndTime;


    //关闭所有控制器、创建特效物体
    private void Start()
    {
        DisableBulletControllers();
        CreatRenerer();

        _renderer.sprite = _effectSprite;
        _effectStartTime = Time.time;
        _effectEndTime = Time.time + _effectTime;
    }
    void DisableBulletControllers()   //假设有的控制器本来就是关闭的会不会被重新打开
    {
        BulletContorllerBase[] controllers = GetComponents<BulletContorllerBase>();
        foreach (BulletContorllerBase controller in controllers)
            if (controller != this)
                controller.enabled = false;
    }
    void CreatRenerer()
    {
        GameObject effectObject = new GameObject("Effect Object");
        effectObject.transform.position = transform.position;
        effectObject.transform.localScale = Vector3.one * _effectStartDiameter;
        _renderer = effectObject.AddComponent<SpriteRenderer>();
    }


    //播放特效
    private void Update()
    {
        UpdateDiameter();
        UpdateTransparency();

        if (Time.time > _effectEndTime)
            EffectEnd();
    }
    void UpdateDiameter()
    {
        float diameterRate = Mathf.Min(1, Mathf.InverseLerp(_effectStartTime, _effectEndTime, Time.time));
        float diameter = Mathf.Lerp(_effectStartDiameter, _effectEndDiameter, diameterRate);
        _renderer.transform.localScale = Vector3.one * diameter;
    }
    void UpdateTransparency()
    {
        float transparencyRate = Mathf.Min(1, Mathf.InverseLerp(_effectStartTime, _effectEndTime, Time.time));
        float transparency = Mathf.Lerp(0, 2, transparencyRate);

        Color spriteColor = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, transparency);
        _renderer.color = spriteColor;
    }



    //特效播放完毕，销毁特效物体，启动所有控制器，销毁特效脚本
    void EffectEnd()
    {
        Destroy(_renderer.gameObject);

        EnableComponents();
        Destroy(this);
    }
    void EnableComponents()
    {
        BulletContorllerBase[] controllers = GetComponents<BulletContorllerBase>();
        foreach (BulletContorllerBase controller in controllers)
            if (controller != this)
                controller.enabled = true;
    }
}
