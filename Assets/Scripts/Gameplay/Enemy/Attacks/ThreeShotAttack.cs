using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThreeShotAttacks", menuName = "ScriptableObjects/Enemy/Attacks/ThreeShotAttacks")]
public class ThreeShotAttack : AttackStategy
{
    public float _fireRate;
    public override void Attack(Transform transform, MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine(ShootThreeTimes(transform));
    }
    
    private IEnumerator ShootThreeTimes(Transform transform)
    {
        int amountOfTimesToShoot = 3;

        for(int i = 0; i < amountOfTimesToShoot; i++)
        {
            InstantiateProjectile(transform, transform.forward);
        }
        yield return new WaitForSeconds(_fireRate);
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
