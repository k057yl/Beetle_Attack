using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseBullet : MonoBehaviour, IBullet
{
    [SerializeField] protected GameObject _damageTextPrefab;
    [SerializeField] protected GameObject _impactPrefab;
    
    protected const float LIFE_TIME_PISTOL_BULLET = 0.5f;
    protected const float LIFE_TIME_SHOTGUN_BULLET = 0.25f;
    
    //Pistol
    protected const int MIN_PISTOL_DAMAGE = 7;
    protected const int MAX_PISTOL_DAMAGE = 13;
    
    //Shotgun
    protected const int MIN_SHOTGUN_DAMAGE = 45;
    protected const int MAX_SHOTGUN_DAMAGE = 66;
    
    public abstract void DestroyBulletTime();
    protected abstract int GetRandomDamage();
    protected abstract float GetLifeTime();
    
    protected void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.ENEMY))
        {
            AbstractEnemy enemy = collision.gameObject.GetComponent<AbstractEnemy>();
            
            if (enemy != null)
            {
                Vector2 contactPoint = collision.contacts[0].point;
                var impact = Instantiate(_impactPrefab, contactPoint, Quaternion.identity);
                Destroy(impact, Constants.TWO_TENTHS);
                
                int randomDamage = GetRandomDamage();
                enemy.TakeDamage(randomDamage);
                
                var damageText = Instantiate(_damageTextPrefab, contactPoint, Quaternion.identity);
                damageText.GetComponent<TextMesh>().text = randomDamage.ToString();
                Destroy(damageText, GetLifeTime());
            }
            
            Destroy(gameObject);
        }
    }
}