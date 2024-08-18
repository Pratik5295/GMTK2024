using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTimer : MonoBehaviour
{
    [SerializeField] float removalDelay;
    void Start()
    {
        Invoke("Disable", removalDelay);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
