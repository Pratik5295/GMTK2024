using System.Collections;
using System.Collections.Generic;
using System.Xml;

using UnityEngine;

public class DamageGun : MonoBehaviour
{
    [SerializeField] float damage = 1f;
    [SerializeField] float timeBetweenShooting = 0f;
    [SerializeField] float spread, range, reloadTime, timeBetweenShots;
    [SerializeField] int magazineSize, bulletsPerTap;
    int bulletsLeft, bulletsShot;


    bool reloading, readyToShoot;

    private Transform PlayerCamera;

    [SerializeField] KeyCode reloadKey = KeyCode.R;
    //[SerializeField] Camera cam;
    [SerializeField] Transform attackPoint;
    [SerializeField] RaycastHit rayHit;
    [SerializeField] LayerMask whatIsEnemy;

    private void Start()
    {
        PlayerCamera = Camera.main.transform;

        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(reloadKey) && bulletsLeft < magazineSize && !reloading) Reload();
        
    }

    public void OnShoot()
    {
        if (readyToShoot && !reloading && bulletsLeft > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        readyToShoot = false;

        //Spread
        bool ifMoving = PlayerMovement.player.GetComponent<Rigidbody>().velocity.magnitude > 0;
            
        float tempSpread = ifMoving ? spread * 1.5f : spread;

        float x = Random.Range(-tempSpread, tempSpread);
        float y = Random.Range(-tempSpread, tempSpread);

        //Calculate the spread direction
        Vector3 direction = PlayerCamera.forward + new Vector3(x, y, 0);

        

        Ray gunRay = new Ray(PlayerCamera.position, direction);
        if (Physics.Raycast(gunRay, out RaycastHit hitInfo, range))
        {
            Debug.Log(hitInfo.collider.name);
            if (hitInfo.collider.gameObject.TryGetComponent(out Entity enemy))
            {
                enemy.Health -= damage;
            }

        }
        bulletsLeft--;
        bulletsShot++;
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            bulletsShot = bulletsPerTap;
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
