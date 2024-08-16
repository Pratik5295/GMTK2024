using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance {get; private set;}

    [Header("Enemies")]
    private Dictionary<string, List<GameObject>> _enemyPools = new Dictionary<string, List<GameObject>>();
    public List<EnemyPoolItem> _enemyPoolItems;
    
    //[Header("Bullets")]
    // public List<GameObject> _pulledBullets = new List<GameObject>();
    // public GameObject _bulletToPool;
    // public int _amountOfBulletsToPool;

    private void Awake()
    {
        Instance = this;
    }
    private void InitializeAllPools()
    {
        //InitializePool(_bulletToPool, _pulledBullets, _amountOfBulletsToPool);
        foreach(var item in _enemyPoolItems)
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
    // public GameObject GetPooledBullet()
    // {
    //     return GetPooledObject(_bulletToPool, _pulledBullets, _amountOfBulletsToPool);
    // }
    public GameObject GetPooledEnemy(string enemyType)
    {
        if (_enemyPools.ContainsKey(enemyType))
        {
            var pool = _enemyPools[enemyType];
            return GetPooledObject(pool);
        }

        return null;
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
[System.Serializable]
public class EnemyPoolItem
{
    public string enemyType;
    public GameObject enemyPrefab;
    public int amountToPool;
}