using UnityEngine;
using System;
using Unity.VisualScripting;


public class Observer : MonoBehaviour
{
    public static Observer Instance {get; private set;}
    private void Awake() 
    {
        Instance = this;
    }
    public class EnemyEventArgs: EventArgs
    {
        public GameObject EnemyObj;
        public EnemyEventArgs(GameObject enemyObj)
        {
            EnemyObj = enemyObj;
        }
    }
    public event EventHandler<EnemyEventArgs> OnEnemyAttackEvent;
    public event EventHandler<EnemyEventArgs> OnEnemyChaseEvent;
    public event EventHandler<EnemyEventArgs> OnWaitingToAttack;
    public event EventHandler<EnemyEventArgs> OnAttackEnded;
    public void EnemyAttack(GameObject enemyObj)
    {
        OnEnemyAttackEvent?.Invoke(this, new EnemyEventArgs(enemyObj));
    }
    public void WaitToAttack(GameObject enemyObj)
    {
        OnWaitingToAttack?.Invoke(this, new EnemyEventArgs(enemyObj));
    }
    public void EnemyChase(GameObject enemyObj)
    {
        OnEnemyChaseEvent?.Invoke(this, new EnemyEventArgs(enemyObj));
    }
    public void AttackEnded(GameObject enemyObj)
    {
        OnAttackEnded?.Invoke(this, new EnemyEventArgs(enemyObj));
    }
}
