using UnityEngine;
using Zenject;

public abstract class AbstractEnemy : MonoBehaviour
{
    [SerializeField] private Config _config;
    
    private Transform _characterPosition;

    [Inject]
    private void Construct(CharacterController characterController)
    {
        _characterPosition = characterController.transform;
    }

    private void Update()
    {
        if (_characterPosition != null)
        {
            FollowingPlayer();
        }
    }

    protected virtual void FollowingPlayer()
    {
        var diference = _characterPosition.transform.position - transform.position;
        var rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg - Constants.ANGLE_90;
        transform.position = Vector2.MoveTowards(transform.position, _characterPosition.transform.position,
            Time.deltaTime * _config.Speed);
        transform.rotation = Quaternion.Euler(Constants.ZERO, Constants.ZERO, rotateZ);
    }
}
