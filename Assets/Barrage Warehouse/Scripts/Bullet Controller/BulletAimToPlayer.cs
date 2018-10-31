using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAimToPlayer : BulletContorllerBase
{
    public float startTime
    {
        get { return _startTime; }
        set { _startTime = value; }
    }
    [SerializeField]
    float _startTime;

    public float aimTime
    {
        get { return _aimTime; }
        set { _aimTime = value; }
    }
    [SerializeField]
    float _aimTime;

    BulletMoveUp _bulletMove;

    float _startAimTime;
    float _endAimTime;

    Transform _playerTransform;


    private void Awake()
    {
        _bulletMove = GetComponent<BulletMoveUp>();
    }


    private void OnEnable()
    {
        _startAimTime = Time.time + _startTime;
        _endAimTime = _startAimTime + _aimTime;

        _playerTransform = BarrageBase.GetPlayerTransform();
    }


    private void Update()
    {
        if (Time.time < _startAimTime) return;

        if (_playerTransform != null)
        {
            float rate = Mathf.Min(1, Mathf.InverseLerp(_startAimTime, _endAimTime, Time.time));
            
            Quaternion targetRotation = BarrageBase.GetAimRotation(_bulletMove.transform.position, _playerTransform.position);
            _bulletMove.rotation = Quaternion.Lerp(_bulletMove.rotation, targetRotation, rate);
        }
        else
        {
            _playerTransform = BarrageBase.GetPlayerTransform();
        }

        if (Time.time > _endAimTime)
            enabled = false;
    }


    //复位
    public override void Restore()
    {
        enabled = true;
    }




    private void OnValidate()
    {
        if (_aimTime < 0.001f)      //瞄准时间不能为0，如果瞄准时间为0那么瞄准起止时间相同，计算比率时就会出错
            _aimTime = 0.001f;
    }
}
