using UnityEngine;
using System;


public class Observer : MonoBehaviour
{
    public static Observer Instance {get; private set;}
    private void Awake() 
    {
        Instance = this;
    }
}
