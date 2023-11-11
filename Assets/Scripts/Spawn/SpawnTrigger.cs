using System.Collections;
using UnityEngine;
using Zenject;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    [Inject] private EasyEnemyFactory _easyEnemyFactory;
    
    
    private bool _spawned = false;
    

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
            Transform randomSpawnPoint = _spawnPoints[Random.Range(Constants.ZERO, _spawnPoints.Length)];
            
            var enemy = _easyEnemyFactory.Create();
            enemy.transform.position = randomSpawnPoint.position;

            if (enemy != null)
            {
                yield return new WaitForSeconds(Constants.TWO);
            }
        }
        Destroy(gameObject);
    }
}