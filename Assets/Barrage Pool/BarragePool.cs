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
        return null;
    }

    public static GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject newBullet = Get(prefab);

        if (newBullet == null)
        {
            newBullet = Instantiate(prefab, position, rotation);
            newBullet.name = prefab.name;
        }
        else
        {
            newBullet.transform.position = position;
            newBullet.transform.rotation = rotation;
            EnableAllBullerController(newBullet);               //子弹控制器都是生效完毕后关闭，为了功能正常只能在复用时全部启动了
            newBullet.SetActive(true);                          //在子弹控制器全部启动后再激活子弹，这是因为子弹生成特效抢先一步关闭其他组件，之后其他组件又被启动，就不能再次关闭了，子弹会在特效播放完毕前就飞出去
        }


        return newBullet;
    }
    static void EnableAllBullerController(GameObject bullet)
    {
        BulletContorllerBase[] components = bullet.GetComponents<BulletContorllerBase>();
        foreach (BulletContorllerBase component in components)
            component.enabled = true;
    }
}
