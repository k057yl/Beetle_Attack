using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private GameObject _easyEnemyPrefab;
    [SerializeField] private GameObject _middleEnemyPrefab;

    public override void InstallBindings()
    {
        BindEasyEnemy();
        BindMiddleEnemy();
    }

    private void BindEasyEnemy()
    {
        Container
            .BindFactory<EasyEnemy, EasyEnemyFactory>()
            .FromComponentInNewPrefab(_easyEnemyPrefab)
            .AsSingle();
    }
    
    private void BindMiddleEnemy()
    {
        Container
            .BindFactory<MiddleEnemy, MiddleEnemyFactory>()
            .FromComponentInNewPrefab(_middleEnemyPrefab)
            .AsSingle();
    }
}
//Container.BindFactory<Transform, HardEnemy, HardEnemyFactory>();