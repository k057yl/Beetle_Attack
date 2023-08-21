using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private Camera _cameraPrefab;
    
    
    public override void InstallBindings()
    {
        BindCamera();
    }

    private void BindCamera()
    {
        Container
            .Bind<CameraController>()
            .FromComponentInNewPrefab(_cameraPrefab)
            .AsSingle()
            .NonLazy();
    }
}