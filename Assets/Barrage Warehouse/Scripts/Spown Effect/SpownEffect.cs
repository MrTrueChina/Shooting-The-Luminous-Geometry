using UnityEngine;

public class SpownEffect : SpownEffectBase
{
    [SerializeField]
    SpriteRenderer _renderer;

    Sprite _effectSprite;

    [SerializeField]
    float _effectStartDiameter;
    [SerializeField]
    float _effectEndDiameter;

    [SerializeField]
    float _effectStartTransparency;
    [SerializeField]
    float _effectEndTransparency;

    float _effectStartTime;
    float _effectEndTime;



    //初始化
    private void OnEnable()
    {
        transform.localScale = Vector3.one * _effectStartDiameter;

        _effectStartTime = Time.time;
        _effectEndTime = _effectStartTime + effectTIme;
    }



    //播放特效
    private void Update()
    {
        UpdateDiameter();

        if (Time.time > _effectEndTime)
            EffectEnd();
    }
    void UpdateDiameter()
    {
        float rate = Mathf.Min(1, Mathf.InverseLerp(_effectStartTime, _effectEndTime, Time.time));

        float diameter = Mathf.Lerp(_effectStartDiameter, _effectEndDiameter, rate);
        float transparency = Mathf.Lerp(_effectStartTransparency, _effectEndTransparency, rate);

        transform.localScale = Vector3.one * diameter;
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, transparency);
    }



    //特效播放完毕，销毁特效物体，启动所有控制器，销毁特效脚本
    void EffectEnd()
    {
        BarragePool.Set(gameObject);
    }
}
