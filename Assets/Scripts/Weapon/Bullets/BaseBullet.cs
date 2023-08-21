using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    //-----------------
    private Vector2 _direction;
    
    //------------
    private AbstractEnemy _abstractEnemy;
    private CharacterController _characterController;

    [Inject]
    private void Cunstruct(AbstractEnemy abstractEnemy, CharacterController characterController)//-----------
    {
        _abstractEnemy = abstractEnemy;
        _characterController = characterController;
    }

    private void Start()
    {
        //_direction = _characterController.Direction;
        //Vector2 position = _characterController.Direction;
        
        _rigidbody2D.velocity = Vector2.right * _speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out EasyEnemy easyEnemy))
        {
            easyEnemy.TakeDamage(25);
            Destroy(gameObject);
        }
    }
}
