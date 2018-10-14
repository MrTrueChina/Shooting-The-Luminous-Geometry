/*
 *  拖拽移动，玩家控制移动的组件
 *  
 *  鼠标按住时才有效，不按住无效
 *      按下鼠标左键时，进入移动状态
 *      鼠标左键弹起时，退出移动状态
 *  
 *  需要边界
 *  
 *  
 *  每一帧检测鼠标的运动距离，之后移动或乘某个比例移动
 *      保存上一帧的位置，用这一帧的位置进行计算
 *      
 *      如果退出移动状态再进入，会按照上一次保存的位置计算，只要过程中鼠标有移动就会出bug，如何规避？
 *          方案1：在进入时将位置设为现在鼠标的位置
 *          方案2：无论是否移动都计算位置，只在移动状态下移动
 *          
 *          可见方案1节约资源，方案2流畅，先走1再看2
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMove : MonoBehaviour
{
    [SerializeField]
    Border _border;

    [SerializeField]
    float _moveSpeed;

    Transform _transform;

    Vector3 _lastMousePostion;

    bool _moving = false;


    private void Awake()
    {
        _transform = transform;
    }


    private void Update()
    {
        CheckMoving();
        Move();
    }


    void CheckMoving()
    {
        if (Input.GetMouseButtonDown(0))
            StartMove();

        if (Input.GetMouseButtonUp(0))
            EndMove();
    }
    void StartMove()
    {
        UpdateLastMousePosition();

        _moving = true;
    }
    void EndMove()
    {
        _moving = false;
    }


    void Move()
    {
        if (_moving)
            DoMove();
    }

    void DoMove()
    {
        Vector2 displacement = GetMouseDisplacement() * _moveSpeed;       //displacement：[物]位移
        _transform.Translate(displacement);

        ClampPosition();
    }

    Vector2 GetMouseDisplacement()
    {
        Vector2 mouseDisplacement = GetMouseWorldPosition() - _lastMousePostion;
        UpdateLastMousePosition();

        return mouseDisplacement;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);

        return worldPoint;
    }

    void UpdateLastMousePosition()
    {
        _lastMousePostion = GetMouseWorldPosition();
    }

    void ClampPosition()
    {
        float clampedX = Mathf.Clamp(_transform.position.x, _border.left, _border.right);
        float clampedY = Mathf.Clamp(_transform.position.y, _border.bottom, _border.top);
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, _transform.position.z);

        _transform.position = clampedPosition;
    }


    //Gizmo
    private void OnDrawGizmosSelected()
    {
        DrawBorder();
    }

    void DrawBorder()
    {
        Gizmos.color = Color.yellow;

        Vector3 upperRight = new Vector3(_border.right, _border.top, 0);
        Vector3 lowerRight = new Vector3(_border.right, _border.bottom, 0);
        Vector3 lowerLeft = new Vector3(_border.left, _border.bottom, 0);
        Vector3 upperLeft = new Vector3(_border.left, _border.top, 0);

        Gizmos.DrawLine(upperLeft, upperRight);
        Gizmos.DrawLine(upperRight, lowerRight);
        Gizmos.DrawLine(lowerRight, lowerLeft);
        Gizmos.DrawLine(lowerLeft, upperLeft);
    }
}
