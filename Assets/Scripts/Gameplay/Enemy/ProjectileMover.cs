using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private Rigidbody _rb;

    private Vector3 _direction;
    private float _damageDealt;
    private float timer;
    private float timeToDestroy = 5;
    public Vector3 Direction{get{return _direction;} set{_direction = value;}}
    public float DamageDealt {get{return _damageDealt;} set{_damageDealt = value;}}
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        MoveProjectile(_speed);
        if(timer >= timeToDestroy)
        {
            gameObject.SetActive(false);
        }
    }
    public void MoveProjectile(float speed)
    {
        _rb.AddForce(_direction * speed);
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag(IStringDefinitions.PLAYER_TAG))
        {
            Vector3 collisionNormal = other.contacts[0].normal;
            gameObject.SetActive(false);

            PlayerHealth playerHealth = other.collider.gameObject.transform.parent.GetComponent<PlayerHealth>();
            playerHealth.ReduceHealth(_damageDealt);
        }
    }
}
