using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceIco : MonoBehaviour, ISeeker
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
    [SerializeField] private MisslePods pods;
    private bool isStopped = false;
    private bool inFollowRadious = false;
    public bool IsInFollowRadious { set => inFollowRadious = value; }


    private void Start()
    {
        isReady = true;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void Stop()
    {
        isStopped = true;
    }

    public void Go()
    {
        isStopped = false;
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
        if (isStopped)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        if (!inFollowRadious)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        if (followPath)
        {
            FollowPath();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void FollowPath()
    {
        if (path.Count <= pathIndex)
        {
            followPath = false;
            pathIndex = 0;
            rb.velocity = Vector3.zero;
            //transform.position = transform.position;
            return;
        }

        Vector3 dir;
        Vector3 pos = this.transform.position;
        Vector3 targetPos = path[pathIndex];
        targetPos.y = 1;

        /*
        double sqareDistance = (pos.x * pos.x + pos.z * pos.z) - (targetPos.x * targetPos.x + targetPos.z * targetPos.z);
        if (!(sqareDistance > 5 || sqareDistance < -5))
        {
            pathIndex++;
        }
        */

        if (PathFinderGrid.Instance.FindNode(pos).Equals(PathFinderGrid.Instance.FindNode(targetPos)) )
        {
            pathIndex++;
        }

        dir = targetPos-pos;

        dir.y = 0;

        dir.Normalize();

        rb.velocity = dir * speed;
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
        pods.SetTarget(target);
    }

    private void OnDrawGizmos()
    {
        if (isActive && followPath)
        {
            Gizmos.color = Color.red;
            foreach (Vector3 p in path)
            {
                Gizmos.DrawCube(p, new Vector3(1, 1, 1));
            }
        }
    }
}
