using UnityEngine;

public class ShotgunBullet : BaseBullet
{
    [SerializeField] private GameObject _impactPrefab;
    [SerializeField] private int _damage;

    private void Start()
    {
        DestroyBulletTime();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.ENEMY))
        {
            AbstractEnemy enemy = collision.gameObject.GetComponent<AbstractEnemy>();
            
            if (enemy != null)
            {
                Vector2 contactPoint = collision.contacts[0].point;
                var impact = Instantiate(_impactPrefab, contactPoint, Quaternion.identity);
                Destroy(impact, Constants.TWO_TENTHS);
                enemy.TakeDamage(_damage);
            }
            
            Destroy(gameObject);
        }
    }

    public override void DestroyBulletTime()
    {
        Destroy(gameObject, Constants.ONE);
    }
}
