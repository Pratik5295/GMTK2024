using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera _camera;

    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [Tooltip("How low the player can look")]
    [SerializeField] private float minLookDown;
    [Tooltip("How high the player can look")]
    [SerializeField] private float maxLookDown;

    [Tooltip("Keeps track of direction player is facing")]
    Transform orientation;
    Transform model;
    GameObject player;

    float xRotation;
    float yRotation;

    private void Awake()
    {
        if (_camera == null) _camera = this;
    }

    void Start()
    {
        //Being handles in CursorHanlder.cs

        player = PlayerMovement.player.gameObject;
        orientation = player.GetComponent<PlayerMovement>().orientation;
        model = player.transform;

        if (GameManager.Instance == null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookDown, maxLookDown);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        model.rotation = Quaternion.Euler(0, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);


        //Being handles in CursorHanlder.cs
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
        }
    }


}
