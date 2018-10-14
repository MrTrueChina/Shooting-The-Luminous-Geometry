using UnityEngine;


public delegate void DieEventDelegate();


[RequireComponent(typeof(QuadtreeCollider))]
public class Life : MonoBehaviour
{
    [SerializeField]
    float _lifePoint;

    QuadtreeCollisionEventDelegate collisionEventDelegent;


    private void Awake()
    {
        SubscribeCollisionEvent();
    }


    void OnQuadtreeCollision(GameObject collider)
    {
        Damage damage = collider.GetComponent<Damage>();

        if (damage != null)
            damage.CauseDamage(this);
    }


    public void TakeDamage(float damage)
    {
        _lifePoint -= damage;

        CheckDie();
    }

    void CheckDie()
    {
        if (_lifePoint <= 0)
            Die();
    }

    public event DieEventDelegate dieEvent;
    void Die()
    {
        if (dieEvent != null)
            dieEvent();

        Destroy(gameObject);
    }


    private void OnDestroy()
    {
        UnsubscribeCollisionEvent();
    }



    void SubscribeCollisionEvent()
    {
        if(collisionEventDelegent == null)
            collisionEventDelegent = new QuadtreeCollisionEventDelegate(OnQuadtreeCollision);

        GetComponent<QuadtreeCollider>().collisionEvent += collisionEventDelegent;
    }

    void UnsubscribeCollisionEvent()
    {
        GetComponent<QuadtreeCollider>().collisionEvent -= collisionEventDelegent;
    }
}
