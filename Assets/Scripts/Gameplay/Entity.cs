using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float StartingHealth = 10f;
    [Tooltip("In Seconds")][SerializeField] float scaleTiming = .3f;
    private float health;

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
        if (enemyType != EnemyType.enlarge) return;

        health -= damage;

        if (health <= 0f)
        {
            gameObject.SetActive(false);
        }

        Vector3 newScale = transform.localScale * (1 + 1 / damage);

        StartCoroutine(ScaleEnemy(newScale, .2f));

        //transform.localScale = transform.localScale * (1 + 1/damage);
    }

    public void Shrink(float damage)
    {
        if (enemyType != EnemyType.shrink) return;

        health -= damage;
        if (health <= 0f)
        {
            gameObject.SetActive(false);
        }
        
        Vector3 newScale = transform.localScale / (1 + 1 / damage);

        StartCoroutine(ScaleEnemy(newScale, .2f));
        //transform.localScale = transform.localScale / (1 + 1 / damage);
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

}
