/*
 *  移动，需要越界销毁
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    [SerializeField]
    Border _destroyBorder;
    [SerializeField]
    float _speed;

    Transform _transform;


    private void Awake()
    {
        _transform = transform;
    }


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
