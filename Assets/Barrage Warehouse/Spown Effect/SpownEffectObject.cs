using UnityEngine;

public class SpownEffectObject : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer _renderer;

    Sprite _effectSprite;

    float _effectStartDiameter;
    float _effectEndDiameter;

    float _effectStartTime;
    float _effectEndTime;


    //存入数据、计算特效时间
    public void SetEffectData(Sprite sprite,float startDiameter,float endDiameter, float effectTime)
    {
        _renderer.sprite = sprite;

        _effectStartDiameter = startDiameter;
        _effectEndDiameter = endDiameter;

        transform.localScale = Vector3.one * startDiameter;

        _effectStartTime = Time.time;
        _effectEndTime = _effectStartTime + effectTime;
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
        float transparency = Mathf.Lerp(0, 2, rate);

        transform.localScale = Vector3.one * diameter;
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, transparency);
    }



    //特效播放完毕，销毁特效物体，启动所有控制器，销毁特效脚本
    void EffectEnd()
    {
        BarragePool.Set(gameObject);
    }
}
