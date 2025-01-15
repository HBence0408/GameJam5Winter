using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PolarBear : MonoBehaviour, ISeeker
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    private List<Vector3> path;
    [SerializeField] private bool followPath = false;
    private int pathIndex;
    public SeekerData data;
    private bool isReady = false;
    private bool isActive;
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        isReady = true;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void OnPathFound(List<Vector3> path, bool pathFound)
    {
        if (!pathFound)
        {
            return;
        }
        this.path = path;
        pathIndex = 0;
        followPath = true;
    }

    public void Poll()
    {
        if (!isReady)
        {
            return;
        }
        if (followPath)
        {
            FollowPath();
        }
    }

    private void FollowPath()
    {
        if (path.Count <= pathIndex)
        {
            followPath = false;
            pathIndex = 0;
            rb.velocity = Vector3.zero;
            return;
        }

        Vector3 pos = this.transform.position;
        Vector3 targetPos = path[pathIndex];
        targetPos.y = 1;
        double sqareDistance = (pos.x * pos.x + pos.z * pos.z) - (targetPos.x * targetPos.x + targetPos.z * targetPos.z);
        if (!(sqareDistance > 4 || sqareDistance < -4))
        {
            pathIndex++;
        }

        // ezzel gyorsan fordul (snap-pel)
        // this.transform.LookAt(new Vector3(path[pathIndex].x, 0.5f, path[pathIndex].z) );
        // Vector3 yLock = this.transform.eulerAngles;
        // Vector3.ProjectOnPlane(yLock, this.transform.up);
        //this.transform.eulerAngles = yLock;

        //smooth fordulás
        //itt dobálhat out of index errort de még így is fut szóval  ¯\_(ツ)_/¯ (annyira ezért nem mert nem állnak meg a célban)
        // majd dobok ide egy if-et
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(path[pathIndex].x, 0.5f, path[pathIndex].z) - this.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

        rb.velocity = this.transform.forward * speed;
    }

    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            this.gameObject.SetActive(true);
            this.isActive = true;
        }
        else
        {
            this.isActive = false;
            this.gameObject.SetActive(false);
        }
    }

    public void SetData(SeekerData data)
    {
        this.data = data;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnDrawGizmos()
    {
        if (isActive && followPath)
        {
            Gizmos.color = Color.blue;
            foreach (Vector3 p in path)
            {
                Gizmos.DrawCube(p, new Vector3(1, 1, 1));
            }
        }
    }
}
