using System.Collections;
using System.Collections.Generic;
using System.Xml;

using TMPro;
using UnityEngine;

public class DamageGun : MonoBehaviour
{
    [SerializeField] float damage = 1f;
    [SerializeField] float timeBetweenShooting = 0f;
    [SerializeField] float spread = .05f, range = 50f, reloadTime = 1f, timeBetweenShots = .1f;
    [SerializeField] int magazineSize = 10, bulletsPerTap = 1;
    [SerializeField] TMP_Text text;
    int bulletsLeft, bulletsShot;


    bool reloading, readyToShoot;

    private Transform PlayerCamera;

    [SerializeField] KeyCode reloadKey = KeyCode.R;
    [SerializeField] Transform gunBarrelTip;
    [SerializeField] RaycastHit rayHit;
    [SerializeField] LayerMask whatIsEnemy;

<<<<<<< Updated upstream
=======
    [SerializeField] AmmoType currentAmmoType;

    [SerializeField] UnityEvent OnReload;

    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip reloadSound;
    GameObject shootClipSource;
    GameObject reloadClipSource;

    [SerializeField] ParticleSystem primaryParticles;
    [SerializeField] ParticleSystem secondaryParticles;

    enum AmmoType
    {
        normal,
        enlarge,
        shrink
    }

>>>>>>> Stashed changes
    //Graphics
    [SerializeField] GameObject muzzleFlash, bulletHoleGraphic;
    CamShake camShake;
    [SerializeField] float camShakeMagnitude = .15f, camShakeDuration = .45f;

    private void Start()
    {
        PlayerCamera = Camera.main.transform;
        camShake = PlayerCamera.GetComponentInParent<CamShake>();

        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(reloadKey) && bulletsLeft < magazineSize && !reloading) Reload();

        //Set Ammo Text
        text.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
    }

    public void OnShoot()
    {
        Debug.Log("OnShoot");
        if (readyToShoot && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
            
        }
        else if (readyToShoot && !reloading && bulletsLeft == 0)
        {
            Reload();
        }
    }

    void Shoot()
    {
        Debug.Log("Shot");
        readyToShoot = false;

        GameObject player = PlayerMovement.player.gameObject;

        //Spread
        bool ifMoving = player.GetComponent<Rigidbody>().velocity.magnitude > 0;

        float tempSpread = ifMoving ? spread * 1.5f : spread;

        float x = Random.Range(-tempSpread, tempSpread);
        float y = Random.Range(-tempSpread, tempSpread);

        //Calculate the spread direction
        Vector3 direction = PlayerCamera.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(PlayerCamera.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
            if (rayHit.collider.CompareTag("Enemy"))
            {
<<<<<<< Updated upstream
                rayHit.collider.gameObject.GetComponent<Entity>().Health -= damage;
=======
                if(currentAmmoType == AmmoType.normal)
                    rayHit.collider.gameObject.GetComponent<Entity>().Health -= damage;
                else if(currentAmmoType == AmmoType.enlarge)
                {
                    gunBarrelTip.GetComponent<ParticleSystem>().Play();
                    rayHit.collider.gameObject.GetComponent<Entity>().Enlarge(damage);
                }
                else if (currentAmmoType == AmmoType.shrink)
                {
                    rayHit.collider.gameObject.GetComponent<Entity>().Shrink(damage);
                }
>>>>>>> Stashed changes
            }

        }

        //ShakeCamera
        camShake.Shake(camShakeDuration, camShakeMagnitude);

        //Graphics
<<<<<<< Updated upstream
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, gunBarrelTip.position, Quaternion.identity);
=======
        
        GameObject bulletDecalClone = ObjectPool.Instance.GetPooledBulletDecals();
        GameObject muzzleFlashClone = ObjectPool.Instance.GetPooledMuzzleFlashes();
        if (bulletDecalClone != null)
        {
            bulletDecalClone.transform.position = rayHit.point;
            bulletDecalClone.transform.rotation = Quaternion.Euler(0, 180, 0);
            bulletDecalClone.SetActive(true);
        }

        if (muzzleFlashClone != null)
        {
            muzzleFlashClone.transform.position = gunBarrelTip.position;
            muzzleFlashClone.transform.rotation = Quaternion.identity;
            if (currentAmmoType == AmmoType.enlarge) muzzleFlashClone.transform.Find("Enlarge").gameObject.SetActive(true);
            else if (currentAmmoType == AmmoType.shrink) muzzleFlashClone.transform.Find("Shrink").gameObject.SetActive(true);
            muzzleFlashClone.SetActive(true);
        }

        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        //Instantiate(muzzleFlash, gunBarrelTip.position, Quaternion.identity);
>>>>>>> Stashed changes

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

<<<<<<< Updated upstream
=======
    public void PrimaryFire()
    {
        currentAmmoType = AmmoType.enlarge;

        //primaryParticles.Play();
        OnShoot();
    }

    public void SecondaryFire()
    {
        currentAmmoType = AmmoType.shrink;
        //secondaryParticles.Play();
        OnShoot();
    }

>>>>>>> Stashed changes
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
