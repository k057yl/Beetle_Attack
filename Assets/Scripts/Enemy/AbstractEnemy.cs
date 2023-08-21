using UnityEngine;
using Zenject;


public abstract class AbstractEnemy : MonoBehaviour
{
    [SerializeField] private Config _config;

    [SerializeField] private int _health;//------
    [SerializeField] private GameObject _DieEffect;
    
    private CharacterController _characterController;

    [Inject]
    private void Construct(CharacterController characterController)
    {
        _characterController = characterController;
    }

    private void Update()
    {
        if (_characterController != null)
        {
            FollowingPlayer();
        }
    }

    private void FollowingPlayer()
    {
        var diference = _characterController.transform.position - transform.position;
        var rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg - Constants.ANGLE_90;
        transform.position = Vector2.MoveTowards(transform.position, _characterController.transform.position,
            Time.deltaTime * _config.Speed);
        transform.rotation = Quaternion.Euler(Constants.ZERO, Constants.ZERO, rotateZ);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= Constants.ZERO)
        {
            Debug.Log("Die");
            Instantiate(_DieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
/*
public abstract class AbstractEnemy : MonoBehaviour
{
    [SerializeField] private Config _config;
    
    private CharacterController _characterController;
    private Quaternion _initialRotation;
    

    [Inject]
    private void Construct(CharacterController characterController)
    {
        _characterController = characterController;
        _initialRotation = Quaternion.Euler(0f, 0f, Constants.MINUS_ANGLE_90);
    }

    private void Update()
    {
        FollowingPlayer();
    }

    protected virtual void FollowingPlayer()
    {
        var difference = _characterController.transform.position - transform.position;
        var rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.position = Vector2.MoveTowards(transform.position, _characterController.transform.position,
            Time.deltaTime * _config.Speed);
        transform.rotation = _initialRotation * Quaternion.Euler(0f, 0f, rotateZ);
    }
}
*/