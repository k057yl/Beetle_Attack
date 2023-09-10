using UnityEngine;
using Zenject;

public class WeaponSwitcherInstaller : MonoInstaller
{
    [SerializeField] private GameObject _weaponSwitcherPrefab;
    
    public override void InstallBindings()
    {
        BindWeaponSwitcer();
    }

    private void BindWeaponSwitcer()
    {
        Container
            .Bind<WeaponSwitcher>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
}