using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    Vector3 originalPos;
    [SerializeField] KeyCode punchButton = KeyCode.E;

    [SerializeField] float punchDamage = 1f;
    [SerializeField] float punchCooldown = .5f;
    [SerializeField] float punchTiming = .3f;
    float currentCooldown;

    [SerializeField] bool canHold;

    [Header("Audio")]
    [SerializeField] AudioClip punchSound;
    //[SerializeField] AudioClip punchSound;
    //[SerializeField] AudioClip punchSound;


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
                    Punch();
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
                    StartCoroutine(Punch());
                    currentCooldown = punchCooldown;
                }
            }
        }
        currentCooldown -= Time.deltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("On Collision");
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy collision");
            Entity enemy = col.gameObject.GetComponent<Entity>();
            if (enemy.IsScaled())
            {
                Debug.Log("Collision - Enemy scaled and damaged");
                enemy.Health -= punchDamage;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("On Trigger");
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy triggered");
            Entity enemy = col.gameObject.GetComponent<Entity>();
            if (enemy.IsScaled())
            {
                Debug.Log("Trigger - Enemy scaled and damaged");
                enemy.Health -= punchDamage;
            }
        }
    }

    IEnumerator Punch()
    {
        originalPos = transform.localPosition;
        //Transform cam = Camera.main.transform;
        Vector3 newPos = Vector3.forward * 2;

        Debug.Log("cam forward: " + newPos);

        float t = 0f;

        while (t < punchTiming)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, punchTiming);

            t += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;


    }
}
