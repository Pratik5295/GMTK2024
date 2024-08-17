using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConeProjectile", menuName = "ScriptableObjects/Enemy/Attacks/ConeProjectile")]
public class ConeProjectiles : AttackStategy
{
    public float _fireRate;

    public override void Attack(Transform transform, MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine(ShootConeAtPlayer(transform));
    }
    private IEnumerator ShootConeAtPlayer(Transform transform) 
    {
        float initialDistance = 0.5f;
        int numProjectiles = 3;
        float spreadAngle = 30f;

        for (int set = 0; set < 3; set++) 
        {
            for (int i = 0; i < numProjectiles; i++) 
            {
                float angle = spreadAngle * ((float)i / (numProjectiles - 1) - 0.5f);
                Vector3 projectileDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
                float distance = initialDistance / (set + 1);

                Vector3 spawnPosition = transform.position + projectileDirection * distance;

                InstantiateProjectile(transform, projectileDirection);
            }
            yield return new WaitForSeconds(_fireRate);
        }
    }

    private void InstantiateProjectile(Transform transform, Vector3 projectileDirection)
    {
        float offset = 0.5f;
        if(ObjectPool.Instance != null)
        {
            GameObject enemyProjectile = ObjectPool.Instance.GetPooledEnemyProjectiles();

            if(enemyProjectile != null)
            {
                enemyProjectile.transform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
                enemyProjectile.transform.rotation = Quaternion.identity;
                enemyProjectile.SetActive(true);
            }
            ProjectileMover projectileMover = enemyProjectile.GetComponent<ProjectileMover>();
            projectileMover.Direction = projectileDirection;
        }
    }
}
