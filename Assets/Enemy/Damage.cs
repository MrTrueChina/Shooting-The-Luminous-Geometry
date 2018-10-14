using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    float _damage;


    public void CauseDamage(Life life)
    {
        life.TakeDamage(_damage);

        Destroy(gameObject);
    }
}
