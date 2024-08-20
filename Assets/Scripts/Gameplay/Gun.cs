using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    
    [SerializeField] UnityEvent OnGunShoot;
    [SerializeField] UnityEvent OnAltGunShoot;
    //[SerializeField] UnityEvent OnAmmoTypeSwitched;
    //[SerializeField] KeyCode switchAmmoTypeKey = KeyCode.F;
    [Tooltip("Firing cooldown in seconds")][SerializeField] float FireCooldown;
    [SerializeField] float altFireCooldown;

    
    [Tooltip("By default gun is semi")][SerializeField] bool Automatic;
    [Space]
    public Animator armAC, gunAC;

    private float CurrentCooldown;
    private float CurrentAltCooldown;

    bool isPlayerActing;
    private bool reloading;

    void Start()
    {
        CurrentCooldown = FireCooldown;
        CurrentAltCooldown = altFireCooldown;
    }

    void Update()
    {
        
        if (Automatic)
        {
            if (Input.GetMouseButton(0) && !isPlayerActing && !reloading)
            {
                if (CurrentCooldown <= 0f)
                {
                    StartCoroutine(Fire());
                    OnGunShoot?.Invoke();
                    CurrentCooldown = FireCooldown;
                }
            }
            if (Input.GetMouseButton(1) && !isPlayerActing && !reloading)
            {
                if (CurrentAltCooldown <= 0f)
                {
                    StartCoroutine(Fire());
                    OnAltGunShoot?.Invoke();
                    altFireCooldown = FireCooldown;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !isPlayerActing && !reloading)
            {
                if (CurrentCooldown <= 0f)
                {
                    StartCoroutine(Fire());
                    OnGunShoot?.Invoke();
                    CurrentCooldown = FireCooldown;
                }
            }
            if (Input.GetMouseButton(1) && !isPlayerActing && !reloading)
            {
                if (CurrentAltCooldown <= 0f)
                {
                    StartCoroutine(Fire());
                    OnAltGunShoot?.Invoke();
                    altFireCooldown = FireCooldown;
                }
            }
        }

        //if(Input.GetKeyDown(switchAmmoTypeKey)) OnAmmoTypeSwitched?.Invoke();

        CurrentCooldown -= Time.deltaTime;
    }

    public void OnReload()
    {
        StartCoroutine(Reload());
    }

    public void OnPunch()
    {
        //if (isPlayerActing) return false;
        //else
        //{
            StartCoroutine(Punch());
            //return true;
        //}
        
    }

    public IEnumerator Reload()
    {
        isPlayerActing = true;
        reloading = true;
        armAC.SetBool("fire", false);
        armAC.SetBool("reload", true);
        armAC.SetBool("punch", false);
        gunAC.SetBool("reload", true);

        yield return new WaitForSeconds(3.5f);

        isPlayerActing = false;
        reloading = false;
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
}
