using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera cam;
    public float shakeIntensity, shakeDelay;

    [SerializeField]
    bool shouldShake;

    Transform camTransform;
    Vector3 camVector3;
    
    void Awake()
    {
        if (cam == null)
        {
            GetCam();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ShakeToggle(!shouldShake);
        }
    }

    //in order for this script to work the camera needs to be parented to another gameobject
    public void GetCam()
    {
        try
        {
            if (cam == null)
            {
                cam = Camera.main;
            }
            camTransform = cam.transform;
            camVector3 = cam.transform.localPosition;
        }
        catch
        {
            Debug.Log("No Camera Found");
        }
    }


    public void ShakeToggle(bool toggle)
    {
        shouldShake = toggle;
        if(shouldShake)
        {
            StartCoroutine(Shake());
        }
	}


    public IEnumerator Shake()
    {
        GetCam();
        while (shouldShake)
        {
            camTransform.localPosition = camVector3 + Random.insideUnitSphere * shakeIntensity;
            yield return new WaitForSeconds(shakeDelay);
        }
        camTransform.localPosition = camVector3;
        shakeDelay = 0f;
    }
}
