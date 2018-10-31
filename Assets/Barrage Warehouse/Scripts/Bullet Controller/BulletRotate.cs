using UnityEngine;

/// <summary>
/// 控制子弹转向的组件（顺时针），有缺陷，因为旋转用的是Lerp，所以如果两个旋转组件同时作用就会导致后执行的覆盖先执行的，需要时进行改进，很简单，移动控制器已经留了转向接口
/// </summary>
[RequireComponent(typeof(BulletMoveUp))]
public class BulletRotate : BulletContorllerBase
{
    public float startTime
    {
        get { return _startTime; }
        set { _startTime = value; }
    }
    [SerializeField]
    float _startTime;

    public float rotateTime
    {
        get { return _rotateTime; }
        set { _rotateTime = value; }
    }
    [SerializeField]
    float _rotateTime;

    public float angle
    {
        get { return _angle; }
        set { _angle = value; }
    }
    [SerializeField]
    float _angle;

    BulletMoveUp _bulletMove;

    float _startRotateTime;
    float _endRotateTime;
    float _originEulerZ;
    float _targetEulerZ;


    private void Awake()
    {
        _bulletMove = GetComponent<BulletMoveUp>();
    }


    private void OnEnable()
    {
        _startRotateTime = Time.time + _startTime;
        _endRotateTime = _startTime + _rotateTime;

        _originEulerZ = _bulletMove.rotation.eulerAngles.z;
        _targetEulerZ = _originEulerZ - _angle;
    }


    private void Update()
    {
        if (Time.time < _startRotateTime) return;

        float currentEularZ = Mathf.Lerp(_originEulerZ, _targetEulerZ, Mathf.Min(1, Mathf.InverseLerp(_startRotateTime, _endRotateTime, Time.time)));
        
        _bulletMove.rotation = Quaternion.Euler(new Vector3(0, 0, currentEularZ));

        if (Time.time > _endRotateTime)
            enabled = false;
    }


    //复位
    public override void Restore()
    {
        enabled = true;
    }
}
