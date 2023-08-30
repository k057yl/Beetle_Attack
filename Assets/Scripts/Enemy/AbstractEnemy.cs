using System;
using UnityEngine;
using Zenject;


public abstract class AbstractEnemy : MonoBehaviour
{
    [SerializeField] private Config _config;
    [SerializeField] private int _health = Constants.ONE_HUNDRED;
    [SerializeField] private GameObject _skullPrefab;
    
    private CharacterController _characterController;
    private UIController _uiController;//------------

    [Inject]
    private void Construct(CharacterController characterController, UIController uiController)
    {
        _characterController = characterController;
        _uiController = uiController;//------------
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
            if (_uiController != null)
            {
                _characterController.Model.TakeKills(Constants.ONE);
                _uiController.UpdateKilledText();
            }
            Die();
        }
    }

    private void Die()
    {
        Instantiate(_skullPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}