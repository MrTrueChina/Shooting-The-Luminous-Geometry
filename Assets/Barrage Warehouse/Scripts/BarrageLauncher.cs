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
    public static void NewShotABullet(GameObject bullet, Vector3 position, Quaternion rotation)
    {
        StaticCoroutine.Start(DoShotABullet(bullet, position, rotation));
    }

    static IEnumerator DoShotABullet(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject effectPrefab = GetSpownEffectPrefab(bulletPrefab);
        
        if (effectPrefab != null)
            yield return StaticCoroutine.Start(ShotABulletWithSpownEffect(bulletPrefab, effectPrefab, position, rotation));
        else
            yield return ShotABulletWithOutEffect(bulletPrefab, position, rotation);
    }

    static IEnumerator ShotABulletWithSpownEffect(GameObject bulletPrefab, GameObject effectPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject bullet = ShotABulletWithOutEffect(bulletPrefab, position, rotation);
        bullet.SetActive(false);

        GameObject effectObject = Pool.Get(effectPrefab, position, rotation);
        yield return new WaitForSeconds(GetSpownEffectTime(effectObject));

        bullet.SetActive(true);
    }

    static GameObject ShotABulletWithOutEffect(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
    {
        return Pool.Get(bulletPrefab, position, rotation);
    }
    

    /// <summary>
    /// 发射扇形子弹
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="bulletsNumber"></param>
    /// <param name="angle"></param>
    public static GameObject[] ShotFanShapedBullets(GameObject bullet, Vector3 position, Quaternion rotation, int bulletsNumber, float angle)
    {
        List<GameObject> bullets = new List<GameObject>();

        Vector3 eulerAngle = rotation.eulerAngles;
        eulerAngle.z -= angle / 2;

        for (int i = 0; i < bulletsNumber; i++)
        {
            Quaternion currentRotation = new Quaternion();
            currentRotation.eulerAngles = eulerAngle;

            bullets.Add(Pool.Get(bullet, position, currentRotation));

            eulerAngle.z += angle / (bulletsNumber - 1);
        }

        return bullets.ToArray();
    }

    /// <summary>
    /// 发射环状子弹
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="bulletNumber"></param>
    public static GameObject[] ShotRing(GameObject bullet, Vector3 position, Quaternion rotation, int bulletsNumber)
    {
        List<GameObject> bullets = new List<GameObject>();

        Vector3 eulerAngle = rotation.eulerAngles;

        for (int i = 0; i < bulletsNumber; i++)
        {
            Quaternion currentRotation = new Quaternion();
            currentRotation.eulerAngles = eulerAngle;

            bullets.Add(Pool.Get(bullet, position, currentRotation));

            eulerAngle.z += 360f / bulletsNumber;
        }

        return bullets.ToArray();
    }

    public static void NewShotRing(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber)
    {
        StaticCoroutine.Start(DoShotRing(bulletPrefab, position, rotation, bulletsNumber));
    }

    public static IEnumerator DoShotRing(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber)
    {
        GameObject effectPrefab = bulletPrefab.GetComponent<SpownEffectContainer>().effectPrefab;
        float effectTime = effectPrefab.GetComponent<SpownEffectBase>().effectTIme;
        Pool.Get(effectPrefab, position, rotation);

        List<GameObject> bullets = new List<GameObject>();
        Vector3 eulerAngle = rotation.eulerAngles;

        /*
         *  每帧一次
         *  
         *  每帧确认经过时间
         *  
         *  通过经过时间确定需要生成的子弹数量
         *  
         *  时间是循环次数的控制之一，子弹必须最终全部生成
         */
        float startSpownTime = Time.time;
        float spownInterval = effectTime / bulletsNumber;
        for (int i = 0; i < bulletsNumber; i++)
        {
            Quaternion currentRotation = new Quaternion();
            currentRotation.eulerAngles = eulerAngle;

            GameObject bullet = Pool.Get(bulletPrefab, position, currentRotation);
            bullet.SetActive(false);
            bullets.Add(bullet);

            eulerAngle.z += 360f / bulletsNumber;

            if (i * spownInterval > Time.time - startSpownTime)
                yield return new WaitForSeconds(i * spownInterval - (Time.time - startSpownTime));
        }

        foreach (GameObject bullet in bullets)
            bullet.SetActive(true);
    }

    /// <summary>
    /// 发射十字子弹
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="position"></param>
    /// <param name="ratation"></param>
    public static void ShotCross(GameObject bullet, Vector3 position, Quaternion rotation)
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
    public static void ShotTransverselyLine(GameObject bullet, Vector3 position, Quaternion rotation, int bulletsNumber, float angle)
    {
        /*
         *  要发射横行必须计算速度，用模拟横行距离的方式进行计算
         *  
         *  首先用数学方式造一个横行，横行中心点到发射点的距离是 1，以这个点点的速度为最低速度，记为 1
         *  
         *  子弹在横行上按相同间隔取点，计算到中心的距离，这个距离就是速度的比例
         */
         
        float length = Mathf.Tan(angle * Mathf.Deg2Rad / 2) * 2;    //数字横行的长度
        float eulerZ = rotation.eulerAngles.z;

        for (int i = 0; i < bulletsNumber; i++)
        {
            float currentX = length / (bulletsNumber - 1) * i - length / 2;
            
            Quaternion currentRotation = BarrageBase.GetAimRotation(Vector2.zero, new Vector2(currentX, 1));
            currentRotation.eulerAngles = currentRotation.eulerAngles + new Vector3(0, 0, eulerZ);
            
            BulletMoveUp move = Pool.Get(bullet, position, currentRotation).GetComponent<BulletMoveUp>();
            
            move.speed = move.speed * Mathf.Sqrt(1 + currentX * currentX);  //因为到中心点的距离是1，那么所有点的速度比例就和距离相同，直接乘上就行
        }
    }

    /// <summary>
    /// 发射方框子弹
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="bulletsNumberASide"></param>
    public static void ShotPane(GameObject bullet, Vector3 position, Quaternion rotation, int bulletsNumberASide)
    {
        float length = Mathf.Tan(90 * Mathf.Deg2Rad / 2) * 2;    //数字横行的长度
        float eulerZ = rotation.eulerAngles.z;
        
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < bulletsNumberASide - 1; j++)
            {
                float currentX = length / (bulletsNumberASide - 1) * j - length / 2;

                Quaternion currentRotation = BarrageBase.GetAimRotation(Vector2.zero, new Vector2(currentX, 1));
                currentRotation.eulerAngles = currentRotation.eulerAngles + new Vector3(0, 0, eulerZ);

                BulletMoveUp move = Pool.Get(bullet, position, currentRotation).GetComponent<BulletMoveUp>();

                move.speed = move.speed * Mathf.Sqrt(1 + currentX * currentX);  //因为到中心点的距离是1，那么所有点的速度比例就和距离相同，直接乘上就行
            }

            eulerZ += 90;
        }


        
    }


    /// <summary>
    /// 发射螺旋弹幕
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="position"></param>
    /// <param name="startRotation"></param>
    /// <param name="bulletsNumber"></param>
    /// <param name="deltaAngle"></param>
    /// <param name="shotInterval"></param>
    /// <returns></returns>
    public static IEnumerator ShotScrew(GameObject bullet, Vector3 position, Quaternion startRotation, int bulletsNumber, float deltaAngle, float shotInterval)
    {
        float startEulerZ = startRotation.eulerAngles.z;

        WaitForSeconds waitForNextShot = new WaitForSeconds(shotInterval);

        for (int i = 0; i < bulletsNumber; i++)
        {
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, startEulerZ + deltaAngle * i);

            Pool.Get(bullet, position, rotation);

            yield return waitForNextShot;
        }
    }
    public static void StartShotScrew(GameObject bullet, Transform transform, Quaternion startRotation, int bulletsNumber, float deltaAngle, float shotInterval)
    {
        //StartCoroutine(ShotScrew(bullet, transform, startRotation, bulletsNumber, deltaAngle, shotInterval));
    }
    /// <summary>
    /// 发射螺旋弹幕
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="transform"></param>
    /// <param name="startRotation"></param>
    /// <param name="bulletsNumber"></param>
    /// <param name="deltaAngle"></param>
    /// <param name="shotInterval"></param>
    /// <returns></returns>
    public static IEnumerator ShotScrew(GameObject bullet, Transform transform, Quaternion startRotation, int bulletsNumber, float deltaAngle, float shotInterval)
    {
        float startEulerZ = startRotation.eulerAngles.z;

        WaitForSeconds waitForNextShot = new WaitForSeconds(shotInterval);

        for (int i = 0; i < bulletsNumber; i++)
        {
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, startEulerZ - deltaAngle * i);

            Pool.Get(bullet, transform.position, rotation);

            yield return waitForNextShot;
        }
    }







    /// <summary>
    /// 获取子弹预制的生成特效物体预制
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <returns></returns>
    static GameObject GetSpownEffectPrefab(GameObject bulletPrefab)
    {
        SpownEffectContainer spownEffectContainer = bulletPrefab.GetComponent<SpownEffectContainer>();
        if (spownEffectContainer != null)
            return spownEffectContainer.effectPrefab;
        return null;
    }

    /// <summary>
    /// 获取特效物体的特效执行时间
    /// </summary>
    /// <param name="effectObject"></param>
    /// <returns></returns>
    static float GetSpownEffectTime(GameObject effectObject)
    {
        return effectObject.GetComponent<SpownEffectBase>().effectTIme;
    }
}
