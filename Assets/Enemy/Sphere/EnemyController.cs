using UnityEngine;

[RequireComponent(typeof(QuadtreeCollider))]
[RequireComponent(typeof(Life))]
[RequireComponent(typeof(EnemyShot))]
[RequireComponent(typeof(EnemyMove))]
public class EnemyController : MonoBehaviour
{
    QuadtreeCollider _collider;
    Life _life;
    EnemyShot _shot;
    EnemyMove _move;

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
        _shot = GetComponent<EnemyShot>();
        _move = GetComponent<EnemyMove>();
    }
    void CreatEventDelegate()
    {
        _collisionEventDelegate = new QuadtreeCollisionEventDelegate(OnCollision);
        _dieEventDelagate = new DieEventDelegate(OnDie);
    }



    //激活
    private void OnEnable()
    {
        SubscribeEvents();
        EnableCmoponents();
    }
    void EnableCmoponents()
    {
        _collider.enabled = true;
        _shot.enabled = true;
        _move.enabled = true;
    }


    
    //碰撞
    void OnCollision(GameObject collider)
    {
        if (collider.tag == "Player Bullet")
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



    //无效化
    private void OnDisable()
    {
        UnsubscribsEvent();
        DisableCoponents();
    }
    void DisableCoponents()
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
    void UnsubscribsEvent()
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
