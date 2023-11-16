using UnityEngine;

public class NPCBullet : BaseBullet
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
        Destroy(gameObject, LIFE_TIME_PISTOL_BULLET);
    }

    protected override int GetRandomDamage()
    {
        return Constants.ZERO;
    }

    protected override float GetLifeTime()
    {
        return LIFE_TIME_PISTOL_BULLET;
    }
}
