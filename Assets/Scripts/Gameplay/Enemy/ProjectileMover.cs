using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private Rigidbody _rb;

    private Vector3 _direction;
    private float _damageDealt;
    private float _timer;
    private float _timeToDestroy = 5;
    private bool _forceWasUpdated = false;
    public Vector3 Direction{get{return _direction;} set{_direction = value;}}
    public float DamageDealt {get{return _damageDealt;} set{_damageDealt = value;}}
    private void OnEnable()
    {
        _rb.velocity = Vector3.zero;
        _timer = 0;
        _forceWasUpdated = false;
    }
    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if(!_forceWasUpdated) MoveProjectile(_speed);
        if(_timer >= _timeToDestroy)
        {
            gameObject.SetActive(false);
        }
    }
    public void MoveProjectile(float speed)
    {
        _forceWasUpdated = true;
        _rb.AddForce(_direction * speed, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Raphael projectile collided with something");
        if(other.gameObject.CompareTag(IStringDefinitions.PLAYER_TAG))
        {
            Debug.Log("Raphael that something wsa the player");
            Vector3 collisionNormal = other.contacts[0].normal;
            gameObject.SetActive(false);

            PlayerHealth playerHealth = other.collider.gameObject.transform.parent.GetComponent<PlayerHealth>();
            playerHealth.ReduceHealth(_damageDealt);
            Debug.Log("Raphael player health" + playerHealth.GetHealth);
        }
    }
}
