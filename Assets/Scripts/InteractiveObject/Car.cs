using UnityEngine;

public class Car : MonoBehaviour, IDamageable
{
    [SerializeField] private int _hp = 50;

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        
        if (_hp <= Constants.ZERO)
        {
            Destroy(gameObject);
        }
    }
}
