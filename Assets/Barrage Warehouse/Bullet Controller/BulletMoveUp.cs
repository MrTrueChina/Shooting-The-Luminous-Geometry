/*
 *  移动，需要越界销毁
 *  
 *  需要速度、方向、转向接口
 */

using UnityEngine;

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



    //初始化
    private void Awake()
    {
        _transform = transform;
    }



    //移动
    private void Update()
    {
        Move();
        CheckAndDestroy();
    }
    
    void Move()
    {
        _transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }
    
    void CheckAndDestroy()
    {
        if (OutOfBorder())
            Destroy(gameObject);
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
