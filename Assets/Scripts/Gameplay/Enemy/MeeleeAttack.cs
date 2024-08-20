using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Meelee", menuName = "ScriptableObjects/Enemy/Attacks/Meelee")]
public class MeeleeAttack : AttackStategy
{
    private bool _isSyncronized = false;
    private float _syncronizationFloat = 0.45f;
    public int _attackDistance = 2;

    public override void Attack(AttackStategyParamethers e)
    {
        //e.MonoBehaviour.Invoke("SyncronizeAttackWithAnimation", _syncronizationFloat);
        PerfomMeeleeAttack(e.Agent, e.DamageDealt);
    }
    private void SyncronizeAttackWithAnimation()
    {
        _isSyncronized = true;
    }
    private void PerfomMeeleeAttack(NavMeshAgent agent, float damageDealt)
    {
        agent.enabled = false;
        int numProjectiles = 10;
        int spreadAngle = 30;
        RaycastHit[] hits;

        Debug.Log("meelee attack being called");
        for (int i = 0; i < numProjectiles; i++) 
        {
            float angle = spreadAngle * ((float)i / (numProjectiles - 1) - 0.5f);
            hits = Physics.RaycastAll(agent.transform.position, Quaternion.Euler(0, angle, 0) * agent.transform.forward, _attackDistance);
            
            Debug.DrawRay(agent.transform.position, Quaternion.Euler(0, angle, 0) * agent.transform.forward, Color.black, 1.5f);

            foreach(RaycastHit hit in hits)
            {
                if(hit.collider.CompareTag(IStringDefinitions.PLAYER_TAG))
                {
                    Debug.Log("meelee attack hit player");
                    PlayerHealth playerHealth = hit.collider.GetComponentInParent<PlayerHealth>();
                    playerHealth.ReduceHealth(damageDealt);
                }
            }
        }
        agent.enabled = true;
    }
}
