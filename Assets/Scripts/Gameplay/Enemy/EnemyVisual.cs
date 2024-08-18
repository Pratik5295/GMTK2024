using UnityEngine;

public class EnemyVisual : MonoBehaviour, IObserverSubscriber
{
    //[SerializeField] private Animator _enemyAnimator;
    private const string ATTACK_TRIGGER = "Attack";
    private const string CHASING_BOOL = "IsChasing";
    public void EventSubscription()
    {
        if(Observer.Instance != null)
        {
            Observer.Instance.OnEnemyAttack += EnemyAttack;
            Observer.Instance.OnEnemyChase += EnemyChase;
        }
    }

    private void Start()
    {
        EventSubscription();
    }
    public void OnDestroy()
    {
        if(Observer.Instance != null)
        {
            Observer.Instance.OnEnemyAttack -= EnemyAttack;
            Observer.Instance.OnEnemyChase -= EnemyChase;
        }
    }
    private void EnemyAttack(object sender, Observer.OnEnemyVisualEventArgs e)
    {
        if(e.Enemy == gameObject)
        {
            Debug.Log("attack animation should play");
            //_enemyAnimator.SetBool(CHASING_BOOL, false);
            //_enemyAnimator.SetTrigger(ATTACK_TRIGGER);
        }
    }
    private void EnemyChase(object sender, Observer.OnEnemyVisualEventArgs e)
    {
        if(e.Enemy == gameObject)
        {
            Debug.Log("chase animation should play");
            //_enemyAnimator.ResetTrigger(ATTACK_TRIGGER);
            //_enemyAnimator.SetBool(CHASING_BOOL, true);
        }
    }
}
