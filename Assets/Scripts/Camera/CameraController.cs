using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _cam;
    [SerializeField] private float _smoothSpeed;
    
    private Transform _targetCharacter;
    private Vector3 _currentVelocity;

    [Inject]
    private void Construct(CharacterController characterController)
    {
        _targetCharacter = characterController.transform;
    }
    private void FixedUpdate()
    {
        CameraFollow();
    }

    private void CameraFollow()
    {
        if (_targetCharacter != null && _cam != null)
        {
            Vector3 desiredPosition = _targetCharacter.transform.position + _offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(_cam.position, desiredPosition, ref _currentVelocity, _smoothSpeed);
            _cam.position = smoothedPosition;
        }
    }
}