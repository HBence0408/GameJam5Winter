using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float maxSpeed = 3;
    [SerializeField] private Rigidbody rigidB;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform bottom;
    [SerializeField] private float bottomRotation;
    [SerializeField] private Animator playerAnimator;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 movDir2 = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.D))
        {
            movDir2 += this.transform.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movDir2 += -this.transform.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movDir2 += this.transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movDir2 += -this.transform.forward;
        }
        movDir2.Normalize();

        if (Input.GetKey(KeyCode.R))
        {
            playerAnimator.SetBool("reload", true);
        }
        if (Input.GetKey(KeyCode.T))
        {
            playerAnimator.SetBool("reload", false);
        }

        if (movDir2 != new Vector3(0,0,0))
        {
            Vector3 normalOfMoveDir2 = new Vector3(movDir2.z, movDir2.y, -movDir2.x);
            // nem tudom miért nem támogatják ezt ¯\_(ツ)_/¯
            bottom.RotateAround(normalOfMoveDir2, bottomRotation);
            rigidB.velocity = movDir2 * movementSpeed;
        }
        else
        {
            rigidB.velocity = new Vector3(0,0,0);
        }
    }
}
