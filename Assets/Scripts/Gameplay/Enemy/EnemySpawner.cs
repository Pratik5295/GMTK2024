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
            int randomEnemyIndex = Random.Range(0, _firstWave._enemiesList.Count);
            int randomSpawnIndex = Random.Range(0, _spawnPositions.Count);
            EnemySO chosenEnemy = _firstWave._enemiesList[randomEnemyIndex];
            Transform chosenSpawn = _spawnPositions[randomSpawnIndex];

            GameObject enemyClone = ObjectPool.Instance.GetPooledEnemy(chosenEnemy._enemyType);
            if(enemyClone != null)
            {
                enemyClone.transform.position = chosenSpawn.transform.position;
                enemyClone.transform.rotation = Quaternion.identity;
                enemyClone.SetActive(true);
            } 
        }
    }
}
