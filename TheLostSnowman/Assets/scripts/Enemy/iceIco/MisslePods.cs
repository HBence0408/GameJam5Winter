using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class MisslePods : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private List<Transform> pods;
    [SerializeField] private GameObject missle;
    [SerializeField] private IceIco ico;
    private float shootDelay = 6;
    private bool canShoot = false;
    private float missleDelay = 0;
    private int missleShot = 0;
    private bool checkSight = true;
    private float sightCheckDelay = 0;
    private int obstaclesInSight = 0;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - this.transform.position);
        this.transform.rotation = targetRotation;



        if (obstaclesInSight > 0)
        {
            ico.Go();
            return;
        }
        if (!canShoot)
        {
            return;
        }

        ico.Stop();
        if (shootDelay > 5)
        {
            //ShootMissles();

            if (missleDelay > 0.2)
            {
                ShootMissle(missleShot);
                missleShot++;
                missleDelay = 0;
                if (missleShot > 5)
                {
                    shootDelay = 0;
                    missleShot = 0;
                }
            }
            else
            {
                missleDelay += Time.deltaTime;
            }
            
        }
        else
        {
            shootDelay += Time.deltaTime;
        }
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    private void ShootMissles()
    {
        for (int i = 0; i < pods.Count; i++)
        {
           GameObject m = Instantiate(missle, pods[i].position,this.transform.rotation);
            m.GetComponent<Missle>().SetTarget(target);
        }
    }

    private void ShootMissle(int i)
    {
        GameObject m = Instantiate(missle, pods[i].position, this.transform.rotation);
        m.GetComponent<Missle>().SetTarget(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player");
            checkSight = true;
            //ico.Stop();
            canShoot = true;
        }
        if (other.tag == "Obstackle")
        {
            obstaclesInSight++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ico.Go();
            canShoot = false;
            checkSight = false;
        }
        if (other.tag == "Obstackle")
        {
            obstaclesInSight--;
        }
    }
}
