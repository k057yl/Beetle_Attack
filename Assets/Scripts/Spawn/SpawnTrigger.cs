using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    private EasyEnemyFactory _easyEnemyFactory;
    private bool _spawned = false;

    [Inject]
    public void Construct(EasyEnemyFactory easyEnemyFactory)
    {
        _easyEnemyFactory = easyEnemyFactory;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(Constants.CHARACTER) && !_spawned)
        {
            StartCoroutine(SpawnEnemies());
            _spawned = true;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < 10; i++)
        {
            Transform randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            
            var enemy = _easyEnemyFactory.Create();
            enemy.transform.position = randomSpawnPoint.position;

            if (enemy != null)
            {
                yield return new WaitForSeconds(2f);
            }
        }
        
        Destroy(gameObject);
    }
}