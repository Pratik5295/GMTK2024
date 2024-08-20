using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public Animator armAC, gunAC, armorAC, spiderAC;
    public bool isPlayerActing;
    public bool isWalking, isAttacking, isAttackingAlt;
    // Update is called once per frame
    void Update()
    {
        PlayerCheck();
        EnemyCheck();
    }

    public void PlayerCheck()
    {
        if (!isPlayerActing)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartCoroutine(Fire());
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Punch());
            }
        }
    }


    public void EnemyCheck()
    {
        if(!isWalking && !isAttacking && !isAttackingAlt)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                StartCoroutine(Walk());
            }
            else if (Input.GetKeyDown(KeyCode.F2))
            {
                StartCoroutine(Attack());
            }
            else if (Input.GetKeyDown(KeyCode.F3))
            {
                StartCoroutine(AttackAlt());
            }
        }
    }


    public IEnumerator Reload()
    {
        isPlayerActing = true;
        armAC.SetBool("fire", false);
        armAC.SetBool("reload", true);
        armAC.SetBool("punch", false);
        gunAC.SetBool("reload", true);

        yield return new WaitForSeconds(3.5f);

        isPlayerActing = false;
        armAC.SetBool("reload", false);
        gunAC.SetBool("reload", false);
    }


    public IEnumerator Fire()
    {
        isPlayerActing = true;
        armAC.SetBool("fire", true);
        armAC.SetBool("reload", false);
        armAC.SetBool("punch", false);
        gunAC.SetBool("reload", false);

        yield return new WaitForSeconds(0.5f);

        isPlayerActing = false;
        armAC.SetBool("fire", false);
    }


    public IEnumerator Punch()
    {
        isPlayerActing = true;
        armAC.SetBool("fire", false);
        armAC.SetBool("reload", false);
        armAC.SetBool("punch", true);
        gunAC.SetBool("reload", false);

        yield return new WaitForSeconds(0.5f);

        isPlayerActing = false;
        armAC.SetBool("fire", false);
        armAC.SetBool("punch", false);
    }


    public IEnumerator Walk()
    {
        isWalking = true;
        armorAC.SetFloat("speed", 1);
        spiderAC.SetFloat("speed", 1);
        armorAC.SetBool("attack", false);
        armorAC.SetBool("attack alt", false);
        spiderAC.SetBool("attack", false);

        yield return new WaitForSeconds(3f);

        isWalking = false;
        armorAC.SetFloat("speed", 0);
        spiderAC.SetFloat("speed", 0);
    }


    public IEnumerator Attack()
    {
        isAttacking = true;
        armorAC.SetFloat("speed", 0);
        spiderAC.SetFloat("speed", 0);
        armorAC.SetBool("attack", true);
        armorAC.SetBool("attack alt", false);
        spiderAC.SetBool("attack", true);

        yield return new WaitForSeconds(1f);

        isAttacking = false;
        armorAC.SetBool("attack", false);
        spiderAC.SetBool("attack", false);
    }


    public IEnumerator AttackAlt()
    {
        isAttackingAlt = true;
        armorAC.SetFloat("speed", 0);
        spiderAC.SetFloat("speed", 0);
        armorAC.SetBool("attack", false);
        armorAC.SetBool("attack alt", true);

        yield return new WaitForSeconds(1f);

        isAttackingAlt = false;
        armorAC.SetBool("attack alt", false);
    }
}
