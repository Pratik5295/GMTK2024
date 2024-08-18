using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondEnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPositions;
    [SerializeField] private EnemiesListSO _firstWave;
    [SerializeField] private EnemiesListSO _secondWave;
    [SerializeField] private EnemiesListSO _thirdWave;
    [SerializeField] private EnemiesListSO _fourthWave;
    [SerializeField] private float _timeBetweenEnemiesSpawn;
    private float _wavesTimer;

    private void Update()
    {
        _wavesTimer += Time.deltaTime;
        TryToSpawnEnemy();
    }
    private void TryToSpawnEnemy()
    {
        int randomEnemyIndex;
        int randomSpawnIndex;
        if(_wavesTimer >= _timeBetweenEnemiesSpawn)
        {
            _wavesTimer = 0;
            //chooses a random enemy to be spawned and one of the spawn positions on the list

            randomEnemyIndex = Random.Range(0, _firstWave._enemiesList.Count);
            randomSpawnIndex = Random.Range(0, _spawnPositions.Count);
            SpawnEnemy(randomEnemyIndex, randomSpawnIndex);
             
        }
    }
    private void SpawnEnemy(int randomEnemyIndex, int randomSpawnIndex)
    {
        EnemySO chosenEnemy = _firstWave._enemiesList[randomEnemyIndex];
        Transform chosenSpawn = _spawnPositions[randomSpawnIndex];

        if(!SpawnWithingCameraView(chosenSpawn))
        {
            //Pools the enemy from the ObjectPool
            GameObject enemyClone = ObjectPool.Instance.GetPooledEnemy(chosenEnemy._enemyType);
            if(enemyClone != null)
            {
                enemyClone.transform.position = chosenSpawn.transform.position;
                enemyClone.transform.rotation = Quaternion.identity;
                enemyClone.SetActive(true);
            }
        }
    }
    private bool SpawnWithingCameraView(Transform chosenSpawn)
    {   
        Vector3 spawnPosition = new Vector3(chosenSpawn.transform.position.x, chosenSpawn.transform.position.y, chosenSpawn.transform.position.z);
        // Checks if the enemies are within the camera's view
        Camera virtualCamera = Camera.main;
        Vector3 withinCameraView = virtualCamera.WorldToViewportPoint(spawnPosition);

        if (withinCameraView.x < 0f || withinCameraView.x > 1f || withinCameraView.y < 0f || withinCameraView.z <= 0f)
        {
            return true;
        }
        return false;
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
