using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Life))]
[RequireComponent(typeof(DragMove))]
public class PlayerController : MonoBehaviour
{
    QuadtreeCollider _collider;
    Life _life;

    QuadtreeCollisionEventDelegate _collisionEventDelegate;
    DieEventDelegate _dieEventDelagate;



    private void Awake()
    {
        GetAndSetCompoents();
        CreatEventDelegate();
        SubscribeEvents();
    }

    void GetAndSetCompoents()
    {
        _collider = GetComponent<QuadtreeCollider>();
        _life = GetComponent<Life>();
    }

    void CreatEventDelegate()
    {
        _collisionEventDelegate = new QuadtreeCollisionEventDelegate(OnCollision);
        _dieEventDelagate = new DieEventDelegate(OnDie);
    }

    private void SubscribeEvents()
    {
        SubscribeCollisionEvent();
        SubscribeDieEvent();
    }



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



    private void OnDisable()
    {
        UnsubscribeCollisionEvent();
        UnsubscribeDieEvent();
    }



    //订阅和取消订阅
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
