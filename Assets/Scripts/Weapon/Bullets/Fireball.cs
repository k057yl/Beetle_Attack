using UnityEngine;

public class Fireball : BaseBullet
{
    private void Start()
    {
        DestroyBulletTime();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollisionCharacter(collision);
    }
    
    public override void DestroyBulletTime()
    {
        Destroy(gameObject, LIFE_TIME_FIREBALL);
    }

    protected override int GetRandomDamage()
    {
        return Random.Range(MIN_FIREBALL_DAMAGE, MAX_FIREBALL_DAMAGE);
    }

    protected override float GetLifeTime()
    {
        return LIFE_TIME_FIREBALL;
    }
}
