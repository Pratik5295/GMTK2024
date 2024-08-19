using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [SerializeField] Transform punchBox; 
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
        originalPos = transform.localPosition;
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
        if (col.gameObject.CompareTag("Enemy"))
        {
            Entity enemy = col.gameObject.GetComponent<Entity>();
            if (enemy.IsScaled())
                enemy.Health -= punchDamage;
            /*else if ()
            {
                
            }
            else if ()
            {
                
            }*/
        }
    }

    IEnumerator Punch()
    {
        originalPos = punchBox.localPosition;
        //Transform cam = Camera.main.transform;
        Vector3 newPos = Vector3.forward * 2;

        Debug.Log("cam forward: " + newPos);

        float t = 0f;

        while (t < punchTiming)
        {
            punchBox.localPosition = Vector3.Lerp(punchBox.localPosition, newPos, punchTiming);

            t += Time.deltaTime;

            yield return null;
        }

        punchBox.localPosition = originalPos;


    }
}
