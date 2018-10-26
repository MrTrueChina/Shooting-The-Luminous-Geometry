using System.Collections.Generic;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    [SerializeField]
    GameObject _prefab;

    List<GameObject> _clones = new List<GameObject>();



    public void Test()
    {
        GameObject clone = Instantiate(_prefab);
        Debug.Log(clone == _prefab);
    }

    public void SetToPool()
    {
        if (_clones.Count > 0)
        {
            BarragePool.Set(_clones[0]);
            _clones.RemoveAt(0);
        }
    }

    public void GetFromPool()
    {
        _clones.Add(BarragePool.Get(_prefab));
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            Test();

        if (Input.GetKeyDown(KeyCode.S))
            SetToPool();

        if (Input.GetKeyDown(KeyCode.G))
            GetFromPool();
    }
}
