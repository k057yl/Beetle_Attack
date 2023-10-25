using UnityEngine;
using Zenject;

public class WeaponInstaller : MonoInstaller
{
    [SerializeField] private Pistol _pistol;
    [SerializeField] private Shotgun _shotgun;
    
    public override void InstallBindings()
    {
        BindPistol();
        BindShotgun();
    }
    
    private void BindPistol()
    {
        Container
            .Bind<Pistol>()
            .FromComponentInNewPrefab(_pistol)
            .AsSingle()
            .NonLazy();
    }
    
    private void BindShotgun()
    {
        Container
            .Bind<Shotgun>()
            .FromComponentInNewPrefab(_shotgun)
            .AsSingle()
            .NonLazy();
    }
}