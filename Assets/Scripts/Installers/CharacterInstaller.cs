using UnityEngine;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    [SerializeField] private CharacterController _characterController;


    public override void InstallBindings()
    {
        BindCharacter();
    }

    private void BindCharacter()
    {
        Container
            .Bind<CharacterController>()
            .FromComponentInNewPrefab(_characterController)
            .AsSingle()
            .NonLazy();
    }
}