using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private Transform handPos;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform stringPos;
    [SerializeField] private PlayerAttack attackScript;
    private PlayerNose noseScript;
    private Collider nose = null;
    private bool inHand = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collider  " + other + " " + other.tag );
        if (other.tag == "Nose")
        {
            noseScript = other.GetComponent<PlayerNose>();
                                                 
            if (noseScript.NoseState == NoseStateEnum.READY ||noseScript.NoseState == NoseStateEnum.SHOOTING)
            {
                other.transform.position = handPos.position;
                other.transform.rotation = this.transform.rotation;
                other.transform.SetParent(this.transform);
                nose = other;
                noseScript.NoseState = NoseStateEnum.IN_HAND;
                inHand = true;
            }
        }
        if (other.tag == "Muzzle")
        {
            if (inHand && nose != null)
            {
                inHand = false;
                noseScript.NoseState = NoseStateEnum.LOADING;
                nose.transform.position = stringPos.position;
                Vector3 noseRot = nose.transform.eulerAngles;
                noseRot.x = stringPos.eulerAngles.x;
                noseRot.y = stringPos.eulerAngles.y;
                nose.transform.eulerAngles = noseRot;
                nose.transform.SetParent(muzzle);
                nose.GetComponent<PlayerNose>().SetMuzzlePos(muzzle);
                nose = null;
                attackScript.NewNose();
            } 
        }
    }
}
