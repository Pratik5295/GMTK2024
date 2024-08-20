using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPositions;
    public Wave[] _waves;

    [Header("Time Variables")]
    [SerializeField] private float _countDown = 2;
    private int _currentWaveIndex = 0;
    private bool _readyToCountDown, infiniteWave;
    public int CurrentWaveIndex {get{return _currentWaveIndex;}}

    private void Start()
    {
        _readyToCountDown = true;

        for(int i = 0; i < _waves.Length; i++)
        {
            _waves[i]._enemiesLeft = _waves[i]._enemies.Length;
        }
    }
    private void Update()
    {
        Debug.Log("wave index: " + _currentWaveIndex + " >= length" + _waves.Length + " = " + (_currentWaveIndex >= _waves.Length));
        //if we reach the last wave, add a new wave equal to the last one
        if(_currentWaveIndex >= _waves.Length && !infiniteWave)
        {
            _currentWaveIndex --;
            infiniteWave = true;
            //Debug.Log("Add logic to go infinite here");
            //return;
        }

        if(_readyToCountDown == true)
        {
            _countDown -= Time.deltaTime;
        }
        if(_countDown <= 0)
        {
            _readyToCountDown = false;
            _countDown = _waves[_currentWaveIndex]._timeToNextWave;

            StartCoroutine(SpawnWave());
        }

        //if there are no more enemies within the wave, start countdown for a new wave and go to the next wave
        if(_waves[_currentWaveIndex]._enemiesLeft == 0)
        {
            _readyToCountDown = true;
            if (!infiniteWave)
            {
                _currentWaveIndex++;
            }
        }
    }
    private IEnumerator SpawnWave()
    {
        if(_currentWaveIndex < _waves.Length)
        {
            for(int i = 0; i <  _waves[_currentWaveIndex]._enemies.Length; i++)
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
        foreach(Transform t in _spawnPositions)
        {
            Gizmos.DrawSphere(t.position, 0.5f);
        }
    }
}

[System.Serializable]
public class Wave
{
    public EnemyLogic[] _enemies;
    public float _timeToNextEnemy = 2;
    public float _timeToNextWave = 2;

    [HideInInspector] public int _enemiesLeft;
}
