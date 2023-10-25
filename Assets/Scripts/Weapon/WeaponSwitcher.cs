using UnityEngine;
using Zenject;

public class WeaponSwitcher : MonoBehaviour
{
    [Inject] private Pistol _pistol;
    [Inject] private Shotgun _shotgun;

    private WeaponBase _currentWeapon;

    
    public void SwitchWeaponToPistol()
    {
        //Debug.Log("pistol");
        
        _currentWeapon = _pistol;
    }

    public void SwitchWeaponToShotgun()
    {
        //Debug.Log("shotgun");
        
        _currentWeapon = _shotgun;
    }
    public WeaponBase GetCurrentWeapon()
    {
        return _currentWeapon;
    }
}
