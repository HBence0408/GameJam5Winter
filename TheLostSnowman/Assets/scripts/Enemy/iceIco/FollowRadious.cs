using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRadious : MonoBehaviour
{
    [SerializeField] private IceIco ico;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            ico.IsInFollowRadious = true;
        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            ico.IsInFollowRadious = false;
        }
    }
    */
}
