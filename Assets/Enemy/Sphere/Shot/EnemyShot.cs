using UnityEngine;

[RequireComponent(typeof(BarrageLauncher))]
public class EnemyShot : EnemyShotBase
{
    [SerializeField]
    GameObject _bullet;

    [SerializeField]
    float _shotInterval;

    BarrageLauncher _launcher;
    Transform _transform;
    Transform _playerTransform;

    float _nextShot;


    private void Awake()
    {
        _launcher = GetComponent<BarrageLauncher>();
        _transform = transform;
    }


    private void Start()
    {
        FindPlayer();
        _launcher.StartShotScrew(_bullet,_transform,Quaternion.identity,360,10,0.05f);
    }
    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            _playerTransform = player.transform;
    }


    private void Update()
    {
        StateOne();
    }

    void StateOne()
    {
        if (_playerTransform != null)
            Shot();
        else
            FindPlayer();
    }

    void Shot()
    {
        if (Time.time > _nextShot)
            DoShot();
    }

    void DoShot()
    {
        Vector2 shotPoint = new Vector2(Random.Range(-300, 300), Random.Range(0, 500));
        _launcher.ShotRing(_bullet, shotPoint, BarrageBase.GetAimRotation(_transform.position, _playerTransform.position), 18);

        UpdateNextShot();
    }

    void UpdateNextShot()
    {
        _nextShot = Time.time + _shotInterval;
    }
}
