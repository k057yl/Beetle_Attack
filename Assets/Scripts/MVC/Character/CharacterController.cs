using System.Collections;
using UnityEngine;
using Zenject;


public class CharacterController : MonoBehaviour
{
    [SerializeField] private Config _characterConfig;
    [SerializeField] private Transform _bulletSpawnPoint;
    
    [Inject] private IInput _input;
    [Inject] private UIController _uiController;

    private Model _model;
    public Model Model => _model;//-------------------
    private CharacterView _characterView;
    private WeaponSwitcher _weaponSwitcher;
    
    private Transform _objectTransform;
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
        
        if (_input.IsFireTriggered())
        {
            Shot();
        }
        
        UpdateHealthText();
        
        HandleWeaponSwitchInput();
    }

    private void Initialized()
    {
        _model = new Model(Constants.ONE_HUNDRED);

        _characterConfig.Rigidbody2D = GetComponent<Rigidbody2D>();
        _weaponSwitcher = GetComponent<WeaponSwitcher>();
        
        _objectTransform = transform;

        UpdateHealthText();
        
        _weaponSwitcher.SwitchWeaponToPistol();
    }

    private void Move()
    {
        Vector2 movement = _input.GetMovementInput() * _characterConfig.Speed;
        _characterConfig.Rigidbody2D.velocity = new Vector2(movement.x, movement.y);
    }
    
    private void RotateToMouse()
    {
        if (!Input.mousePresent) return;
        
        var direction = _input.GetCursorPosition();
        var difference = direction - _objectTransform.position;
        
        var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - Constants.ANGLE_90;

        _objectTransform.rotation = Quaternion.Euler(Constants.ZERO, Constants.ZERO, rotationZ);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(Constants.ENEMY) && _canTakeDamage)
        {
            _model.TakeDamage(Constants.TEN);
            Debug.Log("Damage 10");
            _canTakeDamage = false;
            StartCoroutine(EnableDamageAfterDelay(Constants.TWO));
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Constants.SKULL))
        {
            col.gameObject.transform.Translate(Vector3.Lerp(transform.position, Vector3.up * 3f, 5f));
            Destroy(col.gameObject, Constants.ONE);
        }
    }

    private IEnumerator EnableDamageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _canTakeDamage = true;
    }

    private void Shot()
    {
        IWeapon currentWeapon = _weaponSwitcher.GetCurrentWeapon();
        if (currentWeapon != null)
        {
            currentWeapon.Fire(_bulletSpawnPoint);
        }
    }
    
    private void UpdateHealthText()
    {
        _uiController.UpdateHealthText(_model.Health);
    }

    private void HandleWeaponSwitchInput()
    {
        if (_input.IsSlotOneTriggered())
        {
            _weaponSwitcher.SwitchWeaponToPistol();
        }
        else if (_input.IsSlotTwoTriggered())
        {
            _weaponSwitcher.SwitchWeaponToShotgun();
        }
    }
}
