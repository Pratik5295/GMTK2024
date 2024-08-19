using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{

    [SerializeField] UnityEvent OnGunShoot;
    [SerializeField] UnityEvent OnAltGunShoot;
    [SerializeField] UnityEvent OnAmmoTypeSwitched;
    [SerializeField] KeyCode switchAmmoTypeKey = KeyCode.F;
    [Tooltip("Firing cooldown in seconds")][SerializeField] float FireCooldown;
[SerializeField] float altFireCooldown;

    
    [Tooltip("By default gun is semi")][SerializeField] bool Automatic;

    private float CurrentCooldown;
    private float CurrentAltCooldown;

    void Start()
    {
        CurrentCooldown = FireCooldown;
        CurrentAltCooldown = altFireCooldown;
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
            if (Input.GetMouseButton(1))
            {
                if (CurrentAltCooldown <= 0f)
                {
                    OnAltGunShoot?.Invoke();
                    altFireCooldown = FireCooldown;
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
            if (Input.GetMouseButton(1))
            {
                if (CurrentAltCooldown <= 0f)
                {
                    OnAltGunShoot?.Invoke();
                    altFireCooldown = FireCooldown;
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
