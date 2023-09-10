using Zenject;
using UnityEngine;

public class WeaponInstaller : MonoInstaller
{
    [SerializeField] private GameObject _pistolPrefab;
    [SerializeField] private GameObject _shotgunPrefab;

    public override void InstallBindings()
    {
        BindPistol();
        BindShotgun();

        BindAmmoSystem();
    }

    private void BindShotgun()
    {
        Container
            .Bind<Shotgun>()
            .FromComponentInNewPrefab(_shotgunPrefab)
            .AsSingle()
            .NonLazy();
    }

    private void BindPistol()
    {
        Container
            .Bind<Pistol>()
            .FromComponentInNewPrefab(_pistolPrefab)
            .AsSingle()
            .NonLazy();
    }
    
    private void BindAmmoSystem()
    {
        Container
            .Bind<AmmoSystem>()
            .AsSingle();
    }
}