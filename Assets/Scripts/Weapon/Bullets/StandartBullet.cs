using UnityEngine;

public class StandartBullet : BaseBullet
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
        Destroy(gameObject, LIFE_TIME_PISTOL_BULLET);
    }

    protected override int GetRandomDamage()
    {
        return Random.Range(MIN_PISTOL_DAMAGE, MAX_PISTOL_DAMAGE);
    }

    protected override float GetLifeTime()
    {
        return LIFE_TIME_PISTOL_BULLET;
    }
}
