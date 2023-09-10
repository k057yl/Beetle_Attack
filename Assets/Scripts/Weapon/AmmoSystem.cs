using System.Threading.Tasks;
using UnityEngine;

public class AmmoSystem
{
    private int _maxAmmo;
    private int _magazineSize;
    private int _currentAmmo;
    private float _reloadTime;

    private bool _isReloading;
    
    public AmmoSystem(int maxAmmo, int magazineSize, float reloadTime)
    {
        _maxAmmo = maxAmmo;
        _magazineSize = magazineSize;
        _reloadTime = reloadTime;
        
        _currentAmmo = _magazineSize;
        _isReloading = false;
    }

    public int GetCurrentAmmo()
    {
        return _currentAmmo;
    }

    public int GetMaxAmmo()
    {
        return _maxAmmo;
    }

    public bool IsEmpty()
    {
        return _currentAmmo <= 0;
    }

    public void ConsumeAmmo()
    {
        if (_currentAmmo > 0)
        {
            _currentAmmo--;
        }
    }
    
    public async Task Reload()
    {
        await ReloadAsync();
    }
    
    public async Task ReloadAuto()
    {
        await ConsumeAmmoAsync();
    }
    
    private async Task ReloadAsync()
    {
        if (!_isReloading)
        {
            _isReloading = true;
            await Task.Delay((int)(_reloadTime * 1000));
            _currentAmmo = _magazineSize;
            _isReloading = false;
        }
    }
    
    public async Task ConsumeAmmoAsync()
    {
        if (_currentAmmo > 0)
        {
            _currentAmmo--;
        }
        else if (_maxAmmo > 0)
        {
            await ReloadAsync();
        }
        else
        {
            Debug.Log("Нет патронов.");
        }
    }
}