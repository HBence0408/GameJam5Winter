using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallEffect : MonoBehaviour
{
    [SerializeField] private float growth;
    private float growthDelay;
    private Vector3 startPos;
    [SerializeField] private float startDelay;
    private float startCounter = 0;
    private bool isUp = false;
    [SerializeField] private float upDelay;
    private float upCounter = 0;
    [SerializeField] private GameObject self;

    private void Awake()
    {
        startPos = this.transform.localPosition;
        this.transform.localPosition = new Vector3(startPos.x, -7, startPos.z);
    }

    private void Update()
    {
        startCounter += Time.deltaTime;

        if (startCounter < startDelay)
        {
            return;
        }

        if (!isUp)
        {


            if (this.transform.localPosition.y < 0 && growthDelay > 0.01)
            {
                Vector3 newPos = new Vector3(startPos.x, this.transform.position.y + growth, startPos.z);
                //this.transform.localScale = newZ;
                this.transform.localPosition = newPos;
                growthDelay = 0;
            }
            else
            {
                growthDelay += Time.deltaTime;
                if (this.transform.localPosition.y > 0)
                {
                    isUp = true;
                    growthDelay = 0;
                }
            }
        }
        if (isUp)
        {
            if (upCounter < upDelay)
            {
                upCounter += Time.deltaTime;

            }
            else
            {
                if (this.transform.localPosition.y > -7 && growthDelay > 0.01)
                {
                    Vector3 newPos = new Vector3(startPos.x, this.transform.position.y - growth, startPos.z);
                    //this.transform.localScale = newZ;
                    this.transform.localPosition = newPos;
                    growthDelay = 0;
                }
                else
                {
                    growthDelay += Time.deltaTime;
                }
            }
        }

        if (startCounter > 10)
        {
            Destroy(self);
        }
    }
}
