using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private float mouseSensitivity = 10;
    [SerializeField] private float rotX = 0;
    [SerializeField] private float rotZ = 0;
    [SerializeField] private Transform head;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float collisionOffset;
    [SerializeField] private float collisonRadius;
    [SerializeField] private Transform camera;
    private Vector3 cameraStartPos;
    private Vector3 collisionPoint;
    static readonly ProfilerMarker cameraProfiler = new ProfilerMarker("camera collision handler");

    private void Start()
    {
        cameraStartPos = camera.localPosition;
    }

    private void Update()
    {
        Vector3 mouseInput = new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * mouseSensitivity;
        Vector3 bodyEuler = body.transform.eulerAngles;
        body.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(bodyEuler.x, bodyEuler.y + mouseInput.y, bodyEuler.z));

        Vector3 cameraRotation = camera.eulerAngles;
        Vector3 headRotation = head.eulerAngles;
        rotX -= mouseInput.x;
        rotZ -= mouseInput.x;

        if(rotX < -60)
        {
            rotX = -60;
        }
        else if(rotX > 60)
        {
            rotX = 60;
        }

        if (rotZ < -40)
        {
            rotZ = -40;
        }
        if (rotZ > 30)
        {
            rotZ = 30;
        }

        cameraRotation.x = rotX;

        headRotation.z = rotZ * 0.5f;

        camera.eulerAngles = cameraRotation;
        head.eulerAngles = headRotation;

        cameraProfiler.Begin();
        HandleCollision();           // a profilere- lehet nézni milyen az fps (attól félek a raycastolás drága lesz)
        cameraProfiler.End();
    }


    private void HandleCollision()
    {
        bool posAltered = false;
        Vector3 targetPos = cameraStartPos;

        RaycastHit hit;
        Ray ray = new Ray(cameraPivot.position, camera.position - cameraPivot.position);

        if (Physics.Raycast(ray, out hit, Mathf.Abs(targetPos.z) + 0.2f, collisionLayer))
        {
            // targetPos.z = -(hit.distance - collisionOffset);
            collisionPoint = hit.point;
            float distance = Vector3.Distance(hit.point, cameraPivot.position);
            float targetZ;
            targetZ = -(distance - collisionOffset);
            targetPos.z = Mathf.Lerp(camera.localPosition.z, targetZ, 5 * Time.deltaTime);
            camera.localPosition = targetPos;
            Debug.Log("cameera collision at " + hit.point);
            posAltered = true;
        }
        Vector3 rightSide = new Vector3(0, 0, 1.2f);
        ray = new Ray(cameraPivot.position, rightSide - cameraPivot.position);
        if (Physics.Raycast(ray, out hit, Mathf.Abs(targetPos.x) + 0.2f, collisionLayer))
        {
            // targetPos.z = -(hit.distance - collisionOffset);
            collisionPoint = hit.point;
            float distance = Vector3.Distance(hit.point, cameraPivot.position);
            float targetX;
            targetX = -(distance - (collisionOffset - 0.8f));
            targetPos.x = Mathf.Lerp(camera.localPosition.x, targetX, 5 * Time.deltaTime);
            camera.localPosition = targetPos;
            Debug.Log("cameera collision at " + hit.point);
            posAltered = true;
        }

        if (!posAltered)
        {
            targetPos.z = Mathf.Lerp(camera.localPosition.z, cameraStartPos.z, 5 * Time.deltaTime);
            targetPos.x = Mathf.Lerp(camera.localPosition.x, cameraStartPos.x, 5 * Time.deltaTime);
            //camera.localPosition = cameraStartPos;
            camera.localPosition = targetPos;
        }    
    }

    private void OnDrawGizmos()
    {
        
        Ray ray = new Ray(cameraPivot.position, cameraStartPos - cameraPivot.position);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(ray);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(collisionPoint, new Vector3(1,1,1));
    }
}
