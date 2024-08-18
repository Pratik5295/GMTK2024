using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] float positionAmount = 0.02f;
    [SerializeField] float maxPositionAmount = 0.06f;
    [SerializeField] float smoothPositionAmount = 6f;

    [Header("Rotation")]
    public float rotationAmount = 4;
    public float maxRotationAmount = 5;
    public float smoothRotationAmount = 12;

    [Space]

    [SerializeField] bool rotationX;
    [SerializeField] bool rotationY;
    [SerializeField] bool rotationZ;

    private Vector3 startPos;
    private Quaternion startRot;

    private float InputX, InputY;

    void Start()
    {
        startPos = transform.localPosition;
        startRot = transform.localRotation;
    }


    void Update()
    {
        CalculateSway();

        MoveSway();
        TiltSway();
    }

    void CalculateSway()
    {
        InputX = -Input.GetAxis("Mouse X");
        InputY = -Input.GetAxis("Mouse Y");
    }

    void MoveSway()
    {
        float moveX = Mathf.Clamp(InputX * positionAmount, -maxPositionAmount, maxPositionAmount);
        float moveY = Mathf.Clamp(InputY * positionAmount, -maxPositionAmount, maxPositionAmount);

        Vector3 finalPos = new Vector3(moveX, moveY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPos + startPos, Time.deltaTime * smoothPositionAmount);
    }

    private void TiltSway()
    {
        float tiltX = Mathf.Clamp(InputY * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltY = Mathf.Clamp(InputX * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion finalRot = Quaternion.Euler(new Vector3(
            rotationX ? -tiltX : 0f,
            rotationY ? tiltY : 0f,
            rotationZ ? tiltY : 0f
            ));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRot * startRot, Time.deltaTime * smoothRotationAmount);
    }
}
