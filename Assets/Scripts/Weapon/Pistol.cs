using UnityEngine;
using Zenject;

public class Pistol : WeaponBase
{
    [SerializeField] private GameObject _bulletPrefab;
    
    private int _currentAmmo;
    private float _lastFiredTime;

    private IInput _input;

    public Camera CameraFPS;
    
//----------
    [Inject]
    private void Construct(IInput input)
    {
        _input = input;
    }
//-----------    
    private void Awake()
    {
        _currentAmmo = 7;
    }
    
    public override void Fire(Transform bulletSpawnPoint)
    {
        if (!Input.mousePresent) return;
        Debug.Log("Piu-piu");


        var cursorPosition = CameraFPS.ScreenToWorldPoint(Input.mousePosition);


        var bullet = Instantiate(_bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);//-----------
        
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.Lerp(transform.position, cursorPosition, 100f) * 500f);//-----------
    }
    
    public override void ButtonRecharge()
    {
        
    }
}