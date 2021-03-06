﻿using UnityEngine;

/// <summary>
/// 分裂出环装弹幕
/// </summary>
public class FissionRing : BulletContorllerBase
{
    public GameObject bullet
    {
        get { return _bullet; }
        set { _bullet = value; }
    }
    [SerializeField]
    GameObject _bullet;

    public float fissionTime
    {
        get { return _fissionTime; }
        set { _fissionTime = value; }
    }
    [SerializeField]
    float _fissionTime;

    public int bulletsNumber
    {
        get { return _bulletsNumber; }
        set { _bulletsNumber = value; }
    }
    [SerializeField]
    int _bulletsNumber;


    float _doFissionTIme;


    //数据准备
    private void OnEnable()
    {
        _doFissionTIme = Time.time + _fissionTime;
    }


    //检测时间
    private void Update()
    {
        if (Time.time > _doFissionTIme)
        {
            Fission();
            Pool.SetBullet(gameObject);
        }
    }


    //分裂
    void Fission()
    {
        BarrageLauncher.ShotRing(_bullet, transform.position, transform.rotation, _bulletsNumber);
    }
}
