using UnityEngine;

public class ShotgunBullet : BaseBullet
{
    private void Start()
    {
        DestroyBulletTime();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
    }

    public override void DestroyBulletTime()
    {
        Destroy(gameObject, LIFE_TIME_SHOTGUN_BULLET);
    }

    protected override int GetRandomDamage()
    {
        return Random.Range(MIN_SHOTGUN_DAMAGE, MAX_SHOTGUN_DAMAGE);
    }

    protected override float GetLifeTime()
    {
        return LIFE_TIME_SHOTGUN_BULLET;
    }
}
