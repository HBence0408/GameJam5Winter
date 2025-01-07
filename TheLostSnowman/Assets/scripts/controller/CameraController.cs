using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _body;
    [SerializeField] private float _mouseSensitivity = 10;
    [SerializeField] private float _rotX = 0;
    [SerializeField] private Transform head;

    public void Update()
    {
        Vector3 mouseInput = new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * _mouseSensitivity;
        Vector3 bodyEuler = _body.transform.eulerAngles;
        _body.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(bodyEuler.x, bodyEuler.y + mouseInput.y, bodyEuler.z));

        Vector3 cameraRotation = this.transform.eulerAngles;
        Vector3 headRotation = head.eulerAngles;
        _rotX -= mouseInput.x;
        if(_rotX < -60)
        {
            _rotX = -60;
        }
        else if(_rotX > 60)
        {
            _rotX = 60;
        }
        cameraRotation.x = _rotX;
        headRotation.z = _rotX;
        this.transform.eulerAngles = cameraRotation;

        head.eulerAngles = headRotation;
    }
}
