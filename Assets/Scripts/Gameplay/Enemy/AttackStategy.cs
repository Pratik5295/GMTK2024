using UnityEngine;
using UnityEngine.AI;

public abstract class AttackStategy : ScriptableObject
{
    public abstract void Attack(AttackStategyParamethers attackStategyParamethers);
}
public class AttackStategyParamethers
{
    public NavMeshAgent Agent {get;}
    public Transform PlayerTransform {get;}
    public float DamageDealt {get;}
    public MonoBehaviour MonoBehaviour {get;}
    public AttackStategyParamethers(NavMeshAgent agent, Transform playerTransform, float damageDealt, MonoBehaviour monobehaviour)
    {
        Agent = agent;
        PlayerTransform = playerTransform;
        DamageDealt = damageDealt;
        MonoBehaviour = monobehaviour;
    }
}
