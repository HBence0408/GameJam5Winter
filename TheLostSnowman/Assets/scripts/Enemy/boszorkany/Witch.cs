using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool iceAttackOnCD = false;
    private bool iceWallOnCD = false;
    private bool iceMissleOnCD =false;
    private bool isWalking = true;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform target;
    public Transform Target { get => target; }
    [SerializeField] private WichStates currentState = WichStates.NONE;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float longRangeMaxDistance;
    [SerializeField] private float longRangeMinDistance;
    [SerializeField] private float midRangeMaxDistance;
    [SerializeField] private float midRangeMinDistance;
    [SerializeField] private float closeRangeMaxDistance;
    [SerializeField] private float closeRangeMinDistance;
    private bool lockRot = false;
    private bool lockMovement = false;
    private bool isOnIdleFrame = true;
    [SerializeField] private float wallCounter = 30;
    private float moveSpeedBonus = 1;
    private float iceAttackCounter = 0;
    public WichStates State  { get => currentState; }
    private float stateSwichCounter = 0;



    public bool CanIceAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("metarig|groundSpikes") && !iceAttackOnCD) 
        {
            iceAttackOnCD = true;
            iceAttackCounter = 0;
            return true;
        }

        return false;
    }

    public bool CanPutWall()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("metarig|goundWall") && !iceWallOnCD) 
        {
            iceWallOnCD = true;
            wallCounter = 0;
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

    private void SetAnimToShoot()
    {
        animator.SetBool("PutWall", false);
        animator.SetBool("Idle", false);
        animator.SetBool("IceSpikes", false);
        animator.SetBool("Shoot", true);
    }
    private void SetAnimToPutWall()
    {
        animator.SetBool("PutWall", true);
        animator.SetBool("Idle", false);
        animator.SetBool("IceSpikes", false);
        animator.SetBool("Shoot", false);
        lockRot = true;

    }
    private void SetAnimToIceSpikes()
    {
        animator.SetBool("PutWall", false);
        animator.SetBool("Idle", false);
        animator.SetBool("IceSpikes", true);
        animator.SetBool("Shoot", false);
        lockRot = true;
        lockMovement = true;
    }
    private void SetAnimToIdle()
    {
        animator.SetBool("PutWall", false);
        animator.SetBool("Idle", true);
        animator.SetBool("IceSpikes", false);
        animator.SetBool("Shoot", false);
    }

    private void SwitchState()
    {
        switch (currentState)
        {
            case WichStates.IDLE:
                break;
            case WichStates.NONE:
                break;
            case WichStates.LONG_RANGE:
                if (Random.value > 0.5)
                {
                    currentState = WichStates.MID_RANGE;
                }
                else
                {
                    currentState = WichStates.CLOSE_RANGE;
                }
                break;
            case WichStates.MID_RANGE:
                if (Random.value > 0.5)
                {
                    currentState = WichStates.LONG_RANGE;
                }
                else
                {
                    currentState = WichStates.CLOSE_RANGE;
                }
                break;
            case WichStates.CLOSE_RANGE:
                if (Random.value > 0.5)
                {
                    currentState = WichStates.MID_RANGE;
                }
                else
                {
                    currentState = WichStates.LONG_RANGE;
                }
                break;
            default:
                break;
        }
        stateSwichCounter = 0;
    }

    private void Update()
    {
        moveSpeedBonus = 1;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("metarig|Action"))
        {
            iceAttackOnCD = false;
            iceWallOnCD = false;
            iceMissleOnCD = false;
            isOnIdleFrame = true;
            lockRot = false;
            lockMovement = false;
        }
        else
        {
            isOnIdleFrame = false;
        }

        if (!lockRot)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - this.transform.position);
            targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);
            this.transform.rotation = targetRotation;
        }

        if (stateSwichCounter > 35) // ezt meg biztosan kell allitani, lehet randomizalni akar
        {
            SwitchState();
        }

        Vector3 dir = Vector3.zero;
        switch (currentState)
        {
            case WichStates.IDLE:
                break;
            case WichStates.NONE:
                break;
            case WichStates.LONG_RANGE:

                if (Vector3.Distance(this.transform.position, target.position) > longRangeMaxDistance)
                {
                    dir = (target.position - this.transform.position).normalized;
                    dir.y = 0;
                    if (isOnIdleFrame)
                    {
                        SetAnimToShoot();
                    }
                    moveSpeedBonus = 1.1f;
                    
                }else if (Vector3.Distance(this.transform.position, target.position) < longRangeMinDistance)
                {
                    dir = (this.transform.position - target.position).normalized;
                    dir.y = 0;
                    if (isOnIdleFrame && wallCounter > 15)
                    {
                        SetAnimToPutWall();
                    }
                    else if (isOnIdleFrame)
                    {
                        SetAnimToShoot();
                    }
                    moveSpeedBonus = 0.8f;
                }
                else
                {
                    if (isOnIdleFrame)
                    {
                        SetAnimToShoot();
                    }
                }
                break;

            case WichStates.MID_RANGE:
                if (Vector3.Distance(this.transform.position, target.position) > midRangeMaxDistance)
                {
                    dir = (target.position - this.transform.position).normalized;
                    dir.y = 0;
                    if (isOnIdleFrame && wallCounter > 15)
                    {
                        SetAnimToPutWall();
                    }
                    else if (isOnIdleFrame)
                    {
                        SetAnimToShoot();
                    }

                }
                else if (Vector3.Distance(this.transform.position, target.position) < midRangeMinDistance)
                {
                    dir = (this.transform.position - target.position).normalized;
                    dir.y = 0;
                    moveSpeedBonus = 1.5f;

                }
                else
                {
                    if (isOnIdleFrame)
                    {
                        if (iceAttackCounter > 4 && Random.value > 0.3)
                        {
                            SetAnimToIceSpikes();
                        }
                        else
                        {
                            SetAnimToShoot();
                        }
                    }
                }
                break;

            case WichStates.CLOSE_RANGE:
                if (Vector3.Distance(this.transform.position, target.position) > closeRangeMaxDistance)
                {
                    dir = (target.position - this.transform.position).normalized;
                    dir.y = 0;
                    if (isOnIdleFrame)
                    {
                        moveSpeedBonus = 1.2f;
                    }
                    

                }
                else if (Vector3.Distance(this.transform.position, target.position) < closeRangeMinDistance)
                {
                    dir = (this.transform.position - target.position).normalized;
                    dir.y = 0;
                    if (isOnIdleFrame && wallCounter > 15)
                    {
                        SetAnimToPutWall();
                    }
                    else if (isOnIdleFrame)
                    {
                        SetAnimToShoot();
                    }
                    moveSpeedBonus = 1.2f;
                }
                else
                {
                    if (isOnIdleFrame && iceAttackCounter > 4)
                    {
                        SetAnimToIceSpikes();
                    }
                    else if (Random.value > 0.5f)
                    {
                        SetAnimToShoot();
                    }
                }
                break;
            default:
                break;
        }

        if (lockMovement)
        {
            dir = Vector3.zero;
        }

        rb.velocity = dir * movementSpeed * moveSpeedBonus;



        iceAttackCounter += Time.deltaTime;
        stateSwichCounter += Time.deltaTime;
        wallCounter += Time.deltaTime;
    }
}
