using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondEnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPositions;
    [SerializeField] private EnemiesListSO _firstWave;
    [SerializeField] private float _timeToSpawnAnEnemy;
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        SpawnEnemy();
    }
    private void SpawnEnemy()
    {
        if(_timer >= _timeToSpawnAnEnemy)
        {
            _timer = 0;
            //chooses a random enemy to be spawned and one of the spawn positions on the list
            int randomEnemyIndex = Random.Range(0, _firstWave._enemiesList.Count);
            int randomSpawnIndex = Random.Range(0, _spawnPositions.Count);
            EnemySO chosenEnemy = _firstWave._enemiesList[randomEnemyIndex];
            Transform chosenSpawn = _spawnPositions[randomSpawnIndex];
            
            float offsetRange = 3;
            float offsetX = Random.Range(-offsetRange, offsetRange);
            float offsetZ = Random.Range(-offsetRange, offsetRange);
            
            //Pools the enemy from the ObjectPool
            GameObject enemyClone = ObjectPool.Instance.GetPooledEnemy(chosenEnemy._enemyType);
            if(enemyClone != null)
            {
                enemyClone.transform.position = new Vector3(chosenSpawn.transform.position.x + offsetX, chosenSpawn.transform.position.y, chosenSpawn.transform.position.z + offsetZ);
                enemyClone.transform.rotation = Quaternion.identity;
                enemyClone.SetActive(true);
            } 
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(Transform t in _spawnPositions)
        {
            Gizmos.DrawSphere(t.position, 0.5f);
        }
    }
}
