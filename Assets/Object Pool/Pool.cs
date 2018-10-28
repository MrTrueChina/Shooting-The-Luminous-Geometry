using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 简易对象池，注意有些物体有专用存入方法
/// </summary>
public class Pool : MonoBehaviour
{
    static Dictionary<string, List<GameObject>> _pool = new Dictionary<string, List<GameObject>>();


    /// <summary>
    /// 存入普通物体的方法，存弹幕用 SetBullet
    /// </summary>
    /// <param name="setObject"></param>
    public static void Set(GameObject setObject)
    {
        setObject.SetActive(false);

        if (_pool.ContainsKey(setObject.name))                  //Dictionary.ContainsKey(TKey key)：传一个键值进去，看有没有这个键值的元素，有的话反 true，没有反 false
        {
            _pool[setObject.name].Add(setObject);               //字典的索引是 key 值，和数组下标使用很像
        }
        else
        {
            _pool.Add(setObject.name, new List<GameObject>());  //Dictionary.Add(TKey key, TValue value)：添加一个这个键值的元素
            _pool[setObject.name].Add(setObject);
        }
    }


    /// <summary>
    /// 存弹幕的方法
    /// </summary>
    /// <param name="bullet"></param>
    public static void SetBullet(GameObject bullet)
    {
        bullet.SetActive(false);            //似乎物体禁用后组件启用也不会调用 OnEnable，用这个方法来阻止组件复位的时候进行的激活调用 OnEnable

        BulletContorllerBase[] controllers = bullet.GetComponents<BulletContorllerBase>();
        foreach (BulletContorllerBase controller in controllers)
            controller.Restore();

        Set(bullet);        //处理完后通过普通方法存入池
    }



    public static GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject getObject = GetFromDictionary(prefab, position, rotation);

        if (getObject == null)
            getObject = CreatNewObject(prefab, position, rotation);

        return getObject;
    }

    static GameObject GetFromDictionary(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (_pool.ContainsKey(prefab.name))
        {
            List<GameObject> list = _pool[prefab.name];
            if (list.Count > 0)
            {
                GameObject getObject = list[0];
                list.RemoveAt(0);

                getObject.transform.position = position;
                getObject.transform.rotation = rotation;

                getObject.SetActive(true);
                return getObject;
            }
        }
        return null;
    }

    static GameObject CreatNewObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject newObject = Instantiate(prefab, position, rotation);
        newObject.name = prefab.name;
        return newObject;
    }
}
