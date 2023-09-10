using UnityEngine;


public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _pistolPrefab;
    [SerializeField] private GameObject _shotgunPrefab;

    private WeaponBase _pistol;
    private WeaponBase _shotgun;
    private WeaponBase _activeWeapon;

    private void Start()
    {
        _pistol = Instantiate(_pistolPrefab).GetComponent<WeaponBase>();
        _shotgun = Instantiate(_shotgunPrefab).GetComponent<WeaponBase>();
        
        SwitchWeapon(WeaponType.Pistol);
    }

    public void SwitchWeapon(WeaponType weaponType)
    {
        _pistol.gameObject.SetActive(weaponType == WeaponType.Pistol);
        _shotgun.gameObject.SetActive(weaponType == WeaponType.Shotgun);
        
        _activeWeapon = weaponType == WeaponType.Pistol ? _pistol : _shotgun;
    }

    public WeaponBase GetActiveWeapon()
    {
        return _activeWeapon;
    }
}