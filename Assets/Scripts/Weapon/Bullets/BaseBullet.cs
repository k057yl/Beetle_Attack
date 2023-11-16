using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseBullet : MonoBehaviour, IBullet
{
    [SerializeField] protected GameObject _impactPrefab;
    
    protected const float LIFE_TIME_PISTOL_BULLET = 0.5f;
    protected const float LIFE_TIME_SHOTGUN_BULLET = 0.25f;
    protected const float LIFE_TIME_FIREBALL = 5.5f;
    
    //Fireball
    protected const int MIN_FIREBALL_DAMAGE = 30;
    protected const int MAX_FIREBALL_DAMAGE = 51;
    
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
        if (collision.gameObject.CompareTag(Constants.ENEMY) || collision.gameObject.CompareTag(Constants.BARREL))
        {
            AbstractEnemy enemy = collision.gameObject.GetComponent<AbstractEnemy>();
            
            if (enemy != null)
            {
                Vector2 contactPoint = collision.contacts[0].point;
                var impact = Instantiate(_impactPrefab, contactPoint, Quaternion.identity);
                Destroy(impact, Constants.TWO_TENTHS);
                
                int randomDamage = GetRandomDamage();
                enemy.TakeDamage(randomDamage);
            }
            
            Explosion explosion = collision.gameObject.GetComponent<Explosion>();
            if (explosion != null)
            {
                Vector2 contactPoint = collision.contacts[0].point;
                var impact = Instantiate(_impactPrefab, contactPoint, Quaternion.identity);
                Destroy(impact, Constants.TWO_TENTHS);
                
                int randomDamage = GetRandomDamage();
                explosion.TakeDamage(randomDamage);
            }
            
            Destroy(gameObject);
        }
    }
    
    protected void HandleCollisionCharacter(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Car car))
        {
            if (collision.gameObject.TryGetComponent(out CharacterController characterController))
            {
                if (characterController != null)
                {
                    Vector2 contactPoint = collision.contacts[0].point;
                    var impact = Instantiate(_impactPrefab, contactPoint, Quaternion.identity);
                    Destroy(impact, Constants.TWO_TENTHS);
                
                    int randomDamage = GetRandomDamage();
                    characterController.Model.TakeDamage(randomDamage);
                }
            
                Destroy(gameObject);
            }
        }
        else
        {
            Vector2 contactPoint = collision.contacts[0].point;
            var impact = Instantiate(_impactPrefab, contactPoint, Quaternion.identity);
            Destroy(impact, Constants.TWO_TENTHS);
        
            car.TakeDamage(51);
        }
        /*
        if (collision.gameObject.TryGetComponent(out CharacterController characterController))
        {
            characterController = collision.gameObject.GetComponent<CharacterController>();

            if (characterController != null)
            {
                Vector2 contactPoint = collision.contacts[0].point;
                var impact = Instantiate(_impactPrefab, contactPoint, Quaternion.identity);
                Destroy(impact, Constants.TWO_TENTHS);
                
                int randomDamage = GetRandomDamage();
                characterController.Model.TakeDamage(randomDamage);
            }
            
            Destroy(gameObject);
        }

        if (collision.gameObject.TryGetComponent(out Car car))
        {
            car = collision.gameObject.GetComponent<Car>();

            if (car != null)
            {
                Vector2 contactPoint = collision.contacts[0].point;
                var impact = Instantiate(_impactPrefab, contactPoint, Quaternion.identity);
                Destroy(impact, Constants.TWO_TENTHS);
                
                car.TakeDamage(51);
            }
        }
        */
    }
}