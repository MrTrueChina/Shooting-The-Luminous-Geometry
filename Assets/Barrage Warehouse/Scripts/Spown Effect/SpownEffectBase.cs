using UnityEngine;

public class SpownEffectBase : MonoBehaviour
{
    public float effectTIme
    {
        get { return _effectTime; }
    }
    [SerializeField]
    protected float _effectTime;



    private void OnValidate()
    {
        if (_effectTime < 0)
            _effectTime = 0;
    }
}
