using System.Collections;
using UnityEngine;
using Zenject;


public class CharacterController : MonoBehaviour, IDamageable
{
    private const float DESTROY_DROPS = 0.1f;
    private const float FLASH_DURATION = 2f;
    private const float FLASH_INTERVAL = 0.1f;
    

    [SerializeField] private Config _characterConfig;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private SpriteRenderer _childSpriteRenderer;
    [SerializeField] private SpriteRenderer _weaponCurrent;
    [SerializeField] private Sprite[] _spriteWeapon;
    
    [Inject] private IInput _input;
    [Inject] private UIController _uiController;

    private Model _model;
    public Model Model => _model;
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
        RotateToMouse();
    }

    private void Update()
    {
        UpdateHealthText();
        
        HandleWeaponSwitchInput();
    }

    private void Initialized()
    {
        _model = new Model(Constants.ONE_HUNDRED);

        _characterConfig.Rigidbody2D = GetComponent<Rigidbody2D>();
        _weaponSwitcher = GetComponent<WeaponSwitcher>();
        
        SwitchToPistol();
        
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
            SoundBox.Instance.PlaySound(SoundType.Damage);

            _model.TakeDamage(Constants.TEN);
            Debug.Log("Damage 10");
            _canTakeDamage = false;
            StartCoroutine(EnableDamageAfterDelay(Constants.TWO));
            
            StartCoroutine(FlashCharacter());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(Constants.SKULL))
        {
            SoundBox.Instance.PlaySound(SoundType.TakeItem);
            
            _model.CollectedBones(Constants.ONE);
            _uiController.UpdateBonesText();
            
            Destroy(col.gameObject, DESTROY_DROPS);
        }
    }

    private IEnumerator EnableDamageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _canTakeDamage = true;
    }
    
    private IEnumerator FlashCharacter()
    {
        for (float i = 0; i < FLASH_DURATION; i += FLASH_INTERVAL)
        {
            _weaponCurrent.enabled = !_weaponCurrent.enabled;
            _childSpriteRenderer.enabled = !_childSpriteRenderer.enabled;
            
            yield return new WaitForSeconds(FLASH_INTERVAL);
        }

        _childSpriteRenderer.enabled = true;
    }

    private void Shot()
    {
        WeaponBase currentWeapon = _weaponSwitcher.GetCurrentWeapon();
        
        if (currentWeapon != null)
        {
            currentWeapon.Fire(_bulletSpawnPoint);
        }
    }
    
    private void UpdateHealthText()
    {
        _uiController.UpdateHealthText(_model.Health);
    }

    private void SwitchToPistol()
    {
        _weaponSwitcher.SwitchWeaponToPistol();
        _weaponCurrent.sprite = _spriteWeapon[0];
        _weaponSwitcher.GetCurrentWeapon().UpdateText();
    }
    
    private void SwitchToShotgun()
    {
        _weaponSwitcher.SwitchWeaponToShotgun();
        _weaponCurrent.sprite = _spriteWeapon[1];
        _weaponSwitcher.GetCurrentWeapon().UpdateText();
    }
    
    private void HandleWeaponSwitchInput()
    {
        if (_input.IsSlotOneTriggered())
        {
            SwitchToPistol();
            
        }
        else if (_input.IsSlotTwoTriggered())
        {
            SwitchToShotgun();
        }
        
        else if (_input.IsFireTriggered())
        {
            Shot();
        }
        
        else if (_input.IsReloadedTriggered())
        {
            _weaponSwitcher.GetCurrentWeapon().ButtonRecharge();
        }
    }

    public void TakeDamage(int damage)
    {
        Model.TakeDamage(damage);
        
        StartCoroutine(EnableDamageAfterDelay(Constants.TWO));
        StartCoroutine(FlashCharacter());
    }
}
