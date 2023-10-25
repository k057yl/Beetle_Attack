using System.Collections;
using UnityEngine;

public class Shotgun : WeaponBase
{
    [SerializeField] private GameObject _bulletPrefab;

    private IAmmoSystem _ammoSystem;

    private void Awake()
    {
        _ammoSystem = new AmmoSystem(_maxAmmo, _maxMagazine);
    }
    
    protected override void FireLogic(Transform bulletSpawnPoint, Vector3 cursorPosition)
    {
        int currentMagazineAmmo = _ammoSystem.GetCurrentMagazineAmmo();

        if (currentMagazineAmmo > ZERRO)
        {
            var bullet = Instantiate(_bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.Lerp(transform.position, cursorPosition, ONE_HUNDRED) * FIVE_HUNDRED);

            _ammoSystem.UseAmmo();
            _maxAmmo = _ammoSystem.GetCurrentAmmo();
            _maxMagazine = _ammoSystem.GetCurrentMagazineAmmo();

            UpdateText();
        }

        if (_maxMagazine <= ZERRO)
        {
            _uiController.CoolDownPistolImage();
            StartCoroutine(Reload());
        }
    }
 
    public override void ButtonRecharge()
    {
        if (_ammoSystem.CanReload() && !_isReloading)
        {
            _uiController.CoolDownShotgunImage();
            StartCoroutine(ReloadButton());
        }
    }
    
    private IEnumerator Reload()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTime);

        _ammoSystem.Reload();

        _maxMagazine = _ammoSystem.GetCurrentMagazineAmmo();
        _maxAmmo = _ammoSystem.GetCurrentAmmo();
        
        UpdateText();
        _isReloading = false;
    }
    
    private IEnumerator ReloadButton()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTime);

        _ammoSystem.ReloadButton();

        _maxMagazine = _ammoSystem.GetCurrentMagazineAmmo();
        _maxAmmo = _ammoSystem.GetCurrentAmmo();
        
        UpdateText();
        _isReloading = false;
    }
}
