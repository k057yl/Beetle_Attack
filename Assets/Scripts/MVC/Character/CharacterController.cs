using System.Collections;
using UnityEngine;
using Zenject;


public class CharacterController : MonoBehaviour
{
    [SerializeField] private Config _characterConfig;
    [SerializeField] private Transform _bulletSpawnPoint;

    private Model _model;
    public Model Model => _model;
    private CharacterView _characterView;
    private IInput _input;
    private UIController _uiController;
    
    
    private Transform _objectTransform;
    
    private bool _canTakeDamage = true;
//***************
    private WeaponBase _activeWeapon;
    private WeaponSwitcher _weaponSwitcher;
    

    [Inject]
    private void Construct(IInput input, UIController uiController)
    {
        _input = input;
        _uiController = uiController;
    }

    private void Start()
    {
        Initialized();
        _uiController.UpdateHealthText();//-----------
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

        if (_input.IsReloadTriggered())
        {
            Reloaded();
        }

        HandleWeaponSwitchInput();
    }

    private void Initialized()
    {
        _model = new Model(Constants.ONE_HUNDRED);

        _characterConfig.Rigidbody2D = GetComponent<Rigidbody2D>();

        _objectTransform = transform;

        _activeWeapon = GetComponent<WeaponBase>();
        _weaponSwitcher = GetComponent<WeaponSwitcher>();
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
            _uiController.UpdateHealthText();//---------
            
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
        Debug.Log("Fire");
        _activeWeapon.Fire(_bulletSpawnPoint);
    }

    private void Reloaded()//*****************
    {
        _activeWeapon.ReloadByButton();
    }

    private void HandleWeaponSwitchInput()
    {
        WeaponType weaponType = WeaponType.None;

        if (_input.IsSlotOneTriggered())
        {
            weaponType = WeaponType.Pistol;
            Debug.Log("Pistol");
        }
        else if (_input.IsSlotTwoTriggered())
        {
            weaponType = WeaponType.Shotgun;
            Debug.Log("Shotgun");
        }

        if (weaponType != WeaponType.None)
        {
            _weaponSwitcher.SwitchWeapon(weaponType);
            
            _activeWeapon.SetStrategy(_weaponSwitcher.GetActiveWeapon());
        }
    }
/*
    private void HandleWeaponSwitchInput()
    {
        if (_input.IsSlotOneTriggered())
        {
            Debug.Log("Pistol");
            _weaponSwitcher.SwitchToPistol();
            _activeWeapon.SetStrategy(_weaponSwitcher.GetActiveWeapon());//------------
        }
        else if (_input.IsSlotTwoTriggered())
        {
            Debug.Log("Shotgun");
            _weaponSwitcher.SwitchToShotgun();
            _activeWeapon.SetStrategy(_weaponSwitcher.GetActiveWeapon());//-------------
        }
    }
*/    
}
/*
public class CharacterController : MonoBehaviour
{
    [SerializeField] private Config _characterConfig;
    [SerializeField] private Transform _bulletSpawnPoint;

    private Model _model;
    public Model Model => _model;
    private CharacterView _characterView;
    private IInput _input;
    private UIController _uiController;
    private WeaponSwitcher _weaponSwitcher;
    
    private Transform _objectTransform;
    
    private bool _canTakeDamage = true;
//-------------
    private WeaponBase _weaponBase;//
    

    [Inject]
    private void Construct(IInput input, UIController uiController)
    {
        _input = input;
        _uiController = uiController;
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
        //-----------------
        _weaponBase = new WeaponBase();
        _weaponBase.SetStrategy(new Pistol());
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
        _weaponBase.Fire(_bulletSpawnPoint);
        
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
*/