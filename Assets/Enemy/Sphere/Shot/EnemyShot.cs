using UnityEngine;


public class EnemyShot : EnemyShotBase
{
    [SerializeField]
    GameObject _bullet;

    [SerializeField]
    float _shotInterval;
    
    Transform _transform;
    Transform _playerTransform;

    float _nextShot;


    private void Awake()
    {
        _transform = transform;
    }


    private void Start()
    {
        FindPlayer();
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
        Quaternion rotateToPlayer = BarrageBase.GetAimRotation(shotPoint, _playerTransform.position);
        BarrageLauncher.ShotRing(_bullet, shotPoint, rotateToPlayer, 18);
        //BarrageLauncher.ShotABullet(_bullet, shotPoint, rotateToPlayer);
        //BarrageLauncher.ShotFanShaped(_bullet, shotPoint, rotateToPlayer, 9, 60);
        //BarrageLauncher.ShotTransverselyLine(_bullet, shotPoint, rotateToPlayer, 17, 60);
        //BarrageLauncher.ShotPane(_bullet, shotPoint, rotateToPlayer, 7);
        //BarrageLauncher.ShotRingToCenter(_bullet, _playerTransform.position, Quaternion.identity, 8, 400);

        UpdateNextShot();
    }

    void UpdateNextShot()
    {
        _nextShot = Time.time + _shotInterval;
    }
}
