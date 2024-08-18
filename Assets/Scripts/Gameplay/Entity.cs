using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float StartingHealth;
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

        transform.localScale = transform.localScale * (1 + 1/damage);
    }

    public void Shrink(float damage)
    {
        if (enemyType != EnemyType.shrink) return;

        health -= damage;
        if (health <= 0f)
        {
            gameObject.SetActive(false);
        }

        transform.localScale = transform.localScale / (1 + 1 / damage);
    }

}
