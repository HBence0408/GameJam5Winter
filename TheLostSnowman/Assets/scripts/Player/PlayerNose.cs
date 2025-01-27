using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerNose : MonoBehaviour
{
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private Animator armsAnimator;
    [SerializeField] private NoseStateEnum state = NoseStateEnum.READY;
    private Vector3 dir;

    private Transform muzzlePos;

    public NoseStateEnum NoseState 
    { 
        get => state; 
        set
        {
            state = value;
            if (state == NoseStateEnum.SHOT /*&& armsAnimator.GetCurrentAnimatorStateInfo(1).IsName("BetterCrossBow|noAmmoIdle")*/)
            {
                armsAnimator.SetBool("reload", true);
                armsAnimator.SetBool("shoot", false);
            }
        }
    }

    private void FixedUpdate()
    {

        
        if (state == NoseStateEnum.SHOT)
        {
            Debug.Log("rep�l a r�pa ki tudja hol �ll meg");
            this.transform.position -= this.transform.forward * 0.1f;
        }
        
        
    }

    private void Update()
    {
        /*
        if (armsAnimator.GetCurrentAnimatorStateInfo(1).IsName("BetterCrossBow|trueCrossBowHold"))
        {
            NoseState = NoseStateEnum.LOADED;
        }
        */
    }

    //amikor shooting akkor ki l�je

    //ha eltal�lt valamit akkor �zenet (egyenl�re)

    public void SetAnimatorReference(Animator anim)
    {
        armsAnimator = anim;
    }

    public void SetMuzzlePos(Transform muzzle)
    {
        muzzlePos = muzzle;
    }

    public void Shoot()
    {
        if (state != NoseStateEnum.SHOOTING)
        {
            return;
        }
        NoseState = NoseStateEnum.SHOT;
        this.transform.SetParent(null);

        // ez m�g t�bb lesz
        //rigid.velocity = this.transform.forward;
        //Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("shot: " + collision);
    }
}
