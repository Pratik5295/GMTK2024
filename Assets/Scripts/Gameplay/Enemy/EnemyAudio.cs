using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour, IObserverSubscriber
{
    public void EventSubscription()
    {
        if(Observer.Instance != null)
        {
            Observer.Instance.OnEnemyAttack += EnemyAttack;
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
        }
    }
    private void EnemyAttack(object sender, Observer.OnEnemyVisualEventArgs e)
    {
        if(e.Enemy == gameObject.transform.parent.gameObject)
        {
            //Play attack sound
        }
    }
}
