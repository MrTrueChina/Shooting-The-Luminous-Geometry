using UnityEngine;

public static partial class MyGizmos
{
    public static void DrawRectangle(Vector3 upperRight, Vector3 lowerRight, Vector3 lowerLeft, Vector3 upperLeft)
    {
        Gizmos.DrawLine(upperLeft, upperRight);
        Gizmos.DrawLine(upperRight, lowerRight);
        Gizmos.DrawLine(lowerRight, lowerLeft);
        Gizmos.DrawLine(lowerLeft, upperLeft);
    }
}
