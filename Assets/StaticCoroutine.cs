using System.Collections;
using UnityEngine;

public class StaticCoroutine : MonoBehaviour
{
    static StaticCoroutine instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            _instance = new GameObject("Static Return Coroutine Instance").AddComponent<StaticCoroutine>();
            return _instance;
        }
    }
    static StaticCoroutine _instance;


    public static Coroutine Start(IEnumerator coroutine)
    {
        return instance.StartCoroutine(coroutine);
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}


public class ReturnCoroutine<T>
{
    public Coroutine coroutine
    {
        get { return _coroutine; }
    }
    Coroutine _coroutine;

    public T result
    {
        get { return _result; }
    }
    T _result;

    
    public ReturnCoroutine(IEnumerator coroutine)
    {
        _coroutine = StaticCoroutine.Start(Start(coroutine));
    }
    public ReturnCoroutine(MonoBehaviour mono, IEnumerator coroutine)
    {
        _coroutine = mono.StartCoroutine(Start(coroutine));
    }


    IEnumerator Start(IEnumerator coroutine)
    {
        while (coroutine.MoveNext())
            yield return coroutine.Current;
        _result = (T)coroutine.Current;
    }
}