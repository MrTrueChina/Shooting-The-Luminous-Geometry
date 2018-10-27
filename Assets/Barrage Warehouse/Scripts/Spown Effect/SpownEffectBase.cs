using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownEffectBase : MonoBehaviour
{
    public float effectTIme
    {
        get { return _effectTime; }
    }
    [SerializeField]
    protected float _effectTime;
}
