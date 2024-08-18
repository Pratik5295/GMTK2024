using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float StartingHealth;
    private float health;

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
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
        Health = StartingHealth;
    }

    public void Enlarge(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            gameObject.SetActive(false);
        }

        transform.localScale = transform.localScale * (1 + 1/damage);
    }

}
