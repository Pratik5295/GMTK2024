using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPositions;
    public List<Wave> _waves = new List<Wave>();

    [Header("Time Variables")]
    [SerializeField] private float _countDown = 2;
    private int _currentWaveIndex = 0;
    private bool _readyToCountDown;
    public int CurrentWaveIndex {get{return _currentWaveIndex;}}

    private void Start()
    {
        _readyToCountDown = true;

        for(int i = 0; i < _waves.Count; i++)
        {
            _waves[i]._enemiesLeft = _waves[i]._enemies.Count;
        }
    }
    private void Update()
    {
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

        if(_readyToCountDown == true)
        {
            _countDown -= Time.deltaTime;
        }
        if(_countDown <= 0 )
        {
            _readyToCountDown = false;
            _countDown = _waves[_currentWaveIndex]._timeToNextWave;

            StartCoroutine(SpawnWave());
        }

        //if there are no more enemies within the wave, start countdown for a new wave and go to the next wave
        if(_waves[_currentWaveIndex]._enemiesLeft == 0)
        {
            _readyToCountDown = true;
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

                yield return new WaitForSeconds(_waves[_currentWaveIndex]._timeToNextEnemy);
            }
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
