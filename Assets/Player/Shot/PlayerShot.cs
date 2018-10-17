using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField]
    Transform[] _shotPoints;

    [SerializeField]
    GameObject _bullet;

    [SerializeField]
    float _shotInterval;
    

    float _nextShot;


    private void Update()
    {
        if (Time.time > _nextShot)
            Shot();
    }

    void Shot()
    {
        foreach(Transform shotPoint in _shotPoints)
            Instantiate(_bullet, shotPoint.position, shotPoint.rotation);

        _nextShot = Time.time + _shotInterval;
    }


    private void OnDrawGizmosSelected()
    {
        DrawTrajectory();
    }

    void DrawTrajectory()
    {
        Gizmos.color = Color.red;

        foreach (Transform shotPoint in _shotPoints)
        {
            Vector3 endPoint = shotPoint.position + shotPoint.up * 10000;

            Gizmos.DrawLine(shotPoint.position, endPoint);
        }
    }
}
