using UnityEngine;
using Zenject;

public class WeaponSwitcher : MonoBehaviour
{
    [Inject] private Pistol _pistol;
    [Inject] private Shotgun _shotgun;

    private WeaponBase _currentWeapon;

    
    public void SwitchWeaponToPistol()
    {
        _currentWeapon = _pistol;
    }

    public void SwitchWeaponToShotgun()
    {
        _currentWeapon = _shotgun;
    }
    public WeaponBase GetCurrentWeapon()
    {
        return _currentWeapon;
    }
}
