using UnityEngine;

[System.Serializable]
public class Border
{
    public float top
    {
        get { return _top; }
    }
    float _top;
    public float right
    {
        get { return _right; }
    }
    float _right;
    public float bottom
    {
        get { return _bottom; }
    }
    float _bottom;
    public float left
    {
        get { return _left; }
    }
    float _left;


    public Border(float top, float right, float bottom, float left)
    {
        _top = top;
        _right = right;
        _bottom = bottom;
        _left = left;
    }


    public bool Inside(Vector2 position)
    {
        return (position.x >= _left && position.x <= right && position.y >= _bottom && position.y <= top);
    }
}