using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using System;

public struct SeekerData 
{
    private Transform unit;
    private Transform target;
    private ISeeker unitScript;
    public ISeeker Script { get => unitScript; }
    public bool requestPath;
    private string name;
    public SeekerData(Transform u, Transform t, ISeeker sc)
    {
        unit = u;
        target = t;
        unitScript = sc;
        requestPath = true;
        name = unit.name;
    }

    public void Update()
    {
        if (requestPath)
        {
            if (unit != null)
            {
                PathRequest request = new PathRequest(unit.position, target.position, unitScript.OnPathFound);
                Task pathfiniding = Task.Run(() => PathRequestManager.Instance.FindPath(request));
                requestPath = false;
            }
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (!(obj is SeekerData))
        {
            return false;
        }
        return ((SeekerData)obj).unitScript == this.unitScript;
    }

    public void DeleteScript()
    {
        unitScript = null;
    }

    public void TeleportToPosition(Vector3 pos)
    {
        unit.position = pos;
    }
}
