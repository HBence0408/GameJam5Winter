using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject Nose;
    [SerializeField] private GameObject nosePos;
    [SerializeField] private Animator armsAnimator;
    //private Queue<PlayerNose> noses;
    [SerializeField] private PlayerNose currentNose;
    private GameObject newNose;
    private PlayerNose newNoseScript;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        if (armsAnimator.GetCurrentAnimatorStateInfo(1).IsName("BetterCrossBow|trueCrossBowHold"))
        {
            currentNose.NoseState = NoseStateEnum.SHOOTING;
            currentNose = newNoseScript;
            armsAnimator.SetBool("shoot", true);
            armsAnimator.SetBool("reload", false);
            //NewNose();
            //nose script shoot
        }
    }

    public void NewNose()
    {
        Debug.Log("new nose");
        newNose = Instantiate(Nose, nosePos.transform.position, nosePos.transform.rotation);
        newNose.transform.SetParent(nosePos.transform);
        newNose.transform.localScale = new Vector3(1, 1, 1);
        //newNose.AddComponent<PlayerNose>();
        newNoseScript = newNose.GetComponent<PlayerNose>();
        newNoseScript.SetAnimatorReference(armsAnimator);
    }
}
