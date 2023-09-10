using UnityEngine;

public class WeaponBase : MonoBehaviour, IWeapon
{
    private IWeapon _weapon;

    protected AmmoSystem _ammo;
    
    protected void InitializeAmmo(int maxAmmo, int magazineSize, float reloaded)//, UIController uiController
    {
        _ammo = new AmmoSystem(maxAmmo, magazineSize, reloaded);//, uiController
    }

    public void SetStrategy(IWeapon strategy)
    {
        _weapon = strategy;
    }
    
    public virtual void Fire(Transform shootPoint)
    {
        if (_weapon != null)
        {
            _weapon.Fire(shootPoint);
        }
    }
    
    public virtual void ReloadByButton()
    {
        _weapon.ReloadByButton();
    }
    
    public bool GetIsReloading()
    {
        return _weapon.GetIsReloading();
    }
}