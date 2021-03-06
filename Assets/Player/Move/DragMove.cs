﻿using UnityEngine;

public class DragMove : MonoBehaviour
{
    Transform _transform;
    
    bool _moving = false;
    Vector3 _lastMousePostion;



    //初始化
    private void Awake()
    {
        _transform = transform;
    }



    //移动
    private void Update()
    {
        CheckMoving();

        if (_moving)
        {
            Move();
            UpdateLastMousePosition();
        }
    }
    
    //启动和关闭
    void CheckMoving()
    {
        if (Input.GetMouseButtonDown(0))
            StartMove();

        if (Input.GetMouseButtonUp(0))
            EndMove();
    }
    void StartMove()
    {
        UpdateLastMousePosition();              //如果在关闭移动后鼠标有移动那么下次启动计算鼠标位移就会出现瞬移，要在启动时将上一帧鼠标位置更新到现在的鼠标的位置

        _moving = true;
    }
    void EndMove()
    {
        _moving = false;
    }
    
    //移动
    void Move()
    {
        Vector3 moveToPosition = _transform.position + GetMouseDisplacement();
        moveToPosition = GetClampedPosition(moveToPosition);

        _transform.position = moveToPosition;
    }
    Vector3 GetMouseDisplacement()          //displacement：[物]位移
    {
        Vector3 mouseDisplacement = GetMouseWorldPosition() - _lastMousePostion;

        return mouseDisplacement;
    }
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);

        return worldPoint;
    }
    Vector3 GetClampedPosition(Vector3 position)
    {
        float clampedX = Mathf.Clamp(position.x, StandardValue.viewBorder.left, StandardValue.viewBorder.right);
        float clampedY = Mathf.Clamp(position.y, StandardValue.viewBorder.bottom, StandardValue.viewBorder.top);
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, position.z);

        return clampedPosition;
    }
    
    //上一帧鼠标位置部分
    void UpdateLastMousePosition()
    {
        _lastMousePostion = GetMouseWorldPosition();
    }




    //Gizmo
    private void OnDrawGizmosSelected()
    {
        DrawBorder();
    }

    void DrawBorder()
    {
        Gizmos.color = Color.yellow;

        Vector3 upperRight = new Vector3(StandardValue.viewBorder.right, StandardValue.viewBorder.top, 0);
        Vector3 lowerRight = new Vector3(StandardValue.viewBorder.right, StandardValue.viewBorder.bottom, 0);
        Vector3 lowerLeft = new Vector3(StandardValue.viewBorder.left, StandardValue.viewBorder.bottom, 0);
        Vector3 upperLeft = new Vector3(StandardValue.viewBorder.left, StandardValue.viewBorder.top, 0);

        MyGizmos.DrawRectangle(upperRight, lowerRight, lowerLeft, upperLeft);
    }
}
