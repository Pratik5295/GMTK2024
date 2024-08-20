using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp1 : MonoBehaviour
{
    [SerializeField] float stompDamage = 1f;

    [Header("Audio")]
    [SerializeField] AudioClip punchSound;
    [SerializeField] AudioClip successfulHitSound;
    [SerializeField] AudioClip failedHitSound;

    void OnTriggerStay(Collider col)
    {
        //Debug.Log("On Trigger");
        if (col.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Enemy triggered");
            Entity enemy = col.gameObject.GetComponent<Entity>();
            if (enemy.IsScaled())
            {
                //Debug.Log("Trigger - Enemy scaled and damaged");
                if (enemy.enemyType == Entity.EnemyType.shrink)
                {
                    enemy.Health -= stompDamage;
                }
            }
        }
    }


}
