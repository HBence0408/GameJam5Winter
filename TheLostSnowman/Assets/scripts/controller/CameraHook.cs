using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHook : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Update()
    {
        _camera.transform.position = this.transform.position;
        Vector3 cameraRotation = _camera.transform.eulerAngles;

        cameraRotation.y = this.transform.eulerAngles.y;

        _camera.transform.eulerAngles = cameraRotation;
    }
}
