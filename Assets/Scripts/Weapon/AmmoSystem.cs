using UnityEngine;

public class AmmoSystem : IAmmoSystem
{
    private const int ZERRO = 0; 
    
    private int _maxAmmo;
    private int _currentAmmo;
    private int _maxMagazineAmmo;
    private int _currentMagazineAmmo;

    public AmmoSystem(int maxAmmo, int maxMagazineAmmo)
    {
        _maxAmmo = maxAmmo;
        _currentAmmo = maxAmmo;
        _maxMagazineAmmo = maxMagazineAmmo;
        _currentMagazineAmmo = maxMagazineAmmo;
    }
    
    public int GetCurrentAmmo()
    {
        return _currentAmmo;
    }
    
    public int GetCurrentMagazineAmmo()
    {
        return _currentMagazineAmmo;
    }
    
    public void UseAmmo()
    {
        _currentMagazineAmmo--;
    }

    public void AmmoKitFifty()
    {
        _currentAmmo += 50;
        if (_currentAmmo > _maxAmmo)
        {
            _currentAmmo = _maxAmmo;
        }
    }

    public void AmmoKitTen()
    {
        _currentAmmo += 10;
        if (_currentAmmo > _maxAmmo)
        {
            _currentAmmo = _maxAmmo;
        }
    }
    
    public void Reload()
    {
        if (_currentMagazineAmmo == ZERRO && _currentAmmo > ZERRO)
        {
            int ammoToReload = Mathf.Min(_maxMagazineAmmo, _currentAmmo);
            _currentMagazineAmmo = ammoToReload;
            _currentAmmo -= ammoToReload;
        }
    }
    
    public void ReloadButton()
    {
        if (_currentMagazineAmmo < _maxMagazineAmmo && _currentAmmo > ZERRO)
        {
            int ammoToReload = Mathf.Min(_maxMagazineAmmo, _currentAmmo);
            _currentMagazineAmmo = ammoToReload;
            _currentAmmo -= ammoToReload;
        }
    }

    public bool CanReload()
    {
        return _currentAmmo > ZERRO && _currentMagazineAmmo < _maxMagazineAmmo;
    }
}