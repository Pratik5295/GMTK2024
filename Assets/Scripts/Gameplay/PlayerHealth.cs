using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 5f;
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private bool isAlive;
    [SerializeField] private bool invincible;
    [SerializeField] private bool wasJustHit = false;
    [SerializeField] private float invincibilityDuration = 0.5f;
    [SerializeField] private float invincibilityTimer = 0.5f;


    public Action OnPlayerDeathEvent;
    public Action<float,float> OnPlayerDamageEvent;

    public float GetHealth => health;
    public float GetMaxHealth => maxHealth;

    private void Start()
    {
        health = maxHealth;

        invincibilityTimer = invincibilityDuration;

        isAlive = IsAlive();
        wasJustHit = false;

        GameManager.Instance.SetPlayer(this);
    }


    private void Update()
    {
        if (wasJustHit) 
        {
            if (invincibilityTimer > 0)
            {
                invincibilityTimer -= Time.deltaTime;
            }
            else if (invincibilityTimer <= 0)
            {
                wasJustHit= false;
                invincible = false;
                invincibilityTimer = invincibilityDuration;
            }
        }
    }

    private bool IsAlive()
    {
        return health > 0;
    }

    public void ReduceHealth(float amount)
    {
        Debug.Log(amount);
        if (!IsAlive() || invincible) return;

        health -= amount;
        OnPlayerDamageEvent?.Invoke(health,maxHealth);

        if (!IsAlive())
        {
            isAlive = false;
            OnPlayerDeathEvent?.Invoke();
        }
        else
        {
            invincible = true;
            wasJustHit = true;
        }
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }
}
