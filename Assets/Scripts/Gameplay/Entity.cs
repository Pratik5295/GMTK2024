using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float StartingHealth = 1f;
    [Tooltip("In Seconds")][SerializeField] float scaleTiming = .3f;
    private float health;
    [Tooltip("How many times should the enemy be scaled(in the correct type) before being vulnerable?")]
    [SerializeField] int scaleRequired;
    private int timesScaled;

    [SerializeField] EnemyType enemyType = EnemyType.normal;

    enum EnemyType
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
            if (enemyType != EnemyType.normal) return;
            health = value;
            //Debug.Log(health);
            if(health <= 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
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
