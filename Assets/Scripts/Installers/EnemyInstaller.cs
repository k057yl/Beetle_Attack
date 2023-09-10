using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private GameObject _easyEnemyPrefab;

    public override void InstallBindings()
    {
        BindEasyEnemy();
    }

    private void BindEasyEnemy()
    {
        Container
            .BindFactory<EasyEnemy, EasyEnemyFactory>()
            .FromComponentInNewPrefab(_easyEnemyPrefab)
            .AsSingle();
    }
}
//Container.BindFactory<Transform, MediumEnemy, MediumEnemyFactory>();
//Container.BindFactory<Transform, HardEnemy, HardEnemyFactory>();