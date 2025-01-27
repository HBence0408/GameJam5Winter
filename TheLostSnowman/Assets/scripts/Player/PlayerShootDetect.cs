using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Nose")
        {
            other.GetComponent<PlayerNose>().Shoot();
            //shoot
        }
    }
}
