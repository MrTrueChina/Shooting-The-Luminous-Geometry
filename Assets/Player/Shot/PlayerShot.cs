using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField]
    Transform[] _shotPoints;
    [SerializeField]
    GameObject _bullet;
    [SerializeField]
    float _shotInterval;
    
    float _nextShotTime;



    //射击
    private void Update()
    {
        if (Time.time > _nextShotTime)
            Shot();
    }

    void Shot()
    {
        foreach(Transform shotPoint in _shotPoints)
            Instantiate(_bullet, shotPoint.position, shotPoint.rotation);

        UpdateNextShotTime();
    }

    void UpdateNextShotTime()
    {
        _nextShotTime = Time.time + _shotInterval;
    }



    //Gizmo
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
