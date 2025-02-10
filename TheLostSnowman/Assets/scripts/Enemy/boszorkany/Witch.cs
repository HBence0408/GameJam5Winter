using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool iceAttackOnCD = false;
    private bool iceWallOnCD = false;
    private bool iceMissleOnCD =false;
    [SerializeField] private Transform target;
    [SerializeField] private WichStates currentState = WichStates.NONE;
    [SerializeField] private float movementSpeed;

    public bool CanIceAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("metarig|groundSpikes") && !iceAttackOnCD) 
        {
            iceAttackOnCD = true;
            return true;
        }

        return false;
    }

    public bool CanPutWall()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("metarig|goundWall") && !iceWallOnCD) 
        {
            iceWallOnCD = true;
            return true;
        }

        return false;
    }

    public bool CanShoot()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("metarig|shoot") && !iceWallOnCD)
        {
            iceMissleOnCD = true;
            return true;
        }

        return false;
    }

    private void Update()
    {

        if (Vector3.Distance(this.transform.position, target.position) > 50)
        {
            currentState = WichStates.WALKING;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("metarig|Action"))
        {
            iceAttackOnCD = false;
            iceWallOnCD = false;
            iceMissleOnCD = false;
        }

        Quaternion targetRotation = Quaternion.LookRotation(target.position - this.transform.position);
        targetRotation.eulerAngles = new Vector3(0,targetRotation.eulerAngles.y, 0);
        this.transform.rotation = targetRotation;

        if (currentState == WichStates.WALKING)
        {
            animator.SetBool("", true);
        }
    }
}
