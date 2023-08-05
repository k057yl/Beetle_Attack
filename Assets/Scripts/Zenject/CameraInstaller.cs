using UnityEngine;
using Zenject;


public class CameraInstaller : MonoInstaller
{
    [SerializeField] private CameraController _cameraPrefab;
    
    public override void InstallBindings()
    {
        Container.InstantiatePrefabForComponent<CameraController>(_cameraPrefab, transform.position, Quaternion.identity, null);
    }
}