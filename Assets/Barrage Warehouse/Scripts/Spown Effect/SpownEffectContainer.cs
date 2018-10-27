using UnityEngine;

public class SpownEffectContainer : MonoBehaviour
{
    public GameObject effectPrefab
    {
        get { return _effectPrefab; }
    }
    [SerializeField]
    GameObject _effectPrefab;
}
