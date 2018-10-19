using UnityEngine;

/// <summary>
/// 改变子弹速度的脚本
/// </summary>
[RequireComponent(typeof(MoveUp))]
public class BulletSpeedChange : BulletContorllerBase
{
    public float startTime
    {
        get { return _startTime; }
        set { _startTime = value; }
    }
    [SerializeField]
    float _startTime;

    public float changeTime
    {
        get { return _changeTime; }
        set { _changeTime = value; }
    }
    [SerializeField]
    float _changeTime;

    public float targetSpeed
    {
        get { return _targetSpeed; }
        set { _targetSpeed = value; }
    }
    [SerializeField]
    float _targetSpeed;

    MoveUp _bulletMove;

    float _startChangeTime;
    float _endChangeTime;
    float _originSpeed;


    private void Start()
    {
        _bulletMove = GetComponent<MoveUp>();

        _startChangeTime = Time.time + _startTime;
        _endChangeTime = _startTime + _changeTime;
        _originSpeed = _bulletMove.speed;
    }


    private void Update()
    {
        if (Time.time < _startChangeTime) return;

        _bulletMove.speed = Mathf.Lerp(_originSpeed, _targetSpeed, Mathf.Min(1, Mathf.InverseLerp(_startChangeTime, _endChangeTime, Time.time)));

        if (Time.time > _endChangeTime)
            Destroy(this);
    }


    public override void CopyToGameobject(GameObject target)
    {
        BulletSpeedChange copy = target.AddComponent<BulletSpeedChange>();

        copy.startTime = _startTime;
        copy.changeTime = _changeTime;
        copy.targetSpeed = _targetSpeed;
    }
}
