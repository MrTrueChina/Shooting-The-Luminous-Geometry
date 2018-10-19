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

    /// <summary>
    /// 发射横行（háng）子弹
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="bulletsNumber"></param>
    /// <param name="angle"></param>
    public void ShotTransverselyLine(GameObject bullet, Vector3 position, Quaternion rotation, int bulletsNumber, float angle)
    {
        /*
         *  要发射横行必须计算速度，用模拟横行距离的方式进行计算
         *  
         *  首先用数学方式造一个横行，横行中心点到发射点的距离是 1，以这个点点的速度为最低速度，记为 1
         *  
         *  子弹在横行上按相同间隔取点，计算到中心的距离，这个距离就是速度的比例
         */
         
        float length = Mathf.Tan(angle * Mathf.Deg2Rad / 2) * 2;    //数字横行的长度
        Debug.Log("length = " + length);


        Vector3 eulerAngle = rotation.eulerAngles;

        for (int i = 0; i < bulletsNumber; i++)
        {


            //位置
            float currentX = length / (bulletsNumber - 1) * i - length / 2;

            Vector2 currentSpeedPosition = new Vector2(currentX, 1);
            currentSpeedPosition.x = currentX;


            
            //旋转
            Quaternion currentRotation = BarrageBase.GetAimRotation(Vector2.zero, new Vector2(currentX, 1));
            currentRotation.eulerAngles = currentRotation.eulerAngles + new Vector3(0, 0, eulerAngle.z);


            MoveUp move = Instantiate(bullet, position, currentRotation).GetComponent<MoveUp>();




            //速度
            float currentDistance = Mathf.Sqrt(1 + currentX * currentX);

            move.speed = move.speed * currentDistance;

        }
    }
}
