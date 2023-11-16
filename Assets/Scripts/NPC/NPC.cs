using UnityEngine;
using Zenject;

public class NPC : MonoBehaviour
{
    private const float FOLLOWSPEED = 2f;
    private const float DISTANCE = 5f;
    private const float DISTANCE_MAX = 10f;

    [Inject] private CharacterController _characterController;
    [Inject] private PopupController _popupController;
    
    [SerializeField] private GameObject _bulletPrefab;

    private void Update()
    {
        if (_popupController.IsFollow)
        {
            Follow();
        }
    }

    private void Follow()
    {
        Vector3 playerPosition = _characterController.transform.position;
        Vector3 targetPosition = playerPosition + _characterController.transform.forward * DISTANCE;

        float distance = Vector3.Distance(transform.position, playerPosition);
        
        Rotation();

        if (distance >= DISTANCE_MAX)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, FOLLOWSPEED * Time.deltaTime);
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    private void Rotation()
    {
        var diference = _characterController.transform.position - transform.position;
        var rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg - Constants.ANGLE_90;
        
        transform.rotation = Quaternion.Euler(Constants.ZERO, Constants.ZERO, rotateZ);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out CharacterController characterController))
        {
            _popupController.FollowNPC();
        }
        if (col.gameObject.TryGetComponent(out Car car))
        {
            CreateBullet();
        }
    }


    private void CreateBullet()
    {
        if (_bulletPrefab != null)
        {
            GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);

            Vector2 direction = (_characterController.transform.position - transform.position).normalized;

            bullet.GetComponent<Rigidbody2D>().velocity = direction * Constants.TWENTY;
        }
    }
}