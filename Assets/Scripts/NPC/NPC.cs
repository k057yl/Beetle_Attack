using UnityEngine;
using Zenject;

public class NPC : MonoBehaviour
{
    private const float FOLLOWSPEED = 2f;
    private const float DISTANCE = 5f;
    private const float DISTANCE_MAX = 10f;

    [Inject] private CharacterController _characterController;
    [Inject] private PopupController _popupController;

    private void Update()
    {
        if (_popupController.IsFollow)
        {
            Follow();
        }
    }

    private void Follow()
    {
        Vector3 playerPosition = _characterController.transform.position;
        Vector3 targetPosition = playerPosition + _characterController.transform.forward * DISTANCE;

        float distance = Vector3.Distance(transform.position, playerPosition);

        if (distance >= DISTANCE_MAX)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, FOLLOWSPEED * Time.deltaTime);
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out CharacterController characterController))
        {
            _popupController.FollowNPC();
        }
    }
}