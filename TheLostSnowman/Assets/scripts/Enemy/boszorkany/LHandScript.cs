using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHandScript : MonoBehaviour
{
    [SerializeField] private GameObject iceAttack;
    //[SerializeField] private Transform target;
    [SerializeField] private Transform attackDir;
    [SerializeField] private Witch mainScript;
    [SerializeField] private GameObject iceWall;
    [SerializeField] private Transform wallSpawnPos;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.tag == "LPelvis")
        {
            if (mainScript.CanIceAttack())
            {
                Attack(mainScript.Target.position - (this.transform.forward * 5));
            }
            if (mainScript.CanPutWall())
            {
                if (mainScript.State == WichStates.MID_RANGE)
                {
                    PutWall(mainScript.Target.position + (this.transform.forward * 5));
                }
                else
                {
                    PutWall(wallSpawnPos.position);
                }
                
            }
        }

    }


    private void Attack(Vector3 spawnPos)
    {
        spawnPos.y = 0.1f;
        GameObject m = Instantiate(iceAttack, spawnPos, attackDir.rotation);
        //m.GetComponent<Missle>().SetTarget(target);
    }

    private void PutWall(Vector3 spawnPos)
    {
        spawnPos.y = 0.1f;
        Instantiate(iceWall, spawnPos, wallSpawnPos.rotation);
    }
}
