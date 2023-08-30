using UnityEngine;
using Zenject;

public class Shotgun : WeaponBase
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Camera CameraFPS;

    
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
}
