using UnityEngine;

/// <summary>
/// 改变子弹速度到指定值的脚本，有缺陷，因为用了Lerp，如果两个变速控制器一起生效后执行的会覆盖先执行的，修改思路：在移动控制器里增加变速接口，但在设计逻辑上这个事情发生概率不是很大
/// </summary>
[RequireComponent(typeof(BulletMoveUp))]
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

    BulletMoveUp _bulletMove;

    float _startChangeTime;
    float _endChangeTime;
    float _originSpeed;


    //初始化
    private void Awake()
    {
        _bulletMove = GetComponent<BulletMoveUp>();
    }


    //数据准备
    private void OnEnable()
    {
        _startChangeTime = Time.time + _startTime;
        _endChangeTime = _startTime + _changeTime;
        _originSpeed = _bulletMove.speed;
    }


    //变速
    private void Update()
    {
        if (Time.time < _startChangeTime) return;

        _bulletMove.speed = Mathf.Lerp(_originSpeed, _targetSpeed, Mathf.Min(1, Mathf.InverseLerp(_startChangeTime, _endChangeTime, Time.time)));

        if (Time.time > _endChangeTime)
            enabled = false;
    }


    //复位
    public override void Restore()
    {
        enabled = true;
    }
}
