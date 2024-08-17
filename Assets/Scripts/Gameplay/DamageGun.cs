using System.Collections;
using System.Collections.Generic;
using System.Xml;

using UnityEngine;

public class DamageGun : MonoBehaviour
{
    [SerializeField] float Damage;
    [SerializeField] float BulletRange;
    private Transform PlayerCamera;

    private void Start()
    {
        PlayerCamera = Camera.main.transform;
    }

    public void Shoot()
    {
        Ray gunRay = new Ray(PlayerCamera.position, PlayerCamera.forward);
        if(Physics.Raycast(gunRay, out RaycastHit hitInfo, BulletRange))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out Entity enemy)){
                enemy.Health -= Damage;
            }
            
        }
    }
}
