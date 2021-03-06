﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//也许可以改成传延迟时间来生成弹幕，毕竟等待的只是特效的时间而不是特效本身
//之后还可以用传Transform来进行到时间时通过Transform确定位置角度
//进而分裂控制器也要修改，改成激活时启动传Transform延时发射弹幕的协程，等协程执行完毕就存入池
//还有问题，如果生成过程中子弹被消弹了或越界了怎么处理
//以及如果越界了或消弹了之后又被取出来激活了怎么办，这种时候通过激活状态来做判断很明显不行

public class BarrageLauncher : MonoBehaviour
{
    /// <summary>
    /// 发射单发子弹
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    public static void ShotABullet(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject effectObject = CreatSpownEffect(bulletPrefab, position, rotation);
        float effectTime = GetEffectTime(effectObject);

        StaticCoroutine.Start(ShotAndReturnABullet(bulletPrefab, position, rotation, effectTime));
    }
    static IEnumerator ShotAndReturnABullet(GameObject bulletPrefab, Vector3 position, Quaternion rotation, float spownTime)
    {
        GameObject bullet = GetInactiveBullet(bulletPrefab, position, rotation);
        yield return new WaitForSeconds(spownTime);

        bullet.SetActive(true);

        yield return bullet;
    }



    /// <summary>
    /// 发射扇形子弹
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="bulletsNumber"></param>
    /// <param name="angle"></param>
    public static void ShotFanShaped(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber, float angle)
    {
        GameObject effectObject = CreatSpownEffect(bulletPrefab, position, rotation);
        float effectTime = GetEffectTime(effectObject);

        StaticCoroutine.Start(StartShotFanShaped(bulletPrefab, position, rotation, bulletsNumber, angle, effectTime));
    }
    static IEnumerator StartShotFanShaped(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber, float angle, float effectTime)
    {
        ReturnCoroutine<GameObject[]> returnCoroutine = new ReturnCoroutine<GameObject[]>(ShotAndReturnFanShapedBullets(bulletPrefab, position, rotation, bulletsNumber, angle, effectTime));
        yield return new WaitForSeconds(effectTime);

        if (returnCoroutine.result == null)     //如果游戏在生成的最后一帧卡住了，有可能导致子弹生成被卡在最后一次生成但激活的倒计时已经结束了，这时就发生了空引用，在检测到返回值还没返回时等一帧让生成过程完成
            yield return null;

        foreach (GameObject bullet in returnCoroutine.result)
            bullet.SetActive(true);
    }
    static IEnumerator ShotAndReturnFanShapedBullets(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber, float angle, float spownTime)
    {
        List<GameObject> bullets = new List<GameObject>();

        Vector3 originEulerAngle = rotation.eulerAngles;
        float startEulerZ = originEulerAngle.z - angle / 2;
        float ballisticAngle = angle / (bulletsNumber - 1);

        float startTime = Time.time;
        for (int i = 0; i < bulletsNumber; i++)
        {
            Quaternion currentRotation = Quaternion.Euler(originEulerAngle.x, originEulerAngle.y, startEulerZ + i * ballisticAngle);

            bullets.Add(GetInactiveBullet(bulletPrefab, position, currentRotation));

            float expectedTime = spownTime / bulletsNumber * i;     //生成到当前这一个子弹预期要经过的时间
            float elapsedTime = Time.time - startTime;              //从生成开始实际经过的时间
            if (expectedTime > elapsedTime)
                yield return new WaitForSeconds(expectedTime - elapsedTime);    //如果预期时间比实际时间多，则说明生成超过计划，等待中间的时间差
        }

        yield return bullets.ToArray();
    }



    /// <summary>
    /// 发射环状子弹
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="bulletsNumber"></param>
    public static void ShotRing(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber)
    {
        GameObject effectObject = CreatSpownEffect(bulletPrefab, position, rotation);
        float effectTime = GetEffectTime(effectObject);

        StaticCoroutine.Start(StartShotRing(bulletPrefab, position, rotation, bulletsNumber, effectTime));
    }
    static IEnumerator StartShotRing(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber, float effectTime)
    {
        ReturnCoroutine<GameObject[]> returnCoroutine = new ReturnCoroutine<GameObject[]>(ShotAndReturnRingBullet(bulletPrefab, position, rotation, bulletsNumber, effectTime));
        yield return new WaitForSeconds(effectTime);

        if (returnCoroutine.result == null)
            yield return null;

        foreach (GameObject bullet in returnCoroutine.result)
            bullet.SetActive(true);
    }
    static IEnumerator ShotAndReturnRingBullet(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber, float spownTime)
    {
        List<GameObject> bullets = new List<GameObject>();

        Vector3 originEuler = rotation.eulerAngles;
        float ballisticAngle = 360f / bulletsNumber;

        float startTime = Time.time;
        for (int i = 0; i < bulletsNumber; i++)
        {
            Quaternion currentRotation = Quaternion.Euler(originEuler.x, originEuler.y, originEuler.z + ballisticAngle * i);

            bullets.Add(GetInactiveBullet(bulletPrefab, position, currentRotation));

            float expectedTime = spownTime / bulletsNumber * i;
            float elapsedTime = Time.time - startTime;
            if (expectedTime > elapsedTime)
                yield return new WaitForSeconds(expectedTime - elapsedTime);
        }

        yield return bullets.ToArray();
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
    /// <param name="bulletPrefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="bulletsNumber"></param>
    /// <param name="angle"></param>
    public static void ShotTransverselyLine(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber, float angle)
    {
        GameObject effectObject = CreatSpownEffect(bulletPrefab, position, rotation);
        float effectTime = GetEffectTime(effectObject);

        StaticCoroutine.Start(StartShotTransverselyLine(bulletPrefab, position, rotation, bulletsNumber, angle, effectTime));
    }
    static IEnumerator StartShotTransverselyLine(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber, float angle, float effectTime)
    {
        ReturnCoroutine<GameObject[]> returnCoroutine = new ReturnCoroutine<GameObject[]>(ShotAndReturnTransverselyLineBullets(bulletPrefab, position, rotation, bulletsNumber, angle, effectTime));
        yield return new WaitForSeconds(effectTime);

        if (returnCoroutine.result == null)
            yield return null;

        foreach (GameObject bullet in returnCoroutine.result)
            bullet.SetActive(true);
    }
    static IEnumerator ShotAndReturnTransverselyLineBullets(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber, float angle, float spownTime)
    {
        List<GameObject> bullets = new List<GameObject>();

        float length = Mathf.Tan(angle * Mathf.Deg2Rad / 2) * 2;    //数字横行的长度
        float eulerZ = rotation.eulerAngles.z;

        float startTime = Time.time;
        for (int i = 0; i < bulletsNumber; i++)
        {
            float currentX = length / (bulletsNumber - 1) * i - length / 2;

            Quaternion currentRotation = BarrageBase.GetAimRotation(Vector2.zero, new Vector2(currentX, 1));
            currentRotation.eulerAngles = currentRotation.eulerAngles + new Vector3(0, 0, eulerZ);

            GameObject bullet = GetInactiveBullet(bulletPrefab, position, currentRotation);
            bullets.Add(bullet);

            BulletMoveUp move = bullet.GetComponent<BulletMoveUp>();
            move.speed = move.speed * Mathf.Sqrt(1 + currentX * currentX);  //因为到中心点的距离是1，那么所有点的速度比例就和距离相同，直接乘上就行
            
            float expectedTime = spownTime / bulletsNumber * i;
            float elapsedTime = Time.time - startTime;
            if (expectedTime > elapsedTime)
                yield return new WaitForSeconds(expectedTime - elapsedTime);
        }
        
        yield return bullets.ToArray();
    }



    /// <summary>
    /// 发射方框子弹
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="bulletsNumberASide"></param>
    public static void ShotPane(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumberASide)
    {
        GameObject effectObject = CreatSpownEffect(bulletPrefab, position, rotation);
        float effectTime = GetEffectTime(effectObject);

        StaticCoroutine.Start(StartShotPane(bulletPrefab, position, rotation, bulletsNumberASide, effectTime));
    }
    static IEnumerator StartShotPane(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumberASide, float effectTime)
    {
        ReturnCoroutine<GameObject[]> returnCoroutine = new ReturnCoroutine<GameObject[]>(ShotAndReturnPaneBullets(bulletPrefab, position, rotation, bulletsNumberASide, effectTime));
        yield return new WaitForSeconds(effectTime);

        if (returnCoroutine.result == null)
            yield return null;

        foreach (GameObject bullet in returnCoroutine.result)
            bullet.SetActive(true);
    }
    static IEnumerator ShotAndReturnPaneBullets(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumberASide, float spownTime)
    {
        List<GameObject> bullets = new List<GameObject>();

        float length = Mathf.Tan(90 * Mathf.Deg2Rad / 2) * 2;    //数字横行的长度
        float eulerZ = rotation.eulerAngles.z;

        float startTime = Time.time;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < bulletsNumberASide - 1; j++)
            {
                float currentX = length / (bulletsNumberASide - 1) * j - length / 2;

                Quaternion currentRotation = BarrageBase.GetAimRotation(Vector2.zero, new Vector2(currentX, 1));
                currentRotation.eulerAngles = currentRotation.eulerAngles + new Vector3(0, 0, eulerZ);

                GameObject bullet = GetInactiveBullet(bulletPrefab, position, currentRotation);
                bullets.Add(bullet);

                BulletMoveUp move = bullet.GetComponent<BulletMoveUp>();
                move.speed = move.speed * Mathf.Sqrt(1 + currentX * currentX);  //因为到中心点的距离是1，那么所有点的速度比例就和距离相同，直接乘上就行

                float expectedTime = spownTime / ((bulletsNumberASide - 1) * 4) * (i * (bulletsNumberASide - 1) + j);
                float elapsedTime = Time.time - startTime;
                if (expectedTime > elapsedTime)
                    yield return new WaitForSeconds(expectedTime - elapsedTime);
            }

            eulerZ += 90;
        }

        yield return bullets.ToArray();
    }


    
    /// <summary>
    /// 发射向中央聚集的环状子弹
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="bulletsNumber"></param>
    /// <param name="radius"></param>
    public static void ShotRingToCenter(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber, float radius)
    {
        GameObject effectPrefab = GetEffectPrefab(bulletPrefab);
        SpownRingEffects(effectPrefab, position, rotation, bulletsNumber, radius);
        float effectTime = GetEffectTime(effectPrefab);

        StaticCoroutine.Start(StartShotRingToCenter(bulletPrefab, position, rotation, bulletsNumber, radius, effectTime));
    }
    static void SpownRingEffects(GameObject effectPrefab, Vector3 position, Quaternion rotation, int effectsNumber, float radius)
    {
        if (effectPrefab == null) return;

        Vector3 originEuler = rotation.eulerAngles;
        float effectsAngle = 360f / effectsNumber;
        
        for (int i = 0; i < effectsNumber; i++)
        {
            float currentEulerZ = originEuler.z + effectsAngle * i;
            Quaternion currentRotation = Quaternion.Euler(originEuler.x, originEuler.y, currentEulerZ);

            Vector3 currentPosition = position + new Vector3(Mathf.Cos(currentEulerZ * Mathf.Deg2Rad) * radius, Mathf.Sin(currentEulerZ * Mathf.Deg2Rad) * radius, 0);

            if (!StandardValue.viewBorder.Inside(currentPosition))
                continue;

            Pool.Get(effectPrefab, currentPosition, currentRotation);
        }
    }
    static IEnumerator StartShotRingToCenter(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber, float radius, float effectTime)
    {
        ReturnCoroutine<GameObject[]> returnCoroutine = new ReturnCoroutine<GameObject[]>(ShotAndReturnRingToCenter(bulletPrefab, position, rotation, bulletsNumber, radius, effectTime));
        yield return new WaitForSeconds(effectTime);

        if (returnCoroutine.result == null)
            yield return null;

        foreach (GameObject bullet in returnCoroutine.result)
            bullet.SetActive(true);
    }
    static IEnumerator ShotAndReturnRingToCenter(GameObject bulletPrefab, Vector3 position, Quaternion rotation, int bulletsNumber, float radius, float spownTime)
    {
        List<GameObject> bullets = new List<GameObject>();

        Vector3 originEuler = rotation.eulerAngles;
        float effectsAngle = 360f / bulletsNumber;

        float startTime = Time.time;
        for (int i = 0; i < bulletsNumber; i++)
        {
            float currentEulerZ = originEuler.z + effectsAngle * i;
            Vector3 currentPosition = position + new Vector3(Mathf.Cos(currentEulerZ * Mathf.Deg2Rad) * radius, Mathf.Sin(currentEulerZ * Mathf.Deg2Rad) * radius, 0);

            Quaternion currentRotation = Quaternion.Euler(originEuler.x, originEuler.y, currentEulerZ + 90);    //+90°的原因很明显是因为角度的起始点不一致，但有些说不清，去掉90再看效果就知道了

            if (!StandardValue.viewBorder.Inside(currentPosition))
                continue;

            bullets.Add(GetInactiveBullet(bulletPrefab, currentPosition, currentRotation));

            float expectedTime = spownTime / bulletsNumber * i;
            float elapsedTime = Time.time - startTime;
            if (expectedTime > elapsedTime)
                yield return new WaitForSeconds(expectedTime - elapsedTime);
        }

        yield return bullets.ToArray();
    }



    //基础方法
    /// <summary>
    /// 生成子弹储存的特效并返回特效物体，没有储存特效则返回null
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    static GameObject CreatSpownEffect(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
    {
        SpownEffectContainer container = bulletPrefab.GetComponent<SpownEffectContainer>();
        if (container != null && container.effectPrefab != null)
            return Pool.Get(container.effectPrefab, position, rotation);

        return null;
    }

    /// <summary>
    /// 获取特效物体的特效执行时间，如果找不到则返回0
    /// </summary>
    /// <param name="effectObject"></param>
    /// <returns></returns>
    static float GetEffectTime(GameObject effectObject)
    {
        if (effectObject != null)
        {
            SpownEffectBase effectBase = effectObject.GetComponent<SpownEffectBase>();
            if (effectBase != null)
                return effectBase.effectTIme;
        }
        return 0;
    }

    /// <summary>
    /// 获取不活动的子弹
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    static GameObject GetInactiveBullet(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Pool.Get(bulletPrefab, position, rotation);
        bullet.SetActive(false);
        return bullet;
    }

    /// <summary>
    /// 获取特效预制
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <returns></returns>
    static GameObject GetEffectPrefab(GameObject bulletPrefab)
    {
        SpownEffectContainer container = bulletPrefab.GetComponent<SpownEffectContainer>();
        if (container != null)
            return container.effectPrefab;
        return null;
    }
}
