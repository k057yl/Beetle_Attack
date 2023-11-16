using UnityEngine;
using Zenject;

public class DoorInstaller : MonoInstaller
{
    [SerializeField] private GameObject _doorPrefab;

    public override void InstallBindings()
    {
        BindDoor();
    }
    
    private void BindDoor()
    {
        Container
            .BindFactory<Doors, DoorFactory>()
            .FromComponentInNewPrefab(_doorPrefab)
            .AsSingle();
    }
}