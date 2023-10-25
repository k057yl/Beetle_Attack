using System.Collections;
using UnityEngine;
using Zenject;

public abstract class WeaponBase : MonoBehaviour
{
    private const float CAN_FIRE_TIME = 0.4f;
    
    protected const int ZERRO = 0;
    protected const int ONE_HUNDRED = 100;
    protected const int FIVE_HUNDRED = 500;
    
    [SerializeField] protected float _reloadTime;
    [SerializeField] protected int _maxAmmo;
    [SerializeField] protected int _maxMagazine;
    [SerializeField] protected Camera _cameraFPS;
    
    [Inject] protected UIController _uiController;
    
    protected bool _isReloading = false;
    private bool _canFire = true;

    protected abstract void FireLogic(Transform bulletSpawnPoint, Vector3 cursorPosition);
    
    public void Fire(Transform bulletSpawnPoint)
    {
        if (!_isReloading && _canFire)
        {
            SoundBox.Instance.PlaySound(SoundType.Shoot);
            var cursorPosition = _cameraFPS.ScreenToWorldPoint(Input.mousePosition);
            FireLogic(bulletSpawnPoint, cursorPosition);
            StartCoroutine(ShotCooldown());
        }
    }
    public abstract void ButtonRecharge();

    public void UpdateText()
    {
        _uiController.UpdateAmmoText(_maxMagazine,_maxAmmo);
    }
    
    private IEnumerator ShotCooldown()
    {
        _canFire = false;
        yield return new WaitForSeconds(CAN_FIRE_TIME);
        _canFire = true;
    }
}