using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    public abstract void Fire(Transform bulletSpawnPoint);

    public abstract void ButtonRecharge();

    public bool Reload()
    {
        return false;
    }
}