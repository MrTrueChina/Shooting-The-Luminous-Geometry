using UnityEngine;

public static class BarrageBase
{
    /// <summary>
    /// 获取从起始点到目标点的四元数旋转值
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static Quaternion GetAimRotation(Vector2 origin, Vector2 target)
    {
        float angle = GetAimAngle(origin, target);
        Vector3 eulerAngle = new Vector3(0, 0, angle);

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = eulerAngle;

        return rotation;
    }
    static float GetAimAngle(Vector2 origin, Vector2 target)
    {
        Vector2 direction = target - origin;
        float angle = Vector2.Angle(Vector2.up, direction);

        if (target.x > origin.x)
            angle = -angle;

        return angle;
    }

    /// <summary>
    /// 获取玩家Transform组件
    /// </summary>
    /// <returns></returns>
    public static Transform GetPlayerTransform()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            return player.transform;
        return null;
    }
}
