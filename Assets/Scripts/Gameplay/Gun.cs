using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{

    [SerializeField] UnityEvent OnGunShoot;
    [SerializeField] UnityEvent OnAmmoTypeSwitched;
    [SerializeField] KeyCode switchAmmoTypeKey = KeyCode.F;
    [Tooltip("Firing cooldown in seconds")][SerializeField] float FireCooldown;

    
    [Tooltip("By default gun is semi")][SerializeField] bool Automatic;

    private float CurrentCooldown;

    void Start()
    {
        CurrentCooldown = FireCooldown;
    }

    void Update()
    {
        if(Automatic)
        {
            if (Input.GetMouseButton(0))
            {
                if (CurrentCooldown <= 0f)
                {
                    OnGunShoot?.Invoke();
                    CurrentCooldown = FireCooldown;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (CurrentCooldown <= 0f)
                {
                    OnGunShoot?.Invoke();
                    CurrentCooldown = FireCooldown;
                }
            }
        }

        if(Input.GetKeyDown(switchAmmoTypeKey))
        {
            OnAmmoTypeSwitched?.Invoke();
        }

        CurrentCooldown -= Time.deltaTime;
    }
}
