using UnityEngine;
using System;


public class Observer : MonoBehaviour
{
    public static Observer Instance {get; private set;}
    private void Awake() 
    {
        Instance = this;
    }
    public class OnEnemyVisualEventArgs: EventArgs
    {
        public GameObject Enemy;
        public OnEnemyVisualEventArgs(GameObject enemy)
        {
            Enemy = enemy;
        }
    }
    public event EventHandler<OnEnemyVisualEventArgs> OnEnemyAttack;
    public event EventHandler<OnEnemyVisualEventArgs> OnEnemyChase;
    public void EnemyAttack(GameObject enemy)
    {
        OnEnemyAttack?.Invoke(this, new OnEnemyVisualEventArgs(enemy));
    }
    public void EnemyChase(GameObject enemy)
    {
        OnEnemyChase?.Invoke(this, new OnEnemyVisualEventArgs(enemy));
    }
}
