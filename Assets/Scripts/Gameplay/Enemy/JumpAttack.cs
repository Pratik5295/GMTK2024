using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "JumpAttack", menuName = "ScriptableObjects/Enemy/Attacks/JumpAttack")]
public class JumpAttack : AttackStategy
{
    public LayerMask playerLayer;
    public float _jumpSpeed;
    public AnimationCurve _heightCurve;
    public override void Attack(AttackStategyParamethers e)
    {
        e.MonoBehaviour.StartCoroutine(JumpLogic(e.Agent, e.BulletSpawner, e.DamageDealt));
    }
    private IEnumerator JumpLogic(NavMeshAgent agent, Transform bulletSpawner, float damageDealt)
    {
        Debug.Log("jump being called");
        agent.enabled = false;
        Vector3 startingPosiiton = agent.transform.position;
        Transform playerTransform = PlayerMovement.player.transform;
        RaycastHit hit;

        for(float time = 0; time < 1; time += Time.deltaTime * _jumpSpeed)
        {
            //make the enemie position lerp towards the player
            agent.transform.position = Vector3.Lerp(startingPosiiton,playerTransform.position, time)
            + Vector3.up * _heightCurve.Evaluate(time);

            //make sure the enemie is facing the player
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation,
            Quaternion.LookRotation(playerTransform.position - agent.transform.position), time);

            //detect collision with player
            float attackRadius = 0.5f;
            if(Physics.SphereCast(bulletSpawner.transform.position, attackRadius, bulletSpawner.transform.forward, 
            out hit, agent.stoppingDistance, playerLayer))
            {
                Debug.Log("Jump attack hit player");
                PlayerHealth playerHealth = hit.collider.gameObject.transform.parent.GetComponent<PlayerHealth>();
                //playerHealth.ReduceHealth(damageDealt);
            }

            yield return null;
        }

        //Reenable agent since we manually moved it and make sure it understands where it is now
        agent.enabled = true;
        if(NavMesh.SamplePosition(playerTransform.position, out NavMeshHit navMeshHit, 1f, agent.areaMask))
        {
            agent.Warp(navMeshHit.position);
        }
    }
}
