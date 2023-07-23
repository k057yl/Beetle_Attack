using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private Config _characterConfig;

    private Model _model;
    private InpunSystem _inpunSystem;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        Initialized();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Initialized()
    {
        _model = new Model(Constants.ONE_HUNDRED);
        _inpunSystem = InpunSystem.Instance;
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Move()
    {
        Vector2 movement = _inpunSystem.GetMovementInput() * _characterConfig.Speed;
        _rigidbody2D.velocity = new Vector2(movement.x, movement.y);
    }

    private void Rotation()
    {
        
    }
}
