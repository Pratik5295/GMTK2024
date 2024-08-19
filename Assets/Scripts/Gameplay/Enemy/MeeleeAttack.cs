using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Meelee", menuName = "ScriptableObjects/Enemy/Attacks/Meelee")]
public class MeeleeAttack : AttackStategy
{
    private LayerMask playerLayer;
    public override void Attack(AttackStategyParamethers e)
    {
        PerfomMeeleeAttack(e.Agent, e.BulletSpawner, e.DamageDealt);
    }
    private void PerfomMeeleeAttack(NavMeshAgent agent, Transform bulletSpawner, float damageDealt)
    {
        float attackRadius = 0.5f;
        RaycastHit hit;

        if(Physics.SphereCast(bulletSpawner.transform.position, attackRadius, bulletSpawner.transform.forward, 
        out hit, agent.stoppingDistance, playerLayer))
        {
            Debug.Log("Meelee attack hit player");
            PlayerHealth playerHealth = hit.collider.gameObject.transform.parent.GetComponent<PlayerHealth>();
            playerHealth.ReduceHealth(damageDealt);
        }
    }
}
