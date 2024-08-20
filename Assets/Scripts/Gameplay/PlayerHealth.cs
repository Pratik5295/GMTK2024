using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 5f;
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private bool isAlive;
    [SerializeField] private bool invincible;

    public Action OnPlayerDeathEvent;
    public Action<float,float> OnPlayerDamageEvent;

    public float GetHealth => health;
    public float GetMaxHealth => maxHealth;

    private void Start()
    {
        health = maxHealth;

        isAlive = IsAlive();

        GameManager.Instance.SetPlayer(this);
    }

    private bool IsAlive()
    {
        return health > 0;
    }

    public void ReduceHealth(float amount)
    {
        Debug.Log("called");
        if (!IsAlive() || invincible) return;

        health -= amount;
        OnPlayerDamageEvent?.Invoke(health,maxHealth);

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
