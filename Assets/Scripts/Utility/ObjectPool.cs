using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance {get; private set;}

    [Header("Enemies")]
    private Dictionary<EnemyType, List<GameObject>> _enemyPools = new Dictionary<EnemyType, List<GameObject>>();
    public List<EnemyPoolItem> _enemyPoolItems = new List<EnemyPoolItem>();

    [Header("Enemy Projectiles")]
    public List<GameObject> _pulledEnemyProjectile = new List<GameObject>();
    public GameObject _enemyProjectilesToPool;
    public int _amountOfEnemyProjectilesToPool;

    [Header("Player Objects")]
    public List<GameObject> _pulledBulletDecals = new List<GameObject>();
    public GameObject bulletDecal;
    public int bulletDecalsToPool;
    public List<GameObject> _pulledMuzzleFlashes = new List<GameObject>();
    public GameObject muzzleFlash;
    public int muzzleFlashsToPool;


    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
    private void InitializeAllPools()
    {
        InitializePool(_enemyProjectilesToPool, _pulledEnemyProjectile, _amountOfEnemyProjectilesToPool);
        InitializePool(bulletDecal, _pulledBulletDecals, bulletDecalsToPool);
        InitializePool(muzzleFlash, _pulledMuzzleFlashes, muzzleFlashsToPool);
        foreach (var item in _enemyPoolItems)
        {
            var poolList = new List<GameObject>();
            InitializePool(item.enemyPrefab, poolList, item.amountToPool);
            _enemyPools.Add(item.enemyType, poolList);
        }
    }
    private void Start()
    {
        InitializeAllPools();
    }
    public GameObject GetPooledEnemy(EnemyType enemyType)
    {
        if (_enemyPools.ContainsKey(enemyType))
        {
            var pool = _enemyPools[enemyType];
            return GetPooledObject(pool);
        }

        return null;
    }
    public GameObject GetPooledEnemyProjectiles()
    {
        return GetPooledObject(_enemyProjectilesToPool, _pulledEnemyProjectile, _amountOfEnemyProjectilesToPool);
    }

    public GameObject GetPooledBulletDecals()
    {
        return GetPooledObject(bulletDecal, _pulledBulletDecals, bulletDecalsToPool);
    }

    public GameObject GetPooledMuzzleFlashes()
    {
        return GetPooledObject(muzzleFlash, _pulledMuzzleFlashes, muzzleFlashsToPool);
    }

    private GameObject GetPooledObject(GameObject objectToPool, List<GameObject> listToAddObjects, int amountToPool)
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if (listToAddObjects[i] == null)
            {
                // Log an error if you find a null reference in the pool list
                Debug.LogError($"Object at index {i} in the pool list {listToAddObjects}is null.");
                continue;
            }
            if(!listToAddObjects[i].activeInHierarchy)
            {
                return listToAddObjects[i];
            }
        }
        GameObject newObj = Instantiate(objectToPool);
        newObj.SetActive(false);
        listToAddObjects.Add(newObj);
        return newObj;
    }
    private GameObject GetPooledObject(List<GameObject> listToAddObjects)
    {
        foreach (var obj in listToAddObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        if (listToAddObjects.Count > 0)
        {
            GameObject objectToPool = listToAddObjects[0];
            GameObject newObj = Instantiate(objectToPool);
            newObj.SetActive(false);
            listToAddObjects.Add(newObj);
            return newObj;
        }

        return null;
    }
    private GameObject[] InitializePool(GameObject objectToPool, List<GameObject> listToAddObjects, int amountOfPool)
    {
        GameObject tmp;

        for(int i =0; i < amountOfPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            listToAddObjects.Add(tmp);
        }
        return null;
    }
}
public enum EnemyType 
{
    Basic,
    Big,
    Small,
    Fast,
}

[System.Serializable]
public class EnemyPoolItem
{
    public EnemyType enemyType;
    public GameObject enemyPrefab;
    public int amountToPool;
}