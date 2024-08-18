using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AttackStategy : ScriptableObject
{
    public float _attackDistance;
    public float _timeBetweenAttacks;
    public abstract void Attack(AttackStategyParamethers attackStategyParamethers);
}
public class AttackStategyParamethers
{
    public NavMeshAgent Agent;
    public Transform BulletSpawner {get;}
    public Transform Player {get;}
    public MonoBehaviour MonoBehaviour {get;}
    public AttackStategyParamethers(NavMeshAgent agent, Transform bulletSpawner, Transform player, MonoBehaviour monobehaviour)
    {
        Agent = agent;
        BulletSpawner = bulletSpawner;
        Player = player;
        MonoBehaviour = monobehaviour;
    }
}
