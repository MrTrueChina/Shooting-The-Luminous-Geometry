using UnityEngine;


[RequireComponent(typeof(QuadtreeCollider))]
[RequireComponent(typeof(Life))]
[RequireComponent(typeof(PlayerShot))]
[RequireComponent(typeof(DragMove))]
public class PlayerController : MonoBehaviour
{
    QuadtreeCollider _collider;
    Life _life;
    PlayerShot _shot;
    DragMove _move;

    QuadtreeCollisionEventDelegate _collisionEventDelegate;
    DieEventDelegate _dieEventDelagate;



    //初始化
    private void Awake()
    {
        GetAndSetCompoents();
        CreatEventDelegate();
    }
    void GetAndSetCompoents()
    {
        _collider = GetComponent<QuadtreeCollider>();
        _life = GetComponent<Life>();
        _shot = GetComponent<PlayerShot>();
        _move = GetComponent<DragMove>();
    }
    void CreatEventDelegate()
    {
        _collisionEventDelegate = new QuadtreeCollisionEventDelegate(OnCollision);
        _dieEventDelagate = new DieEventDelegate(OnDie);
    }



    //起效
    private void OnEnable()
    {
        SubscribeEvents();
        EnableComponents();
    }
    void EnableComponents()
    {
        _collider.enabled = true;
        _shot.enabled = true;
        _move.enabled = true;
    }

    

    //碰撞
    void OnCollision(GameObject collider)
    {
        if (collider.tag == "Enemy Bullet")
            GetShot(collider.GetComponent<Damage>());
    }
    void GetShot(Damage damage)
    {
        damage.CauseDamage(_life);
    }
    void OnDie()
    {
        Destroy(gameObject);
    }
    


    //失效
    private void OnDisable()
    {
        UnsubscribeEvents();
        DisableComponents();
    }
    void DisableComponents()
    {
        _collider.enabled = false;
        _shot.enabled = false;
        _move.enabled = false;
    }
    



    //订阅和取消订阅
    void SubscribeEvents()
    {
        SubscribeCollisionEvent();
        SubscribeDieEvent();
    }
    void UnsubscribeEvents()
    {
        UnsubscribeCollisionEvent();
        UnsubscribeDieEvent();
    }

    void SubscribeCollisionEvent()
    {
        _collider.collisionEvent += _collisionEventDelegate;
    }
    void UnsubscribeCollisionEvent()
    {
        _collider.collisionEvent -= _collisionEventDelegate;
    }

    void SubscribeDieEvent()
    {
        _life.dieEvent += _dieEventDelagate;
    }
    void UnsubscribeDieEvent()
    {
        _life.dieEvent -= _dieEventDelagate;
    }
}
