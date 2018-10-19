using UnityEngine;


/*
 *  移动，或者叫飘动、浮动
 *  
 *  在移动范围内，按照不超过最高速度的速度向随机方向移动
 *  
 *  在边界处速度方向不能向外
 *      1.在靠近边界的区域增加向内的加速度
 *          离开边界后要讲边界带来的加速度清除掉
 *              1.边界速度独立出来，进入中央区域后逐渐减少
 *              2.边界速度一直减少，在到达边界后才增加
 *              
 *              减少应该可以用 Lerp(Speed, 0, TIme.deltaTime)
 *              
 *      2.持续增加向中心的加速度
 *      
 *      3.在一定范围内设定一个目标点，向这个目标点方向增加速度，到达一定距离后重新随机一个目标点
 *      
 *  在内部速度和方向缓慢变化
 *      1.不断随机加速度
 *      2.每隔一定时间随机加速度
 */


public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    Border _moveBorder;
    [SerializeField]
    Border _adjustmentBorder;
    [SerializeField]
    Border _targetBorder;

    [SerializeField]
    float _maxAcceleration;
    [SerializeField]
    float _maxSpeed;

    [SerializeField]
    float _nextTargetDistance;

    Transform _transform;

    Vector2 _speed;
    Vector2 _borderSpeed;

    Vector2 _target;



    private void Awake()
    {
        _transform = transform;
    }


    private void Start()
    {
        GetNewTarget();
    }


    private void Update()
    {
        ComputeSpeed();
        AddBorderSpeed();
        Move();
        UpdateTarget();
    }


    void ComputeSpeed()
    {
        _speed += (Vector2)(Vector3.Normalize(_target - (Vector2)_transform.position)) * _maxAcceleration * Time.deltaTime;

        if (Vector2.Distance(Vector2.zero, _speed) > _maxSpeed)
            _speed = (Vector2)(Vector3.Normalize(_speed)) * _maxSpeed;
    }


    void AddBorderSpeed()
    {
        if (_transform.position.x < _adjustmentBorder.left)
            _speed.x += _maxAcceleration * Time.deltaTime;
        else if (_transform.position.x > _adjustmentBorder.right)
            _speed.x -= _maxAcceleration * Time.deltaTime;

        if (_transform.position.y < _adjustmentBorder.bottom)
            _speed.y += _maxAcceleration * Time.deltaTime;
        else if (_transform.position.y > _adjustmentBorder.top)
            _speed.y -= _maxAcceleration * Time.deltaTime;
    }


    void Move()
    {
        Vector3 newPosition = _transform.position + (Vector3)_speed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, _moveBorder.left, _moveBorder.right);

        _transform.Translate(_speed * Time.deltaTime, Space.World);
    }



    void UpdateTarget()
    {
        if (Vector2.Distance(_transform.position, _target) < _nextTargetDistance)
            GetNewTarget();
    }
    void GetNewTarget()
    {
        _target = new Vector2(Random.Range(_targetBorder.left, _targetBorder.right), Random.Range(_targetBorder.bottom, _targetBorder.top));
    }


    /*方法一
    private void Update()
    {
        UpdateSpeed();
        UpdateBorderSpeed();
        ClampSpeed();
        Move();
    }


    void UpdateSpeed()
    {
        Vector2 deltaSpeed = new Vector2(Random.Range(-_maxDeltaSpeed, _maxDeltaSpeed), Random.Range(-_maxDeltaSpeed, _maxDeltaSpeed)) * Time.deltaTime;

        _speed += deltaSpeed;
    }


    void UpdateBorderSpeed()
    {
        if (_transform.position.x > _adjustmentBorder.left && _transform.position.x < _adjustmentBorder.right)
            _borderSpeed.x = Mathf.Lerp(_borderSpeed.x, 0, Time.deltaTime);
        else if (_transform.position.x > _adjustmentBorder.right)
            _borderSpeed.x = Mathf.Lerp(_borderSpeed.x, -_maxSpeed, Time.deltaTime);
        else if (_transform.position.x < _adjustmentBorder.left)
            _borderSpeed.x = Mathf.Lerp(_borderSpeed.x, _maxSpeed, Time.deltaTime);
        
        if (_transform.position.y > _adjustmentBorder.bottom && _transform.position.y < _adjustmentBorder.top)
            _borderSpeed.y = Mathf.Lerp(_borderSpeed.y, 0, Time.deltaTime);
        else if (_transform.position.y > _adjustmentBorder.top)
            _borderSpeed.y = Mathf.Lerp(_borderSpeed.y, -_maxSpeed, Time.deltaTime);
        else if (_transform.position.y < _adjustmentBorder.bottom)
            _borderSpeed.y = Mathf.Lerp(_borderSpeed.y, _maxSpeed, Time.deltaTime);
    }


    void Move()
    {
        Vector3 newPosition = _transform.position + (Vector3)(_speed + _borderSpeed) * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, _moveBorder.left, _moveBorder.right);
        newPosition.y = Mathf.Clamp(newPosition.y, _moveBorder.bottom, _moveBorder.top);

        _transform.position = newPosition;
    }


    void ClampSpeed()
    {
        _speed.x = Mathf.Clamp(_speed.x, -_maxSpeed, _maxSpeed);
        _speed.y = Mathf.Clamp(_speed.y, -_maxSpeed, _maxSpeed);
    }
    */



    private void OnDrawGizmosSelected()
    {
        DrawMoveBorder();
        DrawAdjustmentBorder();
        DrawTargetBorder();
        Gizmos.DrawSphere(_target, 10);
    }

    void DrawMoveBorder()
    {
        Gizmos.color = Color.red;

        Vector3 upperRight = new Vector3(_moveBorder.right, _moveBorder.top, 0);
        Vector3 lowerRight = new Vector3(_moveBorder.right, _moveBorder.bottom, 0);
        Vector3 lowerLeft = new Vector3(_moveBorder.left, _moveBorder.bottom, 0);
        Vector3 upperLeft = new Vector3(_moveBorder.left, _moveBorder.top, 0);

        MyGizmos.DrawRectangle(upperRight, lowerRight, lowerLeft, upperLeft);
    }

    void DrawAdjustmentBorder()
    {
        Gizmos.color = Color.yellow;

        Vector3 upperRight = new Vector3(_adjustmentBorder.right, _adjustmentBorder.top, 0);
        Vector3 lowerRight = new Vector3(_adjustmentBorder.right, _adjustmentBorder.bottom, 0);
        Vector3 lowerLeft = new Vector3(_adjustmentBorder.left, _adjustmentBorder.bottom, 0);
        Vector3 upperLeft = new Vector3(_adjustmentBorder.left, _adjustmentBorder.top, 0);

        MyGizmos.DrawRectangle(upperRight, lowerRight, lowerLeft, upperLeft);
    }

    void DrawTargetBorder()
    {
        Gizmos.color = Color.green;

        Vector3 upperRight = new Vector3(_targetBorder.right, _targetBorder.top, 0);
        Vector3 lowerRight = new Vector3(_targetBorder.right, _targetBorder.bottom, 0);
        Vector3 lowerLeft = new Vector3(_targetBorder.left, _targetBorder.bottom, 0);
        Vector3 upperLeft = new Vector3(_targetBorder.left, _targetBorder.top, 0);

        MyGizmos.DrawRectangle(upperRight, lowerRight, lowerLeft, upperLeft);
    }
}
