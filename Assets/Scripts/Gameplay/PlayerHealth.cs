using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private bool isAlive;

    public Action OnPlayerDeathEvent;
    public Action<float> OnPlayerDamageEvent;

    public float GetHealth => health;

    private void Start()
    {
        maxHealth = MetaConstants.MaxPlayerHealth;
        health = maxHealth;

        isAlive = IsAlive();
    }

    private bool IsAlive()
    {
        return health > 0;
    }

    public void ReduceHealth(float amount)
    {
        if (!IsAlive()) return;

        health -= amount;
        OnPlayerDamageEvent?.Invoke(health);

        if (!IsAlive())
        {
            isAlive = false;
            OnPlayerDeathEvent?.Invoke();
        }
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }
}
