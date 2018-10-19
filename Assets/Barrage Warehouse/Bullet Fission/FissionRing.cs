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

    public BulletContorllerBase[] componts
    {
        get { return _componts; }
        set { _componts = value; }
    }
    [SerializeField]
    BulletContorllerBase[] _componts;           //这个属性按照现在的实现方式是无效的

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
        GameObject[] bullets = GetComponent<BarrageLauncher>().ShotRing(_bullet, transform.position, transform.rotation, _bulletsNumber);

        foreach (GameObject bullet in bullets)
            foreach (BulletContorllerBase compoent in _componts)
            {
                compoent.CopyToGameobject(bullet);
            }
    }


    public override void CopyToGameobject(GameObject target)
    {
        FissionRing copy = target.AddComponent<FissionRing>();

        copy.bullet = _bullet;
        copy.fissionTime = _fissionTime;
        copy.bulletsNumber = _bulletsNumber;
        copy.componts = _componts;
    }
}
