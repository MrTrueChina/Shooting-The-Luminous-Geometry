using UnityEngine;

public class BulletContorllerBase : MonoBehaviour
{
    public virtual void Restore() { }       //复原方法，在存入池的时候进行复原，在取出时就是全新的了
}
