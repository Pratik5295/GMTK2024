using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Events;

public class PlayerPunch : MonoBehaviour
{
    Vector3 originalPos;
    [SerializeField] UnityEvent OnPunch;
    [SerializeField] KeyCode punchButton = KeyCode.E;

    [SerializeField] float punchDamage = 1f;
    [SerializeField] float punchCooldown = .5f;
    [SerializeField] float punchTiming = .3f;
    [SerializeField] float punchFreezeTimeScale = .05f;
    [SerializeField] float punchFreezeLength = .1f;
    float currentCooldown;

    [SerializeField] bool canHold;
    bool isPunching;

    [Header("Audio")]
    [SerializeField] AudioClip punchSound;
    [SerializeField] AudioClip successfulHitSound;
    [SerializeField] AudioClip failedHitSound;


    void Start()
    {
        currentCooldown = punchCooldown;
        originalPos = base.transform.localPosition;
    }

    private void Update()
    {
        if (canHold)
        {
            if (Input.GetKey(punchButton))
            {
                if (currentCooldown <= 0f)
                {
                    OnPunch?.Invoke();
                    FindObjectOfType<DamageGun>().StopActions();
                    AudioManager.Instance.PlayForeground(punchSound);
                    StartCoroutine(Punch());
                    currentCooldown = punchCooldown;
                }
            }
            
        }
        else
        {
            if (Input.GetKeyDown(punchButton))
            {
                if (currentCooldown <= 0f)
                {
                    OnPunch?.Invoke();
                    FindObjectOfType<DamageGun>().StopActions();
                    AudioManager.Instance.PlayForeground(punchSound);
                    StartCoroutine(Punch());
                    currentCooldown = punchCooldown;
                }
            }
        }
        currentCooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.CompareTag("Enemy") && isPunching)
        {
            if (failedHitSound != null) AudioManager.Instance.PlayForeground(failedHitSound);
        }
    }

    void OnTriggerStay(Collider col)
    {
        //Debug.Log("On Trigger");
        if (col.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Enemy triggered");
            Entity enemy = col.gameObject.GetComponent<Entity>();
            if (enemy.IsScaled() && isPunching)
            {
                //Debug.Log("Trigger - Enemy scaled and damaged");
                if (enemy.enemyType == Entity.EnemyType.enlarge)
                {
                    GameObject.FindObjectOfType<DamageGun>();
                    if (successfulHitSound != null) AudioManager.Instance.PlayForeground(successfulHitSound);
                    StartCoroutine(PunchFreeze());
                    enemy.Health -= punchDamage;
                }
                if (enemy.enemyType == Entity.EnemyType.shrink)
                {
                    if (failedHitSound != null) AudioManager.Instance.PlayForeground(failedHitSound);
                }

            }
            return;
        }
    }

    IEnumerator Punch()
    {
        originalPos = transform.localPosition;
        isPunching = true;
        //Transform cam = Camera.main.transform;
        Vector3 newPos = Vector3.forward * 2;

        //Debug.Log("cam forward: " + newPos);

        float t = 0f;

        while (t < punchTiming)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, punchTiming);

            t += Time.deltaTime;

            yield return null;
        }
        
        isPunching = false;
        transform.localPosition = originalPos;


    }

    IEnumerator PunchFreeze()
    {
        Time.timeScale = punchFreezeTimeScale;
               
        yield return new WaitForSecondsRealtime(punchFreezeLength);

        Time.timeScale = 1;
    }
}
