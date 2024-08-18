using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "JumpAttack", menuName = "ScriptableObjects/Enemy/Attacks/JumpAttack")]
public class JumpAttack : AttackStategy
{
    public float _jumpForce;
    public override void Attack(AttackStategyParamethers e)
    {
        JumpLogic(e.Agent, e.Player);
    }
    private void JumpLogic(NavMeshAgent agent, Transform player)
    {
        Vector3 jumpDirection = agent.transform.position - player.transform.position;
        
        agent.transform.Translate(jumpDirection * Time.deltaTime);
    }
}
