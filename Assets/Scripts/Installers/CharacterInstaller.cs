using UnityEngine;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _spawnCharacterPoint;
    

    public override void InstallBindings()
    {
        BindCharacter();
    }

    private void BindCharacter()
    {
        Container
            .Bind<CharacterController>()
            .FromComponentInNewPrefab(_characterController)
            .UnderTransform(_spawnCharacterPoint)
            .AsSingle()
            .NonLazy();
    }
}