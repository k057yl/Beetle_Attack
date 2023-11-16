using System;
using UnityEngine;

public class Explosion : MonoBehaviour, IDamageable
{
    [SerializeField] private int _hp;
    [SerializeField] private LayerMask _damageableLayer;
    [SerializeField] private int _explosionDamage;
    [SerializeField] private float _explosionRadius;

    [SerializeField] private Animator _animator;
    
    private bool _hasExploded = false;

    void Explode()
    {
        if (!_hasExploded)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, _damageableLayer);

            foreach (Collider2D collider in colliders)
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(_explosionDamage);
                }
            }
            _animator.SetBool(Constants.BOOM, true);

            Destroy(gameObject, 0.35f);

            _hasExploded = true;
        }
    }

    private void Update()
    {
        if (_hp <= 0)
        {
            Explode();
        }
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
    }
}