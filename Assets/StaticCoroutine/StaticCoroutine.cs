using System.Collections;
using UnityEngine;

public class StaticCoroutine : MonoBehaviour
{
    static StaticCoroutine instance
    {
        get { return GetInstance(); }
    }
    static StaticCoroutine _instance;



    static StaticCoroutine GetInstance()
    {
        if (_instance != null)
            return _instance;

        _instance = new GameObject("Static Coroutine").AddComponent<StaticCoroutine>();
        return _instance;
    }



    public static void Start(IEnumerator coroutine)
    {
        instance.StartCoroutine(coroutine);
    }



    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}