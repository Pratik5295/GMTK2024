using UnityEngine;

public class TriggerBox : MonoBehaviour,ITriggerable
{
    public virtual void FireTrigger()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        var collided = other.gameObject;

        if(collided.tag == "Player")
        {
            Debug.Log("Collision deetected");
            FireTrigger();
        }
    }
}
