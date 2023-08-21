using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private UIController _uiPrefab;
    [SerializeField] private Transform _spawnUIPoint;


    public override void InstallBindings()
    {
        BindUIBar();
    }
    
    private void BindUIBar()
    {
        Container
            .Bind<UIController>()
            .FromComponentInNewPrefab(_uiPrefab)
            .UnderTransform(_spawnUIPoint)
            .AsSingle()
            .NonLazy();
    }
}