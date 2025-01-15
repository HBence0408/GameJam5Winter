using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private float mouseSensitivity = 10;
    [SerializeField] private float rotX = 0;
    [SerializeField] private Transform head;

    public void Update()
    {
        Vector3 mouseInput = new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * mouseSensitivity;
        Vector3 bodyEuler = body.transform.eulerAngles;
        body.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(bodyEuler.x, bodyEuler.y + mouseInput.y, bodyEuler.z));

        Vector3 cameraRotation = this.transform.eulerAngles;
        Vector3 headRotation = head.eulerAngles;
        rotX -= mouseInput.x;

        if(rotX < -60)
        {
            rotX = -60;
        }
        else if(rotX > 60)
        {
            rotX = 60;
        }

        cameraRotation.x = rotX;
        headRotation.z = rotX;

        this.transform.eulerAngles = cameraRotation;
        head.eulerAngles = headRotation;
    }
}
