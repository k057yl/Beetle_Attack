using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    [SerializeField] private CharacterFactory _characterFactory;
    [SerializeField] private GameObject _cameraPrefab;

    private CharacterController _characterController;
    private CameraFactory _cameraFactory;
    

    private void Awake()
    {
        InitializeManagers();
        InitializeCamera();
    }

    private void InitializeManagers()
    {
        _characterController = _characterFactory.CreateCharacter();
        
        _cameraFactory = new CameraFactory();
        _cameraFactory.CreateCamera(_cameraPrefab);
    }

    private void InitializeCamera()
    {
        if (_characterController != null)
        {
            _cameraFactory.SetTargetCharacter(_characterController.transform);
        }
        else
        {
            Debug.LogWarning("CharacterController is not assigned or not yet initialized.");
        }
    }
}
