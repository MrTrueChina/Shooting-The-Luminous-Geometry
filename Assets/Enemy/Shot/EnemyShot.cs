using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
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
        Quaternion rotationToPlayer = GetRotationToPlayer();

        Instantiate(_bullet, _transform.position, rotationToPlayer);

        UpdateNextShot();
    }

    Quaternion GetRotationToPlayer()
    {
        float angle = GetAngleToPlayer();
        Vector3 eulerAngle = new Vector3(0, 0, angle);

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = eulerAngle;

        return rotation;
    }

    float GetAngleToPlayer()
    {
        Vector2 direction = _playerTransform.position - _transform.position;
        float angle = Vector2.Angle(Vector2.up, direction);

        if (_playerTransform.position.x > _transform.position.x)
            angle = -angle;

        return angle;
    }

    void UpdateNextShot()
    {
        _nextShot = Time.time + _shotInterval;
    }
}
