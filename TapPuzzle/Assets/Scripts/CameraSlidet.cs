using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.InputSystem;
public class CameraSlidet : MonoBehaviour
{
    public Transform rotationCenter;
    public float rotationSpeed = 5f;
    public float verticalRotationRange = 60f;
    public float cameraDistance = 5f;

    private bool isRotating = false;
    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;



    private void Update()
    {
        PlayCameraControler();
    }

    void PlayCameraControler()
    {

        if ((Input.GetMouseButtonDown(0) ))
        {
            isRotating = true;
        }
        else if ((Input.GetMouseButtonUp(0)))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            float verticalInput = Input.GetAxis("Mouse Y");

            horizontalRotation += horizontalInput * rotationSpeed;
            verticalRotation -= verticalInput * rotationSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationRange, verticalRotationRange);

            Quaternion rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
            Vector3 offset = rotation * new Vector3(0f, 0f, -cameraDistance);
            transform.position = rotationCenter.position + offset;
            transform.LookAt(rotationCenter);
        }
    }


}







