using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public Animator armAC, gunAC;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            armAC.SetBool("fire", false);
            armAC.SetBool("reload", true);
            gunAC.SetBool("reload", true);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            armAC.SetBool("fire", true);
            armAC.SetBool("reload", false);
            gunAC.SetBool("reload", false);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            armAC.SetBool("fire", false);
            armAC.SetBool("reload", false);
            gunAC.SetBool("reload", false);
            armAC.SetFloat("walk", 1);
        }
        else
        {
            armAC.SetFloat("walk", 0);
        }
    }
}
