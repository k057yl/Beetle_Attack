using UnityEngine;
using Zenject;


public class CharacterInstaller : MonoInstaller
{
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private Transform _spawnCharacterPoint;
    
    
    public override void InstallBindings()
    {
        CharacterController character = Container.InstantiatePrefabForComponent<CharacterController>(_characterPrefab,
            _spawnCharacterPoint.position, Quaternion.identity, null);
        
        Container.
            Bind<CharacterController>().
            FromInstance(character).
            AsSingle().
            NonLazy();
    }
}