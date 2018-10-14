using UnityEngine;


public delegate void DieEventDelegate();


[RequireComponent(typeof(QuadtreeCollider))]
public class Life : MonoBehaviour
{
    [SerializeField]
    float _lifePoint;


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
    }
}
