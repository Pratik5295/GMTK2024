using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [SerializeField] Transform punchBox; 
    Vector3 originalPos;
    [SerializeField] KeyCode punchButton = KeyCode.E;

    [SerializeField] float punchCooldown = .5f;
    [SerializeField] float punchTiming = .3f;
    float currentCooldown;

    [SerializeField] bool canHold;

    

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
