using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private Config _characterConfig;

    private Model _model;
    private InpunSystem _inpunSystem;
    private Rigidbody2D _rigidbody2D;
    
    private Camera mainCamera;
    private Transform objectTransform;
    private Vector3 _direction;
    

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
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        mainCamera = Camera.main;
        objectTransform = transform;
    }

    private void Move()
    {
        Vector2 movement = _inpunSystem.GetMovementInput() * _characterConfig.Speed;
        _rigidbody2D.velocity = new Vector2(movement.x, movement.y);
    }

    private void RotateToMouse()
    {
        if (!Input.mousePresent) return;

        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var difference = mousePosition - objectTransform.position;
        var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - Constants.ANGLE_90;

        objectTransform.rotation = Quaternion.Euler(Constants.ZERO, Constants.ZERO, rotationZ);
    }
}
