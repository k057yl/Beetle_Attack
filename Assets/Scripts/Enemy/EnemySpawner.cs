using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _easyEnemyPrefab;
    [SerializeField] private Transform _spawnPointCenter;
    
    
    private IEnumerator SpawnEasyEnemiesCoroutine(int numberOfEnemies, float spawnInterval, Vector3 spawnPoint)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(_easyEnemyPrefab, GetRandomSpawnPoint(spawnPoint), Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPoint(Vector3 spawnPoint)
    {
        float randomX = Random.Range(Constants.MINUS_FIVE, Constants.FIVE);
        float randomY = Random.Range(Constants.MINUS_FIVE, Constants.FIVE);
        float zPosition = Constants.MINUS_ONE;

        return new Vector3(spawnPoint.x + randomX, spawnPoint.y + randomY, zPosition);
    }
    
    public void SpawnEasyEnemies(int numberOfEnemies, float spawnInterval, Vector3 spawnPoint)
    {
        StartCoroutine(SpawnEasyEnemiesCoroutine(numberOfEnemies, spawnInterval, spawnPoint));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<CharacterController>())
        {
            Destroy(GetComponent<Collider2D>());
            Vector3 spawnPoint = col.transform.position + _spawnPointCenter.position;
            SpawnEasyEnemies(Constants.TEN, Constants.ONE, spawnPoint);
        }
    }
}