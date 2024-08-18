using UnityEngine;

public class TriggerBox : MonoBehaviour,ITriggerable
{
    public virtual void FireTrigger()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        var collided = other.gameObject.transform.parent;

        if(collided.tag == "Player")
        {
            FireTrigger();
        }
    }
}
