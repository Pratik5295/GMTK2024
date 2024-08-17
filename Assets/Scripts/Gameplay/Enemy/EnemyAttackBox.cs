using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttackBox : MonoBehaviour, IObserverSubscriber
{
    [SerializeField] private BoxCollider[] _boxColliders;
    private float _damageDealt = 0;
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
    private void EnemyAttack(object sender, Observer.OnEnemyAttackEventArgs e)
    {
        foreach(BoxCollider box in _boxColliders)
        {
            box.isTrigger = false;
            _damageDealt = e.DamageDealt;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == IStringDefinitions.PLAYER_TAG)
        {
            Debug.Log("player should take " + _damageDealt + "damage");
            foreach(BoxCollider box in _boxColliders)
            {
                box.isTrigger = true;
                _damageDealt = 0;
            }
        }
    }
    public void OnDestroy()
    {
        if(Observer.Instance != null)
        {
            Observer.Instance.OnEnemyAttack -= EnemyAttack;
        }
    }
}
