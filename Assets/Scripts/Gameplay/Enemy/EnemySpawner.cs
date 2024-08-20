using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPositions;
    [HideInInspector]
    public Wave _waves;
    public EnemyLogic[] _enemies;
    public int enemyPerWave = 1, enemyIncreasePerWave = 1, maxEnemyPerWave = 10;
    public float timeToNextWave = 5, timeToNextSpawn = 1;
    [Header("Time Variables")]
    [SerializeField] private float _countDown = 2;
    private int _currentWaveIndex = 0;
    private bool _readyToCountDown, infiniteWave;
    public int CurrentWaveIndex { get { return _currentWaveIndex; } }

    private void Start()
    {
        _readyToCountDown = true;

        for (int i = 0; i < enemyPerWave; i++)
        {
            _waves._enemiesLeft = enemyPerWave;
        }
    }
    private void Update()
    {
        if (_readyToCountDown == true)
        {
            _countDown -= Time.deltaTime;
        }
        if (_countDown <= 0)
        {
            _readyToCountDown = false;
            _countDown = timeToNextWave;

            StartCoroutine(SpawnWave());
        }

        //if there are no more enemies within the wave, start countdown for a new wave and go to the next wave
        //end wave check
        if (_waves._enemiesLeft == 0)
        {
            _readyToCountDown = true;
            enemyPerWave += enemyIncreasePerWave;
            if (enemyPerWave > maxEnemyPerWave)
            {
                enemyPerWave = maxEnemyPerWave;
            }
            _waves._enemiesLeft = enemyPerWave;
            Debug.Log("enemy left: " + _waves._enemiesLeft);
        }
    }


    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemyPerWave; i++)
        {
            Debug.Log(i + " " + enemyPerWave);
            int chosenIndex = Random.Range(0, _spawnPositions.Count);
            int rand = Random.Range(0, _enemies.Length);

            EnemyLogic enemyLogic = _enemies[rand];
            InstantiateEnemy(enemyLogic.EnemyType, chosenIndex);

            yield return new WaitForSeconds(timeToNextSpawn);
        }
    }
    private void InstantiateEnemy(EnemyType enemyType, int chosenIndex)
    {
        GameObject enemy = ObjectPool.Instance.GetPooledEnemy(enemyType);
        if (enemy != null)
        {
            enemy.transform.position = _spawnPositions[chosenIndex].position;
            enemy.transform.rotation = Quaternion.identity;
            enemy.transform.SetParent(gameObject.transform);
            enemy.SetActive(true);
        }
    }
    private bool SpawnWithingCameraView(Transform chosenSpawn)
    {
        //Checks if the enemies are within the camera's view
        Vector3 spawnPosition = new Vector3(chosenSpawn.transform.position.x, chosenSpawn.transform.position.y, chosenSpawn.transform.position.z);
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
        foreach (Transform t in _spawnPositions)
        {
            Gizmos.DrawSphere(t.position, 0.5f);
        }
    }

    [System.Serializable]
    public class Wave
    {
        public List<EnemyLogic> _enemies = new List<EnemyLogic>();
        public float _timeToNextEnemy = 2;
        public float _timeToNextWave = 2;

        [HideInInspector] public int _enemiesLeft;
    }
}
