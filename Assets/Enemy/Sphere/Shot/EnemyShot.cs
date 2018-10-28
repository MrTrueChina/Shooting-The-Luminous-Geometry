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
        //BarrageLauncher.NewShotRing(_bullet, shotPoint, BarrageBase.GetAimRotation(shotPoint, _playerTransform.position), 18);
        BarrageLauncher.NewShotABullet(_bullet, shotPoint, BarrageBase.GetAimRotation(shotPoint, _playerTransform.position));

        UpdateNextShot();
    }

    void UpdateNextShot()
    {
        _nextShot = Time.time + _shotInterval;
    }
}
