using UnityEngine;

/// <summary>
/// 控制子弹转向的组件（顺时针）
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


    private void Start()
    {
        _bulletMove = GetComponent<BulletMoveUp>();

        _startRotateTime = Time.time + _startTime;
        _originEulerZ = _bulletMove.rotation.eulerAngles.z;
        _targetEulerZ = _originEulerZ - _angle;
        _endRotateTime = _startTime + _rotateTime;
    }


    private void Update()
    {
        if (Time.time < _startRotateTime) return;

        float currentEularZ = Mathf.Lerp(_originEulerZ, _targetEulerZ, Mathf.Min(1, Mathf.InverseLerp(_startRotateTime, _endRotateTime, Time.time)));

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, currentEularZ);
        _bulletMove.rotation = rotation;

        if (Time.time > _endRotateTime)
            Destroy(this);
    }


    public override void CopyToGameobject(GameObject target)
    {
        BulletRotate copy = target.AddComponent<BulletRotate>();

        copy.startTime = _startTime;
        copy.rotateTime = _rotateTime;
        copy.angle = _angle;
    }
}
