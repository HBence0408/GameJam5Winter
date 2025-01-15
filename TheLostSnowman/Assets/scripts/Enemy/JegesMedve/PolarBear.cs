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
        Debug.Log("unit poll");
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
        Debug.Log("following");
        Debug.Log(path.Count);
        if (path.Count <= pathIndex)
        {
            followPath = false;
            pathIndex = 0;
            Debug.Log(path.Last().x + " " + path.Last().y);
            return;
        }

        try
        {
            Vector3 pos = this.transform.position;
            Vector3 targetPos = path[pathIndex];
            targetPos.y = 1;
            Debug.Log(pathIndex);
            double sqareDistance = (pos.x * pos.x + pos.z * pos.z) - (targetPos.x * targetPos.x + targetPos.z * targetPos.z);
            if (!(sqareDistance > 3 || sqareDistance < -3))
            {
                pathIndex++;
            }
            Debug.Log(sqareDistance);
            // itt elég összehasonlításhoz csak a távolság négyzete is ezért a gyökvonást le hagyva annak a számítását le lehet spórolni és jobb lesz a teljesítmény
            // ide 3d verzio kell, meg lehetne optimalizálni hogy ne move towards legyen
            //this.transform.position = Vector2.MoveTowards(this.transform.position, path[pathIndex], speed * Time.deltaTime);
            //this.transform.position = Vector3.MoveTowards(this.transform.position, path[pathIndex], Time.deltaTime);
            //Vector3 dir = targetPos - pos;
            // dir.Normalize();
            // rb.velocity = dir * speed;
            //  rb.velocity = dir * speed;
            // rb.AddForce(dir * 100, ForceMode.Force);
            this.transform.LookAt(new Vector3(path[pathIndex].x, 0.5f, path[pathIndex].z) ) ;
            Vector3 yLock = this.transform.eulerAngles;
            Vector3.ProjectOnPlane(yLock, this.transform.up);
            this.transform.eulerAngles = yLock;
            rb.velocity = this.transform.forward * speed;
        }
        catch (Exception e)
        {

            Debug.Log("not moving");
            Debug.Log(e.Message);
        }
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
