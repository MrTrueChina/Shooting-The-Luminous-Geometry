using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 简易对象池，用来存弹幕
/// </summary>
public class BarragePool : MonoBehaviour
{
    static Dictionary<string, List<GameObject>> _bulletPool = new Dictionary<string, List<GameObject>>();


    public static void Set(GameObject bullet)
    {
        bullet.SetActive(false);
        if (_bulletPool.ContainsKey(bullet.name))
        {
            _bulletPool[bullet.name].Add(bullet);
        }
        else
        {
            _bulletPool.Add(bullet.name, new List<GameObject>());
            _bulletPool[bullet.name].Add(bullet);
        }
    }


    public static GameObject Get(GameObject prefab)
    {
        if (_bulletPool.ContainsKey(prefab.name))
        {
            List<GameObject> list = _bulletPool[prefab.name];
            if (list.Count > 0)
            {
                GameObject bullet = list[0];
                list.RemoveAt(0);
                return bullet;
            }
        }
        GameObject newBullet = Instantiate(prefab);
        newBullet.name = prefab.name;
        return newBullet;
    }

    public static GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject newBullet = Get(prefab);
        newBullet.transform.position = position;
        newBullet.transform.rotation = rotation;
        newBullet.SetActive(true);
        return newBullet;
    }
}
