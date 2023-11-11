using UnityEngine;
using Zenject;

public class DoorInstaller : MonoInstaller
{
    [SerializeField] private GameObject _doorPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    public override void InstallBindings()
    {
        BindDoor();
    }
    
    private void BindDoor()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Container
                .Bind<Doors>()
                .FromComponentInNewPrefab(_doorPrefab)
                .UnderTransform(_spawnPoints[i])
                .AsTransient()
                .NonLazy();
        }
    }
}