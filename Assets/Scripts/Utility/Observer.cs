using UnityEngine;
using System;


public class Observer : MonoBehaviour
{
    public static Observer Instance {get; private set;}
    private void Awake() 
    {
        Instance = this;
    }
    public class OnEnemyAttackEventArgs: EventArgs
    {
        public float DamageDealt;
        public GameObject Enemy;
        public OnEnemyAttackEventArgs(float damageDealt, GameObject enemy)
        {
            Enemy = enemy;
            DamageDealt = damageDealt;
        }
    }
    public event EventHandler<OnEnemyAttackEventArgs> OnEnemyAttack;
    public void EnemyAttack(float damageDealt, GameObject enemy)
    {
        OnEnemyAttack?.Invoke(this, new OnEnemyAttackEventArgs(damageDealt, enemy));
    }
}
