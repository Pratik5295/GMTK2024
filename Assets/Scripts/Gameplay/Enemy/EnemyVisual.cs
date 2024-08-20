using System;
using UnityEngine;

public class EnemyVisual : MonoBehaviour, IObserverSubscriber
{
    [SerializeField] private Animator _enemyAnimator;
    private const string ATTACK_ALT = "attack alt";
    private const string ATTACK = "attack";
    private const string SPEED = "speed";

    public void EventSubscription()
    {
        if(Observer.Instance != null)
        {
            Observer.Instance.OnEnemyAttackEvent += EnemyAttack;
            Observer.Instance.OnEnemyChaseEvent += EnemyChase;   
            Observer.Instance.OnWaitingToAttack += WaitToAttack;     
            //Observer.Instance.OnAttackEndedEvent += AttackEnded;  
        }
    }

    private void AttackEnded(object sender, Observer.EnemyEventArgs e)
    {
        if(e.EnemyObj == gameObject.transform.parent.gameObject)
        {   
            Debug.Log("attack animation should play");
            _enemyAnimator.SetBool(ATTACK, false);
        }
    }

    public void OnDestroy()
    {
        if(Observer.Instance != null)
        {
            Observer.Instance.OnEnemyAttackEvent -= EnemyAttack;
            Observer.Instance.OnEnemyChaseEvent -= EnemyChase;            
        }
    }
    private void Start()
    {
        EventSubscription();
    }
    private void EnemyAttack(object sender, Observer.EnemyEventArgs e)
    {
        if(e.EnemyObj == gameObject.transform.parent.gameObject)
        {   
            int chosenAttack = UnityEngine.Random.Range(0, 2);
            _enemyAnimator.SetBool(ATTACK, true);
            //if(chosenAttack == 2) _enemyAnimator.SetBool(ATTACK_ALT, true);
        }
    }
    private void WaitToAttack(object sender, Observer.EnemyEventArgs e)
    {
        if(e.EnemyObj == gameObject.transform.parent.gameObject)
        {
            _enemyAnimator.SetFloat(SPEED, 0);
        }
    }
    private void EnemyChase(object sender, Observer.EnemyEventArgs e)
    {
        if(e.EnemyObj == gameObject.transform.parent.gameObject)
        {   
            //Debug.Log("chase animatin should play");
            float speedValue = 1;

            _enemyAnimator.SetBool(ATTACK, false);
            _enemyAnimator.SetFloat(SPEED, speedValue);
        }
    }
}
