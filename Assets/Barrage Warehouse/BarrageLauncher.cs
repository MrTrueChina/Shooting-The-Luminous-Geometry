using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageLauncher : MonoBehaviour
{
    /// <summary>
    /// 发射单发子弹
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    public void ShotABullet(GameObject bullet, Vector3 position, Quaternion rotation)
    {
        Instantiate(bullet, position, rotation);
    }

    /// <summary>
    /// 发射扇形子弹
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="bulletsNumber"></param>
    /// <param name="angle"></param>
    public void ShotFanShapedBullets(GameObject bullet, Vector3 position, Quaternion rotation, int bulletsNumber, float angle)
    {
        Vector3 eulerAngle = rotation.eulerAngles;
        eulerAngle.z -= angle / 2;

        for (int i = 0; i < bulletsNumber; i++)
        {
            Quaternion currentRotation = new Quaternion();
            currentRotation.eulerAngles = eulerAngle;

            Instantiate(bullet, position, currentRotation);

            eulerAngle.z += angle / (bulletsNumber - 1);
        }
    }

    /// <summary>
    /// 发射环状子弹
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="bulletNumber"></param>
    public void ShotRing(GameObject bullet, Vector3 position, Quaternion rotation, int bulletsNumber)
    {
        Vector3 eulerAngle = rotation.eulerAngles;

        for (int i = 0; i < bulletsNumber; i++)
        {
            Quaternion currentRotation = new Quaternion();
            currentRotation.eulerAngles = eulerAngle;

            Instantiate(bullet, position, currentRotation);

            eulerAngle.z += 360f / bulletsNumber;
        }
    }

    /// <summary>
    /// 发射十字子弹
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="position"></param>
    /// <param name="ratation"></param>
    public void ShotCross(GameObject bullet, Vector3 position, Quaternion rotation)
    {
        ShotRing(bullet, position, rotation, 4);
    }
}
