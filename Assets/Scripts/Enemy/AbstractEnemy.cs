using UnityEngine;
using Zenject;

public abstract class AbstractEnemy : MonoBehaviour
{
    protected const float DESTROY_TIME = 0.3f;
    
    [SerializeField] private Config _config;
    [SerializeField] private int _health = Constants.ONE_HUNDRED;
    [SerializeField] private GameObject _skullPrefab;

    [Inject] private CharacterController _characterController;
    [Inject] private UIController _uiController;

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
    }

    private void Die()
    {
        SoundBox.Instance.PlaySound(SoundType.Dead);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        
        Instantiate(_skullPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, DESTROY_TIME);
    }
}