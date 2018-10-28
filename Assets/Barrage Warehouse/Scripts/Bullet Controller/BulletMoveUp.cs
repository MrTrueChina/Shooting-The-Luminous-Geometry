using UnityEngine;

/// <summary>
/// 控制子弹移动的组件
/// </summary>
public class BulletMoveUp : BulletContorllerBase
{
    [SerializeField]
    Border _destroyBorder;
    public float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    [SerializeField]
    float _speed;
    public Quaternion rotation
    {
        get { return _transform.rotation; }
        set { _transform.rotation = value; }
    }
    
    Transform _transform;


    float _originSpeed;      //最初始的速度，因为有改变速度的控制器，所以要在入库的时候把速度改回来



    //初始化
    private void Awake()
    {
        _transform = transform;
        _originSpeed = _speed;
    }



    //移动和越界检测
    private void Update()
    {
        Move();
        CheckAndSetToPool();
    }
    
    void Move()
    {
        _transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }
    
    void CheckAndSetToPool()
    {
        if (OutOfBorder())
            Pool.SetBullet(gameObject);
    }
    bool OutOfBorder()
    {
        return _transform.position.x > _destroyBorder.right || _transform.position.x < _destroyBorder.left || _transform.position.y > _destroyBorder.top || _transform.position.y < _destroyBorder.bottom;
    }



    //转向
    public void Rotate(float angle)
    {
        _transform.Rotate(_transform.forward, angle);
    }


    
    //复位
    public override void Restore()
    {
        _speed = _originSpeed;
    }
    



    //Gizmo
    private void OnDrawGizmosSelected()
    {
        DrawBorder();
    }

    void DrawBorder()
    {
        Gizmos.color = Color.yellow;

        Vector3 upperRight = new Vector3(_destroyBorder.right, _destroyBorder.top, 0);
        Vector3 lowerRight = new Vector3(_destroyBorder.right, _destroyBorder.bottom, 0);
        Vector3 lowerLeft = new Vector3(_destroyBorder.left, _destroyBorder.bottom, 0);
        Vector3 upperLeft = new Vector3(_destroyBorder.left, _destroyBorder.top, 0);

        MyGizmos.DrawRectangle(upperRight, lowerRight, lowerLeft, upperLeft);
    }
}
