using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private Pistol _pistol;
    [SerializeField] private Shotgun _shotgun;
    

    private IWeapon _currentWeapon;

    public void SwitchWeaponToPistol()
    {
        Debug.Log("pistol");
        
        _currentWeapon = _pistol;
    }

    public void SwitchWeaponToShotgun()
    {
        Debug.Log("shotgun");
        
        _currentWeapon = _shotgun;
    }

    public IWeapon GetCurrentWeapon()
    {
        return _currentWeapon;
    }
}
