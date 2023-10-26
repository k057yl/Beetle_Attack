using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class AbstractEnemy : MonoBehaviour
{
    private const float DESTROY_TIME = 0.3f;
    
    [SerializeField] private Config _config;
    [SerializeField] private int _health;
    [SerializeField] private GameObject _skullPrefab;
    [SerializeField] private GameObject _healthBar;

    [Inject] private CharacterController _characterController;
    [Inject] private UIController _uiController;

    protected abstract int MaxHealth { get; }
    protected abstract SoundType DeathSoundType { get; }
    
    private void Start()
    {
        ConversionHP();
    }

    private void ConversionHP()
    {
        float fillAmount = (float)_health / MaxHealth;
        Image healthImage = _healthBar.GetComponentInChildren<Image>();
        healthImage.fillAmount = fillAmount;

        if (fillAmount > 0.6f)
        {
            healthImage.color = Color.green;
        }
        else if (fillAmount > 0.25f)
        {
            healthImage.color = Color.yellow;
        }
        else
        {
            healthImage.color = Color.red;
        }
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