using System.Collections;
using UnityEngine;
using Zenject;

public class MiddleSpawn : MonoBehaviour
{
    private const int DELAY_SPAWN = 6;
    
    [SerializeField] private Transform _spawnPoint;

    [Inject] private MiddleEnemyFactory _middleEnemyFactory;
    
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
        for (int i = 0; i < 3; i++)
        {
            var enemy = _middleEnemyFactory.Create();
            enemy.transform.position = _spawnPoint.position;

            if (enemy != null)
            {
                yield return new WaitForSeconds(DELAY_SPAWN);
            }
        }
        
        Destroy(gameObject);
    }
}
