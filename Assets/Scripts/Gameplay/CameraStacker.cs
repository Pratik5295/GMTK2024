using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraStacker : MonoBehaviour
{
    [Header("Cameras")]
    public Camera baseCamera;
    public Camera overlayCamera;

    private void Start()
    {
        var baseCam = baseCamera.GetUniversalAdditionalCameraData();

        baseCam.cameraStack.Add(overlayCamera);
    }
}
