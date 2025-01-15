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

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 movDir2 = new Vector3(0, 0, 0);
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            movDir2 += this.transform.right;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            movDir2 += -this.transform.right;
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            movDir2 += this.transform.forward;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            movDir2 += -this.transform.forward;
        }
        movDir2.Normalize();

       

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
