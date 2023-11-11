using UnityEngine;
using Zenject;

public class ParallaxInstaller : MonoInstaller
{
    [SerializeField] private GameObject _parallaxPrefab;

    public override void InstallBindings()
    {
        BindParallax();
    }

    private void BindParallax()
    {
        Container
            .Bind<Parallax>()
            .FromComponentInNewPrefab(_parallaxPrefab)
            .AsSingle()
            .NonLazy();
    }
}