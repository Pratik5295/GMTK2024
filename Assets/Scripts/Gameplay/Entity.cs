using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float StartingHealth = 1f;
    [Tooltip("In Seconds")][SerializeField] float scaleTiming = .3f;
    private float health;
    private EnemySpawner _enemySpawner;

    [Tooltip("How many times should the enemy be scaled(in the correct type) before being vulnerable?")]
    [SerializeField] int scaleRequired = 4;
    private Vector3 originalSize;
    private int timesScaled;

    [SerializeField] public EnemyType enemyType = EnemyType.normal;

    public enum EnemyType
    {
        normal,
        enlarge,
        shrink
    }

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            //if (enemyType != EnemyType.normal) return;
            health = value;
            //Debug.Log(health);
            if(health <= 0f)
            {
                if (_enemySpawner != null)
                    _enemySpawner._waves[_enemySpawner.CurrentWaveIndex]._enemiesLeft--;
                gameObject.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        originalSize = transform.localScale;
    }

    private void Start()
    {
        
        _enemySpawner = GetComponentInParent<EnemySpawner>();
        health = StartingHealth;
    }
    private void OnEnable()
    {
        timesScaled = 0;
        transform.localScale = originalSize;
        health = StartingHealth;
    }

    public void Enlarge(float damage)
    {

        if (enemyType == EnemyType.shrink)
        {
            Vector3 newScale = transform.localScale * (1 + 1 / (damage * 2.5f));

            StartCoroutine(ScaleEnemy(newScale, scaleTiming));

            timesScaled--;

        }
        else if (enemyType == EnemyType.enlarge)
        {

            /*health -= damage;

            if (health <= 0f)
            {
                gameObject.SetActive(false);
            }*/

            if (IsScaled()) return;

            Vector3 newScale = transform.localScale * (1 + 1 / (damage * 2.5f));

            StartCoroutine(ScaleEnemy(newScale, scaleTiming));

            timesScaled++;

        }

        

        //transform.localScale = transform.localScale * (1 + 1/damage);
    }

    public void Shrink(float damage)
    {

        if (enemyType == EnemyType.enlarge)
        {
            Vector3 newScale = transform.localScale / (1 + 1 / (damage * 2.5f));

            StartCoroutine(ScaleEnemy(newScale, scaleTiming));

            timesScaled--;
        }
        else if (enemyType == EnemyType.shrink)
        {

            /*health -= damage;

            if (health <= 0f)
            {
                gameObject.SetActive(false);
            }*/

            if (IsScaled()) return;

            Vector3 newScale = transform.localScale / (1 + 1 / (damage * 2.5f));

            StartCoroutine(ScaleEnemy(newScale, scaleTiming));

            timesScaled++;
        }
        
    }

    IEnumerator ScaleEnemy(Vector3 scale, float time)
    {
        float t = 0;

        while (t < time)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scale, time);

            t += Time.deltaTime;
            yield return null;
        }
        
    }

    public bool IsScaled()
    {
        return timesScaled == scaleRequired;
    }
}
