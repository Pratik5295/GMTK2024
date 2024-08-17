using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Meelee", menuName = "ScriptableObjects/Enemy/Attacks/Meelee")]
public class MeeleeAttack : AttackStategy
{
    public override void Attack(Transform transform, MonoBehaviour monoBehaviour)
    {
        PerfomMeeleeAttack(transform);
    }
    private void PerfomMeeleeAttack(Transform transform)
    {
        RaycastHit hit;
        float attackRadius = 0.5f;
        float attackDistance = 0.5f;
        if(Physics.SphereCast(transform.position, attackRadius, transform.forward, out hit, attackDistance))
        {
            //Implement Damage Logic
        }
    }
}
