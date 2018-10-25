using UnityEngine;

[RequireComponent(typeof(BarrageLauncher))]
/// <summary>
/// 分裂出环装弹幕
/// </summary>
public class FissionRing : BulletContorllerBase
{
    public GameObject bullet
    {
        get { return _bullet; }
        set { _bullet = value; }
    }
    [SerializeField]
    GameObject _bullet;

    public float fissionTime
    {
        get { return _fissionTime; }
        set { _fissionTime = value; }
    }
    [SerializeField]
    float _fissionTime;

    public int bulletsNumber
    {
        get { return _bulletsNumber; }
        set { _bulletsNumber = value; }
    }
    [SerializeField]
    int _bulletsNumber;


    float _doFissionTIme;


    private void Start()
    {
        _doFissionTIme = Time.time + _fissionTime;
    }


    private void Update()
    {
        if (Time.time > _doFissionTIme)
        {
            Fission();
            Destroy(gameObject);
        }
    }


    void Fission()
    {
        GetComponent<BarrageLauncher>().ShotRing(_bullet, transform.position, transform.rotation, _bulletsNumber);
    }
}
