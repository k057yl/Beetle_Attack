using System.Collections;
using UnityEngine;
using Zenject;


public class CharacterController : MonoBehaviour
{
    [SerializeField] private Config _characterConfig;

    private Model _model;
    private InpunSystem _inpunSystem;

    private Camera _mainCamera;
    private Transform _objectTransform;
    private Vector3 _direction;

    private bool _canTakeDamage = true;

    private void Start()
    {
        Initialized();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        RotateToMouse();
    }

    private void Initialized()
    {
        _model = new Model(Constants.ONE_HUNDRED);
        _inpunSystem = InpunSystem.Instance;
        
        _characterConfig.Rigidbody2D = GetComponent<Rigidbody2D>();

        _mainCamera = Camera.main;
        _objectTransform = transform;
    }

    private void Move()
    {
        Vector2 movement = _inpunSystem.GetMovementInput() * _characterConfig.Speed;
        _characterConfig.Rigidbody2D.velocity = new Vector2(movement.x, movement.y);
    }
    
    private void RotateToMouse()
    {
        if (!Input.mousePresent) return;

        var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var difference = mousePosition - _objectTransform.position;
        var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - Constants.ANGLE_90;

        _objectTransform.rotation = Quaternion.Euler(Constants.ZERO, Constants.ZERO, rotationZ);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(Constants.EASY_ENEMY) && _canTakeDamage)
        {
            _model.TakeDamage(Constants.TEN);
            Debug.Log("Damage 10");
            _canTakeDamage = false;
            StartCoroutine(EnableDamageAfterDelay(Constants.TWO));
        }
    }
    
    private IEnumerator EnableDamageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _canTakeDamage = true;
    }
}
