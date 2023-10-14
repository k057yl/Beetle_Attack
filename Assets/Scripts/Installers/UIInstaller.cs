using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private UIController _uiPrefab;


    public override void InstallBindings()
    {
        BindUIBar();
    }
    
    private void BindUIBar()
    {
        Container
            .Bind<UIController>()
            .FromComponentInNewPrefab(_uiPrefab)
            .AsSingle()
            .NonLazy();
    }
}