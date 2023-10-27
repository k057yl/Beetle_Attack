using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class AbstractEnemy : MonoBehaviour
{
    private const int DISTANCE_RAY = 10;
    
    private const float DESTROY_TIME = 0.3f;
    private const float LINE_COLOR_YELLOW = 0.6f;
    private const float LINE_COLOR_RED = 0.25f;
    private const float SHOOT_DELAY = 2.5f;
    
    
    [SerializeField] private Config _config;
    [SerializeField] private int _health;
    [SerializeField] private GameObject _skullPrefab;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private GameObject _bulletPrefab;

    
    [Inject] private CharacterController _characterController;
    [Inject] private UIController _uiController;

    
    protected abstract int MaxHealth { get; }
    protected abstract SoundType DeathSoundType { get; }
    protected abstract bool CanShoot();
    
    
    private bool _canShoot = true;
    
    
    private void Start()
    {
        ConversionHP();
    }

    private void Update()
    {
        if (_characterController == null)
            return;

        FollowingPlayer();
            
        if (_canShoot && CanShoot() && CanSeePlayer())
        {
            StartCoroutine(DelayedShoot());
            _canShoot = false;
        }
    }
    
    private void CreateBullet()
    {
        if (_bulletPrefab != null)
        {
            GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        
            Vector2 direction = (_characterController.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * Constants.TWENTY;
        }
    }
  
    private IEnumerator DelayedShoot()
    {
        yield return new WaitForSeconds(SHOOT_DELAY);
        _canShoot = true;
        
        if (CanShoot())
        {
            CreateBullet();
        }
    }
    
    private bool CanSeePlayer()
    {
        Vector2 direction = _characterController.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, DISTANCE_RAY, LayerMask.GetMask(Constants.CHARACTER));
        if (hit.collider != null && hit.collider.CompareTag(Constants.CHARACTER))
        {
            return true;
        }
        return false;
    }
    
    private void ConversionHP()
    {
        float fillAmount = (float)_health / MaxHealth;
        Image healthImage = _healthBar.GetComponentInChildren<Image>();
        healthImage.fillAmount = fillAmount;

        if (fillAmount > LINE_COLOR_YELLOW)
        {
            healthImage.color = Color.green;
        }
        else if (fillAmount > LINE_COLOR_RED)
        {
            healthImage.color = Color.yellow;
        }
        else
        {
            healthImage.color = Color.red;
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
            if (_uiController != null)
            {
                _characterController.Model.TakeKills(Constants.ONE);
                _uiController.UpdateKilledText();
            }
            Die();
        }
        ConversionHP();
    }

    private void Die()
    {
        SoundBox.Instance.PlaySound(DeathSoundType);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        
        Instantiate(_skullPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, DESTROY_TIME);
    }
}