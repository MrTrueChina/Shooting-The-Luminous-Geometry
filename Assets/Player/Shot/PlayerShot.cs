using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField]
    GameObject _bullet;

    [SerializeField]
    float _shotInterval;


    Transform _transform;

    float _nextShot;


    private void Awake()
    {
        _transform = transform;
    }


    private void Update()
    {
        if (Time.time > _nextShot)
            Shot();
    }

    void Shot()
    {
        Instantiate(_bullet, _transform.position, _transform.rotation);

        _nextShot = Time.time + _shotInterval;
    }
}
