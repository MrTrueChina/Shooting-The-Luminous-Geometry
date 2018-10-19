using UnityEngine;

[RequireComponent(typeof(BarrageLauncher))]
public class EnemyShot : MonoBehaviour
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
    }
    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            _playerTransform = player.transform;
    }


    private void Update()
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
        //_launcher.ShotABullet(_bullet, _transform.position, BarrageBase.GetAimRotation(_transform.position, _playerTransform.position));
        //_launcher.ShotFanShapedBullets(_bullet, _transform.position, BarrageBase.GetAimRotation(_transform.position, _playerTransform.position), 7, 60);
        //_launcher.ShotRing(_bullet, _transform.position, BarrageBase.GetAimRotation(_transform.position, _playerTransform.position), 12);
        //_launcher.ShotCross(_bullet, _transform.position, BarrageBase.GetAimRotation(_transform.position, _playerTransform.position));
        //_launcher.ShotTransverselyLine(_bullet, _transform.position, BarrageBase.GetAimRotation(_transform.position, _playerTransform.position), 15, 45);
        _launcher.ShotPane(_bullet, _transform.position, BarrageBase.GetAimRotation(_transform.position, _playerTransform.position), 9);

        UpdateNextShot();
    }

    void UpdateNextShot()
    {
        _nextShot = Time.time + _shotInterval;
    }
}
