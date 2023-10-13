using UnityEngine;

public interface IWeapon
{
    void Fire(Transform bulletSpawnPoint);
    void ButtonRecharge();
    bool Reload();
}