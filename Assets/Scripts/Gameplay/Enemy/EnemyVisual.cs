using System;
using UnityEngine;

public class EnemyVisual : MonoBehaviour, IObserverSubscriber
{
    [SerializeField] private Animator _enemyAnimator;
    private const string ATTACK_ALT = "Attack_Alt";
    private const string ATTACK = "Attack";
    private const string ISWALKING = "IsWalking";

    public void EventSubscription()
    {
        if(Observer.Instance != null)
        {
            Observer.Instance.OnEnemyAttackEvent += EnemyAttack;
            Observer.Instance.OnEnemyChaseEvent += EnemyChase;   
            Observer.Instance.OnWaitingToAttack += WaitToAttack;      
        }
    }
    public void OnDestroy()
    {
        if(Observer.Instance != null)
        {
            Observer.Instance.OnEnemyAttackEvent -= EnemyAttack;
            Observer.Instance.OnEnemyChaseEvent -= EnemyChase;
            Observer.Instance.OnWaitingToAttack -= WaitToAttack;    
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
            if(chosenAttack == 1) _enemyAnimator.SetTrigger(ATTACK);
            if(chosenAttack == 2) _enemyAnimator.SetTrigger(ATTACK_ALT);
            Debug.Log("enemy attack being called");
        }
    }
    private void WaitToAttack(object sender, Observer.EnemyEventArgs e)
    {
        if(e.EnemyObj == gameObject.transform.parent.gameObject)
        {
            Debug.Log("wait to attack being called");
            _enemyAnimator.SetBool(ISWALKING, false);
        }
    }
    private void EnemyChase(object sender, Observer.EnemyEventArgs e)
    {
        if(e.EnemyObj == gameObject.transform.parent.gameObject)
        {   
            _enemyAnimator.ResetTrigger(ATTACK);
            _enemyAnimator.ResetTrigger(ATTACK_ALT);
            _enemyAnimator.SetBool(ISWALKING, true);
        } 
    }
}
