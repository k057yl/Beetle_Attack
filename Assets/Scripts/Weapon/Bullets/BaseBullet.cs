using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseBullet : MonoBehaviour, IBullet
{
    public abstract void DestroyBulletTime();
}