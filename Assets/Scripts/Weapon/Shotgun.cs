using System.Threading.Tasks;
using UnityEngine;


public class Shotgun : WeaponBase
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Camera _cameraFPS;

    private void Awake()
    {
        InitializeAmmo(50, 2, 3);
    }
    
    public override void Fire(Transform bulletSpawnPoint)
    {
        if (!_ammo.IsEmpty())
        {
            Debug.Log("Piu-piu");

            var cursorPosition = _cameraFPS.ScreenToWorldPoint(Input.mousePosition);

            var bullet = Instantiate(_bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.Lerp(transform.position, cursorPosition, 100f) * 500f);
            _ammo.ConsumeAmmo();
        }
        else
        {
            _ammo.ReloadAuto();
        }
    }

    public override void ReloadByButton()
    {
        Debug.Log("Start");
        _ammo.Reload();
        Debug.Log("Start reload");
    }

/*
public class Shotgun : WeaponBase
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Camera CameraFPS;
//**************
    private AmmoSystem _ammoSystem;
    private UIController _uiController;

    public Shotgun(UIController uiController)
    {
        _ammoSystem = new AmmoSystem(30, 2, uiController);
        _uiController = uiController;
    }
    
    public override void Fire(Transform bulletSpawnPoint)
    {
        if (_ammoSystem.CanFire())
        {
            Debug.Log("Bang");

            var cursorPosition = CameraFPS.ScreenToWorldPoint(Input.mousePosition);

            var bullet = Instantiate(_bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>()
                .AddForce(Vector2.Lerp(transform.position, cursorPosition, Constants.ONE_HUNDRED) *
                          Constants.FIVE_HUNDRED);
        }
    }

    public override void ButtonRecharge()
    {
    }
    */
 //***************   
 /*   
    public override void Fire(Transform bulletSpawnPoint)
    {
        Debug.Log("Bang");

        var cursorPosition = CameraFPS.ScreenToWorldPoint(Input.mousePosition);
        
        var bullet = Instantiate(_bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            
        bullet.GetComponent<Rigidbody2D>()
            .AddForce(Vector2.Lerp(transform.position, cursorPosition, Constants.ONE_HUNDRED) * Constants.FIVE_HUNDRED);
    }

    public override void ButtonRecharge()
    {
    }
*/    
}
