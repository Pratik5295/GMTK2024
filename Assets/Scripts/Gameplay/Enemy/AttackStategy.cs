using UnityEngine;
using UnityEngine.AI;

public abstract class AttackStategy : ScriptableObject
{
    public abstract void Attack(AttackStategyParamethers attackStategyParamethers);
}
public class AttackStategyParamethers
{
    public NavMeshAgent Agent {get;}
    public Transform BulletSpawner {get;}
    public float DamageDealt {get;}
    public MonoBehaviour MonoBehaviour {get;}
    public AttackStategyParamethers(NavMeshAgent agent, Transform bulletSpawner, float damageDealt, MonoBehaviour monobehaviour)
    {
        Agent = agent;
        BulletSpawner = bulletSpawner;
        MonoBehaviour = monobehaviour;
        DamageDealt = damageDealt;
    }
}
