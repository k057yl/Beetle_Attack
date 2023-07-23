using UnityEngine;

public class CameraFactory
{
    private CameraController _cameraController;
    private Camera CameraInstance { get; set; }
    
    
    public void CreateCamera(GameObject cameraPrefab)
    {
        GameObject cameraObject = GameObject.Instantiate(cameraPrefab);
        CameraInstance = cameraObject.GetComponent<Camera>();
        _cameraController = cameraObject.GetComponent<CameraController>();
    }

    public void SetTargetCharacter(Transform target)
    {
        _cameraController?.SetTargetCharacter(target);
    }
}