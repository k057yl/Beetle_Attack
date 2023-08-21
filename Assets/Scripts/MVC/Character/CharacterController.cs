using System.Collections;
using UnityEngine;
using Zenject;


public class CharacterController : MonoBehaviour
{
    [SerializeField] private Config _characterConfig;

    private Model _model;
    private CharacterView _characterView;

    private Camera _mainCamera;
    private Transform _objectTransform;
    private Vector3 _direction;

    private bool _canTakeDamage = true;
    
    //-------------
    private IInput _input;
    private UIController _uiController;
    //-------------
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;
    public Vector3 Direction;//-----------

    [Inject]
    private void Construct(IInput input, UIController uiController)//-------------
    {
        _input = input;
        _uiController = uiController;//-----------
    }

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
        if (_input.IsFireTriggered())
        {
            Fire();
        }
    }

    private void Initialized()
    {
        _model = new Model(Constants.ONE_HUNDRED);

        _characterConfig.Rigidbody2D = GetComponent<Rigidbody2D>();

        _mainCamera = Camera.main;
        _objectTransform = transform;
    }

    private void Move()
    {
        Vector2 movement = _input.GetMovementInput() * _characterConfig.Speed;
        _characterConfig.Rigidbody2D.velocity = new Vector2(movement.x, movement.y);
    }
    
    private void RotateToMouse()
    {
        if (!Input.mousePresent) return;
        
        Direction = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var difference = Direction - _objectTransform.position;
        //var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //var difference = mousePosition - _objectTransform.position;
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Constants.SPAWN_POINT))
        {
            Debug.Log("Collision!!!");
        }
    }

    private IEnumerator EnableDamageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _canTakeDamage = true;
    }

    private void Fire()
    {
        Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
    }
}
