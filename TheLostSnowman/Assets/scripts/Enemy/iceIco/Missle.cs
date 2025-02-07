using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject impactEffect;
    private bool dirLocked = false;


    private void Awake()
    {
        //this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
       // this.transform.localScale = new Vector3(1, 1, 1);
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    private void Update()
    {
        /*
        if (this.transform.localScale.x < 1 )
        {
            float rnd = Random.value;
            this.transform.localScale += new Vector3(rnd,rnd,rnd);
            return;
        }
        */
        this.transform.SetParent(null);

        if (Vector3.Distance(target.position, this.transform.position) < 5)
        {
            dirLocked = true;
        }
        if (Vector3.Distance(target.position, this.transform.position) > 1000 && dirLocked)
        {
            Destroy(this.gameObject);
        }
        if (!dirLocked)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - this.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        }

        rb.velocity = this.transform.forward * 20;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name != "Cone(Clone)" && collision.transform.tag != "IceIco")
        {
            Debug.Log(collision.gameObject);
            Instantiate(impactEffect, this.transform.position, Quaternion.Euler(-this.transform.eulerAngles));
            Destroy(this.gameObject);
        }

    }
}
