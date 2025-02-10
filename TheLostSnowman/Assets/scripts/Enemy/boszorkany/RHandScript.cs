using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RHandScript : MonoBehaviour
{
    [SerializeField] private GameObject missle;
    [SerializeField] private Transform target;
    [SerializeField] private Transform missleDir;
    [SerializeField] private Witch mainScript;
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
        if (other.tag == "RPelvis")
        {
            Shoot();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
    }

    private void Shoot()
    {
        GameObject m = Instantiate(missle, this.transform.position, missleDir.rotation);
        m.GetComponent<Missle>().SetTarget(target);
    }

}
