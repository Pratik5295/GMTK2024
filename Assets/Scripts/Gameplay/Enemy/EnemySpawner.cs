using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPositions;
<<<<<<< Updated upstream
    public List<Wave> _waves = new List<Wave>();

=======
    [HideInInspector]
    public Wave _waves;
    public EnemyLogic[] _enemies;
    public int enemyPerWave = 1, enemyIncreasePerWave = 1, maxEnemyPerWave = 10;
    public float timeToNextWave = 5, timeToNextSpawn = 1;
>>>>>>> Stashed changes
    [Header("Time Variables")]
    [SerializeField] private float _countDown = 2;
    private int _currentWaveIndex = 0;
    private bool _readyToCountDown;
    public int CurrentWaveIndex {get{return _currentWaveIndex;}}

    private void Start()
    {
        _readyToCountDown = true;

<<<<<<< Updated upstream
        for(int i = 0; i < _waves.Count; i++)
        {
            _waves[i]._enemiesLeft = _waves[i]._enemies.Count;
=======
        for(int i = 0; i < enemyPerWave; i++)
        {
            _waves._enemiesLeft = enemyPerWave;
>>>>>>> Stashed changes
        }
    }
    private void Update()
    {
<<<<<<< Updated upstream
        Debug.Log("wave index: " + _currentWaveIndex + " >= length" + _waves.Count + " = " + (_currentWaveIndex >= _waves.Count));
        //if we reach the last wave, add a new wave equal to the last one
        if(_currentWaveIndex + 1 > _waves.Count - 1)
        {
            if(_waves[_currentWaveIndex]._enemiesLeft == 0)
            {
                Debug.LogError("you won the game!!!");
                return;
            } 
        }

=======
>>>>>>> Stashed changes
        if(_readyToCountDown == true)
        {
            _countDown -= Time.deltaTime;
        }
        if(_countDown <= 0 )
        {
            _readyToCountDown = false;
            _countDown = timeToNextWave;

            StartCoroutine(SpawnWave());
        }

        //if there are no more enemies within the wave, start countdown for a new wave and go to the next wave
        //end wave check
        if(_waves._enemiesLeft == 0)
        {
            _readyToCountDown = true;
<<<<<<< Updated upstream
            _currentWaveIndex++;
        }
    }
    private void CreateNewWave()
    {
        float additionalEnemies = 3;
        Wave lastWave = _waves.Last();

        Wave newWave = new Wave
        {
            _enemies = new List<EnemyLogic>(lastWave._enemies),
            _timeToNextEnemy = lastWave._timeToNextEnemy,
            _timeToNextWave = lastWave._timeToNextEnemy
        };

        for(int i = 0; i < additionalEnemies; i++)
        {
            int randomEnemiesToAdd = Random.Range(0, lastWave._enemies.Count);
            newWave._enemies.Add(lastWave._enemies[randomEnemiesToAdd]);
        }

        _waves.Add(newWave);
    }
    private IEnumerator SpawnWave()
    {
        if(_currentWaveIndex < _waves.Count)
        {
            for(int i = 0; i <  _waves[_currentWaveIndex]._enemies.Count; i++)
            {
                int chosenIndex = Random.Range(0, _spawnPositions.Count);
                
                EnemyLogic enemyLogic = _waves[_currentWaveIndex]._enemies[i];
                InstantiateEnemy(enemyLogic.EnemyType, chosenIndex);
=======
            enemyPerWave += enemyIncreasePerWave;
            if(enemyPerWave > maxEnemyPerWave)
            {
                enemyPerWave = maxEnemyPerWave;
            }
            _waves._enemiesLeft = enemyPerWave;
            Debug.Log("enemy left: " + _waves._enemiesLeft);
        }
    }


    private IEnumerator SpawnWave()
    {
        for(int i = 0; i <  enemyPerWave; i++)
        {
            Debug.Log(i + " " + enemyPerWave);
            int chosenIndex = Random.Range(0, _spawnPositions.Count);
            int rand = Random.Range(0, _enemies.Length);
>>>>>>> Stashed changes

            EnemyLogic enemyLogic = _enemies[rand];
            InstantiateEnemy(enemyLogic.EnemyType, chosenIndex);

            yield return new WaitForSeconds(timeToNextSpawn);
        }
    }
    private void InstantiateEnemy(EnemyType enemyType, int chosenIndex)
    {
        GameObject enemy = ObjectPool.Instance.GetPooledEnemy(enemyType);
        if(enemy != null)
        {
            enemy.transform.position = _spawnPositions[chosenIndex].position;
            enemy.transform.rotation = Quaternion.identity;
            enemy.transform.SetParent(gameObject.transform);
            enemy.SetActive(true);
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

[System.Serializable]
public class Wave
{
    public List<EnemyLogic> _enemies = new List<EnemyLogic>();
    public float _timeToNextEnemy = 2;
    public float _timeToNextWave = 2;

    [HideInInspector] public int _enemiesLeft;
}
