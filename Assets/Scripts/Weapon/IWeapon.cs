using UnityEngine;

public interface IWeapon
{
    void Fire(Transform shootPoint);
    void ReloadByButton();
    bool GetIsReloading();
}