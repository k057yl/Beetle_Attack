using UnityEngine;
using Zenject;

public class EasyEnemyInstaller : MonoInstaller
{
    [SerializeField] private GameObject _easyEnemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    
    public override void InstallBindings()
    {
        Container.InstantiatePrefabForComponent<EasyEnemy>(_easyEnemyPrefab, _spawnPoint.position, Quaternion.identity, null);
    }
}